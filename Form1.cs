using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using SigmatekCommunication;
using KraussMaffeiData;
using System.IO;
using System.Xml.Serialization;
using System.CodeDom.Compiler;
using System.Reflection;
using System.Collections;
using System.Collections.Specialized;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Office.Utils;
using DevExpress.XtraRichEdit.API.Native;
using System.Threading;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using System.Resources;
using System.Globalization;
using Microsoft.Win32;
using DevExpress.XtraEditors.Repository;



namespace TS_ECT2
{
    public partial class Form1 : DevExpress.XtraEditors.XtraForm
    {
        GlobalVar.GlobalData kmGlobalData = new GlobalVar.GlobalData();
        public BindingList<GlobalVar.Teststep> liTeststepList = new BindingList<GlobalVar.Teststep>();
        public BindingList<GlobalVar.Teststep> liTeststepListSimulation = new BindingList<GlobalVar.Teststep>();
        public BindingList<GlobalVar.ParameterDefinition> liParameterDefinition= new BindingList<GlobalVar.ParameterDefinition>();
        //public BindingList<GlobalVar.param>
        Simulation mySimulation = new Simulation();
        System.Windows.Forms.Timer mainTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer serverUpdateTimer = new System.Windows.Forms.Timer();
        System.Windows.Forms.Timer UIUpdateTimer = new System.Windows.Forms.Timer();
        KraussMaffeiFunctions kmFunctions = new KraussMaffeiFunctions();
        Stopwatch stopWatch = new Stopwatch();
        private static ResourceManager rm;

        

        //private string strResourcesPath = "d:\\synchronics\\software\\resourcedata\\";
        private string strCulture = "en-US";
        private CellColorHelper _CellColorHelper;
        GlobalVar.Testmodule selectedTestModule = new GlobalVar.Testmodule();
        bool updateDigIn = false;

        public Form1()
        {
            InitializeComponent();
            _CellColorHelper = new CellColorHelper(gridViewTestFunctions);
            kmFunctions.SetGlobalData(kmGlobalData);
        }

     

        public static ResourceManager RM
        {
            get
            {
                return rm;
            }
        }

        private void SetCulture()
        {
            CultureInfo objCI = new CultureInfo(strCulture);
            Thread.CurrentThread.CurrentCulture = objCI;
            Thread.CurrentThread.CurrentUICulture = objCI;
        }
        private void SetResource()
        {
            string applicationLocation = System.Reflection.Assembly.GetEntryAssembly().Location;
            string applicationDirectory = Path.GetDirectoryName(applicationLocation);

            rm = ResourceManager.CreateFileBasedResourceManager("resource", applicationDirectory + "\\ResourceData\\", null);
        }

        private void GlobalizeApp()
        {
            SetCulture();
            SetResource();
            SetUIChanges();
        }

        private void SetUIChanges()
        {
            if (String.Compare(strCulture, "en-US") == 0)
            {
                //picTop.Image = picE.Image;
            }

            if (String.Compare(strCulture, "de") == 0)
            {
                //picTop.Image = picS.Image;
            }

            if (String.Compare(strCulture, "fr-FR") == 0)
            {
                //picTop.Image = picF.Image;
            }
            //ribbonPage1.Text = rm.GetString("FailureString");

            btnStartCompleteTest.Text = rm.GetString("CompleteTestString");
            btnStartSelectedTest.Text = rm.GetString("StartSelectedTestsString");
            btnStartfromFirstSelectedTest.Text = rm.GetString("StartFromFirstSelectedTestString");
            btnSelectAll.Text = rm.GetString("SelectAllString");
            btnSelectNone.Text = rm.GetString("SelectNoneString");
            btnDeleteResults.Text = rm.GetString("DeleteResultsString");

            panelSGMInput.Text = rm.GetString("InputsString");
            panelSGMOutput.Text=rm.GetString("OutputsString");
            panelIOWatch.Text= rm.GetString("IOWatchString");

            tabNavigationPageFunctions.Caption = rm.GetString("FunctionSelectionString");
            tabNavigationPageProgram.Caption = rm.GetString("ProgramString");
            tabNavigatorPageMaintenance.Caption = rm.GetString("AdministrationString");

            gridViewWatch.Columns[0].Caption = rm.GetString("ChannelNameString");
            gridViewWatch.Columns[1].Caption = rm.GetString("ValueString");
            gridViewWatch.Columns[2].Caption = rm.GetString("DescriptionString");
            gridViewWatch.Columns[3].Caption = rm.GetString("ServerString");

            gridViewInputs.Columns[0].Caption = rm.GetString("ChannelNameString");
            gridViewInputs.Columns[1].Caption = rm.GetString("ValueString");
            gridViewInputs.Columns[2].Caption = rm.GetString("DescriptionString");
            gridViewInputs.Columns[3].Caption = rm.GetString("ModuleString");
           
            gridViewOutputs.Columns[0].Caption = rm.GetString("ChannelNameString");
            gridViewOutputs.Columns[1].Caption = rm.GetString("ValueString");
            gridViewOutputs.Columns[2].Caption = rm.GetString("DescriptionString");
            gridViewOutputs.Columns[3].Caption = rm.GetString("ModuleString");

            kmGlobalData.inputNotString = rm.GetString("InputNotString");
            kmGlobalData.statusIncorrectString = rm.GetString("StatusIncorrectString");
            kmGlobalData.messageString = rm.GetString("MessageString");
            kmGlobalData.doesNotExistString = rm.GetString("DoesNotExistString");
            kmGlobalData.inputNotChangedString = rm.GetString("InputNotChangedString");
            kmGlobalData.wrongSignalValueString = rm.GetString("WrongSignalValueString");
            kmGlobalData.analogOutputString = rm.GetString("AnalogOutputString");
            kmGlobalData.analogInputString = rm.GetString("AnalogInputString");
            kmGlobalData.inputString = rm.GetString("InputString");
            kmGlobalData.toleranceFailureString = rm.GetString("ToleranceFailureString");
            kmGlobalData.nominalValueString = rm.GetString("NominalValueString");
            kmGlobalData.actualValueString = rm.GetString("ActualValueString");
            kmGlobalData.statusChangedString = rm.GetString("StatusChangedString");
            kmGlobalData.unexpectedChangeString = rm.GetString("UnexpectedChangeString");

            //Hardwarecheck

            kmGlobalData.hWSStatusDigitalInputsString = rm.GetString("hWSStatusDigitalInputsString");
            kmGlobalData.hwDigitalInputsAnalogValuesPosString = rm.GetString("hwDigitalInputsAnalogValuesPosString");
            kmGlobalData.hwDigitalInputsAnalogValuesNegString = rm.GetString("hwDigitalInputsAnalogValuesNegString");
            kmGlobalData.hwDigitalOutputString = rm.GetString("hwDigitalOutputString");
            kmGlobalData.hwAnalogInputString = rm.GetString("hwAnalogInputString");
            kmGlobalData.hwRelaisString = rm.GetString("hwRelaisString");
            kmGlobalData.hwAnalogOutputString = rm.GetString("hwAnalogOutputString");
            kmGlobalData.hwTemperatureOutputString = rm.GetString("hwTemperatureOutputString");
            kmGlobalData.hwOnString = rm.GetString("hwOnString");
            kmGlobalData.hwPullupString = rm.GetString("hwPullupString");
            kmGlobalData.hwPlugString = rm.GetString("hwPlugString");
            kmGlobalData.hwSupplyString = rm.GetString("hwSupplyString");
            kmGlobalData.hwInterfaceString = rm.GetString("hwInterfaceString");
            kmGlobalData.hwPTCRelaisString = rm.GetString("hwPTCRelaisString");
            kmGlobalData.hwMultiIOString = rm.GetString("hwMultiIOString");
            kmGlobalData.hwMultiIOPlugString = rm.GetString("hwMultiIOPlugString");
            kmGlobalData.hwSupply3PhaseString = rm.GetString("hwSupply3PhaseString");
            kmGlobalData.hwSupply6PhaseString = rm.GetString("hwSupply6PhaseString");
            kmGlobalData.hwSupply1PhaseString = rm.GetString("hwSupply1PhaseString");
            kmGlobalData.hwSupplyPlugString = rm.GetString("hwSupplyPlugString");
            kmGlobalData.hwInterfaceRelaisString = rm.GetString("hwInterfaceRelaisString");
        }

        private void UIUpdateTimer_Tick(object sender, EventArgs e)
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            XmlSerializer xmlSer = new XmlSerializer(typeof(string));
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Synchronics"); 
            bool startModuleDefinition = false;
            int startIndex = 0;
            int endIndex = 0;
            List<string> stringListTestModules = new List<string>();
            kmGlobalData.liParameterDefinition = new BindingList<GlobalVar.ParameterDefinition>();
            Application.Idle += new EventHandler(Form1_Idle);

            string applicationLocation = System.Reflection.Assembly.GetEntryAssembly().Location;
            string applicationDirectory = Path.GetDirectoryName(applicationLocation);


            if (File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Configuration.xml"))
            {
                XmlSerializer vXmlSerializer = new XmlSerializer(typeof(BindingList<GlobalVar.ParameterDefinition>));
                Stream fs = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Configuration.xml", FileMode.Open);
                liParameterDefinition = (BindingList<GlobalVar.ParameterDefinition>)(vXmlSerializer.Deserialize(fs));
                fs.Dispose();
                fs.Close();
            }

            for (int i = 1; i <= 7; i++)
            {
                bool valid = false;

                foreach (GlobalVar.ParameterDefinition myParameter in liParameterDefinition)
                {
                    if (myParameter.ParameterNumber == i)
                    {
                        valid = true;
                        break;
                    }
                }

                if (!valid)
                {
                    GlobalVar.ParameterDefinition parameter = new GlobalVar.ParameterDefinition();
                    parameter.ParameterNumber = i;
                    parameter.ParameterValue = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics";
                    liParameterDefinition.Add(parameter);
                }
            }

            //Sprache laden

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

            strCulture = RegistryAccess.GetStringRegistryValue(@"Language", "de");

            if (String.Compare(strCulture, "en-US") == 0)
            {
                chkEn.Checked = true;
            }

            if (String.Compare(strCulture, "de") == 0)
            {
                chkDE.Checked = true;
            }

            GlobalizeApp();
            ///
           

            System.IO.DirectoryInfo ParentDirectory = new System.IO.DirectoryInfo(applicationDirectory + "\\VSTest_30-05-16");

            StreamReader myFile = new StreamReader(applicationDirectory + "\\VSTest_30-05-16\\Test.c", System.Text.Encoding.Default, false, 10000000);
            string[] myLines = System.IO.File.ReadAllLines(applicationDirectory + "\\VSTest_30-05-16\\Test.c");
            bool startDefinition = false;
            
            foreach (string line in myLines)
            {
                if ((!startDefinition) && (line.Contains("hier Testmodule einbinden")))
                {
                    startDefinition = true;
                    continue;
                }
              
                if ((startDefinition) && (line == ""))
                {
                    startDefinition = false;
                    break;
                }
                       
                if (line.Contains("#include") && (startDefinition))
                {
                    startIndex = line.IndexOf("\"");
                    endIndex = line.IndexOf("\"", startIndex + 1);
                    string moduleName = line.Substring(startIndex+1, endIndex - startIndex-1);
                    moduleName = moduleName.Replace(".h", ".c");
                    stringListTestModules.Add(moduleName);       
                }
            }

            foreach (string testModuleName in stringListTestModules)
            {

              
                string[] lines = System.IO.File.ReadAllLines(applicationDirectory + "\\VSTest_30-05-16\\" + testModuleName);
                foreach (string line in lines)
                {
                    string myLine = line;

                    if ((!line.StartsWith("PUBLIC const TESTMODULE") && (!startModuleDefinition)))
                        continue;
                    if (!startModuleDefinition)
                    {
                        startModuleDefinition = true;
                        selectedTestModule = new GlobalVar.Testmodule();
                        continue;
                    }


                    if (line.StartsWith("{"))
                        continue;

                    if (selectedTestModule.ReportPath == "")
                    {
                        myLine = cleanTestModuleString(line);
                        selectedTestModule.ReportPath = myLine;
                        continue;
                    }

                    if (selectedTestModule.Description == "")
                    {
                        myLine = cleanTestModuleString(line);
                        selectedTestModule.Description = myLine;
                        continue;
                    }

                    for (int i = 0; i <= 4; i++)
                    {
                        if (selectedTestModule.Batchfile[i] == "")
                        {
                            if (!myLine.Contains("// Name der Lasal Batchdatei"))
                                break;
                            myLine = cleanTestModuleString(line);
                            selectedTestModule.Batchfile[i] = myLine;
                            break;
                        }
                    }
                }

                if (startModuleDefinition)
                {
                    selectedTestModule.FileName = applicationDirectory + "\\VSTest_30-05-16\\" + testModuleName;
                    kmGlobalData.listTestModules.Add(selectedTestModule);
                    startModuleDefinition = false;
                }
            }

            MachineSelection();

            //gridControlMachineSelection.DataSource = kmGlobalData.listTestModules;

            //ZE-LIste
            StartSigmatek(1, "10.100.100.1");
            rtTextInfoLines.Document.Unit = DevExpress.Office.DocumentUnit.Millimeter;
            rtTextInfoLines.Document.Sections[0].Page.PaperKind = System.Drawing.Printing.PaperKind.A4;
            rtTextInfoLines.Document.Sections[0].Page.Landscape = true;
            rtTextInfoLines.Document.Sections[0].Margins.Left = 2.0f;
            rtTextInfoLines.Document.Sections[0].Margins.Top = 2.0f;
            rtTextInfoLines.ActiveView.ZoomFactor = 1.0f;
            //rtTextInfoLines.Document.Delete(rtTextInfoLines.Document.Range);
            gridIOWatch.DataSource = kmGlobalData.listIOValues;
            gridViewWatch.Columns["WatchListEntry"].FilterInfo = new ColumnFilterInfo("[WatchLIstEntry] LIKE TRUE");
            gridSGMInputs.DataSource = kmGlobalData.listIOValues;
            gridViewInputs.Columns["IsOutputValue"].FilterInfo = new ColumnFilterInfo("[IsOutputValue] LIKE FALSE");

            RepositoryItemCheckEdit riCheckEditPerformTest = new RepositoryItemCheckEdit();
            gridViewTestFunctions.Columns["Perform"].ColumnEdit = riCheckEditPerformTest;
            riCheckEditPerformTest.EditValueChanged += OnEditValueChanged;

            RepositoryItemCheckEdit riCheckEditSimulation = new RepositoryItemCheckEdit();
            gridViewInputs.Columns["Simulation"].ColumnEdit = riCheckEditSimulation;
            riCheckEditSimulation.EditValueChanged += OnEditValueChanged;

            kmFunctions.UpdateAnalogOutputs();
            kmFunctions.UpdateDigitalOutputs(false);
            gridSGMOutputs.DataSource = kmGlobalData.listIOValues;
            gridViewOutputs.Columns["IsOutputValue"].FilterInfo = new ColumnFilterInfo("[IsOutputValue] LIKE TRUE");
            UIUpdateTimer.Interval = 50;
            UIUpdateTimer.Tick += new EventHandler(UIUpdateTimer_Tick);
            UIUpdateTimer.Start();
            this.WindowState = FormWindowState.Maximized;
            ZEs zes = new ZEs(kmGlobalData, kmFunctions);
            zes.Show();
        }

