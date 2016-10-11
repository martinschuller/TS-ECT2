using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using KraussMaffeiData;
using System.Collections;
using DevExpress.XtraNavBar;
using DevExpress.XtraNavBar.ViewInfo;
using DevExpress.Utils.Drawing;
using System.Drawing.Drawing2D;


namespace TS_ECT2
{
    public partial class MultiIO : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        Timer myTimer = new Timer();
        int selectedIOPlug = 1;
        int selectedSupplyPlug = 1;
        int arrayOffsetDI = 0;
        int arrayOffsetAI = 0;
        int arrayOffsetDO = 0;
        int arrayOffsetAO = 0;
        int arrayOffsetTS = 0;
        int arrayOffsetPTC = 0;
        int arrayOffsetLI = 10;
        int arrayOffsetPI = 4;
        int arrayOffsetLIValues = 12;
        int arrayOffsetPIValues = 12;
        int arrayOffsetMIValues = 12;
        int arrayOffsetMI = 1;
        int arrayOffsetPIOut = 4;
        

        GlobalVar.GlobalData kmGlobalData = new GlobalVar.GlobalData();
        KraussMaffeiFunctions kmFunctions = new KraussMaffeiFunctions();
        int oldselectedIOPlug = 0;
        int oldselectedSupplyPlug = 0;
        Byte[] bytearray = new Byte[2];
        Color defaultbtnForeColor;
        Color defaultbtnBackColor;

        public MultiIO()
        {
            InitializeComponent();
        }
        public MultiIO(GlobalVar.GlobalData kmGlobalData, KraussMaffeiFunctions kmFunctions)
        {
            InitializeComponent();
            this.kmGlobalData = kmGlobalData;
            this.kmFunctions = kmFunctions; 
        }

        private void MultiIO_Load(object sender, EventArgs e)
        {
            myTimer = new Timer();
            myTimer.Interval = 200; 
            myTimer.Tick += new EventHandler(MyTimer_Tick);
            myTimer.Start();
            defaultbtnBackColor = btnPlugIO_1.Appearance.BackColor;
            defaultbtnForeColor = btnPlugIO_1.Appearance.ForeColor;
            groupDigitalInputs.Text = kmGlobalData.hWSStatusDigitalInputsString;
            groupAnalogInputs.Text = kmGlobalData.hwAnalogInputString;
            groupAnalogOutputs.Text = kmGlobalData.hwAnalogOutputString;
            groupRelais.Text = kmGlobalData.hwRelaisString;
            groupPTCRelais.Text = kmGlobalData.hwPTCRelaisString;
            groupTemperatureOutputs.Text = kmGlobalData.hwTemperatureOutputString;
            btnPlugIO_1.Caption = kmGlobalData.hwPlugString + " 1";
            btnPlugIO_2.Caption = kmGlobalData.hwPlugString + " 2";
            btnPlugIO_3.Caption = kmGlobalData.hwPlugString + " 3";
            btnPlugIO_4.Caption = kmGlobalData.hwPlugString + " 4";
            btnPlugIO_5.Caption = kmGlobalData.hwPlugString + " 5";
            btnPlugIO_6.Caption = kmGlobalData.hwPlugString + " 6";
            btnPlugIO_7.Caption = kmGlobalData.hwPlugString + " 7";
            btnPlugIO_8.Caption = kmGlobalData.hwPlugString + " 8";
            btnPlugIO_9.Caption = kmGlobalData.hwPlugString + " 9";
            btnPlugIO_10.Caption = kmGlobalData.hwPlugString + " 10";
            btnPlugIO_11.Caption = kmGlobalData.hwPlugString + " 11";
            btnPlugIO_12.Caption = kmGlobalData.hwPlugString + " 12";
            btnPlugIO_13.Caption = kmGlobalData.hwPlugString + " 13";
            btnPlugIO_14.Caption = kmGlobalData.hwPlugString + " 14";
            btnPlugIO_15.Caption = kmGlobalData.hwPlugString + " 15";
            btnPlugIO_16.Caption = kmGlobalData.hwPlugString + " 16";
            btnPlugIO_17.Caption = kmGlobalData.hwPlugString + " 17";
            btnPlugIO_18.Caption = kmGlobalData.hwPlugString + " 18";
            btnPlugIO_19.Caption = kmGlobalData.hwPlugString + " 19";
            btnPlugIO_20.Caption = kmGlobalData.hwPlugString + " 20";

            chkDOEin1.Text = kmGlobalData.hwOnString;
            chkDOEin2.Text = kmGlobalData.hwOnString;
            chkDOEin3.Text = kmGlobalData.hwOnString;
            chkDOEin4.Text = kmGlobalData.hwOnString;
            chkDOEin5.Text = kmGlobalData.hwOnString;
            chkDOEin6.Text = kmGlobalData.hwOnString;
            chkDOEin7.Text = kmGlobalData.hwOnString;
            chkDOEin8.Text = kmGlobalData.hwOnString;

            chkDOUPullup1.Text = kmGlobalData.hwPullupString;
            chkDOUPullup2.Text = kmGlobalData.hwPullupString;
            chkDOUPullup3.Text = kmGlobalData.hwPullupString;
            chkDOUPullup4.Text = kmGlobalData.hwPullupString;
            chkDOUPullup5.Text = kmGlobalData.hwPullupString;
            chkDOUPullup6.Text = kmGlobalData.hwPullupString;
            chkDOUPullup7.Text = kmGlobalData.hwPullupString;
            chkDOUPullup8.Text = kmGlobalData.hwPullupString;

            xtraTabPageSupply.Text = kmGlobalData.hwSupplyString;
            xtraTabPageInterfaces.Text = kmGlobalData.hwInterfaceString;
            xtraTabPageMulit_IO.Text = kmGlobalData.hwMultiIOString;

            groupStatusSupply1.Text = kmGlobalData.hwSupply1PhaseString;
            groupStatusSupply3.Text = kmGlobalData.hwSupply3PhaseString;
            groupStatusSupply6.Text = kmGlobalData.hwSupply6PhaseString;

            btnSupply_1.Caption = kmGlobalData.hwPlugString + " 1";
            btnSupply_2.Caption = kmGlobalData.hwPlugString + " 2";
            btnSupply_3.Caption = kmGlobalData.hwPlugString + " 3";
            btnSupply_4.Caption = kmGlobalData.hwPlugString + " 4";
            btnSupply_5.Caption = kmGlobalData.hwPlugString + " 5";
            btnSupply_6.Caption = kmGlobalData.hwPlugString + " 6";
            btnSupply_7.Caption = kmGlobalData.hwPlugString + " 7";
            btnSupply_8.Caption = kmGlobalData.hwPlugString + " 8";
            btnSupply_1.Caption = kmGlobalData.hwPlugString + " 1";

            navBarSupplyPlug.Caption = kmGlobalData.hwSupplyPlugString;
            groupInterfaceRelais.Text = kmGlobalData.hwInterfaceRelaisString;


        }

