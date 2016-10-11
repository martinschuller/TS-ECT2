using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using KraussMaffeiData;
using System.Xml.Serialization;
using System.IO;

namespace TS_ECT2
{
    public partial class PathSelection : Form
    {
        BindingList<GlobalVar.ParameterDefinition> liParameterDefinition;
        BindingList<GlobalVar.ParameterDefinition> liOldParameterDefinition;

        GlobalVar.GlobalData KMGlobalData;

        public PathSelection(BindingList<GlobalVar.ParameterDefinition> LIParameterDefinition, GlobalVar.GlobalData KMGlobalData)
        {
            this.liParameterDefinition = LIParameterDefinition;
            this.KMGlobalData = KMGlobalData;
            liOldParameterDefinition = liParameterDefinition;
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (GlobalVar.ParameterDefinition parameter in liParameterDefinition)
            {
                if (parameter.ParameterNumber == 1)
                    parameter.ParameterValue = txtReportPath.Text;
                if (parameter.ParameterNumber == 2)
                    parameter.ParameterValue = txtMC6Program.Text;
                if (parameter.ParameterNumber == 3)
                    parameter.ParameterValue = txtMC6Python.Text;
                if (parameter.ParameterNumber == 6)
                    parameter.ParameterValue = txtSourceCode.Text;
                if (parameter.ParameterNumber == 7)
                    parameter.ParameterValue = txtIPAddressIPC.Text;
                if (parameter.ParameterNumber == 8)
                    parameter.ParameterValue = txtIPAddressTester.Text;
                if (parameter.ParameterNumber == 9)
                    parameter.ParameterValue = txtCompiledTestDLLWritePath.Text;
                if (parameter.ParameterNumber == 10)
                    parameter.ParameterValue = txtCompiledTestDLLReadPath.Text;
                if (parameter.ParameterNumber == 11)
                    parameter.ParameterValue = txtCompiledSimulationDLLWritePath.Text;
                if (parameter.ParameterNumber == 12)
                    parameter.ParameterValue = txtCompiledSimulationDLLReadPath.Text;

            }

            XmlSerializer vXmlSerializer = new XmlSerializer(typeof(BindingList<GlobalVar.ParameterDefinition>));
            Stream fs = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Configuration.xml", FileMode.Create);
            vXmlSerializer.Serialize(fs, liParameterDefinition);
            fs.Close();
            Close();
        }

        private void btnAbort_Click(object sender, EventArgs e)
        {
            liParameterDefinition = liOldParameterDefinition;
            Close();
        }

        private void PathSelection_Load(object sender, EventArgs e)
        {
            if (File.Exists(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Configuration.xml"))
            { 
                XmlSerializer vXmlSerializer = new XmlSerializer(typeof(BindingList<GlobalVar.ParameterDefinition>));
                Stream fs = new FileStream(System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + @"\Synchronics\Configuration.xml", FileMode.Open);
                liParameterDefinition = (BindingList<GlobalVar.ParameterDefinition>)(vXmlSerializer.Deserialize(fs));
                fs.Dispose();
                fs.Close();
            }

            for (int i = 1; i <= 12; i++)
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

            foreach (GlobalVar.ParameterDefinition myParameter in liParameterDefinition)
            {
                if (myParameter.ParameterNumber == 1)
                    txtReportPath.Text = myParameter.ParameterValue;
                if (myParameter.ParameterNumber == 2)
                    txtMC6Program.Text = myParameter.ParameterValue;
                if (myParameter.ParameterNumber == 3)
                    txtMC6Python .Text = myParameter.ParameterValue;
                if (myParameter.ParameterNumber == 6)
                    txtSourceCode.Text = myParameter.ParameterValue;
                if (myParameter.ParameterNumber == 7)
                    txtIPAddressIPC.Text = myParameter.ParameterValue;
                if (myParameter.ParameterNumber == 8)
                    txtIPAddressTester.Text = myParameter.ParameterValue;
                if (myParameter.ParameterNumber == 9)
                    txtCompiledTestDLLWritePath.Text = myParameter.ParameterValue;
                if (myParameter.ParameterNumber == 10)
                    txtCompiledTestDLLReadPath.Text = myParameter.ParameterValue;
                if (myParameter.ParameterNumber == 11)
                    txtCompiledSimulationDLLWritePath.Text = myParameter.ParameterValue;
                if (myParameter.ParameterNumber == 12)
                    txtCompiledSimulationDLLReadPath.Text = myParameter.ParameterValue;

            }
        }

        private void btnSelectReportPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                liParameterDefinition[0].ParameterValue = folderBrowserDialog.SelectedPath;
                txtReportPath.Text = liParameterDefinition[0].ParameterValue;
            }
        }

        private void btnSelectMC6Program_Click(object sender, EventArgs e)
        {
           OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                liParameterDefinition[1].ParameterValue = openFileDialog.FileName;
                txtMC6Program.Text = liParameterDefinition[1].ParameterValue;
            }
        }

        private void btnSelectMC6Python_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                liParameterDefinition[2].ParameterValue = folderBrowserDialog.SelectedPath;
                txtMC6Python.Text = liParameterDefinition[2].ParameterValue;
            }
        }

        private void btnSelectSourceCode_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();

            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                liParameterDefinition[5].ParameterValue = folderBrowserDialog.SelectedPath;
               txtSourceCode.Text = liParameterDefinition[5].ParameterValue;
            }
        }

        private void labelControl10_Click(object sender, EventArgs e)
        {

        }

        private void textEdit1_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void btnCompiledDLLWritePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                liParameterDefinition[8].ParameterValue = openFileDialog.FileName;
                txtCompiledTestDLLWritePath.Text = liParameterDefinition[8].ParameterValue;
            }
        }

        private void btnCompiledDLLReadPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                liParameterDefinition[9].ParameterValue = openFileDialog.FileName;
                txtCompiledTestDLLReadPath.Text = liParameterDefinition[9].ParameterValue;
            }
        }

        private void btnCompiledSimulationDLLWritePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                liParameterDefinition[10].ParameterValue = openFileDialog.FileName;
                txtCompiledSimulationDLLWritePath.Text = liParameterDefinition[10].ParameterValue;
            }
        }

        private void btnCompiledSimulationDLLReadPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                liParameterDefinition[11].ParameterValue = openFileDialog.FileName;
                txtCompiledSimulationDLLReadPath.Text = liParameterDefinition[11].ParameterValue;
            }
        }
    }
}