        private void Form1_Idle(object sender, System.EventArgs e)
        {
            stopWatch.Stop();
            stopWatch.Reset();
            stopWatch.Start();
            
            if (kmGlobalData.systemSigmatekMachine.IsOnlineH())
            {
                btnOnlineSigmatekMachine.ItemAppearance.Normal.BackColor = Color.LightGreen;
                if (!serverUpdateTimer.Enabled)
                {
                    serverUpdateTimer.Interval = 500;
                    serverUpdateTimer.Tick += new EventHandler(SigmatekUpdateTimer_Tick);
                    serverUpdateTimer.Start();
                }
            }

            else
            { 
                btnOnlineSigmatekMachine.ItemAppearance.Normal.BackColor = Color.Transparent;
            }

            if (kmGlobalData.systemSigmatekTester.IsOnlineH())
            {
                btnOnlineSigmatekTester.ItemAppearance.Normal.BackColor = Color.LightGreen;
            }
            else
            {
                btnOnlineSigmatekTester.ItemAppearance.Normal.BackColor = Color.Transparent;
            }
            if (kmGlobalData.unprocessedTestInfoCommand)
            {
                DocumentRange range = null;

                //Messagetype Standard
                if (kmGlobalData.testInfoMessageType == 0)
                {
                    range = rtTextInfoLines.Document.InsertText(rtTextInfoLines.Document.CaretPosition, kmGlobalData.testInfoLine + "\n");
                    txtTeststep.Caption = kmGlobalData.testInfoLine;
                    rtTextInfoLines.ScrollToCaret();
                }

                //Messagetype Bookmark
                if (kmGlobalData.testInfoMessageType == 1)
                {
                    range = rtTextInfoLines.Document.InsertText(rtTextInfoLines.Document.CaretPosition, kmGlobalData.testInfoLine + "\n");
                    rtTextInfoLines.ScrollToCaret();
                    rtTextInfoLines.Document.CreateBookmark(range, "Bookmark");
                }

                //Messagetype Replace and Delete Bookmark
                if (kmGlobalData.testInfoMessageType == 2)
                {
                    string name = rtTextInfoLines.Document.Bookmarks[0].Name;
                    DocumentPosition bmStart = rtTextInfoLines.Document.Bookmarks[0].Range.Start;
                    rtTextInfoLines.Document.Delete(rtTextInfoLines.Document.Bookmarks[0].Range);
                    DocumentRange docRange = rtTextInfoLines.Document.InsertText(bmStart, kmGlobalData.testInfoLine + "\n");
                }

                //Messagetype Replace and Keep Bookmark
                if (kmGlobalData.testInfoMessageType == 3)
                {
                    string name = rtTextInfoLines.Document.Bookmarks[0].Name;
                    DocumentPosition bmStart = rtTextInfoLines.Document.Bookmarks[0].Range.Start;
                    rtTextInfoLines.Document.Delete(rtTextInfoLines.Document.Bookmarks[0].Range);
                    DocumentRange docRange = rtTextInfoLines.Document.InsertText(bmStart, kmGlobalData.testInfoLine + "\n");
                    rtTextInfoLines.Document.CreateBookmark(docRange, "Bookmark");
                }

                if (kmGlobalData.testInfoMessageType == 4)
                {
                    if (rtTextInfoLines.Document.Bookmarks.Count != 0)
                    {
                        Bookmark myBookmark = rtTextInfoLines.Document.Bookmarks[0];
                    }
                }

                string reportPath = liParameterDefinition[0].ParameterValue;
                string orderNumber = kmGlobalData.szOrderNumber;

                rtTextInfoLines.SaveDocument(reportPath + "\\" + orderNumber + ".rtf", DevExpress.XtraRichEdit.DocumentFormat.Rtf);
                gridIOWatch.RefreshDataSource();
                gridTestFunctions.RefreshDataSource();
                kmGlobalData.unprocessedTestInfoCommand = false;


            }

            if (!kmGlobalData.isCycleProgram)
            {
                bool stepSelected = false;
                gridTestFunctions.RefreshDataSource();
                foreach (GlobalVar.Teststep myTestStep in liTeststepList)
                {
                    if (myTestStep.Perform)
                    {
                        stepSelected = true;
                        break;
                    }
                }

                if (kmGlobalData.testSequenceRunning)
                {
                    gridViewTestFunctions.Columns["Perform"].OptionsColumn.AllowEdit = false;
                }
                else
                {
                    gridViewTestFunctions.Columns["Perform"].OptionsColumn.AllowEdit = true;
                }

                if (kmGlobalData.testSequenceRunning)
                    btnStartCompleteTest.Enabled = false;
                else
                    btnStartCompleteTest.Enabled = true;

                if ((kmGlobalData.testSequenceRunning) || (!stepSelected))
                {   
                    btnStartfromFirstSelectedTest.Enabled = false;
                    btnStartSelectedTest.Enabled = false;
                    btnSelectAll.Enabled = false;
                    btnSelectNone.Enabled = false;
                    btnDeleteResults.Enabled = false;
                }
                else
                {   
                    btnStartfromFirstSelectedTest.Enabled = true;
                    btnStartSelectedTest.Enabled = true;
                    btnSelectAll.Enabled = true;
                    btnSelectNone.Enabled = true;
                    btnDeleteResults.Enabled = true;
                }
            }
            else
            {
                if (!kmGlobalData.testSequenceRunning)
                    btnStartCompleteTest.Enabled = true;
                else
                    btnStartCompleteTest.Enabled = false;
            }

            if (kmGlobalData.testSequenceRunning)
            {
                btnRunning.ItemAppearance.Normal.BackColor = Color.LightGreen;
                btnRunning.Caption = "...running";
            }

            else
            {
                btnRunning.ItemAppearance.Normal.BackColor = Color.LightGray;
                btnRunning.Caption = "";
            }

            txtTestName.Caption = kmGlobalData.testNameLong;

            bool updatedAO = kmFunctions.UpdateAnalogOutputs();
            bool updatedDO = kmFunctions.UpdateDigitalOutputs(false);

            if (updatedAO || updatedDO)
            { 
                stopWatch.Start();
            }
            if (stopWatch.ElapsedMilliseconds > 1000)
            {
                kmFunctions.UpdateDigitalInputs(0);
                kmFunctions.UpdateAnalogInputs();
                stopWatch.Stop();
                stopWatch.Reset();
            }
        }