        private void MyTimer_Tick(object sender, EventArgs e)
        {
            kmFunctions.UpdateAnalogInputs();
            kmFunctions.UpdateDigitalInputs(0);

            arrayOffsetDI = (selectedIOPlug - 1) * 20;
            arrayOffsetAI = (selectedIOPlug - 1) * 4;
            arrayOffsetDO = (selectedIOPlug - 1) * 8;
            arrayOffsetAO = (selectedIOPlug - 1) * 8;
            arrayOffsetTS = (selectedIOPlug - 1) * 8;
            arrayOffsetPTC = (selectedIOPlug - 1);
            arrayOffsetLI =  (selectedSupplyPlug - 1) * 15;
            arrayOffsetPI = (selectedSupplyPlug - 1) * 15 + 10;
            arrayOffsetLIValues = (selectedSupplyPlug - 1) * 25;
            arrayOffsetPIValues = (selectedSupplyPlug - 1) * 25 + 10;
            arrayOffsetMI = (selectedSupplyPlug - 1) * 15 + 14;
            arrayOffsetMIValues = (selectedSupplyPlug - 1) * 25 + 22;
            arrayOffsetPIOut = (selectedSupplyPlug - 1) * 4;

            //Digital Inputs

            txtDI1.Text = kmGlobalData.arrayDI[arrayOffsetDI].ToString();
            txtDI_AINeg1.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI].ToString();
            txtDI_AIPos1.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI].ToString();

            txtDI2.Text = kmGlobalData.arrayDI[arrayOffsetDI+1].ToString();
            txtDI_AINeg2.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI+1].ToString();
            txtDI_AIPos2.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI+1].ToString();

            txtDI3.Text = kmGlobalData.arrayDI[arrayOffsetDI+2].ToString();
            txtDI_AINeg3.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 2].ToString();
            txtDI_AIPos3.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 2].ToString();

            txtDI4.Text = kmGlobalData.arrayDI[arrayOffsetDI + 3].ToString();
            txtDI_AINeg4.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 3].ToString();
            txtDI_AIPos4.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 3].ToString();

            txtDI5.Text = kmGlobalData.arrayDI[arrayOffsetDI + 4].ToString();
            txtDI_AINeg5.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 4].ToString();
            txtDI_AIPos5.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 4].ToString();

            txtDI6.Text = kmGlobalData.arrayDI[arrayOffsetDI + 5].ToString();
            txtDI_AINeg6.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 5].ToString();
            txtDI_AIPos6.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 5].ToString();

            txtDI7.Text = kmGlobalData.arrayDI[arrayOffsetDI + 6].ToString();
            txtDI_AINeg7.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 6].ToString();
            txtDI_AIPos7.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 6].ToString();

            txtDI8.Text = kmGlobalData.arrayDI[arrayOffsetDI + 7].ToString();
            txtDI_AINeg8.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 7].ToString();
            txtDI_AIPos8.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 7].ToString();

            txtDI9.Text = kmGlobalData.arrayDI[arrayOffsetDI + 8].ToString();
            txtDI_AINeg9.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 8].ToString();
            txtDI_AIPos9.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 8].ToString();
        
            txtDI10.Text = kmGlobalData.arrayDI[arrayOffsetDI + 9].ToString();
            txtDI_AINeg10.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 9].ToString();
            txtDI_AIPos10.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 9].ToString();

            txtDI11.Text = kmGlobalData.arrayDI[arrayOffsetDI+10].ToString();
            txtDI_AINeg11.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 10].ToString();
            txtDI_AIPos11.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 10].ToString();

            txtDI12.Text = kmGlobalData.arrayDI[arrayOffsetDI + 11].ToString();
            txtDI_AINeg12.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 11].ToString();
            txtDI_AIPos12.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 11].ToString();

            txtDI13.Text = kmGlobalData.arrayDI[arrayOffsetDI + 12].ToString();
            txtDI_AINeg13.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 12].ToString();
            txtDI_AIPos13.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 12].ToString();

            txtDI14.Text = kmGlobalData.arrayDI[arrayOffsetDI + 13].ToString();
            txtDI_AINeg14.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 13].ToString();
            txtDI_AIPos14.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 13].ToString();

            txtDI15.Text = kmGlobalData.arrayDI[arrayOffsetDI + 14].ToString();
            txtDI_AINeg15.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 14].ToString();
            txtDI_AIPos15.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 14].ToString();

            txtDI16.Text = kmGlobalData.arrayDI[arrayOffsetDI + 15].ToString();
            txtDI_AINeg16.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 15].ToString();
            txtDI_AIPos16.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 15].ToString();

            txtDI17.Text = kmGlobalData.arrayDI[arrayOffsetDI + 16].ToString();
            txtDI_AINeg17.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 16].ToString();
            txtDI_AIPos17.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 16].ToString();

            txtDI18.Text = kmGlobalData.arrayDI[arrayOffsetDI + 17].ToString();
            txtDI_AINeg18.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 17].ToString();
            txtDI_AIPos18.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 17].ToString();

            txtDI19.Text = kmGlobalData.arrayDI[arrayOffsetDI + 18].ToString();
            txtDI_AINeg19.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 18].ToString();
            txtDI_AIPos19.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 18].ToString();

            txtDI20.Text = kmGlobalData.arrayDI[arrayOffsetDI + 19].ToString();
            txtDI_AINeg20.Text = kmGlobalData.arrayDI_AINeg[arrayOffsetDI + 19].ToString();
            txtDI_AIPos20.Text = kmGlobalData.arrayDI_AIPos[arrayOffsetDI + 19].ToString();

            //Analog Inputs

            txtAI1.Text = kmGlobalData.arrayAI[arrayOffsetAI].ToString();
            txtAI2.Text = kmGlobalData.arrayAI[arrayOffsetAI+1].ToString();
            txtAI3.Text = kmGlobalData.arrayAI[arrayOffsetAI+2].ToString();
            txtAI4.Text = kmGlobalData.arrayAI[arrayOffsetAI+3].ToString();

            //Inputs of Analog Ouptuts;

            txtAOIn_1.Text = kmGlobalData.arrayAOIn[arrayOffsetAO].ToString();
            txtAOIn_2.Text = kmGlobalData.arrayAOIn[arrayOffsetAO + 1].ToString();
            txtAOIn_3.Text = kmGlobalData.arrayAOIn[arrayOffsetAO + 2].ToString();
            txtAOIn_4.Text = kmGlobalData.arrayAOIn[arrayOffsetAO + 3].ToString();

            txtTSAin_1.Text = kmGlobalData.arrayTSIn[arrayOffsetTS].ToString();
            txtTSAin_2.Text = kmGlobalData.arrayTSIn[arrayOffsetTS + 1].ToString();
            txtTSAin_3.Text = kmGlobalData.arrayTSIn[arrayOffsetTS + 2].ToString();
            txtTSAin_4.Text = kmGlobalData.arrayTSIn[arrayOffsetTS + 3].ToString();

            //Analog Inputs of DO

            txtDOValue_1.Text = kmGlobalData.arrayDOAI[arrayOffsetDO].ToString();
            txtDOValue_2.Text = kmGlobalData.arrayDOAI[arrayOffsetDO + 1].ToString();
            txtDOValue_3.Text = kmGlobalData.arrayDOAI[arrayOffsetDO + 2].ToString();
            txtDOValue_4.Text = kmGlobalData.arrayDOAI[arrayOffsetDO + 3].ToString();
            txtDOValue_5.Text = kmGlobalData.arrayDOAI[arrayOffsetDO + 4].ToString();
            txtDOValue_6.Text = kmGlobalData.arrayDOAI[arrayOffsetDO + 5].ToString();
            txtDOValue_7.Text = kmGlobalData.arrayDOAI[arrayOffsetDO + 6].ToString();
            txtDOValue_8.Text = kmGlobalData.arrayDOAI[arrayOffsetDO + 7].ToString();

            //Supply Inputs

            txtLI1.Text = kmGlobalData.arraySupplyValues[arrayOffsetLI].ToString();
            txtLI2.Text = kmGlobalData.arraySupplyValues[arrayOffsetLI+1].ToString();
            txtLI3.Text = kmGlobalData.arraySupplyValues[arrayOffsetLI+2].ToString();
            txtLI4.Text = kmGlobalData.arraySupplyValues[arrayOffsetLI+3].ToString();
            txtLI5.Text = kmGlobalData.arraySupplyValues[arrayOffsetLI+4].ToString();
            txtLI6.Text = kmGlobalData.arraySupplyValues[arrayOffsetLI+5].ToString();
            txtLI7.Text = kmGlobalData.arraySupplyValues[arrayOffsetLI+6].ToString();
            txtLI8.Text = kmGlobalData.arraySupplyValues[arrayOffsetLI+7].ToString();
            txtLI9.Text = kmGlobalData.arraySupplyValues[arrayOffsetLI+8].ToString();
            txtLI10.Text = kmGlobalData.arraySupplyValues[arrayOffsetLI+9].ToString();

            txtPI1.Text = kmGlobalData.arraySupplyValues[arrayOffsetPI].ToString();
            txtPI2.Text = kmGlobalData.arraySupplyValues[arrayOffsetPI+1].ToString();
            txtPI3.Text = kmGlobalData.arraySupplyValues[arrayOffsetPI+2].ToString();
            txtPI4.Text = kmGlobalData.arraySupplyValues[arrayOffsetPI+3].ToString();

            txtMI_1.Text = kmGlobalData.arraySupplyValues[arrayOffsetMI].ToString();
    
            //Supply Voltages

            txtValueL1.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetLIValues].ToString();
            txtValueL2.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetLIValues + 1].ToString();
            txtValueL3.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetLIValues + 2].ToString();
            txtValueL4.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetLIValues + 3].ToString();
            txtValueL5.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetLIValues + 4].ToString();
            txtValueL6.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetLIValues + 5].ToString();
            txtValueL7.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetLIValues + 6].ToString();
            txtValueL8.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetLIValues + 7].ToString();
            txtValueL9.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetLIValues + 8].ToString();
            txtValueL10.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetLIValues + 9].ToString();

            txtPI1L1.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetPIValues].ToString();
            txtPI1L2.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetPIValues + 1].ToString();
            txtPI1L3.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetPIValues + 2].ToString();

            txtPI2L1.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetPIValues + 3].ToString();
            txtPI2L2.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetPIValues + 4].ToString();
            txtPI2L3.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetPIValues + 5].ToString();

            txtPI3L1.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetPIValues + 6].ToString();
            txtPI3L2.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetPIValues + 7].ToString();
            txtPI3L3.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetPIValues + 8].ToString();

            txtPI4L1.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetPIValues + 9].ToString();
            txtPI4L2.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetPIValues + 10].ToString();
            txtPI4L3.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetPIValues + 11].ToString();

            txtMI1L1.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetMIValues].ToString();
            txtMI1L2.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetMIValues + 1].ToString();
            txtMI1L3.Text = kmGlobalData.arraySupplyVoltage[arrayOffsetMIValues + 2].ToString();



            if (selectedIOPlug != oldselectedIOPlug)
            {

                oldselectedIOPlug = selectedIOPlug;

                if (kmGlobalData.arrayRO[arrayOffsetDO] == 1)
                    chkRel1.Checked = true;
                else
                    chkRel1.Checked = false;

                if (kmGlobalData.arrayRO[arrayOffsetDO + 1] == 1)
                    chkRel2.Checked = true;
                else
                    chkRel2.Checked = false;

                if (kmGlobalData.arrayRO[arrayOffsetDO + 2] == 1)
                    chkRel3.Checked = true;
                else
                    chkRel3.Checked = false;

                if (kmGlobalData.arrayRO[arrayOffsetDO + 3] == 1)
                    chkRel4.Checked = true;
                else
                    chkRel4.Checked = false;

                if (kmGlobalData.arrayRO[arrayOffsetDO + 4] == 1)
                    chkRel5.Checked = true;
                else
                    chkRel5.Checked = false;

                if (kmGlobalData.arrayRO[arrayOffsetDO + 5] == 1)
                    chkRel6.Checked = true;
                else
                    chkRel6.Checked = false;

                if (kmGlobalData.arrayRO[arrayOffsetDO + 6] == 1)
                    chkRel7.Checked = true;
                else
                    chkRel7.Checked = false;

                if (kmGlobalData.arrayRO[arrayOffsetDO + 7] == 1)
                    chkRel8.Checked = true;
                else
                    chkRel8.Checked = false;

                // Digital Ouputs

                if (kmGlobalData.arrayDO[arrayOffsetDO] == 0)
                {
                    chkDOEin1.Checked = false;
                    chkDOUPullup1.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO] == 1)
                {
                    chkDOEin1.Checked = true;
                    chkDOUPullup1.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO] == 2)
                {
                    chkDOEin1.Checked = true;
                    chkDOUPullup1.Checked = true;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 1] == 0)
                {
                    chkDOEin2.Checked = false;
                    chkDOUPullup2.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 1] == 1)
                {
                    chkDOEin2.Checked = true;
                    chkDOUPullup2.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 1] == 2)
                {
                    chkDOEin2.Checked = true;
                    chkDOUPullup2.Checked = true;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 2] == 0)
                {
                    chkDOEin3.Checked = false;
                    chkDOUPullup3.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 2] == 1)
                {
                    chkDOEin3.Checked = true;
                    chkDOUPullup3.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 2] == 2)
                {
                    chkDOEin3.Checked = true;
                    chkDOUPullup3.Checked = true;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 3] == 0)
                {
                    chkDOEin4.Checked = false;
                    chkDOUPullup4.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 3] == 1)
                {
                    chkDOEin4.Checked = true;
                    chkDOUPullup4.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 3] == 2)
                {
                    chkDOEin4.Checked = true;
                    chkDOUPullup4.Checked = true;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 4] == 0)
                {
                    chkDOEin5.Checked = false;
                    chkDOUPullup5.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 4] == 1)
                {
                    chkDOEin5.Checked = true;
                    chkDOUPullup5.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 4] == 2)
                {
                    chkDOEin5.Checked = true;
                    chkDOUPullup5.Checked = true;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 5] == 0)
                {
                    chkDOEin6.Checked = false;
                    chkDOUPullup6.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 5] == 1)
                {
                    chkDOEin6.Checked = true;
                    chkDOUPullup6.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 5] == 2)
                {
                    chkDOEin6.Checked = true;
                    chkDOUPullup6.Checked = true;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 6] == 0)
                {
                    chkDOEin7.Checked = false;
                    chkDOUPullup7.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 6] == 1)
                {
                    chkDOEin7.Checked = true;
                    chkDOUPullup7.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 6] == 2)
                {
                    chkDOEin7.Checked = true;
                    chkDOUPullup7.Checked = true;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 7] == 0)
                {
                    chkDOEin8.Checked = false;
                    chkDOUPullup8.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 7] == 1)
                {
                    chkDOEin8.Checked = true;
                    chkDOUPullup8.Checked = false;
                }

                if (kmGlobalData.arrayDO[arrayOffsetDO + 7] == 2)
                {
                    chkDOEin8.Checked = true;
                    chkDOUPullup8.Checked = true;
                }

                //AO

                Byte[] intArray = new Byte[2];
                Array.Copy(kmGlobalData.arrayAO, arrayOffsetAO, intArray, 0, 2);
                int i = BitConverter.ToInt16(intArray, 0);
                txtAO1.Text = i.ToString();

                Array.Copy(kmGlobalData.arrayAO, arrayOffsetAO + 2, intArray, 0, 2);
                i = BitConverter.ToInt16(intArray, 0);
                txtAO2.Text = i.ToString();

                Array.Copy(kmGlobalData.arrayAO, arrayOffsetAO + 4, intArray, 0, 2);
                i = BitConverter.ToInt16(intArray, 0);
                txtAO3.Text = i.ToString();

                Array.Copy(kmGlobalData.arrayAO, arrayOffsetAO + 6, intArray, 0, 2);
                i = BitConverter.ToInt16(intArray, 0);
                txtAO4.Text = i.ToString();

                //TS

                Array.Copy(kmGlobalData.arrayTS, arrayOffsetTS, intArray, 0, 2);
                i = BitConverter.ToInt16(intArray, 0);
                txtTS1.Text = i.ToString();

                Array.Copy(kmGlobalData.arrayTS, arrayOffsetTS + 2, intArray, 0, 2);
                i = BitConverter.ToInt16(intArray, 0);
                txtTS2.Text = i.ToString();

                Array.Copy(kmGlobalData.arrayTS, arrayOffsetTS + 4, intArray, 0, 2);
                i = BitConverter.ToInt16(intArray, 0);
                txtTS3.Text = i.ToString();

                Array.Copy(kmGlobalData.arrayTS, arrayOffsetTS + 6, intArray, 0, 2);
                i = BitConverter.ToInt16(intArray, 0);
                txtTS4.Text = i.ToString();

                uint  PTCData = kmGlobalData.arrayPT[arrayOffsetPTC];
                BitArray bitValue = new BitArray(3);
                bitValue = ToBinary((int)PTCData);

                if (bitValue[0])
                    chkPTC1.Checked = true;
                else
                    chkPTC1.Checked = false;

                if (bitValue[1])
                    chkPTC2.Checked = true;
                else
                    chkPTC2.Checked = false;

                if (bitValue[2])
                    chkPTC3.Checked = true;
                else
                    chkPTC3.Checked = false;
            }

            if (selectedSupplyPlug != oldselectedSupplyPlug)
            {
                oldselectedSupplyPlug = selectedSupplyPlug;
                if (kmGlobalData.arrayPIStatus[arrayOffsetPIOut] == 1)
                {
                    chkPI_1.Checked = true;
                }
                else
                {
                    chkPI_1.Checked = false;
                }

  
            }


            if (kmGlobalData.arrayRO041[0] == 1)
                chkRO041_1.Checked = true;
            else
                chkRO041_1.Checked = false;

            kmFunctions.UpdateAnalogOutputs();
            kmFunctions.UpdateDigitalOutputs(true);

        }

        public static BitArray ToBinary(int numeral)
        {
            BitArray binary = new BitArray(new int[] { numeral });
            bool[] bits = new bool[binary.Count];
            binary.CopyTo(bits, 0);
            return binary;
        }


        private void btnPlug1_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 1;
        }

        private void btnPlug2_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 2;
        }

        private void btnPlug3_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 3;
        }

        private void btnPlug4_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 4;
        }

        private void btnPlug5_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 5;
        }

        private void btnPlug6_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 6;
        }

        private void btnPlug7_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 7;
        }

        private void btnPlug8_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 8;
        }

        private void btnPlug9_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 9;
        }

        private void btnPlug10_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 10;
        }

        private void btnPlug11_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 11;
        }

        private void btnPlug12_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 12;
        }

        private void btnPlug13_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 13;
        }

        private void btnPlug14_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 14;
        }

        private void btnPlug15_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 15;
        }

        private void btnPlug16_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 16;
        }

        private void btnPlug17_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 17;
        }

        private void btnPlug18_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 18;

        }

        private void btnPlug19_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 19;
        }

        private void btnPlug20_LinkPressed(object sender, DevExpress.XtraNavBar.NavBarLinkEventArgs e)
        {
            selectedIOPlug = 20;
        }

        private void chkRel1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRel1.Checked)
                kmGlobalData.arrayRO[arrayOffsetDO] = 1;
            else
                kmGlobalData.arrayRO[arrayOffsetDO] = 0;
        }

        private void chkRel2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRel2.Checked)
                kmGlobalData.arrayRO[arrayOffsetDO + 1] = 1;
            else
                kmGlobalData.arrayRO[arrayOffsetDO + 1] = 0;
        }

        private void chkRel3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRel3.Checked)
                kmGlobalData.arrayRO[arrayOffsetDO + 2] = 1;
            else
                kmGlobalData.arrayRO[arrayOffsetDO + 2] = 0;
        }

        private void chkRel4_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRel4.Checked)
                kmGlobalData.arrayRO[arrayOffsetDO + 3] = 1;
            else
                kmGlobalData.arrayRO[arrayOffsetDO + 3] = 0;
        }

        private void chkRel5_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRel5.Checked)
                kmGlobalData.arrayRO[arrayOffsetDO + 4] = 1;
            else
                kmGlobalData.arrayRO[arrayOffsetDO + 4] = 0;
        }

        private void chkRel6_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRel6.Checked)
                kmGlobalData.arrayRO[arrayOffsetDO + 5] = 1;
            else
                kmGlobalData.arrayRO[arrayOffsetDO + 5] = 0;
        }

        private void chkRel7_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRel7.Checked)
                kmGlobalData.arrayRO[arrayOffsetDO + 6] = 1;
            else
                kmGlobalData.arrayRO[arrayOffsetDO + 6] = 0;
        }

        private void chkRel8_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRel8.Checked)
                kmGlobalData.arrayRO[arrayOffsetDO + 7] = 1;
            else
                kmGlobalData.arrayRO[arrayOffsetDO + 7] = 0;
        }

        private void chkDOEin1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin1.Checked && !chkDOUPullup1.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO] = 0;

            if (!chkDOEin1.Checked && chkDOUPullup1.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO] = 1;

            if (chkDOEin1.Checked )
                kmGlobalData.arrayDO[arrayOffsetDO] = 2;
        }

        private void chkDOUPullup1_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin1.Checked && !chkDOUPullup1.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO] = 0;

            if (!chkDOEin1.Checked && chkDOUPullup1.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO] = 1;

            if (chkDOEin1.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO] = 2;

        }

        private void chkDOEin2_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin2.Checked && !chkDOUPullup2.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 1] = 0;

            if (!chkDOEin2.Checked && chkDOUPullup2.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO  + 1] = 1;

            if (chkDOEin2.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 1] = 2;
        }

        private void chkDOUPullup2_CheckedChanged(object sender, EventArgs e)
        {

            if (!chkDOEin2.Checked && !chkDOUPullup2.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 1] = 0;

            if (!chkDOEin2.Checked && chkDOUPullup2.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 1] = 1;

            if (chkDOEin2.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 1] = 2;

        }

        private void chkDOEin3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin3.Checked && !chkDOUPullup3.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 2] = 0;

            if (!chkDOEin3.Checked && chkDOUPullup3.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 2] = 1;

            if (chkDOEin3.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 2] = 2;
        }

        private void chkDOUPullup3_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin3.Checked && !chkDOUPullup3.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 2] = 0;

            if (!chkDOEin3.Checked && chkDOUPullup3.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 2] = 1;

            if (chkDOEin3.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 2] = 2;
        }

        private void chkDOEin4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin4.Checked && !chkDOUPullup4.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 3] = 0;

            if (!chkDOEin4.Checked && chkDOUPullup4.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 3] = 1;

            if (chkDOEin4.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 3] = 2;

        }

        private void chkDOUPullup4_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin4.Checked && !chkDOUPullup4.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 3] = 0;

            if (!chkDOEin4.Checked && chkDOUPullup4.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 3] = 1;

            if (chkDOEin4.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 3] = 2;
        }

        private void chkDOEin5_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin5.Checked && !chkDOUPullup5.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 4] = 0;

            if (!chkDOEin5.Checked && chkDOUPullup5.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 4] = 1;

            if (chkDOEin5.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 4] = 2;
        }

        private void chkDOUPullup5_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin5.Checked && !chkDOUPullup5.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 4] = 0;

            if (!chkDOEin5.Checked && chkDOUPullup5.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 4] = 1;

            if (chkDOEin5.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 4] = 2;

        }

        private void chkDOEin6_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin6.Checked && !chkDOUPullup6.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 5] = 0;

            if (!chkDOEin6.Checked && chkDOUPullup6.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 5] = 1;

            if (chkDOEin6.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 5] = 2;
        }

        private void chkDOUPullup6_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin6.Checked && !chkDOUPullup6.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 5] = 0;

            if (!chkDOEin6.Checked && chkDOUPullup6.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 5] = 1;

            if (chkDOEin6.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 5] = 2;
        }

        private void chkDOEin7_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin7.Checked && !chkDOUPullup7.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 6] = 0;

            if (!chkDOEin7.Checked && chkDOUPullup7.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 6] = 1;

            if (chkDOEin7.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 6] = 2;
        }

        private void chkDOUPullup7_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin7.Checked && !chkDOUPullup7.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 6] = 0;

            if (!chkDOEin7.Checked && chkDOUPullup7.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 6] = 1;

            if (chkDOEin7.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 6] = 2;
        }

        private void chkDOEin8_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin8.Checked && !chkDOUPullup8.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 7] = 0;

            if (!chkDOEin8.Checked && chkDOUPullup8.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 7] = 1;

            if (chkDOEin8.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 7] = 2;
        }

        private void chkDOUPullup8_CheckedChanged(object sender, EventArgs e)
        {
            if (!chkDOEin8.Checked && !chkDOUPullup8.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 7] = 0;

            if (!chkDOEin8.Checked && chkDOUPullup8.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 7] = 1;

            if (chkDOEin8.Checked)
                kmGlobalData.arrayDO[arrayOffsetDO + 7] = 2;
        }

        private Byte[] ConvertInt16ToByte(uint Value)
        {
            byte[] bytes = BitConverter.GetBytes(Value);
            return bytes;
        }

  
        private void txtAO1_KeyUp(object sender, KeyEventArgs e)
        {
            uint j = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (UInt32.TryParse(txtAO1.Text, out j))
                {
                    bytearray = ConvertInt16ToByte(j);
                    kmGlobalData.arrayAO[arrayOffsetAO] = bytearray[0];
                    kmGlobalData.arrayAO[arrayOffsetAO + 1] = bytearray[1];
                }
            }
        }
        private void txtAO2_KeyUp(object sender, KeyEventArgs e)
        {
            uint j = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (UInt32.TryParse(txtAO2.Text, out j))
                {
                    bytearray = ConvertInt16ToByte(j);
                    kmGlobalData.arrayAO[arrayOffsetAO + 2] = bytearray[0];
                    kmGlobalData.arrayAO[arrayOffsetAO + 3] = bytearray[1];
                }
            }
        }

        private void txtAO3_KeyUp(object sender, KeyEventArgs e)
        {
            uint j = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (UInt32.TryParse(txtAO3.Text, out j))
                {
                    bytearray = ConvertInt16ToByte(j);
                    kmGlobalData.arrayAO[arrayOffsetAO + 4] = bytearray[0];
                    kmGlobalData.arrayAO[arrayOffsetAO + 5] = bytearray[1];
                }
            }
        }     

        private void txtAO4_KeyUp(object sender, KeyEventArgs e)
        {
            uint j = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (UInt32.TryParse(txtAO4.Text, out j))
                {
                    bytearray = ConvertInt16ToByte(j);
                    kmGlobalData.arrayAO[arrayOffsetAO + 6] = bytearray[0];
                    kmGlobalData.arrayAO[arrayOffsetAO + 7] = bytearray[1];
                }
            }
        }

        private void txtTemp1_KeyUp(object sender, KeyEventArgs e)
        {
            uint j = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (UInt32.TryParse(txtTS1.Text, out j))
                {
                    bytearray = ConvertInt16ToByte(j);
                    kmGlobalData.arrayTS[arrayOffsetTS] = bytearray[0];
                    kmGlobalData.arrayTS[arrayOffsetTS + 1] = bytearray[1];
                }
            }
        }
        private void txtTS2_KeyUp(object sender, KeyEventArgs e)
        {
            uint j = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (UInt32.TryParse(txtTS2.Text, out j))
                {
                    bytearray = ConvertInt16ToByte(j);
                    kmGlobalData.arrayTS[arrayOffsetTS + 2] = bytearray[0];
                    kmGlobalData.arrayTS[arrayOffsetTS + 3] = bytearray[1];
                }
            }
        }

        private void txtTS3_KeyUp(object sender, KeyEventArgs e)
        {
            uint j = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (UInt32.TryParse(txtTS3.Text, out j))
                {
                    bytearray = ConvertInt16ToByte(j);
                    kmGlobalData.arrayTS[arrayOffsetTS + 4] = bytearray[0];
                    kmGlobalData.arrayTS[arrayOffsetTS + 5] = bytearray[1];
                }
            }
        }

        private void txtTS4_KeyUp(object sender, KeyEventArgs e)
        {
            uint j = 0;
            if (e.KeyCode == Keys.Enter)
            {
                if (UInt32.TryParse(txtTS4.Text, out j))
                {
                    bytearray = ConvertInt16ToByte(j);
                    kmGlobalData.arrayTS[arrayOffsetTS + 6] = bytearray[0];
                    kmGlobalData.arrayTS[arrayOffsetTS + 7] = bytearray[1];
                }
            }
        }

        private void chkPTC1_CheckStateChanged(object sender, EventArgs e)
        {
            Byte value = 0;
            if (chkPTC1.Checked)
                value = 1;
            if (chkPTC2.Checked)
                value += 2;
            if (chkPTC3.Checked)
                value += 4;
            kmGlobalData.arrayPT[arrayOffsetPTC] = value;
        }

        private void chkPTC2_CheckStateChanged(object sender, EventArgs e)
        {
            Byte value = 0;
            if (chkPTC1.Checked)
                value = 1;
            if (chkPTC2.Checked)
                value += 2;
            if (chkPTC3.Checked)
                value += 4;
            kmGlobalData.arrayPT[arrayOffsetPTC] = value;

        }

        private void chkPTC3_CheckStateChanged(object sender, EventArgs e)
        {
            Byte value = 0;
            if (chkPTC1.Checked)
                value = 1;
            if (chkPTC2.Checked)
                value += 2;
            if (chkPTC3.Checked)
                value += 4;
            kmGlobalData.arrayPT[arrayOffsetPTC] = value;

        }

        private void txtDI5_EditValueChanged(object sender, EventArgs e)
        {

        }

        private void navBarControl1_CustomDrawLink_1(object sender, CustomDrawNavBarElementEventArgs e)
        {
            if (e.ObjectInfo.State == ObjectState.Hot || e.ObjectInfo.State == ObjectState.Pressed)
            {
                LinearGradientBrush brush;
                NavLinkInfoArgs linkInfo = e.ObjectInfo as NavLinkInfoArgs;
                if (e.ObjectInfo.State == ObjectState.Hot)
                {
                    brush = new LinearGradientBrush(e.RealBounds, Color.Orange, Color.PeachPuff,
                        LinearGradientMode.Horizontal);
                }
                else
                    brush = new LinearGradientBrush(e.RealBounds, Color.PeachPuff, Color.Orange,
                        LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(Brushes.OrangeRed, e.RealBounds);
                Rectangle rect = e.RealBounds;
                rect.Inflate(-1, -1);
                e.Graphics.FillRectangle(brush, rect);
                if (e.Image != null)
                {
                    Rectangle imageRect = linkInfo.ImageRectangle;
                    imageRect.X += (imageRect.Width - e.Image.Width) / 2;
                    imageRect.Y += (imageRect.Height - e.Image.Height) / 2;
                    imageRect.Size = e.Image.Size;
                    e.Graphics.DrawImageUnscaled(e.Image, imageRect);
                }
                e.Appearance.DrawString(e.Cache, e.Caption, linkInfo.RealCaptionRectangle, Brushes.White);
                e.Handled = true;
            }

        }
    
        private void chkRO041_1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_1.Checked)
                kmGlobalData.arrayRO041[0] = 1;
            else
                kmGlobalData.arrayRO041[0] = 0;
        }

        private void chkRO041_2_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_2.Checked)
                kmGlobalData.arrayRO041[1] = 1;
            else
                kmGlobalData.arrayRO041[1] = 0;
        }

        private void chkRO041_3_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_3.Checked)
                kmGlobalData.arrayRO041[2] = 1;
            else
                kmGlobalData.arrayRO041[2] = 0;
        }

        private void chkRO041_4_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_4.Checked)
                kmGlobalData.arrayRO041[3] = 1;
            else
                kmGlobalData.arrayRO041[3] = 0;
        }

        private void chkRO041_5_CheckStateChanged(object sender, EventArgs e)
        {
            if (chkRO041_5.Checked)
                kmGlobalData.arrayRO041[4] = 1;
            else
                kmGlobalData.arrayRO041[4] = 0;
        }

        private void chkRO041_6_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_6.Checked)
                kmGlobalData.arrayRO041[5] = 1;
            else
                kmGlobalData.arrayRO041[5] = 0;
        }

        private void chkRO041_7_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_7.Checked)
                kmGlobalData.arrayRO041[6] = 1;
            else
                kmGlobalData.arrayRO041[6] = 0;
        }

        private void chkRO041_8_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_8.Checked)
                kmGlobalData.arrayRO041[7] = 1;
            else
                kmGlobalData.arrayRO041[7] = 0;
        }

        private void chkRO041_9_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_9.Checked)
                kmGlobalData.arrayRO041[8] = 1;
            else
                kmGlobalData.arrayRO041[8] = 0;
        }

        private void chkRO041_10_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_10.Checked)
                kmGlobalData.arrayRO041[9] = 1;
            else
                kmGlobalData.arrayRO041[9] = 0;

        }

        private void chkRO041_11_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_11.Checked)
                kmGlobalData.arrayRO041[10] = 1;
            else
                kmGlobalData.arrayRO041[10] = 0;
        }

        private void chkRO041_12_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_12.Checked)
                kmGlobalData.arrayRO041[11] = 1;
            else
                kmGlobalData.arrayRO041[11] = 0;
        }

        private void chkRO041_13_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_13.Checked)
                kmGlobalData.arrayRO041[12] = 1;
            else
                kmGlobalData.arrayRO041[12] = 0;
        }

        private void chkRO041_14_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_14.Checked)
                kmGlobalData.arrayRO041[13] = 1;
            else
                kmGlobalData.arrayRO041[13] = 0;
        }

        private void chkRO041_15_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_15.Checked)
                kmGlobalData.arrayRO041[14] = 1;
            else
                kmGlobalData.arrayRO041[14] = 0;
        }

        private void chkRO041_16_CheckedChanged(object sender, EventArgs e)
        {
            if (chkRO041_16.Checked)
                kmGlobalData.arrayRO041[15] = 1;
            else
                kmGlobalData.arrayRO041[15] = 0;
        }

        private void MultiIO_FormClosing(object sender, FormClosingEventArgs e)
        {
            myTimer.Stop();
        }

      

        private void btnSupply_1_LinkPressed(object sender, NavBarLinkEventArgs e)
        {
            selectedSupplyPlug =  1;
        }

        private void btnSupply_2_LinkPressed(object sender, NavBarLinkEventArgs e)
        {
            selectedSupplyPlug = 2;
        }

        private void btnSupply_3_LinkPressed(object sender, NavBarLinkEventArgs e)
        {
            selectedSupplyPlug = 3;
        }

        private void btnSupply_4_LinkPressed(object sender, NavBarLinkEventArgs e)
        {
            selectedSupplyPlug = 4;
        }

        private void btnSupply_5_LinkPressed(object sender, NavBarLinkEventArgs e)
        {
            selectedSupplyPlug = 5;
        }

        private void btnSupply_6_LinkPressed(object sender, NavBarLinkEventArgs e)
        {
            selectedSupplyPlug = 6;
        }

        private void btnSupply_7_LinkPressed(object sender, NavBarLinkEventArgs e)
        {
            selectedSupplyPlug = 7;
        }

        private void btnSupply_8_LinkPressed(object sender, NavBarLinkEventArgs e)
        {
            selectedSupplyPlug = 8;
        }

        private void navBarControlSupply_CustomDrawLink(object sender, CustomDrawNavBarElementEventArgs e)
        {
            if (e.ObjectInfo.State == ObjectState.Hot || e.ObjectInfo.State == ObjectState.Pressed)
            {
                LinearGradientBrush brush;
                NavLinkInfoArgs linkInfo = e.ObjectInfo as NavLinkInfoArgs;
                if (e.ObjectInfo.State == ObjectState.Hot)
                {
                    brush = new LinearGradientBrush(e.RealBounds, Color.Orange, Color.PeachPuff,
                        LinearGradientMode.Horizontal);
                }
                else
                    brush = new LinearGradientBrush(e.RealBounds, Color.PeachPuff, Color.Orange,
                        LinearGradientMode.Horizontal);
                e.Graphics.FillRectangle(Brushes.OrangeRed, e.RealBounds);
                Rectangle rect = e.RealBounds;
                rect.Inflate(-1, -1);
                e.Graphics.FillRectangle(brush, rect);
                if (e.Image != null)
                {
                    Rectangle imageRect = linkInfo.ImageRectangle;
                    imageRect.X += (imageRect.Width - e.Image.Width) / 2;
                    imageRect.Y += (imageRect.Height - e.Image.Height) / 2;
                    imageRect.Size = e.Image.Size;
                    e.Graphics.DrawImageUnscaled(e.Image, imageRect);
                }
                e.Appearance.DrawString(e.Cache, e.Caption, linkInfo.RealCaptionRectangle, Brushes.White);
                e.Handled = true;
            }
        }

        private void chkPI_1_CheckedChanged(object sender, EventArgs e)
        {
            if (chkPI_1.Checked)
                kmGlobalData.arrayPIStatus[arrayOffsetPIOut] = 1;
            else
                kmGlobalData.arrayPIStatus[arrayOffsetPIOut] = 0;
        }

       

        

       

    
    }
}