        void MachineSelection()
        {
            FileStream loadStream;
            FileStream saveStream;
            XmlSerializer xmlSer = new XmlSerializer(typeof(string));
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            Directory.CreateDirectory(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\Synchronics");
            string initialPath;
            kmGlobalData.aszZEs.Clear();
            kmGlobalData.listIOValues.Clear();
            //kmGlobalData.listTestModules.Clear();  
            List<string> stringListTestModules = new List<string>();

            MachineSelection machineSelection = new MachineSelection(kmGlobalData, rm);
            machineSelection.ShowDialog();

            if (machineSelection.SelectedMachine != -1)
            {
                for (;;)
                {
                    selectedTestModule = kmGlobalData.listTestModules[machineSelection.SelectedMachine];

                    string testDllResult = CreateTestDLL(selectedTestModule.FileName, liTeststepList, listBox1, txtSourceCode, 0, 0, true);
                    if (testDllResult == "")
                    {
                        gridTestFunctions.DataSource = liTeststepList;
                        gridViewTestFunctions.Columns["Activated"].FilterInfo = new ColumnFilterInfo("[Activated] LIKE TRUE");
                    }
                    else
                        MessageBox.Show("Fehler beim erstellen der DLL " + testDllResult);

                    if (selectedTestModule.FileName.Contains("Cycle_"))
                    {
                        string fileName = selectedTestModule.FileName;
                        fileName = fileName.Replace("Cycle_", "Simul_");
                        string simulationDLLResult = CreateSimulationDLL(fileName, liTeststepListSimulation, listBox1, txtSourceCode, 0, 0, true);
                        kmGlobalData.isCycleProgram = true;
                        btnStartSelectedTest.Enabled = false;
                        btnStartfromFirstSelectedTest.Enabled = false;
                        btnSelectAll.Enabled = false;
                        btnSelectNone.Enabled = false;
                    }
                    else
                    {
                        kmGlobalData.isCycleProgram = false;
                        btnStartSelectedTest.Enabled = true;
                        btnStartfromFirstSelectedTest.Enabled = true;
                        btnSelectAll.Enabled = true;
                        btnSelectNone.Enabled = true;
                    }
                    if (File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\ZE.xml"))
                    {
                        loadStream = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\ZE.xml", FileMode.OpenOrCreate);
                        xmlSer = new XmlSerializer(typeof(string));
                        initialPath = (string)xmlSer.Deserialize(loadStream);
                        loadStream.Close();
                        openFileDialog1.InitialDirectory = initialPath;
                    }
                    openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog1.Title = "Auswahl ZE's";

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {

                        string directoryPath = Path.GetDirectoryName(openFileDialog1.FileName);
                        saveStream = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\ZE.xml", FileMode.Create);
                        xmlSer.Serialize(saveStream, directoryPath);
                        saveStream.Close();

                        ReadZEs(openFileDialog1.FileName, kmGlobalData);
                    }
                    else
                        break;

                    foreach (GlobalVar.Teststep teststep in liTeststepList)
                    {
                        if (teststep.ZE.Count == 0)
                            teststep.Activated = true;
                        else
                        {
                            teststep.Activated = false;
                            for (int i = 0; i <= teststep.ZE.Count - 1; i++)
                            {
                                if (!teststep.ZE[i].StartsWith("!"))
                                { 
                                    if (kmFunctions.fIsZEEnabled(teststep.ZE[i]))
                                    {
                                        teststep.Activated = true;
                                        break;
                                    }
                                }
                                else
                                {
                                    teststep.ZE[i] = teststep.ZE[i].Replace("!", "");
                                    if (!kmFunctions.fIsZEEnabled(teststep.ZE[i]))
                                    {
                                        teststep.Activated = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    gridTestFunctions.RefreshDataSource();

                    string reportPath = liParameterDefinition[0].ParameterValue;
                    string orderNumber = kmGlobalData.szOrderNumber;

                    if (kmGlobalData.isCycleProgram)
                        orderNumber += "_C";

                    if (File.Exists(reportPath + "\\" + orderNumber + ".rtf"))
                    {
                        rtTextInfoLines.LoadDocument(reportPath + "\\" + orderNumber + ".rtf", DevExpress.XtraRichEdit.DocumentFormat.Rtf);

                        if (rtTextInfoLines.Document.Bookmarks.Count > 0)
                        {
                            Bookmark myBookmark = rtTextInfoLines.Document.Bookmarks[0];
                            rtTextInfoLines.Document.Bookmarks.Remove(myBookmark);
                        }
                        rtTextInfoLines.Document.CaretPosition = rtTextInfoLines.Document.Range.End;
                    }

                    if (File.Exists(reportPath + "\\" + orderNumber + ".xml"))
                    {
                        try
                        {
                            XmlSerializer vXmlSerializer = new XmlSerializer(typeof(BindingList<GlobalVar.Teststep>));
                            Stream fs = new FileStream(reportPath + "\\" + orderNumber + ".xml", FileMode.Open);
                            liTeststepList = (BindingList<GlobalVar.Teststep>)(vXmlSerializer.Deserialize(fs));
                            fs.Dispose();
                            fs.Close();

                            foreach (GlobalVar.Teststep myTestStep in liTeststepList)
                            {
                                myTestStep.Perform = false;
                            }

                            gridTestFunctions.DataSource = liTeststepList;
                            UpdateGridColorsLoad();
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }

                        kmGlobalData.actTestProcedure += 1;
                    }

                    //Zuordnungsliste

                    if (File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Zuordnungsliste.xml"))
                    {
                        loadStream = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Zuordnungsliste.xml", FileMode.OpenOrCreate);
                        xmlSer = new XmlSerializer(typeof(string));
                        initialPath = (string)xmlSer.Deserialize(loadStream);
                        loadStream.Close();
                        openFileDialog1.InitialDirectory = initialPath;
                    }

                    openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog1.Title = "Auswahl Zuordnungsliste";

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string directoryPath = Path.GetDirectoryName(openFileDialog1.FileName);
                        saveStream = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Zuordnungsliste.xml", FileMode.Create);
                        xmlSer.Serialize(saveStream, directoryPath);
                        saveStream.Close();
                        ReadAssignmentList(openFileDialog1.FileName, kmGlobalData);
                    }
                    else
                        break;

                    string applicationLocation = System.Reflection.Assembly.GetEntryAssembly().Location;
                    string applicationDirectory = Path.GetDirectoryName(applicationLocation);
                    kmFunctions.ReadFixServers(applicationDirectory + "\\VSTest_30-05-16\\AssignList.c", kmGlobalData);

                    //Adapterliste

                    if (File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Adapterliste.xml"))
                    {
                        loadStream = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Adapterliste.xml", FileMode.OpenOrCreate);
                        xmlSer = new XmlSerializer(typeof(string));
                        initialPath = (string)xmlSer.Deserialize(loadStream);
                        loadStream.Close();
                        openFileDialog1.InitialDirectory = initialPath;
                    }
                    openFileDialog1.Title = "Auswahl Adapterliste";
                    openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string directoryPath = Path.GetDirectoryName(openFileDialog1.FileName);
                        saveStream = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Adapterliste.xml", FileMode.Create);
                        xmlSer.Serialize(saveStream, directoryPath);
                        saveStream.Close();
                        ReadAdapterList(openFileDialog1.FileName, kmGlobalData);
                    }
                    else
                        break;

                    //Anweisungsliste

                    if (File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Anweisungsliste.xml"))
                    {
                        loadStream = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Anweisungsliste.xml", FileMode.OpenOrCreate);
                        xmlSer = new XmlSerializer(typeof(string));
                        initialPath = (string)xmlSer.Deserialize(loadStream);
                        loadStream.Close();
                        openFileDialog1.InitialDirectory = initialPath;
                    }
                    openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                    openFileDialog1.Title = "Auswahl Anweisungsliste";

                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        string directoryPath = Path.GetDirectoryName(openFileDialog1.FileName);
                        saveStream = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Anweisungsliste.xml", FileMode.Create);
                        xmlSer.Serialize(saveStream, directoryPath);
                        saveStream.Close();
                        ReadCommandList(openFileDialog1.FileName, kmGlobalData);
                    }
                    break;
                }
            }
        }

        void OnEditValueChanged(object sender, EventArgs e)
        {
            gridViewTestFunctions.PostEditor();
            gridViewInputs.PostEditor();
        }

        string cleanTestModuleString(string testModuleString)
        {
            int startIndex = 0;

            startIndex = testModuleString.IndexOf("//");
            testModuleString = testModuleString.Substring(0, startIndex);
            testModuleString = testModuleString.Replace("\"", "");
            testModuleString = testModuleString.Replace("\\", "");
            testModuleString = testModuleString.Replace(" ", "");
            testModuleString = testModuleString.Replace(",", "");
            testModuleString = testModuleString.Replace("\t", "");
            return testModuleString;
        }

        private void WriteTestHeader()
        {
            string headerString;
           

            kmFunctions.TestInfoLine("********************************************************************************", 0);
            kmFunctions.TestInfoLine("*                                                                              *", 0);
            headerString = "*     TEST       :   " + selectedTestModule.Description;

            while (headerString.Length < 79)
                headerString += " ";

            headerString += "*";
            kmFunctions.TestInfoLine(headerString, 0);

            headerString = "*     AUFTRAG    :   " + kmGlobalData.szOrderNumber;

            while (headerString.Length < 79)
                headerString += " ";

            headerString += "*";
            kmFunctions.TestInfoLine(headerString, 0);

            headerString = "*     TYP        :   " + kmGlobalData.szMachineType;

            while (headerString.Length < 79)
                headerString += " ";

            headerString += "*";
            kmFunctions.TestInfoLine(headerString, 0);

            headerString = "*     BGV        :   " + kmGlobalData.szBGVStandA;

            while (headerString.Length < 79)
                headerString += " ";

            headerString += "*";
            kmFunctions.TestInfoLine(headerString, 0);

            headerString = "*     DATUM      :   " + DateTime.Now.ToString("dd.MM.yy HH:mm:ss");

            while (headerString.Length < 79)
                headerString += " ";

            headerString += "*";
            kmFunctions.TestInfoLine(headerString, 0);

            headerString = "*     PRÜFER     :   Schuller";

            while (headerString.Length < 79)
                headerString += " ";

            headerString += "*";
            kmFunctions.TestInfoLine(headerString, 0);

    
            kmFunctions.TestInfoLine("*                                                                              *", 0);
            kmFunctions.TestInfoLine("********************************************************************************", 0);
        }

        private void TestThread()
        {
            Testklasse testklasse = new Testklasse();
            testklasse.SetGlobalData(kmGlobalData);
            kmGlobalData.actTestProcedure = 0;
            //txtSourceCode.Clear();

            WriteTestHeader();

            foreach (GlobalVar.Teststep teststep in liTeststepList)
            {
                if (kmGlobalData.breakTestProgram)
                    break;
                if (teststep.Perform && teststep.Activated)
                {
                    teststep.Perform = false;
                    MethodInfo mi = testklasse.GetType().GetMethod(teststep.Testfunction);
                    if (mi != null)
                    {
                        for (;;)
                        {
                            kmGlobalData.testSequenceRunning = true;
                            kmFunctions.TestInfoLine(rm.GetString("StartTestString") + ": " + liTeststepList[kmGlobalData.actTestProcedure].Testdescription, 1);
                            kmGlobalData.repeatTestFunction = false;
                            kmGlobalData.ignoreTestFailure = false;

                            kmGlobalData.changeWatch = true;
                            kmGlobalData.testNameLong = liTeststepList[kmGlobalData.actTestProcedure].Testdescription;

                            mi.Invoke(testklasse, null);

                            if (kmGlobalData.breakTestProgram)
                            {
                                kmFunctions.TestInfoLine(rm.GetString("StartTestString")+ ": " + liTeststepList[kmGlobalData.actTestProcedure].Testdescription + "..." + rm.GetString("AbortString"), 2);
                                kmGlobalData.breakTestProgram = false;
                                break;
                            }
                            
                            for (;;)
                            {
                                if (!kmGlobalData.testSequenceRunning)
                                {    
                                    break;
                                }
                            }

                            if ((!kmGlobalData.breakTestProgram) && (!kmGlobalData.repeatTestFunction) && (!kmGlobalData.ignoreTestFailure))
                            {
                                kmFunctions.TestInfoLine(rm.GetString("StartTestString") + ": " + liTeststepList[kmGlobalData.actTestProcedure].Testdescription + "..." + rm.GetString("OKString"), 2);
                                break;
                            }

                            if ((!kmGlobalData.breakTestProgram) && (kmGlobalData.repeatTestFunction))
                            {
                                kmFunctions.TestInfoLine(rm.GetString("StartTestString") + ": " + liTeststepList[kmGlobalData.actTestProcedure].Testdescription + "..." + rm.GetString("FailureRepeatString"), 2);
                            }

                            if ((!kmGlobalData.breakTestProgram) && (!kmGlobalData.repeatTestFunction) && (kmGlobalData.ignoreTestFailure))
                            {
                                kmFunctions.TestInfoLine(rm.GetString("StartTestString") + ": " + liTeststepList[kmGlobalData.actTestProcedure].Testdescription + "..."+ rm.GetString("FailureIgnoreString"), 2);
                                kmGlobalData.breakTestProgram = false;
                                break;
                            }
                        }

                        UpdateGridColors();      
                    }
                    try
                    {
                        // Serialize   
                        string reportPath = liParameterDefinition[0].ParameterValue;
                        string orderNumber = kmGlobalData.szOrderNumber;


                        XmlSerializer vXmlSerializer = new XmlSerializer(typeof(BindingList<GlobalVar.Teststep>));
                        Stream fs = new FileStream(reportPath + "\\" + orderNumber + ".xml", FileMode.Create);
                        vXmlSerializer.Serialize(fs, liTeststepList);
                        fs.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }

                //rtTextInfoLines.SaveDocument("d:\\Synchronics\\Testresults\\ReportsRTF\\" + kmGlobalData.szOrderNumber + ".rtf", DevExpress.XtraRichEdit.DocumentFormat.Rtf);

                

                kmGlobalData.actTestProcedure += 1;
            }
            if (kmGlobalData.breakTestProgram)
                kmGlobalData.breakTestProgram = false;
            //            rtTextInfoLines.ExportToPdf("d:\\Synchronics\\Reports\\" + kmGlobalData.szOrderNumber" + ".pdf");
            /*
            Testklasse meineTestklasse = new Testklasse();
            meineTestklasse.SetGlobalData(kmGlobalData);
            meineTestklasse.GX_M31_FE0062();
           */
        }

        private void SimulationThread()
        {
            
            SimulationClass simulationClasss = new SimulationClass();
            simulationClasss.SetGlobalData(kmGlobalData);

            foreach (GlobalVar.Teststep teststep in liTeststepListSimulation)
            {
                MethodInfo mi = simulationClasss.GetType().GetMethod(teststep.Testfunction);
                if (mi != null)
                {
                    mi.Invoke(simulationClasss, new object[] {1 });
                }
            }

            for (;;)
            {
                kmFunctions.UpdateDigitalInputs(0);
                kmFunctions.UpdateAnalogInputs();

                foreach (GlobalVar.Teststep teststep in liTeststepListSimulation)
                {
                    MethodInfo mi = simulationClasss.GetType().GetMethod(teststep.Testfunction);
                    if (mi != null)
                    {
                        if (teststep.Testfunction == "Simul_Temp1")
                        {
                            int test = 3;
                        }
                        mi.Invoke(simulationClasss, new object[] { 2 });
                    }
                }

                kmFunctions.UpdateDigitalOutputs(false);
                kmFunctions.UpdateAnalogOutputs();
                kmFunctions.Sleep(200);
            }
            
        }


        public void UpdateGridColors()
        {
            for (int i = 0; i <= gridViewTestFunctions.RowCount - 1; i++)
            {
                string rowValue = gridViewTestFunctions.GetRowCellValue(i, "Testfunction").ToString();
                if (rowValue == liTeststepList[kmGlobalData.actTestProcedure].Testfunction)
                {
                    if ((!kmGlobalData.breakTestProgram) && (!kmGlobalData.repeatTestFunction) && (!kmGlobalData.ignoreTestFailure))
                    {
                        liTeststepList[kmGlobalData.actTestProcedure].Testresult = 1;
                        _CellColorHelper.SetCellColor(Convert.ToInt32(i), gridViewTestFunctions.Columns[Convert.ToInt32(0)], Color.LightGreen);
                    }
                    else
                    {
                        liTeststepList[kmGlobalData.actTestProcedure].Testresult = 2;
                        _CellColorHelper.SetCellColor(Convert.ToInt32(i), gridViewTestFunctions.Columns[Convert.ToInt32(0)], Color.OrangeRed);
                    }
                }
            }
        }

        private void StopSigmatek(int Type)
        {
            if (Type == 1)
            {
                kmGlobalData.systemSigmatekTester.OfflineH();
                mainTimer.Stop();
                

                mainTimer.Stop();
            }
            if (Type == 0)
            {
                kmGlobalData.systemSigmatekMachine.OfflineH();
                
                btnOnlineSigmatekMachine.ItemAppearance.Normal.BackColor = Color.Transparent;
                serverUpdateTimer.Stop();
            }
        }
        private bool StartSigmatek(int Type, string IPAddress)
        {
            uint ID = 0;

            if (Type == 1)
            {
                kmGlobalData.systemSigmatekTester.IPAddress = IPAddress;
                kmGlobalData.systemSigmatekTester.OnlineH();

                if (kmGlobalData.systemSigmatekTester.IsOnlineH())
                {

                    for (int j = 0; j <= 3; j++)
                    {
                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1800_DigitalAnalogInput_" + Convert.ToString(j + 1) + ".svrDI");
                      
                        kmGlobalData.svrDIAddress[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1800_DigitalAnalogInput_" + Convert.ToString(j + 1) + ".svrDIAIPos");
                        kmGlobalData.svrDIAIPosAddress[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1800_DigitalAnalogInput_" + Convert.ToString(j + 1) + ".svrDIAINeg");
                        kmGlobalData.svrDIAINegAddress[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1800_DigitalAnalogInput_" + Convert.ToString(j + 1) + ".svrAI");
                        kmGlobalData.svrAIAddress[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1800_1_DigitalAnalogInput_" + Convert.ToString(j + 1) + ".svrDI");
                        kmGlobalData.svrDIAddress_1[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1800_1_DigitalAnalogInput_" + Convert.ToString(j + 1) + ".svrDIAIPos");
                        kmGlobalData.svrDIAIPosAddress_1[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1800_1_DigitalAnalogInput_" + Convert.ToString(j + 1) + ".svrDIAINeg");
                        kmGlobalData.svrDIAINegAddress_1[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1800_1_DigitalAnalogInput_" + Convert.ToString(j + 1) + ".svrAI");
                        kmGlobalData.svrAIAddress_1[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1802_RelaisOuput_" + Convert.ToString(j + 1) + ".svrRelais");
                        kmGlobalData.svrROAddress[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1801_Output24V_" + Convert.ToString(j + 1) + ".svrDO");
                        kmGlobalData.svrDOAddress[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1801_Output24V_" + Convert.ToString(j + 1) + ".svrDOAI");
                        kmGlobalData.svrDOAIAddress[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1803_AnalogOutput_" + Convert.ToString(j + 1) + ".svrAO");
                        kmGlobalData.svrAOAddress[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1803_AnalogOutput_" + Convert.ToString(j + 1) + ".svrAOIn");
                        kmGlobalData.svrAOInAddress[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1803_AnalogOutput_" + Convert.ToString(j + 1) + ".svrTS");
                        kmGlobalData.svrTSAddress[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1803_AnalogOutput_" + Convert.ToString(j + 1) + ".svrTSIn");
                        kmGlobalData.svrTSInAddress[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                        ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1803_AnalogOutput_" + Convert.ToString(j + 1) + ".svrPTC");
                        kmGlobalData.svrPTCAddress[j] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);
                    }

                    ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1804.svrSupplyValues");
                    kmGlobalData.svrSupplyValuesAddress[0] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                    ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1804.svrSupplyStatus");
                    kmGlobalData.svrSupplyStatusAddress[0] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                    ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("L1804.svrSupplyVoltage");
                    kmGlobalData.svrSupplyVoltageAddress[0] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                    ID = kmGlobalData.systemSigmatekTester.GetLasalIdH("RO041_RelaisOut.svrNRO");
                    kmGlobalData.svrInterfaceStatusRelaisAddress[0] = kmGlobalData.systemSigmatekTester.CallReadMethodOfServerH_UDINT(ID);

                    kmGlobalData.svrStatusUpdateAddress = kmGlobalData.systemSigmatekTester.GetLasalIdH(("L1804.svrUpdateStatus"));
                  
                    mainTimer.Interval = 500;
                    mainTimer.Tick += new EventHandler(MainTimer_Tick);
                    mainTimer.Start();
                }
                return kmGlobalData.systemSigmatekTester.IsOnlineH();
            }
            return false;
        }

        private void SigmatekUpdateTimer_Tick(object sender, EventArgs e)
        {

            

            //Update Sigmatek Input Server SGM

            
            kmGlobalData.machineUpdateGroup += 1;

            if (kmGlobalData.machineUpdateGroup > 5)
                kmGlobalData.machineUpdateGroup = 1;
           

            //Update Input Werte Tester

        
           
        }

        private void MainTimer_Tick(object sender, EventArgs e)
        {

            //kmFunctions.UpdateTesterInputs(true);
            //kmFunctions.UpdateTesterOutputs();
           
            gridSGMInputs.RefreshDataSource();
            gridSGMInputs.RefreshDataSource();

            kmFunctions.UpdateDigitalInputs(1);

            if (updateDigIn)
            { 
                foreach (IOValue ioValue in kmGlobalData.listIOValues)
                {
                    if ((ioValue.IsTesterValue) && (!ioValue.IsAnalogValue) && (!ioValue.IsOutputValue))
                    {
                        if (ioValue.ActValue != ioValue.OldValue)
                        {
                            txtSourceCode.AppendText(ioValue.ChannelName + " Wert alt: " + ioValue.OldValue + " Wert neu: " + ioValue.ActValue + "\n");
                            txtSourceCode.AppendText("NegAlt: " + ioValue.OldHiddenValue1 + " NegNeu: " + ioValue.HiddenValue1 + " PosAlt: " + ioValue.OldHiddenValue2 + " PosNeu: " + ioValue.HiddenValue2 + "\n");
                            ioValue.OldValue = ioValue.ActValue;
                        }
                    }
                }
            }

            if ((kmGlobalData.initSimul) & (!kmGlobalData.simulRunning))
            {
                kmGlobalData.simulRunning = true;
                Thread trd = new Thread(new ThreadStart(this.SimulationThread));
                trd.IsBackground = false;
                trd.Start();
            }

            if ((kmGlobalData.startCycleDialog) & (!kmGlobalData.cycleDialogRunning))
            {
                kmGlobalData.cycleDialogRunning = true;
                mySimulation.KMGlobalData = kmGlobalData;
                mySimulation.SimulationGroup = kmGlobalData.simulationGroup;
                mySimulation.Show();
            }

            if ((kmGlobalData.cycleDialogRunning) && (!kmGlobalData.startCycleDialog))
            {
                mySimulation.Hide();
                kmGlobalData.startCycleDialog = false;
                kmGlobalData.cycleDialogRunning = false;
            }

            if (kmGlobalData.watchSigmatekMachine)
            {
              
            }
        }

        private void btnMultiIO_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MultiIO myDialog = new MultiIO(kmGlobalData,kmFunctions);
            kmGlobalData.ioTestpageopen = true;
            myDialog.ShowDialog();
            kmGlobalData.ioTestpageopen = false;
        }

        private void onlineSigmatekTester_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!kmGlobalData.systemSigmatekTester.IsOnlineH())
            {
                StartSigmatek(1, "10.100.100.1");
            }
            else
            {
                StopSigmatek(1);
            }
        }

        static string ReadZEs(string pszFile, GlobalVar.GlobalData kmGlobalData)
        {
            //FILE* pf;
            //int iLine;
            //CHAR szLine[1024];
            //PSZ psz;
            kmGlobalData.fZE = false;
            kmGlobalData.fZE2 = false;
            kmGlobalData.fSOND = false;
            kmGlobalData.cZEs = 0;
            //BOOL fZE, fZE2, fSOND;
            //ULONG ul;
            //int n;
            //CHAR szSOND[256];
            //CHAR szFU1_2[256];
            string TextVariable;

            StreamReader myFile = new StreamReader(pszFile, System.Text.Encoding.Default, false, 10000000);

            if (File.Exists(pszFile))
            {
                //string[] lines = System.IO.File.ReadAllLines(pszFile);
                string line = "";
                kmGlobalData.aszZEs.Clear();

                    while ((line = myFile.ReadLine()) != null)
                    {
                    string line1 = line.Replace("\n", String.Empty);
                    line1 = line1.TrimStart(' ');

                    if (line1.Substring(0, 1) == "\n" || line1.Substring(0, 1) == "\r" || line1.Substring(0, 1) == "-")
                        continue;

                    if (!kmGlobalData.fZE)
                    {

                        TextVariable = "Auftragsnummer";
                        if (line1.StartsWith(TextVariable))
                        {
                            line1 = line1.Substring(TextVariable.Length);
                            kmGlobalData.szOrderNumber = line1.TrimStart(' ');
                        }

                        TextVariable = "Maschinennummer";
                        if (line1.StartsWith(TextVariable))
                        {
                            line1 = line1.Substring(TextVariable.Length);
                            kmGlobalData.szMachineNumber = line1.TrimStart(' ');
                        }

                        TextVariable = "Maschinentyp";
                        if (line1.StartsWith(TextVariable))
                        {
                            line1 = line1.Substring(TextVariable.Length);
                            kmGlobalData.szMachineType = line1.TrimStart(' ');
                        }

                        TextVariable = "Kunde";
                        if (line1.StartsWith(TextVariable))
                        {
                            line1 = line1.Substring(TextVariable.Length);
                            kmGlobalData.szCustomer = line1.TrimStart(' ');
                        }

                        TextVariable = "BGV-Stand-Allgemein";
                        if (line1.StartsWith(TextVariable))
                        {
                            line1 = line1.Substring(TextVariable.Length);
                            kmGlobalData.szBGVStandA = line1.TrimStart(' ');
                        }

                        TextVariable = "BGV-Stand-Elektroplan";
                        if (line1.StartsWith(TextVariable))
                        {
                            line1 = line1.Substring(TextVariable.Length);
                            kmGlobalData.szBGVStand = line1.TrimStart(' ');
                        }

                        TextVariable = "Schliessentyp";
                        if (line1.StartsWith(TextVariable))
                        {
                            line1 = line1.Substring(TextVariable.Length);
                            kmGlobalData.usSchliessentyp = Int16.Parse(line1.TrimStart(' '));

                        }

                        TextVariable = "1. Spritzentyp";
                        if (line1.StartsWith(TextVariable))
                        {
                            line1 = line1.Substring(TextVariable.Length);
                            line1 = line1.TrimStart(' ');
                            line1 = line1.Replace(".", "");
                            kmGlobalData.usSpritzentyp1 = Int16.Parse(line1);

                        }

                        TextVariable = "2. Spritzentyp";
                        if (line1.StartsWith(TextVariable))
                        {
                            line1 = line1.Substring(TextVariable.Length);
                            line1 = line1.TrimStart(' ');
                            line1 = line1.Replace(".", "");
                            kmGlobalData.usSpritzentyp2 = Int16.Parse(line1);

                        }

                        TextVariable = "Zusatzeinrichtungen";
                        if (line1.StartsWith(TextVariable))
                        {
                            kmGlobalData.fZE = true;

                        }
                    }
                    else if (kmGlobalData.fZE)
                    {
                        if (kmGlobalData.cZEs < kmGlobalData.MAX_ZE)
                        {
                            int index1 = line1.IndexOf(' ');
                            int index2 = line1.IndexOf("\t");

                            if ((index2 != -1) || (index1 != -1))
                            {

                                if (((index2 < index1) && (index2 != -1)) || (index1 == -1))
                                {
                                    index1 = index2;
                                }

                                line1 = line1.Substring(0, index1);
                            }


                            if (line1.StartsWith("07-13"))
                            {
                                kmGlobalData.aszZEs.Add("07-12");
                            }

                            else if (line1.StartsWith("45-70"))
                            {
                                kmGlobalData.aszZEs.Add("Drehtisch");
                            }
                            else
                            {
                                kmGlobalData.aszZEs.Add(line1);
                            }

                            TextVariable = "Zusatzeinrichtungen Spritze-2";
                            if (line1.StartsWith(TextVariable))
                            {
                                kmGlobalData.fZE2 = true;
                                kmGlobalData.fZE = false;
                            }

                            TextVariable = "Sonderausführungen";
                            if (line1.StartsWith(TextVariable))
                            {
                                kmGlobalData.fSOND = true;
                                kmGlobalData.fZE2 = false;
                                kmGlobalData.fZE = false;
                            }

                            kmGlobalData.cZEs++;
                        }

                    }
                    else if (kmGlobalData.fZE2)
                    {
                        int index1 = line1.IndexOf(' ');
                        int index2 = line1.IndexOf("\t");

                        if ((index2 != -1) || (index1 != -1))
                        {

                            if (((index2 < index1) && (index2 != -1)) || (index1 == -1))
                            {
                                index1 = index2;
                            }
                        }

                        line1 = line1.Substring(0, index1);

                        kmGlobalData.aszZEs[kmGlobalData.cZEs] = line1;

                        TextVariable = "Sonderausführungen";
                        if (line1.StartsWith(TextVariable))
                        {
                            kmGlobalData.fSOND = true;
                            kmGlobalData.fZE2 = false;
                        }

                        TextVariable = "07-13";

                        if (line1.StartsWith(TextVariable))
                        {
                            kmGlobalData.aszZEs[kmGlobalData.cZEs] = "07-12_2";

                        }
                        kmGlobalData.cZEs++;
                    }

                    else if (kmGlobalData.fSOND)
                    {
                        int index1 = line1.IndexOf(' ');
                        int index2 = line1.IndexOf("\t");

                        if ((index2 != -1) || (index1 != -1))
                        {

                            if (((index2 < index1) && (index2 != -1)) || (index1 == -1))
                            {
                                index1 = index2;
                            }
                        }

                        line1 = line1.Substring(0, index1);

                        TextVariable = "00";
                        if (line1.StartsWith(TextVariable))
                        {
                            kmGlobalData.aszZEs[kmGlobalData.cZEs] = line1;
                        }
                    }
                }
            }
            return "";
        }

        static string ReadAssignmentList(string pszFile, GlobalVar.GlobalData kmGlobalData)
        {
            int bPin = 0;
            string sz = "";
            bool fCTMS, fCTMS030;
            string TextVariable;
            short absOP = 0;
            string[] aszParts = new string[9];
            int i, c;
            string psz = "";
            int diff = 0; ;
            string hexAdresse = "";
            int pinNummer = 0;
            int[] Addresses = new int[500];
            string[] Names = new string[500];


            StreamReader myFile = new StreamReader(pszFile, System.Text.Encoding.Default, false, 10000000);

            if (File.Exists(pszFile))
            {
                //string[] lines = System.IO.File.ReadAllLines(pszFile);
                string line = "";
                bool firstLine = true;
                while ((line = myFile.ReadLine()) != null)         
                {

                    if (firstLine)
                    {
                        TextVariable = "Absoluter_Operand";
                        if (line.StartsWith(TextVariable))
                        {
                            absOP = 1;
                        }

                        TextVariable = "Steuerung";
                        if (line.StartsWith(TextVariable))
                        {
                            absOP = 0;
                        }
                        firstLine = false;
                        continue;
                    }

                    if (absOP == 1)
                    {

                    }

                    if (absOP == 0)
                    {
                        if (line.StartsWith("\n"))
                        {
                            continue;
                        }
                        
                        if (kmGlobalData.cSymbols >= kmGlobalData.MAX_OPERANDS)
                        {
                            kmGlobalData.cSymbols = 0;
                            return ("Zu viele Einträge");
                        }


                        fSplitLine(line, aszParts, "\t", false);
                        psz = aszParts[1];

                        for (i = 0; i < kmGlobalData.OP_TYPE_MAX; i++)
                        {
                            c = GlobalVar.apszSigmaTypes[i].Length;
                            if (psz.Substring(0, c) == GlobalVar.apszSigmaTypes[i])
                            {
                                psz = aszParts[1].Substring(c);
                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType = i;
                                hexAdresse = psz.Substring(0, 2);
                                int Adresse = ConvertStringToHex(hexAdresse);
                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress = Adresse;
                                psz = psz.Substring(2);

                                if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.OP_TYPE_SI)
                                {
                                    //ToDo
                                }

                               
                                if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.OP_TYPE_SDI) || (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.OP_TYPE_SDPO))
                                {
                                    int steckplatz = Convert.ToInt16(psz.Substring(0, 2));
                                    kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo = steckplatz;
                                    psz = psz.Substring(2);

                                    if (psz.Substring(0, 1) != ".")
                                    {
                                        kmGlobalData.cSymbols = 0;
                                        return ("Kein Punkt");
                                    }

                                    psz = psz.Substring(1);
                                    pinNummer = Convert.ToInt16(psz.Substring(0, 2));
                                    kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo = pinNummer;
                                }
                                else
                                {
                                    if ((psz.Substring(0, 1) == "x") || (psz.Substring(0, 1) == "X"))
                                    {
                                        kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo = 15;
                                    }
                                    else
                                    {
                                        hexAdresse = psz.Substring(0, 1);
                                        int Slot = ConvertStringToHex(hexAdresse);
                                        kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo = Slot;
                                    }

                                    psz = psz.Substring(1);


                                    if (psz.Substring(0, 1) != ".")
                                    {
                                        kmGlobalData.cSymbols = 0;
                                        return ("Kein Punkt");
                                    }

                                    psz = psz.Substring(1);
                                    pinNummer = Convert.ToInt16(psz.Substring(0, 2));
                                    kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo = pinNummer;

                                    psz = aszParts[4];
                                    int index = psz.IndexOf('#');
                                    psz = psz.Substring(index + 1);


                                    
                                }

                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].szModule = aszParts[5];
                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr = aszParts[4];
                                /*
                                if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].szModule == "CRCH081")
                                {
                                    if (aszParts[1].Substring(0, 1) == "D")
                                    {
                                        kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType = GlobalVar._SymbolRec.OP_TYPE_TAO;
                                    }
                                }
                                */
                                break;
                            }
                        }

                        if (i >= GlobalVar._SymbolRec.OP_TYPE_MAX)
                        {
                            continue;
                        }

                        if (aszParts[2] == "A")
                        {
                            //strcat(sazParts[3],"_SP2";
                        }
                        else
                        {
                            aszParts[3] += "SP2";
                        }

                        if (kmGlobalData.szMachineType.Contains("AX/") || kmGlobalData.szMachineType.Contains("EX/") || kmGlobalData.szMachineType.Contains("CX/"))
                        {
                            diff = 0;
                            psz = aszParts[3];

                            if (psz == "T030/P CH1")
                            {
                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName = "T030/P CH1|DIFF";
                                diff = 1;
                            }

                            if (psz == "T030/P CH2")
                            {
                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName = "T030/P CH2|DIFF";
                                diff = 1;
                            }
                            if (psz == "T521")
                            {
                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName = "T521|DIFF";
                                diff = 1;
                            }

                            if (psz == "BG401")
                            {
                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName = "BG401|DIFF";
                                diff = 1;
                            }

                            if (diff == 0)
                            {
                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName = aszParts[3];
                            }
                        }
                        else
                        {
                            kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName = aszParts[3];
                        }

                        
                        if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].szModule == "CRCH081")  // Wenn Modultyp CRCH081 ist
                        {
                            if (aszParts[1].Substring(0,1) == "D")                  // und der erste Buchstabe des Operanden ein D (für DO) ist
                            {
                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType = GlobalVar._SymbolRec.OP_TYPE_TAO;           // dann soll der Typ TAO verwendet werden (Analogausgang)
                            }
                        }
                        ////// Ort //////////////////////////////////////////////////////
                        // wird nicht benötigt (aszParts[5])

                        ////// Kommentar_Operand ////////////////////////////////////////
                    }
                    kmGlobalData.cSymbols++;
                }
            }

            for (int j = 0; j < kmGlobalData.cSymbols; j++)
            {
                sz = "";
                if (kmGlobalData.aSymbols[j].bType >= 128)
                    continue;

                sz = kmGlobalData.aSymbols[j].szModule;
                bPin = kmGlobalData.aSymbols[j].bPinNo;

                if (sz == "CTMS030")
                    fCTMS030 = true;
                else
                    fCTMS030 = false;

                if ((sz == "CTMS") || (sz == "CWB") || (sz == "CM5V2K"))
                {
                    fCTMS = true;

                    if (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_DI)
                    {
                        if (bPin > 16)
                        {
                            sz = "CTMS_DI1";
                            bPin -= 16;
                        }
                        else
                            sz = "CTMS_DI0";
                    }
                    else if ((kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_DO) ||
                             (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_DPO) ||
                             (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_RO))
                    {
                        if (bPin > 16)
                        {
                            sz = "CTMS_DO1";
                            bPin -= 16;
                        }
                        else
                            sz = "CTMS_DO0";
                    }
                    else if ((kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_AI) ||
                             (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_ATI))
                    {
                        sz = "CTMS_AI";
                    }
                    else if (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_AO)
                    {
                        sz = "CTMS_AO";
                    }
                    else if (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_TSI)
                    {
                        sz = "CTMS_TSI";
                    }
                }
                else
                    fCTMS = false;


                sz += "_IM_";
                sz += kmGlobalData.aSymbols[j].bAddress.ToString("X2");
                sz += "_";
                sz += kmGlobalData.aSymbols[j].bSlotNo.ToString("d");

                switch (kmGlobalData.aSymbols[j].bType)
                {
                    case GlobalVar._SymbolRec.OP_TYPE_DI:                 // Digital In
                        sz = sz + ".Input" + bPin.ToString("d");
                        break;
                    case GlobalVar._SymbolRec.OP_TYPE_SDI:                 // Digital In für Savety
                        sz = sz + ".Safe_Input" + bPin.ToString("d");
                        break;
                    case GlobalVar._SymbolRec.OP_TYPE_DO:                 // Digital Out
                    case GlobalVar._SymbolRec.OP_TYPE_DPO:                // Digital Out (Power)
                    case GlobalVar._SymbolRec.OP_TYPE_RO:                 // Relais Out
                        sz = sz + ".Output" + bPin.ToString("d");
                        break;
                    case GlobalVar._SymbolRec.OP_TYPE_SDPO:
                        // Digital Out für Safety
                        sz = sz + ".Safe_Output" + bPin.ToString("d");
                        break;
                    case GlobalVar._SymbolRec.OP_TYPE_AO:                 // Analog Out
                        sz = sz + ".AO" + bPin.ToString("d");
                        break;
                    case GlobalVar._SymbolRec.OP_TYPE_AI:                 // Analog In
                        if (sz == "PVAI011_IM_81_0")     // Bei dem Modul PVAI011 ist der Analoge Input nicht nummeriert
                        {
                            sz = sz + ".AI";
                        }
                        else
                        {
                            sz = sz + ".AI" + bPin.ToString("d");
                        }
                        break;
                    case GlobalVar._SymbolRec.OP_TYPE_ATI:                // Temperaturmessung
                        if (fCTMS030)
                        {
                            //                    sprintf(psz,".ThermoTypKTY%d",bPin);
                            sz = psz + ".ThermoTypJ" + bPin.ToString("d");
                            //                     sprintf(psz,".ThermoClampingUnit%d",bPin);
                            //                    sprintf(psz,".ThermoClampingUnit);
                        }

                        else if (fCTMS)
                            sz = sz + ".Temp" + bPin.ToString("d");
                        else
                            sz = sz + ".TMP_" + bPin.ToString("d");
                        break;
                    case GlobalVar._SymbolRec.OP_TYPE_SI:                 // Schnittstelle
                        break;
                    case GlobalVar._SymbolRec.OP_TYPE_TSI:                // Weggeber
                        sz = sz + "_" + bPin.ToString("d") + ".Position";
                        break;
                    case GlobalVar._SymbolRec.OP_TYPE_TAO:                // Heizung ein für CRCH081 (10000)
                        sz = sz + ".Setpoint_" + bPin.ToString("d");
                        break;

                }
               
                kmGlobalData.aSymbols[j].szServer = sz;
                string signalName;
                string server;
                int address;
                string module;
                string description;
                int slotnumber, pinnumber;

                if ((kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_DI) || (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_SDI))
                {
                    signalName = kmGlobalData.aSymbols[j].szName;
                    server = kmGlobalData.aSymbols[j].szServer;
                    description = kmGlobalData.aSymbols[j].szDescr;
                    module = kmGlobalData.aSymbols[j].szModule;
                    slotnumber = kmGlobalData.aSymbols[j].bSlotNo;
                    pinnumber = kmGlobalData.aSymbols[j].bSlotNo;
                    address = kmGlobalData.aSymbols[j].bAddress;
                    kmGlobalData.listIOValues.Add(new IOValue(signalName, description, server,0, module, address, 0, false, false, false,  kmGlobalData.aSymbols[j].bType, slotnumber, pinnumber));
                }

                else if ((kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_DO) || (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_RO) || (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_RO) || (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_DPO) || (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_SDPO))
                {

                    signalName = kmGlobalData.aSymbols[j].szName;
                   
                   
                    server = kmGlobalData.aSymbols[j].szServer;
                    description = kmGlobalData.aSymbols[j].szDescr;
                    module = kmGlobalData.aSymbols[j].szModule;
                    slotnumber = kmGlobalData.aSymbols[j].bSlotNo;
                    pinnumber = kmGlobalData.aSymbols[j].bSlotNo;
                    address = kmGlobalData.aSymbols[j].bAddress;

                    kmGlobalData.listIOValues.Add(new IOValue(signalName, description, server, 0, module, address, 0, false, false, true, kmGlobalData.aSymbols[j].bType, slotnumber, pinnumber));
                }

                else if ((kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_AI) || (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_ATI) || (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_SDPO))
                {
                    signalName = kmGlobalData.aSymbols[j].szName;
                    server = kmGlobalData.aSymbols[j].szServer;
                    description = kmGlobalData.aSymbols[j].szDescr;
                    module = kmGlobalData.aSymbols[j].szModule;
                    slotnumber = kmGlobalData.aSymbols[j].bSlotNo;
                    pinnumber = kmGlobalData.aSymbols[j].bSlotNo;
                    address = kmGlobalData.aSymbols[j].bAddress;
                    kmGlobalData.listIOValues.Add(new IOValue(signalName, description, server, 0,  module, address, 0, false, true, false, kmGlobalData.aSymbols[j].bType, slotnumber, pinnumber));
                }
                else if ((kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_AO)|| (kmGlobalData.aSymbols[j].bType == GlobalVar._SymbolRec.OP_TYPE_TAO))
                {
                    signalName = kmGlobalData.aSymbols[j].szName;
                    server = kmGlobalData.aSymbols[j].szServer;
                    description = kmGlobalData.aSymbols[j].szDescr;
                    module = kmGlobalData.aSymbols[j].szModule;
                    slotnumber = kmGlobalData.aSymbols[j].bSlotNo;
                    pinnumber = kmGlobalData.aSymbols[j].bSlotNo;
                    address = kmGlobalData.aSymbols[j].bAddress;
                    kmGlobalData.listIOValues.Add(new IOValue(signalName, description, server,0, module, address, 0, false, true, true, kmGlobalData.aSymbols[j].bType, slotnumber, pinnumber));
                }
                //else
                //MessageBox.Show("Dieser Kanal ist nicht zuordenbar: " + sz);
            }
            return "";
        }


        /*
        Adapterdefinition
        Signal       Symbol  Bezeichnung
        M01.AI1	     #K521	 analoger Ausgang Regelung Spritze
        */
        static string ReadAdapterList(string pszFile, GlobalVar.GlobalData kmGlobalData)
        {
            short absOP = 0;
            string[] aszParts = new string[7];
            int i, c;
            string psz;
            int pinNummer;
            int startIndex = 0;
            int stopIndex = 0;
            int cardNumber = 0;

            StreamReader myFile = new StreamReader(pszFile, System.Text.Encoding.Default, false, 10000000);

            if (File.Exists(pszFile))
            {
                //string[] lines = System.IO.File.ReadAllLines(pszFile);
                string line = "";
               
                while ((line = myFile.ReadLine()) != null)
                {
                    if (line.Contains(";"))
                    {
                        continue;
                    }

                    if (line.StartsWith("Signal"))
                    {
                        continue;
                    }

                    if (absOP == 1)
                    {

                    }

                    if (absOP == 0)
                    {
                        if (kmGlobalData.cSymbols >= kmGlobalData.MAX_OPERANDS)
                        {
                            kmGlobalData.cSymbols = 0;
                            return ("Zu viele Einträge");
                        }

                        fSplitLine(line, aszParts, "\t", false);
                        psz = aszParts[1];

                        if (line.StartsWith("M"))
                        {
                            kmGlobalData.aSymbols[kmGlobalData.cSymbols].szModule = "Multi I/O";
                            kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType = GlobalVar._SymbolRec.USB_PROG_MULTI;
                        }

                        else if (line.StartsWith("N"))
                        {
                            kmGlobalData.aSymbols[kmGlobalData.cSymbols].szModule = "Netzspannung";
                            kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType = GlobalVar._SymbolRec.USB_PROG_NS;
                        }

                        else if (line.StartsWith("S"))
                        {
                            kmGlobalData.aSymbols[kmGlobalData.cSymbols].szModule = "Schnittstelle";
                            kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType = GlobalVar._SymbolRec.USB_PROG_COMM;
                        }

                        string strCardNumber = aszParts[0].Substring(1, 2);
                        int myCardNumber = Convert.ToInt16(strCardNumber);
                        kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress = myCardNumber;

                        startIndex = line.IndexOf(".");
                        stopIndex = line.IndexOf("\t");

                        psz = line.Substring(startIndex + 1, line.Length - startIndex - 1);

                        for (i = 0; i < kmGlobalData.TS_TPYE_MAX; i++)
                        {
                            c = GlobalVar.apszTestTypes[i].Length;
                            if (psz.Substring(0, c) == GlobalVar.apszTestTypes[i])
                            {
                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType = i + 128;
                                psz = psz.Substring(c, psz.Length - c);

                                switch (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType)
                                {
                                    case GlobalVar._SymbolRec.TS_TYPE_DI:
                                        {
                                            if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_MULTI)
                                            {
                                                return ("DI nur für Multi I/O Karten möglich");
                                            }

                                            break;
                                        }
                                    case GlobalVar._SymbolRec.TS_TYPE_RI:
                                        {
                                            if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_MULTI)
                                            {
                                                return ("RI nur für Multi I/O Karten möglich");
                                            }

                                            break;
                                        }

                                    case GlobalVar._SymbolRec.TS_TYPE_DO:
                                        {
                                            if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_MULTI)
                                            {
                                                return ("DO nur für Multi I/O Karten möglich");
                                            }

                                            break;
                                        }

                                    case GlobalVar._SymbolRec.TS_TYPE_AI:
                                        {
                                            if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_MULTI) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_COMM))
                                            {
                                                return ("AI nur für Multi I/O Karten und Schnittstellenkarten möglich");
                                            }

                                            break;
                                        }
                                    case GlobalVar._SymbolRec.TS_TYPE_AO:
                                        {
                                            if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_MULTI)
                                            {
                                                return ("AO nur für Multi I/O Karten möglich");
                                            }

                                            break;
                                        }
                                    case GlobalVar._SymbolRec.TS_TYPE_ST:
                                        {
                                            if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_MULTI)
                                            {
                                                return ("ST nur für Multi I/O Karten möglich");
                                            }

                                            break;
                                        }

                                    case GlobalVar._SymbolRec.TS_TYPE_TO:
                                        {
                                            if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_MULTI)
                                            {
                                                return ("TO nur für Multi I/O Karten möglich");
                                            }

                                            break;
                                        }
                                    case GlobalVar._SymbolRec.TS_TYPE_RO:
                                        {
                                            if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_MULTI) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_NS))
                                            {
                                                return ("RO nur für Multi I/O Karten und Netzspannungskarten möglich");
                                            }

                                            break;
                                        }
                                    case GlobalVar._SymbolRec.TS_TYPE_PT:
                                        {
                                            if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_MULTI)
                                            {
                                                return ("PT nur für Multi I/O Karten möglich");
                                            }

                                            break;
                                        }
                                    case GlobalVar._SymbolRec.TS_TYPE_US:
                                        {
                                            if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_MULTI)
                                            {
                                                return ("US nur für Multi I/O Karten möglich");
                                            }

                                            break;
                                        }
                                    case GlobalVar._SymbolRec.TS_TYPE_LI:
                                        {
                                            if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_NS)
                                            {
                                                return ("LI nur für Netzspannungskarten möglich");
                                            }

                                            break;
                                        }
                                    case GlobalVar._SymbolRec.TS_TYPE_PI:
                                        {
                                            if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_NS)
                                            {
                                                return ("PI nur für Netzspannungskarten möglich");
                                            }

                                            break;
                                        }
                                    case GlobalVar._SymbolRec.TS_TYPE_PM:
                                        {
                                            if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_NS)
                                            {
                                                return ("PM nur für Netzspannungskarten möglich");
                                            }

                                            break;
                                        }
                                    case GlobalVar._SymbolRec.TS_TYPE_MI:
                                        {
                                            if (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType != GlobalVar._SymbolRec.USB_PROG_NS)
                                            {
                                                return ("MI nur für Netzspannungskarten möglich");
                                            }

                                            break;
                                        }
                                    default:
                                        {
                                            return "interner Programmfehler (case fehlt)";
                                        }
                                }

                                stopIndex = psz.IndexOf("\t");
                                string strPinNumber = psz.Substring(0, stopIndex);
                                pinNummer = Convert.ToInt16(strPinNumber);


                                if (fIsZEEnabled("Blue", kmGlobalData))
                                {
                                    if (aszParts[1].Substring(0, 5) == "#M010")
                                        aszParts[1] = "#T011";
                                    if (aszParts[1].Substring(0, 5) == "#M095")
                                        aszParts[1] = "#M002";
                                }

                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo = pinNummer;
                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName = aszParts[1];
                                kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr = aszParts[2];
                            }
                        }
                    }

                    int pinNumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo;
                    cardNumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress;

                    //Tester Types
                    string signalName = "";
                    string description = "";
                    int slotnumber = 0; 
                    int pinnumber = 0;
                    int address = 0;

                   string mySignalName = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName;
                   
                   if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType == 1) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.TS_TYPE_DI))
                    {
                        
                        int index = (cardNumber - 1) * 20 + pinNumber-1;
                        signalName = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName;
                        description = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr;
                        slotnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo;
                        pinnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo;
                        address = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress;

                        
                       kmGlobalData.listIOValues.Add(new IOValue(signalName,description, "", 0,"", address, index, true, false, false, kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType,slotnumber,pinnumber));
                    }

                    if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType == 1) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.TS_TYPE_DO))
                    {
                        int index = (cardNumber - 1) * 8 + pinNumber-1;
                        signalName = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName;
                        description = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr;
                        slotnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo;
                        pinnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo;
                        address = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress;
                        kmGlobalData.listIOValues.Add(new IOValue(signalName, description, "",0, "", address, index, true, false, true, kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType, slotnumber, pinnumber));

                    }

                    if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType == 1) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.TS_TYPE_AI))
                    {
                        int index = (cardNumber - 1) * 4 + pinNumber-1;
                        signalName = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName;
                        description = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr;
                        slotnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo;
                        pinnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo;
                        address = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress;
                        kmGlobalData.listIOValues.Add(new IOValue(signalName, description, "",0, "", address, index, true, true, false, kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType, slotnumber, pinnumber));
                    }

                    if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType == 1) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.TS_TYPE_AO))
                    {
                        int index = (cardNumber - 1) * 4 + pinNumber-1;
                        signalName = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName;
                        description = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr;
                        slotnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo;
                        pinnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo;
                        address = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress;
                        kmGlobalData.listIOValues.Add(new IOValue(signalName, description, "",0, "", address, index, true, true, true, kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType, slotnumber, pinnumber));
                    }

                    if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType == 1) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.TS_TYPE_PT))
                    {
                        int index = (cardNumber - 1) + pinNumber-1;
                        signalName = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName;
                        description = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr;
                        slotnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo;
                        pinnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo;
                        address = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress;
                        kmGlobalData.listIOValues.Add(new IOValue(signalName, description, "", 0,"",address, index, true, false, true, kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType, slotnumber, pinnumber));
                    }

                    if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType == 1) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.TS_TYPE_RO))
                    {
                        
                        int index = (cardNumber - 1) * 8 + pinNumber-1;
                        signalName = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName;
                        description = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr;
                        slotnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo;
                        pinnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo;
                        address = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress;
                        kmGlobalData.listIOValues.Add(new IOValue(signalName, description, "",0, "", address, index, true, false, true, kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType, slotnumber, pinnumber));
                    }

                    if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType == 1) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.TS_TYPE_TO))
                    {
                        int index = (cardNumber - 1) * 4 + pinNumber-1;
                        signalName = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName;
                        description = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr;
                        slotnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo;
                        pinnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo;
                        address = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress;
                        kmGlobalData.listIOValues.Add(new IOValue(signalName, description, "", 0,"", address, index, true, true, true, kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType, slotnumber, pinnumber));
                    }

                    if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType == 2) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.TS_TYPE_LI))
                    {
                        int index = (cardNumber - 1) * 15 + pinNumber-1;
                        signalName = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName;
                        description = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr;
                        slotnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo;
                        pinnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo;
                        address = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress;
                        kmGlobalData.listIOValues.Add(new IOValue(signalName, description, "",0, "",address, index, true, false, false, kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType, slotnumber, pinnumber));
                    }

                    if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType == 2) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.TS_TYPE_PI))
                    {
                        int index = (cardNumber - 1) * 15 + pinNumber + 9;
                        signalName = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName;
                        description = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr;
                        slotnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo;
                        pinnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo;
                        address = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress;
                        kmGlobalData.listIOValues.Add(new IOValue(signalName, description, "", 0, "",address, index, true, false, false, kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType, slotnumber, pinnumber));
                    }

                    if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType == 2) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.TS_TYPE_MI))
                    {
                        int index = (cardNumber - 1) * 15 + pinNumber + 13;
                        signalName = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName;
                        description = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr;
                        slotnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo;
                        pinnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo;
                        address = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress;
                        kmGlobalData.listIOValues.Add(new IOValue(signalName, description, "",0, "",address, index, true, false, false, kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType, slotnumber, pinnumber));
                    }

                    if ((kmGlobalData.aSymbols[kmGlobalData.cSymbols].bCardType == 2) && (kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType == GlobalVar._SymbolRec.TS_TYPE_PM))
                    {
                        int index = (cardNumber - 1) * 4 + pinNumber-1;
                        signalName = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szName;
                        description = kmGlobalData.aSymbols[kmGlobalData.cSymbols].szDescr;
                        slotnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bSlotNo;
                        pinnumber = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bPinNo;
                        address = kmGlobalData.aSymbols[kmGlobalData.cSymbols].bAddress;
                        kmGlobalData.listIOValues.Add(new IOValue(signalName, description, "", 0, "", address, index, true, false, false, kmGlobalData.aSymbols[kmGlobalData.cSymbols].bType, slotnumber, pinnumber));
                    }

                    //Modus für Nulleitertest fehlt noch Digitalausgang!!

                    kmGlobalData.cSymbols++;
                }
            }
            return "";
        }

        static string ReadCommandList(string pszFile, GlobalVar.GlobalData kmGlobalData)
        { 
            string[] aszParts = new string[7];
          
            if (File.Exists(pszFile))
            {
                string[] lines = System.IO.File.ReadAllLines(pszFile);
                foreach (string line in lines)
                {
                    if (line.StartsWith("Text"))
                        continue;
                    fSplitLine(line, aszParts, "\t", false);

                    if (kmGlobalData.TesterLanguage == 0)
                        kmGlobalData.messageDictionary.Add(aszParts[0], aszParts[1]);

                    if (kmGlobalData.TesterLanguage == 1)
                        kmGlobalData.messageDictionary.Add(aszParts[0], aszParts[2]);
                }
            }
            return "";
        }

        static public int ConvertStringToHex(string HexValue)
        {
            return Convert.ToInt32(HexValue, 16);
        }

        static bool fSplitLine(string pszLine, string[] asz, string splitChar, bool removeTabs)
        {
            int iPart;
            int tabIndex = 0;
            int oldTabIndex = 0;

            for (iPart = 0; ; iPart++)
            {
                //pszLine = pszLine.TrimStart('\t');
                tabIndex = pszLine.IndexOf(splitChar, oldTabIndex);

                if ((tabIndex != -1) && (iPart < asz.Length - 1))
                {
                    if (iPart == 0)
                    {
                        asz[iPart] = pszLine.Substring(oldTabIndex, tabIndex);
                    }
                    else
                    {
                        asz[iPart] = pszLine.Substring(oldTabIndex, tabIndex - oldTabIndex);
                    }

                    if (removeTabs)
                    {
                        char tab = '\u0009';
                        asz[iPart] = asz[iPart].Replace(tab.ToString(), "");
                        if ((iPart == 2) || iPart == 4)
                            asz[iPart] = asz[iPart].Replace(" ", "");
                        asz[iPart] = asz[iPart].Replace("{", "");
                        asz[iPart] = asz[iPart].Replace("}", "");
                        asz[iPart] = asz[iPart].Replace("\"", "");
                    }
                    oldTabIndex = tabIndex + 1;
                }
                else
                {
                    asz[iPart] = pszLine.Substring(oldTabIndex, pszLine.Length - oldTabIndex);
                    break;
                }
            }

            return false;
        }
        public static bool fIsZEEnabled(string pszZE, GlobalVar.GlobalData kmGlobalData)
        {
            foreach (string myZE in kmGlobalData.aszZEs)
            {
                if (myZE == pszZE)
                {
                    return true;
                }
            }
            return false;
        }

        //Laden der Teststeps aus dem entsprechenden C++ file und befüllen der en tsprechenden Liste
        static string CreateTestDLL(string pszFile, BindingList<GlobalVar.Teststep> myTeststepList, ListBox myListBox, System.Windows.Forms.TextBox myTextBox, int min, int max, bool all)
        {
            //Teststep myTeststep = new Teststep("Vorbereitung", "CX_Prepare");
            myTeststepList.Clear();
            bool startTestSteps = false;
            string[] tsParts = new string[7];
            string[] ZEs = new string[20];
            StreamReader myFile = new StreamReader(pszFile, System.Text.Encoding.Default, false, 10000000);
            bool failure = false;

            if (File.Exists(pszFile))
            {
                //string[] lines = System.IO.File.ReadAllLines(pszFile);
                string line = "";
                while ((line = myFile.ReadLine()) != null)
                {
                    Array.Clear(ZEs, 0, ZEs.Length);
                    if (line.Contains("static const TESTSTEP"))
                    {
                        startTestSteps = true;
                        continue;
                    }
                    if (startTestSteps)
                    {
                        if (line.StartsWith("{ "))
                        {
                            fSplitLineTestStep(line, tsParts);
                        }
                        else
                            continue;
                        GlobalVar.Teststep myTeststep = new GlobalVar.Teststep("a", "b", false);
                        myTeststep.Testdescription = tsParts[1];
                        myTeststep.Testfunction = tsParts[0];
                        if (tsParts[2] != "")
                        {
                            fSplitLine(tsParts[2], ZEs, "|", false);
                            for (int i = 0; ; i++)
                            {
                                if (ZEs[i] != null)
                                {
                                    myTeststep.ZE.Add(ZEs[i]);
                                }
                                else
                                    break;

                            }
                        }
                        myTeststepList.Add(myTeststep);
                    }
                }

                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                CompilerParameters parameters = new CompilerParameters();
                parameters.GenerateInMemory = true;
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                parameters.ReferencedAssemblies.Add("KraussMaffeiData.dll");
                parameters.OutputAssembly = @"d:\Synchronics\DynamicCompiling\MyCompiledAssembly.dll";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"using System;");
                sb.AppendLine(@"using System.Threading;");
                sb.AppendLine(@"using System.Windows.Forms;");
                sb.AppendLine(@"namespace KraussMaffeiData {");
                sb.AppendLine(@"public class Testklasse{");
                sb.AppendLine(@"private GlobalVar.GlobalData KMGlobalData;");
                sb.AppendLine(@"public void SetGlobalData(GlobalVar.GlobalData kmGlobalData)");
                sb.AppendLine(@"{");
                sb.AppendLine(@"KMGlobalData = kmGlobalData;");
                sb.AppendLine(@"}");
                sb.AppendLine(@"KraussMaffeiFunctions kmFunctions = new KraussMaffeiFunctions();");

                myFile.BaseStream.Position = 0;
                string statement = "";

                while ((line = myFile.ReadLine()) != null)
                {
                    statement = line.Trim();
                    if (statement.StartsWith("#define"))
                    {

                        statement = statement.Replace("#define", "int ");
                        statement = statement.Replace("\t\t\t", "\t");
                        statement = statement.Replace("\t", " = ");
                        statement = statement.Replace("= //", "; //");
                        statement += ";";
                        sb.AppendLine(statement);
                    }
                }

                int loopCount = 0;

                foreach (GlobalVar.Teststep myTeststep in myTeststepList)
                {
                    loopCount++;
              
                    if ((loopCount < min || loopCount > max) && (!all))
                        continue;
                    //CodeDomProvider

                    //Klassendefinition muss noch angepasst werden

                    bool functionStart = false;
                    bool firstBracket = false;
                    bool continueStatement = false;
                    bool isAskStatement = false;

                    string statement1 = "";
                    myFile.BaseStream.Position = 0;

                    while ((line = myFile.ReadLine()) != null)
                    {

                        line = line.Trim();
                        if (line.StartsWith("//"))
                            continue;

                        if (!continueStatement)
                        {
                            statement = line;
                            statement = statement.Trim();
                        }
                        else
                        {
                           line = line.Trim();
                           if ((line.StartsWith("\"")) && (isAskStatement))
                                line = line.Substring(1, line.Length - 1);
                            //if (line.EndsWith("\""))
                            //    line = line.Substring(0, line.Length - 1);
                            statement += line;
                            if (statement.Length > 5000)
                            {
                                failure = true;
                                break;
                            }
                        }

                        string functionName = statement.Replace("static LPCSTR ", "");
                        functionName = functionName.Replace("(", "");

                        if (functionName == myTeststep.Testfunction && statement.StartsWith("static LPCSTR ") && !functionStart)
                        {
                            functionStart = true;
                            statement = statement.Replace("static LPCSTR ", "");
                            statement += ")";
                            statement = "public void " + statement;
                        }

         
                        

                        if (!functionStart)
                            continue;

                        if (statement.Contains("BEGIN_TEST") || statement.Contains("END_TEST"))
                            continue;

                        if (statement.Contains("PTESTDATA"))
                            continue;

                        if (statement == "")
                            continue;

                        if (statement.StartsWith("//"))
                            continue;

                        if (statement.StartsWith("TEST(InitUSB"))
                            continue;

                        if ((statement.StartsWith("EXIT_TEST")) | (statement.StartsWith("EMPTY")))
                        {
                            sb.AppendLine("kmFunctions.ResetAllMachineOutputs();");
                            sb.AppendLine("kmFunctions.UpdateDigitalOutputs(false);");
                            sb.AppendLine("kmFunctions.UpdateAnalogOutputs();");
                            sb.AppendLine("KMGlobalData.testSequenceRunning = false;");
                            sb.AppendLine("}");
                            break;
                        }

                        if (statement == "\\n\\")
                            break;

                        if (statement.Contains("PSZ psz"))
                            continue;

                        if (statement.Contains("psz = szTest"))
                            continue;

                        if (statement.StartsWith("DispTestInfoLine("))
                        {
                            statement = "kmFunctions." + statement;
                            statement = statement.Replace("DispTestInfoLine(\"", "DispTestInfoLine(\"\t ");
                        }

                        if (statement.Contains("psz++"))
                            continue;

                        if (statement.StartsWith("Sleep"))
                            statement = "kmFunctions." + statement;

                        if (statement.Contains("strstr"))
                            statement = statement.Replace("strstr", "kmFunctions.strstr");

                        if (statement.Contains("fIsZEEnabled"))
                            statement = statement.Replace("fIsZEEnabled", "kmFunctions.fIsZEEnabled");

                        if (statement.Contains("szBGVStand"))
                            statement = statement.Replace("szBGVStand", "KMGlobalData.szBGVStand");

                        if (statement.Contains("szMachineType"))
                            statement = statement.Replace("szMachineType", "KMGlobalData.szMachineType");

                        if (statement.Contains("usSchliessentyp"))
                            statement = statement.Replace("usSchliessentyp", "KMGlobalData.usSchliessentyp");

                        if (statement.Contains("usSpritzentyp"))
                            statement = statement.Replace("usSpritzentyp", "KMGlobalData.usSpritzentyp");

                        if (statement.StartsWith("CX_Z"))
                            statement = statement.Replace("CX_Z", "kmFunctions.CX_Z");

                        if (statement.Contains("GetDigIn"))
                            statement = statement.Replace("&", "ref ");

                        if (statement.Contains("AskUser") && !statement.Contains("TEST(AskUser") && !statement.Contains("kmFunctions.AskUser"))
                        {
                            statement = statement.Replace("AskUser", "kmFunctions.AskUser");
                        }

                        if (statement.StartsWith("return \""))
                        {
                            int startIndex = statement.IndexOf("\"");
                            int stopIndex = statement.IndexOf("\"", startIndex + 1);
                            statement = statement.Substring(startIndex + 1, stopIndex - startIndex - 1);
                            statement = "kmFunctions.AskUserSpecial(\"" + statement + "\");";
                        }

                        bool test = false;
                        if (test != true)
                            {

                        }

                        if (statement.Contains("ExitSigmatek("))
                            statement = statement.Replace("ExitSigmatek", "kmFunctions.ExitSigmatek");

                        if (statement.Contains("TESTERROR("))
                            statement = statement.Replace("TESTERROR", "kmFunctions.TESTERROR");

                        if (!statement.StartsWith("/*") && !statement.Contains("kmFunctions.TestDigInSingle") && !statement.Contains("kmFunctions.SetWatchListEntries")
                            && (statement.Contains("WaitForInputChange(") || statement.Contains("SetAnaOut(") || statement.Contains("TestAnaIn(") || statement.Contains("TestAnaInMinMax(") || statement.Contains("GetAnaIn(") || statement.Contains("GetDigIn(") || statement.Contains("TEST(TestDigInChange(")
                            || statement.Contains("ResetSigmatek(") || statement.Contains("WaitForInput(") || statement.Contains("WaitForInputLanguage(") || statement.Contains("TestDigInSingle")
                            || statement.Contains("SetWatchListEntries") || statement.Contains("TEST(SaveDigInState(") || statement.Contains("TEST(TestDigIn(") || statement.Contains("TEST(AskUser")
                            || statement.StartsWith("TEST(OpenDialog") || statement.StartsWith("TEST(InitSigmatek") || statement.StartsWith("TEST(InitIO") || statement.StartsWith("TEST(ResetAllDigOut")
                            || statement.StartsWith("TEST(SetDigOut") || statement.StartsWith("DispUsedZE") | statement.Contains("WaitCycleTest")))
                        {
                            statement = statement.Replace(",NULL", "");
                            statement = statement.Replace(",\tNULL", "");
                            statement = statement.Replace(", NULL", "");
                            statement = statement.Replace("(NULL)", "()");
                            statement = statement.Replace("TEST(", "");
                            statement = "kmFunctions." + statement;
                            statement = statement.Replace("))", ")");
                        }

                        if (statement.Contains("ExitSimul"))
                        {
                            statement = "kmFunctions." + statement;
                        }
                        
                        if (statement.Contains("InitSimul"))
                        {
                            statement = statement.Replace("TEST(", "");
                           
                            int index1 = statement.IndexOf("(");
                            statement = statement.Substring(0, index1+1);
                            statement = statement + ");";
                            statement = "kmFunctions." + statement;

                        }
                        
                        if (statement.Contains("InitSigmatek()"))
                            statement = statement.Replace("InitSigmatek()", "InitSigmatek()");

                        if (statement.StartsWith("TEST("))
                        {
                            statement = "kmFunctions." + statement;
                        }


                        if ((statement == "{") && !firstBracket)
                        {
                            statement1 = "kmFunctions.SetGlobalData(KMGlobalData);";
                            firstBracket = true;
                        }

                        string statementWithoutComment;

                        if (statement.Contains("//"))
                        {
                            int commentIndex = statement.IndexOf("//");
                            statementWithoutComment = statement.Substring(0, commentIndex);
                            statementWithoutComment =  statementWithoutComment.Trim();
                        }
                        else
                            statementWithoutComment = statement;

                        if ((statementWithoutComment.EndsWith("\\n\\")) || (statementWithoutComment.EndsWith(",")) || (statementWithoutComment.EndsWith("\\n\"")) || (statementWithoutComment.EndsWith("\\n")) || (statementWithoutComment.EndsWith("\"")))
                        {      
                            if (statementWithoutComment.EndsWith("\\n\""))
                                statementWithoutComment = statement.Substring(0, statement.Length - 1);
                            if (statementWithoutComment.Contains("AskUser"))
                            { 
                                isAskStatement = true;
                                statement = statementWithoutComment;
                            }

                            if (statement.Contains("//"))
                            {
                                int startIndex = statement.IndexOf("//");
                                statement = statement.Substring(0, startIndex);
                            }
    
                            if (statementWithoutComment.EndsWith("\"")) 
                            {     
                                int index;
                                int commentIndex = statement.IndexOf("//");
                                if (commentIndex != -1)
                                    index = statement.LastIndexOf("\"", commentIndex);
                                else
                                    index = statement.LastIndexOf("\"");

                                if (index != statement.LastIndexOf("\","))
                                {
                                    statement = statement.Insert(index + 1, ",");
                                }
                            }
                       
                            continueStatement = true;        
                        }
                        else
                        {
                            if (continueStatement)
                            {
                                continueStatement = false;
                                isAskStatement = false;
                                statement = statement.Replace("));", ");");
                            }
                        }

                        if (statement.EndsWith("\\n\\"))
                        {
                            statement = statement.Replace("\\n\\", "");
                            statement += "\\n";
                        }

                        if (statement.Contains("SetCom20mA"))
                            statement = statement.Replace("SetCom20mA", "kmFunctions.SetCom20mA");

                        if (statement.Contains("SetComRS232"))
                            statement = statement.Replace("SetComRS232", "kmFunctions.SetComRS232");

                        if (statement.Contains("fMainSwitchOn"))
                            statement = statement.Replace("fMainSwitchOn", "kmFunctions.fMainSwitchOn");

                        if (statement.Contains("CHAR szTest[]"))
                            statement = statement.Replace("CHAR szTest[]", "string szTest");

                        if (statement.Contains("CHAR led"))
                            statement = "string led;";

                        if (statement.Contains("sprintf(led"))
                            statement = "led = \"LED\" + i.ToString();";

                        if (statement.StartsWith("LONG lVal"))
                        {
                            statement = statement.Replace("LONG", "long");
                            statement = statement.Replace(",", "=0,");
                            statement = statement.Replace(";", "=0;");
                        }

                        if (statement.StartsWith("LONG l"))
                        {
                            statement = statement.Replace("LONG", "long");
                            statement = statement.Replace(",", "=0,");
                            statement = statement.Replace(";", "=0;");
                        }

                        if (statement.StartsWith("LONG Spritze2"))
                        {
                            statement = statement.Replace("LONG", "long");
                            statement = statement.Replace(",", "=0,");
                            statement = statement.Replace(";", "=0;");
                        }

                        if (statement.Contains("while (*psz)"))
                            statement = statement.Replace("while (*psz)", "foreach (char psz in szTest)");

                        if (statement.Contains("*psz"))
                            statement = statement.Replace("*psz", "psz");

                        if (statement.Contains("LONG"))
                            statement = statement.Replace("LONG", "long");

                        if (statement.Contains("&lVal"))
                            statement = statement.Replace("&lVal", "ref lVal");

                        if (statement.Contains("&l"))
                            statement = statement.Replace("&l", "ref l");

                        if (statement.Contains("OpenEnergyValueBox"))
                        {
                            statement = statement.Replace("hwndFrame,", "");
                            statement = statement.Replace("OpenEnergyValueBox", "kmFunctions.OpenEnergyValueBox");
                        }

                        if (statement.Contains("szErrorTextCancel"))
                            statement = statement.Replace("szErrorTextCancel", "KMGlobalData.szErrorTextCancel");

                        if (statement.StartsWith("break"))
                            statement = statement.Replace("break", "MessageBox.Show(\"Break\");");

                        statement = statement.Replace("NO_ERROR", "true");
                        statement = statement.Replace("IDD_OVERVIEW", "\"Overview\"");
                        statement = statement.Replace("FALSE", "false");
                        statement = statement.Replace(",NULL", "");
                        statement = statement.Replace(", NULL", "");

                        if (statement.Contains("BYTE"))
                        {
                            statement = statement.Replace("BYTE", "byte");
                            statement = statement.Replace(";", "=0;");
                            statement = statement.Replace(",", "=0,");
                        }

                        if (!continueStatement)
                        {

                            if (statement.Contains("AskUser"))
                                statement = statement.Replace("));", ");");

                            if (statement.StartsWith("kmFunctions.TestDigIn"))
                            {
                                if (statementWithoutComment.EndsWith(")"))
                                {
                                    int index;
                                    int commentIndex = statement.IndexOf("//");
                                    if (commentIndex != -1)
                                        index = statement.LastIndexOf(")", commentIndex);
                                    else
                                        index = statement.LastIndexOf(")");

                                    if (index != statement.LastIndexOf(");,"))
                                    {
                                        statement = statement.Insert(index + 1, ";");
                                    }
                                }

                            }

                            sb.AppendLine(statement);
                            
                        }

                        if (statement1 != "")
                        {
                            sb.AppendLine(statement1);
                            statement1 = "";
                        }
                    }
                    if (failure)
                        return "Failure in" + myTeststep.Testfunction;

                    //CodDomProvider Abschluss   
                }

                sb.AppendLine(@"}}");
                string thisString = sb.ToString();
                myTextBox.Text = thisString;

                CompilerResults result = provider.CompileAssemblyFromSource(parameters, sb.ToString());
                myListBox.DataSource = result.Errors;
                myListBox.DisplayMember = "ErrorText";
                if (!result.Errors.HasErrors)
                {
                    object instance = result.CompiledAssembly.CreateInstance("KraussMaffeiNeu.Testklasse");
                    //MethodInfo info = instance.GetType().GetMethod("TestFunktion");
                    //info.Invoke(instance, null);
                }
                else
                {
                    MessageBox.Show("Fehler beim Compilieren");
                }
            }
            return "";
        }

        static void ReplaceDefineStatement(ref string statement)
        {
                int index = 0;
                int index1 = statement.IndexOf(" ", 8);
                int index2 = statement.IndexOf("\t", 7);

                if (index1 == -1)
                    index = index2;
                else if (index2 == -1)
                {
                    index = index1;
                }
                else
                {
                    if ((index1 < index2) && (index1 != -1))
                        index = index1;
                    if ((index2 < index1) && (index2 != -1))
                        index = index2;
                }

                statement += ";";
                if(!statement.Contains("="))
                    statement = statement.Replace("(", "=(");
                if (!statement.Contains("="))
                    statement = statement.Insert(index, "=");
                statement = statement.Replace("//", ";//");
                statement = statement.Replace("#define", "static int ");
        }

        private void AddStaticVariables(string staticString, string functionName)
        {
            string varName = "";
            staticString.Trim();
            int startIndex, endIndex = 0;
            bool end = false;
            startIndex = staticString.IndexOf(" ", 8);
            if (staticString.Contains(","))
            {
                do
                {
                    endIndex = staticString.IndexOf(",", startIndex+1);
                    if (endIndex ==-1)
                    {
                        endIndex = staticString.IndexOf(";");
                        end = true;
                    }
                    
                    varName = staticString.Substring(startIndex, endIndex-startIndex);
                    varName = varName.Trim();
                    kmGlobalData.staticVariablesList.Add(new StaticVariables(varName, functionName+varName, functionName));
                    startIndex = endIndex+1;
                }
                while (!end);
            }
            else
            {
                if (staticString.Contains("["))
                    endIndex = staticString.IndexOf("[");
                else
                    endIndex = staticString.IndexOf(";");
                varName = staticString.Substring(startIndex, endIndex-startIndex);
                varName = varName.Trim();
                kmGlobalData.staticVariablesList.Add(new StaticVariables(varName, functionName + varName, functionName));
            }
        }

        private void ReplaceStaticVariables(ref bool correction, ref string statement, string realFunctionName)
        {
            if (!correction)
            {
                foreach (StaticVariables staticVariable in kmGlobalData.staticVariablesList)
                {
                    if ((statement.Contains(staticVariable.OldVarName) && (staticVariable.FunctionName == realFunctionName)) && (!statement.Contains(staticVariable.NewVarName)))
                    {
                        statement = statement.Replace(staticVariable.OldVarName, staticVariable.NewVarName);
                    }
                }
                correction = true;
            }
        }

        private string CreateSimulationDLL(string pszFile, BindingList<GlobalVar.Teststep> myTeststepList, ListBox myListBox, System.Windows.Forms.TextBox myTextBox, int min, int max, bool all)
        {
            //Teststep myTeststep = new Teststep("Vorbereitung", "CX_Prepare");
            myTeststepList.Clear();
            bool startTestSteps = false;
            string[] tsParts = new string[7];
            string[] ZEs = new string[20];
            StreamReader myFile = new StreamReader(pszFile, System.Text.Encoding.Default, false, 10000000);
            bool failure = false;
            bool simulExit = false;
            bool correction = false;

            if (File.Exists(pszFile))
            {
                //string[] lines = System.IO.File.ReadAllLines(pszFile);
                string line = "";
                while ((line = myFile.ReadLine()) != null)
                {
                    Array.Clear(ZEs, 0, ZEs.Length);
                    if (line.Contains("PUBLIC const PSIMULFUNC"))
                    {
                        startTestSteps = true;
                        continue;
                    }
                    if (startTestSteps)
                    {
                        line = line.Trim();
                        if (line.StartsWith("Simul"))
                        {
                            int index1 = line.IndexOf(",");
                            line = line.Substring(0, index1);
                        }
                        else
                            continue;
                        GlobalVar.Teststep myTeststep = new GlobalVar.Teststep("a", "b", false);
                        myTeststep.Testfunction = line;
                       
                        myTeststepList.Add(myTeststep);
                    }
                }

                CodeDomProvider provider = CodeDomProvider.CreateProvider("CSharp");
                CompilerParameters parameters = new CompilerParameters();
                parameters.GenerateInMemory = true;
                parameters.ReferencedAssemblies.Add("System.dll");
                parameters.ReferencedAssemblies.Add("System.Windows.Forms.dll");
                parameters.ReferencedAssemblies.Add("KraussMaffeiData.dll");
                parameters.OutputAssembly = @"d:\Synchronics\DynamicCompiling\Simulation.dll";
                StringBuilder sb = new StringBuilder();
                sb.AppendLine(@"using System;");
                sb.AppendLine(@"using System.Threading;");
                sb.AppendLine(@"using System.Windows.Forms;");
                sb.AppendLine(@"namespace KraussMaffeiData {");
                sb.AppendLine(@"public class SimulatonClass{");
                sb.AppendLine(@"private GlobalVar.GlobalData KMGlobalData;");
                sb.AppendLine(@"public void SetGlobalData(GlobalVar.GlobalData kmGlobalData)");
                sb.AppendLine(@"{");
                sb.AppendLine(@"KMGlobalData = kmGlobalData;");
                sb.AppendLine(@"}");
                sb.AppendLine(@"KraussMaffeiFunctions kmFunctions = new KraussMaffeiFunctions();");

                myFile.BaseStream.Position = 0;
                string statement = "";
               
                int loopCount = 0;

                //Loop for static variables

                foreach (GlobalVar.Teststep myTeststep in myTeststepList)
                {
                    loopCount++;
                    bool functionStart = false;
                    myFile.BaseStream.Position = 0;
                    string realFunctionName = "";

                    while ((line = myFile.ReadLine()) != null)
                    {

                        statement = line;
                        statement = statement.Trim();
                        string functionName = statement.Replace("static LPCSTR ", "");
                        functionName = functionName.Replace("(", "");
             
                        if (functionName == myTeststep.Testfunction && statement.StartsWith("static LPCSTR ") && !functionStart)
                        {

                            functionStart = true;
                            realFunctionName = functionName;
                            statement = statement.Replace("static LPCSTR ", "");
                            statement += "int dwFunc)";
                            statement = "public int " + statement;
                            simulExit = false;
                        }

                        if ((realFunctionName == myTeststep.Testfunction) && functionStart)
                        {

                            if ((statement.Contains("static")) && (!statement.Contains("static LPCSTR")))
                            {
                                AddStaticVariables(statement, realFunctionName);

                                if (!functionStart)
                                    continue;

                                if ((statement.Contains("static LONG")) && (statement.Contains("[")))
                                {
                                    statement = statement.Replace("[", " = new long[");
                                    statement = statement.Replace("static LONG ", "long[] ");
                                }
                              
                                else if ((statement.Contains("static LONG")) && (!statement.Contains("ULONG")))
                                {
                                    statement = statement.Replace("static LONG ", "long ");
                                    statement = statement.Replace(";", " = 0;");

                                }
                                if ((statement.Contains("static SymbolRec*")) && (statement.Contains("[")))
                                {
                                    statement = statement.Replace("[", " = new IOValue[");
                                    statement = statement.Replace("static SymbolRec* ", "IOValue[] ");
                                }
                                else if (statement.Contains("static SymbolRec*"))
                                {
                                    statement = statement.Replace("static SymbolRec* ", "IOValue ");
                                    statement = statement.Replace(";", " = new IOValue();");
                                }

                                if ((statement.Contains("static double")) && (statement.Contains("[")))
                                {
                                    statement = statement.Replace("[", " = new double[");
                                    statement = statement.Replace("static double ", "double[] ");
                                }
                                else if (statement.Contains("static double"))
                                {
                                    statement = statement.Replace("static double ", "double ");
                                    statement = statement.Replace(";", " = 0.0;");
                                }
                                if ((statement.Contains("static BOOL")) && (statement.Contains("[")))
                                {
                                    statement = statement.Replace("[", " = new bool[");
                                    statement = statement.Replace("static BOOL ", "bool[] ");
                                }
                                else if (statement.Contains("static BOOL"))
                                {
                                    statement = statement.Replace("static BOOL ", "bool ");
                                    statement = statement.Replace(";", " = false;");
                                    statement = statement.Replace(",", " = false,");
                                }

                                if ((statement.Contains("BYTE")) && (statement.Contains("[")))
                                {
                                    statement = statement.Replace("[", " = new byte[");
                                    statement = statement.Replace("static BYTE ", "byte[] ");
                                    statement = statement.Replace("BYTE", "byte[]");
                                }
                                else if (statement.Contains("BYTE"))
                                {
                                    statement = statement.Replace("static BYTE ", "byte ");
                                    statement = statement.Replace(";", " = 0;");
                                }

                                if ((statement.Contains("static PSZ")) && (statement.Contains("[")))
                                {
                                    statement = statement.Replace("[", " = new string[");
                                    statement = statement.Replace("static PSZ ", "string[]");
                                }
                                else if (statement.Contains("static PSZ"))
                                {
                                    statement = statement.Replace("static PSZ ", "string");
                                    statement = statement.Replace(";", " = 0;");

                                }

                                if ((statement.Contains("static ULONG")) && (statement.Contains("[")))
                                {
                                    statement = statement.Replace("[", " = new int[");
                                    statement = statement.Replace("static ULONG ", "int[] ");
                                }
                                else if (statement.Contains("static ULONG"))
                                {
                                    statement = statement.Replace("static ULONG ", "int ");
                                    statement = statement.Replace(";", " = 0;");
                                }

                                if ((statement.Contains("static TMRREC")) && (statement.Contains("[")))
                                {
                                    statement = statement.Replace("[", " = new GlobalVar.TMRREC[");
                                    statement = statement.Replace("static TMRREC ", "GlobalVar.TMRREC[] ");
                                }
                                else if (statement.Contains("static TMRREC"))
                                {
                                    statement = statement.Replace("static TMRREC ", "GlobalVar.TMRREC ");
                                    statement = statement.Replace(";", " = new GlobalVar.TMRREC();");
                                }

                                foreach (StaticVariables staticVariable in kmGlobalData.staticVariablesList)
                                {
                                    if ((statement.Contains(staticVariable.OldVarName) && (staticVariable.FunctionName == realFunctionName)) && (!statement.Contains(staticVariable.NewVarName)))
                                    {
                                        statement = statement.Replace(staticVariable.OldVarName, staticVariable.NewVarName);
                                    }
                                }

                                sb.AppendLine(statement);
                            }


                            if (statement.Contains("SIMUL_EXIT"))
                            {
                                statement = statement.Replace("SIMUL_EXIT", "GlobalVar._SymbolRec.SIMUL_EXIT");
                                simulExit = true;
                            }

                            if ((statement.Contains("return NO_ERROR")) && (simulExit))
                            {
                                statement = "return 1;\n}";
                                functionStart = false;
                            }
                            statement = statement.Trim();
                            if (statement.StartsWith("#define"))
                            {
                                ReplaceDefineStatement(ref statement);
                                sb.AppendLine(statement);
                            }
                        }
                    }
                }

                //End loop for static variables

                string updateStatement = "";
                int bracketCounter = 0;
                bool startSimulTask = false;

               foreach (GlobalVar.Teststep myTeststep in myTeststepList)
                {
                    loopCount++;

                    bool functionStart = false;
                    bool firstBracket = false;
                    bool continueStatement = false;
                    bool isAskStatement = false;
                    string realFunctionName = "";

                    string statement1 = "";
                    myFile.BaseStream.Position = 0;

                    while ((line = myFile.ReadLine()) != null)
                    {

                        line = line.Trim();
                        if (line.StartsWith("//"))
                            continue;

                        if (!continueStatement)
                        {
                            statement = line;
                            statement = statement.Trim();
                        }
                        else
                        {
                            line = line.Trim();
                            if ((line.StartsWith("\"")) && (isAskStatement))
                                line = line.Substring(1, line.Length - 1);
                            //if (line.EndsWith("\""))
                            //    line = line.Substring(0, line.Length - 1);
                            statement += line;
                            if (statement.Length > 5000)
                            {
                                failure = true;
                                break;
                            }
                        }
                        
                        string functionName = statement.Replace("static LPCSTR ", "");
                        functionName = functionName.Replace("(", "");
                        
                   
                        if (functionName == myTeststep.Testfunction && statement.StartsWith("static LPCSTR ") && !functionStart)
                        {
                            
                            functionStart = true;
                            realFunctionName = functionName;
                            statement = statement.Replace("static LPCSTR ", "");
                            statement += "int dwFunc)";
                            statement = "public int " + statement;
                            simulExit = false;
                        }

                        if (statement.Contains("#define"))
                            continue;

                        if (statement.Contains("static"))
                            continue;

                        if (statement == "")
                            continue;

                        if (statement.StartsWith("//"))
                            continue;

                        if (!functionStart)
                            continue;


                        if (statement == "\\n\\")
                            break;

                        if ((statement.Contains("dwFunc)")) && (!statement.Contains("int dwFunc")))
                            continue;


                        
                        if ((statement.Contains("LONG")) && (!statement.Contains("ULONG")) && (!statement.Contains("SetSignalValue")))
                        {
                            statement = statement.Replace("static LONG", "long");
                            statement = statement.Replace("LONG", "long");
                            statement = statement.Replace(";", " =  0;");
                            statement = statement.Replace(",", " = 0,");
                        }
                      
                        if ((statement.Contains("BYTE")) && (statement.Contains("[")))
                        {
                            statement = statement.Replace("[", " = new byte[");
                            statement = statement.Replace("static BYTE", "byte[]");
                            statement = statement.Replace("BYTE", "byte[]");
                        }
                        else if (statement.Contains("BYTE"))
                        {
                            statement = statement.Replace("BYTE", "byte");
                            statement = statement.Replace(";", " = 0;");
                        }

                        if (statement.Contains("TRUE"))
                            statement = statement.Replace("TRUE", "true");

                       

                        if (statement.Contains("max("))
                        {
                            statement = statement.Replace("max(", "kmFunctions.max(");
                        }

                       

                        if (statement.StartsWith("DispTestInfoLine("))
                        {
                            statement = "kmFunctions." + statement;
                            statement = statement.Replace("DispTestInfoLine(\"", "DispTestInfoLine(\"\t ");
                        }
                       
                        if (statement.Contains("fIsZEEnabled"))
                            statement = statement.Replace("fIsZEEnabled", "kmFunctions.fIsZEEnabled");

                        if (statement.Contains("szBGVStand"))
                            statement = statement.Replace("szBGVStand", "KMGlobalData.szBGVStand");

                        correction = false;

         
                        if (statement.Contains("SetSignalValue("))
                        {
                            statement = statement.Replace(",NULL", "");
                            statement = statement.Replace(",\tNULL", "");
                            statement = statement.Replace(", NULL", "");
                            statement = statement.Replace("(NULL)", "()");
                            statement = "kmFunctions." + statement;
                            statement = statement.Replace("))", ")");
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if (statement.Contains("fTimerFinished"))
                        {
                            statement = statement.Replace("fTimerFinished", "kmFunctions.fTimerFinished");
                            statement = statement.Replace("(&", "( ref ");
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if (statement.Contains("GetSignalValue"))
                        {
                            statement = statement.Replace("GetSignalValue", "kmFunctions.GetSignalValue");
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if (statement.Contains("AddCheckListEntry("))
                        {
                            statement = statement.Replace("AddCheckListEntry", "kmFunctions.AddCheckListEntry");
                            statement = statement.Replace("&", "ref ");
                            statement = statement.Replace(",NULL", ",\"\"");
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                            updateStatement = updateStatement +  statement + "\n";
                            
                        }

                        if (statement.Contains("SIM_DISP(GetSignalObject("))
                        {
                            statement = statement.Replace("SIM_DISP(GetSignalObject", "kmFunctions.GetSignalObject");
                            statement = statement.Replace("&", " ref ");
                            statement = statement.Replace("));", ");");
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if (statement.Contains("TimerStart("))
                        {
                            statement = statement.Replace("TimerStart(","kmFunctions.Timerstart(");
                            statement = statement.Replace("&", " ref ");
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if (statement.Contains("TimerAddTime("))
                        {
                            statement = statement.Replace("TimerAddTime(", "kmFunctions.TimerAddTime(");
                            statement = statement.Replace("&", " ref ");
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if (statement.Contains(" = "))
                        {
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if (statement.Contains(" != "))
                        {
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if (statement.Contains("if"))
                        {
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if (statement.Contains("switch"))
                        {
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if (statement.Contains(" += "))
                        {
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if (statement.Contains(" > "))
                        {
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if (statement.Contains("++"))
                        {
                            ReplaceStaticVariables(ref correction, ref statement, realFunctionName);
                        }

                        if ((statement == "{") && !firstBracket)
                        {
                            statement1 = "kmFunctions.SetGlobalData(KMGlobalData);";
                            firstBracket = true;
                        }

                        string statementWithoutComment;

                        if (statement.Contains("//"))
                        {
                            int commentIndex = statement.IndexOf("//");
                            statementWithoutComment = statement.Substring(0, commentIndex);
                            statementWithoutComment = statementWithoutComment.Trim();
                        }
                        else
                            statementWithoutComment = statement;

                        if ((statementWithoutComment.EndsWith("\\n\\")) || (statementWithoutComment.EndsWith(",")) || (statementWithoutComment.EndsWith("\\n\"")) || (statementWithoutComment.EndsWith("\\n")) || (statementWithoutComment.EndsWith("\"")))
                        {
                            if (statementWithoutComment.EndsWith("\\n\""))
                                statementWithoutComment = statement.Substring(0, statement.Length - 1);
                           

                            if (statement.Contains("//"))
                            {
                                int startIndex = statement.IndexOf("//");
                                statement = statement.Substring(0, startIndex);
                            }

                            if (statementWithoutComment.EndsWith("\""))
                            {
                                int index;
                                int commentIndex = statement.IndexOf("//");
                                if (commentIndex != -1)
                                    index = statement.LastIndexOf("\"", commentIndex);
                                else
                                    index = statement.LastIndexOf("\"");

                                if (index != statement.LastIndexOf("\","))
                                {
                                    statement = statement.Insert(index + 1, ",");
                                }
                            }

                            continueStatement = true;
                        }
                        else
                        {
                            if (continueStatement)
                            {
                                continueStatement = false;
                                isAskStatement = false;
                                statement = statement.Replace("));", ");");
                            }
                        }

                        if (statement.EndsWith("\\n\\"))
                        {
                            statement = statement.Replace("\\n\\", "");
                            statement += "\\n";
                        }

                        
                       
                        statement = statement.Replace("FALSE", "false");
                        statement = statement.Replace(",NULL", ",\"\"");
                        statement = statement.Replace(", NULL", ",\"\"");

                        if (statement.Contains("BYTE"))
                        {
                            statement = statement.Replace("BYTE", "byte");
                            statement = statement.Replace(";", "=0;");
                            statement = statement.Replace(",", "=0,");
                        }

                       

                        if (statement.Contains("usSpritzentyp"))
                            statement = statement.Replace("usSpritzentyp", "KMGlobalData.usSpritzentyp");

                        if (statement.Contains("usSchliessentyp"))
                            statement = statement.Replace("usSchliessentyp", "KMGlobalData.usSchliessentyp");

                        if (statement.Contains("(LONG)"))
                            statement = statement.Replace("(LONG)", "(long)");

                        if (statement.Contains("SIMUL_INIT"))
                        { 
                            statement = statement.Replace("SIMUL_INIT", "GlobalVar._SymbolRec.SIMUL_INIT");
                            updateStatement = "";

                        }

                        if (statement.Contains("SIMUL_TASK"))
                        { 
                            statement = statement.Replace("SIMUL_TASK", "GlobalVar._SymbolRec.SIMUL_TASK");
                            bracketCounter = 0;
                            startSimulTask = true;
                        }

                        if ((statement.Contains("{")) && (startSimulTask))
                            bracketCounter += 1;

                        if ((statement.Contains("}")) && (startSimulTask))
                        {
                            bracketCounter -= 1;
                            if (bracketCounter == 0)
                            {
                                startSimulTask = false;                            
                                statement = updateStatement + statement;
                                statement = statement.Replace("AddCheckListEntry", "UpdateCheckListEntry");
                            }
                        }

                        if (statement.Contains("SIMUL_EXIT"))
                        {
                            statement = statement.Replace("SIMUL_EXIT", "GlobalVar._SymbolRec.SIMUL_EXIT");
                            simulExit = true;
                        }

                        if ((statement.Contains("return NO_ERROR")) && (!simulExit))
                        {
                            statement = "return 1;";
                        }

                        if ((statement.Contains("return NO_ERROR")) && (simulExit))
                        {
                            statement = "return 1;\n}";
                            functionStart = false;
                        }

                        if (statement.Contains("strstr"))
                            statement = statement.Replace("strstr", "kmFunctions.strstr");

                        if (statement.Contains("if (l)"))
                            statement = statement.Replace("if (l)", "if (l == 1)");


                        if (!continueStatement)
                        {
                            sb.AppendLine(statement);
                        }

                        if (statement1 != "")
                        {
                            sb.AppendLine(statement1);
                            statement1 = "";
                        }
                    }
                    if (failure)
                        return "Failure in" + myTeststep.Testfunction;

                    //CodDomProvider Abschluss   
                }

                sb.AppendLine(@"}}");
                string thisString = sb.ToString();
                myTextBox.Text = thisString;

                CompilerResults result = provider.CompileAssemblyFromSource(parameters, sb.ToString());
                myListBox.DataSource = result.Errors;
                myListBox.DisplayMember = "ErrorText";
                if (!result.Errors.HasErrors)
                {
                    object instance = result.CompiledAssembly.CreateInstance("KraussMaffeiNeu.Testklasse");
                    //MethodInfo info = instance.GetType().GetMethod("TestFunktion");
                    //info.Invoke(instance, null);
                }
                else
                {
                    MessageBox.Show("Fehler beim Compilieren");
                }
            }
            return "";
        }



        static bool fSplitLineTestStep(string pszLine, string[] asz)
        {
            int endIndex = pszLine.IndexOf("Enter");
            if (endIndex == -1)
                endIndex = pszLine.IndexOf("NULL");
            asz[1] = pszLine.Substring(3, endIndex - 4);
            asz[1] = asz[1].Replace("\t", "");
            asz[1] = asz[1].Replace("\"", "");
            asz[1] = asz[1].Replace(",", "");
            asz[1] = asz[1].Replace(" ", "");


            int startIndex = endIndex;
            startIndex = pszLine.IndexOf(",", startIndex);
            endIndex = pszLine.IndexOf("Exit", startIndex);
            if ((endIndex == -1) | (pszLine.Contains("_Exit")))
                endIndex = pszLine.IndexOf("NULL", startIndex);

            asz[0] = pszLine.Substring(startIndex, endIndex - startIndex);
            asz[0] = asz[0].Replace("\t", "");
            asz[0] = asz[0].Replace("\"", "");
            asz[0] = asz[0].Replace(",", "");
            asz[0] = asz[0].Replace(" ", "");

            startIndex = endIndex;
            startIndex = pszLine.IndexOf(",", startIndex);
            endIndex = pszLine.IndexOf("}", startIndex);
            asz[2] = pszLine.Substring(startIndex, endIndex - startIndex);
            asz[2] = asz[2].Replace("\t", "");
            asz[2] = asz[2].Replace("\"", "");
            asz[2] = asz[2].Replace(",", "");
            asz[2] = asz[2].Replace(" ", "");
            return true;
        }

        private void btnLoadTestSteps_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Filter = "c files (*.c)|*.c|All files (*.*)|*.*";

            int Min = Int32.Parse(txtMin.Text);
            int Max = Int32.Parse(txtMax.Text);
            CreateTestDLL("d:\\Synchronics\\Software\\KraussMaffei\\Test_28-07-15\\Test_GX_M31.c", liTeststepList, listBox1, txtSourceCode, Min, Max,false);
            gridTestFunctions.DataSource = liTeststepList;
        }

        private void btnOnlineSigmatekMachine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (!kmGlobalData.systemSigmatekMachine.IsOnlineH())
            {
                kmFunctions.InitSigmatek();
            }
            else
            {
                kmFunctions.ExitSigmatek();
            }
        }

        private void btnStartCompleteTest_Click(object sender, EventArgs e)
        {
            foreach (GlobalVar.Teststep teststep in liTeststepList)
            {
                teststep.Perform = true;
            }
            Thread trd = new Thread(new ThreadStart(this.TestThread));
            trd.IsBackground = false;
            trd.Start();
        }

        private void btnStartSelectedTest_Click(object sender, EventArgs e)
        {
            Thread trd = new Thread(new ThreadStart(this.TestThread));
            trd.IsBackground = true;
            trd.Start();
        }

   
        private void gridMachineSelection_DoubleClick(object sender, EventArgs e)
        {
            
        }

        private void btnConvertPrograms_Click(object sender, EventArgs e)
        {
            foreach (GlobalVar.Testmodule testmodule in kmGlobalData.listTestModules)
            {
                if (testmodule.Compile)
                {
                    CreateTestDLL(testmodule.FileName, liTeststepList, listBox1, txtSourceCode, 0, 0, true);
                    gridTestFunctions.DataSource = liTeststepList;
                }
            }
        }

        private void barSelectMachine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            MachineSelection();
        }

        private void btnSelectAll_Click(object sender, EventArgs e)
        {
            foreach (GlobalVar.Teststep teststep in liTeststepList)
            {
                teststep.Perform = true;
            }
            gridTestFunctions.RefreshDataSource();
        }

        private void btnSelectNone_Click(object sender, EventArgs e)
        {
            foreach (GlobalVar.Teststep teststep in liTeststepList)
            {
                teststep.Perform = false;
            }
            gridTestFunctions.RefreshDataSource();
        }

        private void btnStartfromFirstSelectedTest_Click(object sender, EventArgs e)
        {
            bool firstActivated = false;
            foreach (GlobalVar.Teststep teststep in liTeststepList)
            {
                if ((!teststep.Perform) && (!firstActivated))
                    continue;
                else
                {
                    firstActivated = true;
                    teststep.Perform = true;
                }
            }

            Thread trd = new Thread(new ThreadStart(this.TestThread));
            trd.IsBackground = true;
            trd.Start();
        }

        private void chkEn_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {  
            if (chkEn.Checked)
            {
                strCulture = "en-US";
                chkDE.Checked = false;
            }
            GlobalizeApp();        
        }

   

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            CheckListWindow checkListWindow = new CheckListWindow();
            checkListWindow.Show();
            //string testDllResult = CreateTestDLL("d:\\synchronics\\software\\kraussmaffei\\VSTest_30-05-16\\Cycle_CX.c", liTeststepList, listBox1, txtSourceCode, 0, 0, true);
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            tabPaneMain.Width = this.Width - panelContainer1.Width - 10;
        }

       

        private void UpdateGridColorsLoad()
        {
            for (int i = 0; i <= gridViewTestFunctions.RowCount - 1; i++)
            {
                foreach (GlobalVar.Teststep teststep in liTeststepList)
                {
                    string rowValue = gridViewTestFunctions.GetRowCellValue(i, "Testfunction").ToString();
                    if (rowValue == teststep.Testfunction)
                    {
                        if (teststep.Testresult == 1)
                            _CellColorHelper.SetCellColor(Convert.ToInt32(i), gridViewTestFunctions.Columns[Convert.ToInt32(0)], Color.LightGreen);
                        else if (teststep.Testresult == 2)
                            _CellColorHelper.SetCellColor(Convert.ToInt32(i), gridViewTestFunctions.Columns[Convert.ToInt32(0)], Color.OrangeRed);
                    }
                }
            }
        }

        private void btnDeleteResults_Click(object sender, EventArgs e)
        {
            foreach (GlobalVar.Teststep teststep in liTeststepList)
            {
                teststep.Perform = false;
                teststep.Testresult = 0;
            }
            gridTestFunctions.RefreshDataSource();

            for (int i = 0; i <= gridViewTestFunctions.RowCount - 1; i++)
            {
                _CellColorHelper.SetCellColor(Convert.ToInt32(i), gridViewTestFunctions.Columns[Convert.ToInt32(0)], Color.White);
            }
        }

        private void gridViewInputs_CellValueChanged(object sender, CellValueChangedEventArgs e)
        {
            if ((e.Column.FieldName == "Value") || (e.Column.FieldName == "Simulation"))
            {
                kmFunctions.UpdateAnalogInputs();
                kmFunctions.UpdateDigitalInputs(0);
            } 
        }

        private void barButtonItem3_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (CheckListEntry checkListEnry in kmGlobalData.checkList)
            {
                if (checkListEnry.Group == 5)
                {
                    checkListEnry.OKVar = true;
                }
            }
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (CheckListEntry checkListEnry in kmGlobalData.checkList)
            {
                    checkListEnry.OKVar = false;            
            }
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (CheckListEntry checkListEnry in kmGlobalData.checkList)
            {
                if (checkListEnry.Group == 2)
                {
                    checkListEnry.OKVar = true;
                }
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            foreach (CheckListEntry checkListEnry in kmGlobalData.checkList)
            {
                if (checkListEnry.Group == 3)
                {
                    checkListEnry.OKVar = true;
                }
            }
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ZEs zes = new ZEs(kmGlobalData, kmFunctions);
            zes.Show();
        }

        private void chkEn_CheckedChanged(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

      

        private void chkDE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (chkDE.Checked)
            {
                strCulture = "de";
                chkEn.Checked = false;
            }
            GlobalizeApp();
        }

        private void barButtonItem8_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //kmFunctions.SetAnaOut(1000, "#T018/P");
            kmFunctions.SetAnaOut(1000, "#BT610");
        }

        private void btnResetSigmatekTester_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            kmGlobalData.systemSigmatekTester.Restart();
            
        }

        private void btnResetSigmatekMachine_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            kmGlobalData.systemSigmatekMachine.Restart();
            kmFunctions.ResetAllMachineOutputs();
        }

        private void btnPathSelection_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            PathSelection pathSelection = new PathSelection(kmGlobalData.liParameterDefinition,kmGlobalData);
            pathSelection.Show();
        }

        private void barButtonItem9_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           rtTextInfoLines.ExportToPdf("d:\\Synchronics\\Reports\\" + kmGlobalData.szOrderNumber + ".pdf");
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }

        private void ribbonControl1_Paint(object sender, PaintEventArgs e)
        {
            Image image;
            int minX;
            double ratioX;
            double ratioY;
            double ratio;
            int newWidth;
            int newHeight;
            Image newImage;
            Graphics graphics;

            DevExpress.XtraBars.Ribbon.ViewInfo.RibbonViewInfo ribbonViewInfo = ribbonControl1.ViewInfo;
            if (ribbonViewInfo == null)
                return;
            DevExpress.XtraBars.Ribbon.ViewInfo.RibbonPanelViewInfo panelViewInfo = ribbonViewInfo.Panel;
            if (panelViewInfo == null)
                return;
            Rectangle bounds = panelViewInfo.Bounds;
            minX = bounds.X;
            DevExpress.XtraBars.Ribbon.ViewInfo.RibbonPageGroupViewInfoCollection groups = panelViewInfo.Groups;
            if (groups == null)
                return;
            if (groups.Count > 0)
                minX = groups[groups.Count - 1].Bounds.Right;

            string applicationLocation = System.Reflection.Assembly.GetEntryAssembly().Location;
            string applicationDirectory = Path.GetDirectoryName(applicationLocation);
            image = Image.FromFile(applicationDirectory + "\\Logos\\krauss-maffei.png");
            //image = DevExpress.Utils.Frames.ApplicationCaption8_1.GetImageLogoEx(LookAndFeel);
                
            ratioX = (double)200 / image.Width;
            ratioY = (double)(bounds.Height/2) / image.Height;
            ratio = Math.Min(ratioX, ratioY);

            newWidth = (int)(image.Width * ratio);
            newHeight = (int)(image.Height * ratio);

            newImage = new Bitmap(newWidth, newHeight);

            using (graphics = Graphics.FromImage(newImage))
            graphics.DrawImage(image, 0, 0, newWidth, newHeight);

            //Image image = DevExpress.Utils.Frames.ApplicationCaption8_1.GetImageLogoEx(LookAndFeel);
            if (bounds.Height < newImage.Height)
                    return;
                int offset = (bounds.Height - newImage.Height) / 6;
                int width = newImage.Width + 15;
                bounds.X = bounds.Width - width;
                if (bounds.X < minX)
                    return;
                bounds.Width = width;
                bounds.Y += offset;
                bounds.Height = newImage.Height;
                e.Graphics.DrawImage(newImage, bounds.Location);
            }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            kmFunctions.SetDigOut(1, "#K521/A,B");
        }

        private void dockManager1_StartDocking(object sender, DevExpress.XtraBars.Docking.DockPanelCancelEventArgs e)
        {
            e.Cancel = true;
        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            kmFunctions.SetDigOut(7,"#B010");
        }
    }

    public class RegistryAccess
    {
        private const string SOFTWARE_KEY = "SOFTWARE";
        private const string COMPANY_NAME = "SYNCHRONICS";
        private const string APPLICATION_NAME = "TS-ECT2";

        // Method for retrieving a Registry Value.
        static public string GetStringRegistryValue(string key, string defaultValue)
        {
            RegistryKey rkCompany;
            RegistryKey rkApplication;

            rkCompany = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, false).OpenSubKey(COMPANY_NAME, false);
            if (rkCompany != null)
            {
                rkApplication = rkCompany.OpenSubKey(APPLICATION_NAME, true);
                if (rkApplication != null)
                {
                    foreach (string sKey in rkApplication.GetValueNames())
                    {
                        if (sKey == key)
                        {
                            return (string)rkApplication.GetValue(sKey);
                        }
                    }
                }
            }
            return defaultValue;
        }

        // Method for storing a Registry Value.
        static public void SetStringRegistryValue(string key, string stringValue)
        {
            RegistryKey rkSoftware;
            RegistryKey rkCompany;
            RegistryKey rkApplication;

            rkSoftware = Registry.CurrentUser.OpenSubKey(SOFTWARE_KEY, true);
            rkCompany = rkSoftware.CreateSubKey(COMPANY_NAME);
            if (rkCompany != null)
            {
                rkApplication = rkCompany.CreateSubKey(APPLICATION_NAME);
                if (rkApplication != null)
                {
                    rkApplication.SetValue(key, stringValue);
                }
            }
        }
    }

    public class CellColorHelper
    {
        Dictionary<MyGridCell, Color> colors = new Dictionary<MyGridCell, Color>();
        private readonly GridView _View;
        /// <summary>
        /// Initializes a new instance of the CellColorHelper class.
        /// </summary>
        public CellColorHelper(GridView view)
        {
            _View = view;
            _View.RowCellStyle += _View_RowCellStyle;
        }

        void _View_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            Color color = GetCellColor(new MyGridCell(e.RowHandle, e.Column));
            if (color.IsEmpty)
                return;
            e.Appearance.BackColor = color;
        }

        public Color GetCellColor(MyGridCell cell)
        {
            Color c = Color.Empty;
            if (colors.TryGetValue(cell, out c))
                return c;
            return Color.Empty;
        }

        public void SetCellColor(int rowHandle, GridColumn column, Color value)
        {
            SetCellColor(new MyGridCell(rowHandle, column), value);
        }

        public void SetCellColor(MyGridCell cell, Color value)
        {
            colors[cell] = value;
            _View.RefreshRowCell(cell.RowHandle, cell.Column);
        }

    }

    public class MyGridCell : GridCell
    {
        public MyGridCell(int rowHandle, GridColumn column)
            : base(rowHandle, column)
        {

        }

        public override int GetHashCode()
        {
            return Column.GetHashCode() + RowHandle.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as GridCell);
        }
    }
}
