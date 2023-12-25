using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;
using System.Threading;
using System.Diagnostics;
using BrightnessLibrary;
using System.Resources;
using System.Reflection;

namespace Brightness_control_8CH
{
    
    public partial class Form1 : Form
    {
        private BrightnessLibrary.Communication Comm = new Communication();
        ResourceManager pic = new ResourceManager("Brightness_control_8CH.Properties.Resources", Assembly.GetExecutingAssembly());

        public Form1()
        {
            InitializeComponent();

        }

        private bool Controller_Button  = false;
        private bool Controller_cycle   = false;
        private bool Controller_random  = false;
        private bool Controller_Unite   = false;
        private bool Controller_Start   = false;
        private bool Controller_Breathe = false;
        private Thread ControllerStatusThread, m_CommDetectionThread;
        private byte F_Brightness_Unit = 0;
        private byte F_BrightnessCH1 = 0;
        private byte F_BrightnessCH1_Min = 0;
        private byte F_BrightnessCH1_Max = 100;
        private byte F_BrightnessCH2 = 0;
        private byte F_BrightnessCH2_Min = 0;
        private byte F_BrightnessCH2_Max = 100;
        private byte F_BrightnessCH3 = 0;
        private byte F_BrightnessCH3_Min = 0;
        private byte F_BrightnessCH3_Max = 100;
        private byte F_BrightnessCH4 = 0;
        private byte F_BrightnessCH4_Min = 0;
        private byte F_BrightnessCH4_Max = 100;
        private byte F_BrightnessCH5 = 0;
        private byte F_BrightnessCH5_Min = 0;
        private byte F_BrightnessCH5_Max = 100;
        private byte F_BrightnessCH6 = 0;
        private byte F_BrightnessCH6_Min = 0;
        private byte F_BrightnessCH6_Max = 100;
        private byte F_BrightnessCH7 = 0;
        private byte F_BrightnessCH7_Min = 0;
        private byte F_BrightnessCH7_Max = 100;
        private byte F_BrightnessCH8 = 0;
        private byte F_BrightnessCH8_Min = 0;
        private byte F_BrightnessCH8_Max = 100;
        private byte Interval = 1;
        private ulong Success = 0,Fail = 0;
        string[] aryPortName = new string[128];
        private int PortNo = 0;

        public string ComProtNmber;
        //int ComPortIndex = 0;



        public SerialPort comport = new SerialPort();
        //string commport;
        string strAutoSetBrightnessMethod = " Increase ";
        int AutoSetBrightnessInterval;
        int UartQuantity;

        private void button1_Click(object sender, EventArgs e)
        {
            Controller_Button = !Controller_Button;
            if (Controller_Button)
            {
                OpenPort();
                cmbPortName.Enabled = false;
                button1.BackgroundImage = pic.GetObject("image322") as Image;
                //label1.Text = Comm.ModeCode();
            }
            else
            {
                label1.Text = "Linking........";
                Comm.ClosePort();
                cmbPortName.Enabled = true;
                button1.BackgroundImage = pic.GetObject("image334") as Image;
            }


        }
        private void Form1_Load(object sender, EventArgs e)
        {
            foreach (string s in SerialPort.GetPortNames())
            {
                cmbPortName.Items.Add(s);
            }
                Implement();
        }

        private int CH;
        private string ControlMode, aisle ="", Judge_aisle = "";

        private int Check_CHnubmer()
        {
            ControlMode = Comm.ModeCode();
            Judge_aisle = ControlMode.Substring(0, 2);
            if (Judge_aisle == "GD")
            {
                aisle = ControlMode.Substring(2, 1);
                switch (aisle)
                {
                    case "1":
                        checkBox2.Checked = true;
                        break;
                    case "2":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        break;
                    case "3":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        break;
                    case "4":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        checkBox5.Checked = true;
                        break;
                    case "5":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        checkBox5.Checked = true;
                        checkBox6.Checked = true;
                        break;
                    case "6":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        checkBox5.Checked = true;
                        checkBox6.Checked = true;
                        checkBox7.Checked = true;
                        break;
                    case "7":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        checkBox5.Checked = true;
                        checkBox6.Checked = true;
                        checkBox7.Checked = true;
                        checkBox8.Checked = true;
                        break;
                    case "8":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        checkBox5.Checked = true;
                        checkBox6.Checked = true;
                        checkBox7.Checked = true;
                        checkBox8.Checked = true;
                        checkBox9.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                aisle = ControlMode.Substring(ControlMode.Length - 1, 1);
                switch (aisle)
                {
                    case "1":
                        checkBox2.Checked = true;
                        break;
                    case "2":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        break;
                    case "3":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        break;
                    case "4":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        checkBox5.Checked = true;
                        break;
                    case "5":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        checkBox5.Checked = true;
                        checkBox6.Checked = true;
                        break;
                    case "6":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        checkBox5.Checked = true;
                        checkBox6.Checked = true;
                        checkBox7.Checked = true;
                        break;
                    case "7":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        checkBox5.Checked = true;
                        checkBox6.Checked = true;
                        checkBox7.Checked = true;
                        checkBox8.Checked = true;
                        break;
                    case "8":
                        checkBox2.Checked = true;
                        checkBox3.Checked = true;
                        checkBox4.Checked = true;
                        checkBox5.Checked = true;
                        checkBox6.Checked = true;
                        checkBox7.Checked = true;
                        checkBox8.Checked = true;
                        checkBox9.Checked = true;
                        break;
                    default:
                        break;
                }
            }
            return CH;

        }
        private int CHnubmer()
        {
             ControlMode = Comm.ModeCode();
             Judge_aisle = ControlMode.Substring(0, 2);
            if (Judge_aisle == "GD")
            {
                aisle = ControlMode.Substring(2, 1);
                switch (aisle)
                {
                    case "1":
                        CH = 1;
                        break;
                    case "2":
                        CH = 2;
                        break;
                    case "3":
                        CH = 3;
                        break;
                    case "4":
                        CH = 4;
                        break;
                    case "5":
                        CH = 5;
                        break;
                    case "6":
                        CH = 6;
                        break;
                    case "7":
                        CH = 7;
                        break;
                    case "8":
                        CH = 8;
                        break;
                    default:
                        break;
                }
            }
            else
            {
                aisle = ControlMode.Substring(ControlMode.Length - 1, 1);
                switch (aisle)
                {
                    case "1":
                        CH = 1;
                        break;
                    case "2":
                        CH = 2;
                        break;
                    case "3":
                        CH = 3;
                        break;
                    case "4":
                        CH = 4;
                        break;
                    case "5":
                        CH = 5;
                        break;
                    case "6":
                        CH = 6;
                        break;
                    case "7":
                        CH = 7;
                        break;
                    case "8":
                        CH = 8;
                        break;
                    default:
                        break;
                }
            }
                return CH;
            
        }
       delegate void GetControllerInformationCallback();
       delegate void CountSuccess(ulong nubmer); 


        void OpenPort()
        {
            // If the port is open, close it.
            if (Controller_Button == false)
            {
                Comm.ClosePort();
            }
            else
            {
                m_CommDetectionThread = new Thread(new ThreadStart(CommDetectionThread));
                m_CommDetectionThread.IsBackground = true;
                m_CommDetectionThread.Start();
            }
        }

        private void CommDetectionThread()
        {
            GetControllerInformationCallback d = new GetControllerInformationCallback(PortList);
            this.Invoke(d, new object[] { });
        }

        void PortList()
        {
            Comm.OpenComPort(ComProtNmber);
            label1.Text = Comm.ModeCode();
            Check_CHnubmer();
        }

        private void run(int channl)
        { 
            if (strAutoSetBrightnessMethod == "Increase")
            {
                switch (channl)
                {
                    case 1:
                        if (F_BrightnessCH1 >= F_BrightnessCH1_Max)
                        {
                            F_BrightnessCH1 = F_BrightnessCH1_Min;
                        }
                        else
                        {
                         F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                         Comm.CH1_SetBrightness(F_BrightnessCH1);
                         Thread.Sleep(AutoSetBrightnessInterval);
                            if(checkBox1.Checked == true)
                            {
                                Comm.CH1_SetBrightness(0);
                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                            }
                        }
                        break;
                    case 2:
                        if (checkBox2.Checked == true)
                        {
                            for (int i = F_BrightnessCH1_Min; i<= F_BrightnessCH1_Max; i += Interval) {
                                F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                Comm.CH1_SetBrightness(F_BrightnessCH1);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                {
                                    F_BrightnessCH1 = F_BrightnessCH1_Min;
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH1_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    break;
                                }
                            }
                        }
                        if (checkBox3.Checked == true)
                        {
                            for (int i = F_BrightnessCH2_Min; i <= F_BrightnessCH2_Max; i += Interval)
                            {
                                F_BrightnessCH2 = (byte)Math.Min(F_BrightnessCH2_Max, F_BrightnessCH2 + Interval);
                                Comm.CH2_SetBrightness(F_BrightnessCH2);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if(F_BrightnessCH2 == F_BrightnessCH2_Max)
                                {
                                    F_BrightnessCH2 = F_BrightnessCH2_Min;
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH2_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    Thread.Sleep(2000);
                                    break;
                                }
                            }
                        }
                        break;
                    case 4:                       
                        if (checkBox2.Checked == true)
                        {
                            for (int i = F_BrightnessCH1_Min; i <= F_BrightnessCH1_Max; i += Interval)
                            {
                                F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                Comm.CH1_SetBrightness(F_BrightnessCH1);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                {
                                    F_BrightnessCH1 = F_BrightnessCH1_Min;
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH1_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    break;
                                }
                            }
                        }
                        if (checkBox3.Checked == true)
                        {
                            for (int i = F_BrightnessCH2_Min; i <= F_BrightnessCH2_Max; i += Interval)
                            {
                                F_BrightnessCH2 = (byte)Math.Min(F_BrightnessCH2_Max, F_BrightnessCH2 + Interval);
                                Comm.CH2_SetBrightness(F_BrightnessCH2);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH2 == F_BrightnessCH2_Max)
                                {
                                    F_BrightnessCH2 = F_BrightnessCH2_Min;
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH2_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    break;
                                }
                            }
                        }
                        if (checkBox4.Checked == true)
                        {
                            for (int i = F_BrightnessCH3_Min; i <= F_BrightnessCH3_Max; i += Interval)
                            {
                                F_BrightnessCH3 = (byte)Math.Min(F_BrightnessCH3_Max, F_BrightnessCH3 + Interval);
                                Comm.CH3_SetBrightness(F_BrightnessCH3);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH3 == F_BrightnessCH3_Max)
                                {
                                    F_BrightnessCH3 = F_BrightnessCH3_Min;
                                    Comm.CH3_SetBrightness(F_BrightnessCH3);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH3_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    break;
                                }
                            }
                        }
                        if (checkBox5.Checked == true)
                        {
                            for (int i = F_BrightnessCH4_Min; i <= F_BrightnessCH4_Max; i += Interval)
                            {
                                F_BrightnessCH4 = (byte)Math.Min(F_BrightnessCH4_Max, F_BrightnessCH4 + Interval);
                                Comm.CH4_SetBrightness(F_BrightnessCH4);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH4 == F_BrightnessCH4_Max)
                                {
                                    F_BrightnessCH4 = F_BrightnessCH4_Min;
                                    Comm.CH4_SetBrightness(F_BrightnessCH4);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH4_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    Thread.Sleep(2000);
                                    break;
                                }
                            }
                        }
                        break;
                    case 8:
                        if (checkBox2.Checked == true)
                        {
                            for (int i = F_BrightnessCH1_Min; i <= F_BrightnessCH1_Max; i += Interval)
                            {
                                F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                Comm.CH1_SetBrightness(F_BrightnessCH1);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                {
                                    F_BrightnessCH1 = F_BrightnessCH1_Min;
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH1_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    break;
                                }
                            }
                        }
                        if (checkBox3.Checked == true)
                        {
                            for (int i = F_BrightnessCH2_Min; i <= F_BrightnessCH2_Max; i += Interval)
                            {
                                F_BrightnessCH2 = (byte)Math.Min(F_BrightnessCH2_Max, F_BrightnessCH2 + Interval);
                                Comm.CH2_SetBrightness(F_BrightnessCH2);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH2 == F_BrightnessCH2_Max)
                                {
                                    F_BrightnessCH2 = F_BrightnessCH2_Min;
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH2_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    break;
                                }
                            }
                        }
                        if (checkBox4.Checked == true)
                        {
                            for (int i = F_BrightnessCH3_Min; i <= F_BrightnessCH3_Max; i += Interval)
                            {
                                F_BrightnessCH3 = (byte)Math.Min(F_BrightnessCH3_Max, F_BrightnessCH3 + Interval);
                                Comm.CH3_SetBrightness(F_BrightnessCH3);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH3 ==  F_BrightnessCH3_Max)
                                {
                                    F_BrightnessCH3 = F_BrightnessCH3_Min;
                                    Comm.CH3_SetBrightness(F_BrightnessCH3);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH3_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    break;
                                }
                            }
                        }
                        if (checkBox5.Checked == true)
                        {
                            for (int i = F_BrightnessCH4_Min; i <= F_BrightnessCH4_Max; i += Interval)
                            {
                                F_BrightnessCH4 = (byte)Math.Min(F_BrightnessCH4_Max, F_BrightnessCH4 + Interval);
                                Comm.CH4_SetBrightness(F_BrightnessCH4);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH4 ==  F_BrightnessCH4_Max)
                                {
                                    F_BrightnessCH4 = F_BrightnessCH4_Min;
                                    Comm.CH4_SetBrightness(F_BrightnessCH4);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH4_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    break;
                                }
                            }
                        }
                        if (checkBox6.Checked == true)
                        {
                            for (int i = F_BrightnessCH5_Min; i <= F_BrightnessCH5_Max; i += Interval)
                            {
                                F_BrightnessCH5 = (byte)Math.Min(F_BrightnessCH5_Max, F_BrightnessCH5 + Interval);
                                Comm.CH5_SetBrightness(F_BrightnessCH5);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH5 == F_BrightnessCH5_Max)
                                {
                                    F_BrightnessCH5 = F_BrightnessCH5_Min;
                                    Comm.CH5_SetBrightness(F_BrightnessCH5);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH5_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    break;
                                }
                            }
                        }
                        if (checkBox7.Checked == true)
                        {
                            for (int i = F_BrightnessCH6_Min; i <= F_BrightnessCH6_Max; i += Interval)
                            {
                                F_BrightnessCH6 = (byte)Math.Min(F_BrightnessCH6_Max, F_BrightnessCH6 + Interval);
                                Comm.CH6_SetBrightness(F_BrightnessCH6);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH6 == F_BrightnessCH6_Max)
                                {
                                    F_BrightnessCH6 = F_BrightnessCH6_Min;
                                    Comm.CH6_SetBrightness(F_BrightnessCH6);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH6_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    break;
                                }
                            }
                        }
                        if (checkBox8.Checked == true)
                        {
                            for (int i = F_BrightnessCH7_Min; i <= F_BrightnessCH7_Max; i += Interval)
                            {
                                F_BrightnessCH7 = (byte)Math.Min(F_BrightnessCH7_Max, F_BrightnessCH7 + Interval);
                                Comm.CH7_SetBrightness(F_BrightnessCH7);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH7 ==  F_BrightnessCH7_Max)
                                {
                                    F_BrightnessCH7 = F_BrightnessCH7_Min;
                                    Comm.CH7_SetBrightness(F_BrightnessCH7);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH7_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    break;
                                }
                            }
                        }
                        if (checkBox9.Checked == true)
                        {
                            for (int i = F_BrightnessCH8_Min; i <= F_BrightnessCH8_Max; i += Interval)
                            {
                                F_BrightnessCH8 = (byte)Math.Min(F_BrightnessCH8_Max, F_BrightnessCH8 + Interval);
                                Comm.CH8_SetBrightness(F_BrightnessCH8);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH8 == F_BrightnessCH8_Max)
                                {
                                    F_BrightnessCH8 = F_BrightnessCH8_Min;
                                    Comm.CH8_SetBrightness(F_BrightnessCH8);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH8_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    Thread.Sleep(2000);
                                    break;
                                }
                            }
                        }
                        break;
                }

            }
            if(strAutoSetBrightnessMethod == "Decrease")
            {
                switch (channl)
                {
                    case 1:
                        if (F_BrightnessCH1 <= F_BrightnessCH1_Min)
                        {
                            F_BrightnessCH1 = F_BrightnessCH1_Max;
                        }
                        else
                        {
                            F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                            Comm.CH1_SetBrightness(F_BrightnessCH1);
                            Thread.Sleep(AutoSetBrightnessInterval);
                            if (checkBox1.Checked == true)
                            {
                                Comm.CH1_SetBrightness(0);
                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                            }
                        }
                        break;
                    case 2:
                        if (checkBox2.Checked == true)
                        {
                            for (int i = F_BrightnessCH1_Max; i >= F_BrightnessCH1_Min; i -= Interval)
                            {
                                F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                Comm.CH1_SetBrightness(F_BrightnessCH1);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                {
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH1_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH1 = F_BrightnessCH1_Max;
                                    break;
                                }
                            }
                        }
                        if (checkBox3.Checked == true)
                        {
                            for (int i = F_BrightnessCH2_Max; i >= F_BrightnessCH2_Min; i -= Interval)
                            {
                                F_BrightnessCH2 = (byte)Math.Max(F_BrightnessCH2_Min, F_BrightnessCH2 - Interval);
                                Comm.CH2_SetBrightness(F_BrightnessCH2);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH2 == F_BrightnessCH2_Min)
                                {
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH2_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH2 = F_BrightnessCH2_Max;
                                    Thread.Sleep(2000);
                                    break;
                                }
                            }
                        }
                        break;
                    case 4:
                        if (checkBox2.Checked == true)
                        {
                            for (int i = F_BrightnessCH1_Max; i >= F_BrightnessCH1_Min; i -= Interval)
                            {
                                F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                Comm.CH1_SetBrightness(F_BrightnessCH1);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                {
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH1_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH1 = F_BrightnessCH1_Max;
                                    break;
                                }
                            }
                        }
                        if (checkBox3.Checked == true)
                        {
                            for (int i = F_BrightnessCH2_Max; i >= F_BrightnessCH2_Min; i -= Interval)
                            {
                                F_BrightnessCH2 = (byte)Math.Max(F_BrightnessCH2_Min, F_BrightnessCH2 - Interval);
                                Comm.CH2_SetBrightness(F_BrightnessCH2);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH2 == F_BrightnessCH2_Min)
                                {
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH2_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH2 = F_BrightnessCH2_Max;
                                    break;
                                }
                            }
                        }
                        if (checkBox4.Checked == true)
                        {
                            for (int i = F_BrightnessCH3_Max; i >= F_BrightnessCH3_Min; i -= Interval)
                            {
                                F_BrightnessCH3 = (byte)Math.Max(F_BrightnessCH3_Min, F_BrightnessCH3 - Interval);
                                Comm.CH3_SetBrightness(F_BrightnessCH3);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH3 == F_BrightnessCH3_Min)
                                {
                                    Comm.CH3_SetBrightness(F_BrightnessCH3);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH3_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH3 = F_BrightnessCH3_Max;
                                    break;
                                }
                            }
                        }
                        if (checkBox5.Checked == true)
                        {
                            for (int i = F_BrightnessCH4_Max; i >= F_BrightnessCH4_Min; i -= Interval)
                            {
                                F_BrightnessCH4 = (byte)Math.Max(F_BrightnessCH4_Min, F_BrightnessCH4 - Interval);
                                Comm.CH4_SetBrightness(F_BrightnessCH4);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH4 == F_BrightnessCH4_Min)
                                {
                                    Comm.CH4_SetBrightness(F_BrightnessCH4);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH4_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH4 = F_BrightnessCH4_Max;
                                    Thread.Sleep(2000);
                                    break;
                                }
                            }
                        }
                        break;
                    case 8:
                        if (checkBox2.Checked == true)
                        {
                            for (int i = F_BrightnessCH1_Max; i >= F_BrightnessCH1_Min; i -= Interval)
                            {
                                F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                Comm.CH1_SetBrightness(F_BrightnessCH1);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                {
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH1_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH1 = F_BrightnessCH1_Max;
                                    break;
                                }
                            }
                        }
                        if (checkBox3.Checked == true)
                        {
                            for (int i = F_BrightnessCH2_Max; i >= F_BrightnessCH2_Min; i -= Interval)
                            {
                                F_BrightnessCH2 = (byte)Math.Max(F_BrightnessCH2_Min, F_BrightnessCH2 - Interval);
                                Comm.CH2_SetBrightness(F_BrightnessCH2);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH2 == F_BrightnessCH2_Min)
                                {
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH2_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH2 = F_BrightnessCH2_Max;
                                    break;
                                }
                            }
                        }
                        if (checkBox4.Checked == true)
                        {
                            for (int i = F_BrightnessCH3_Max; i >= F_BrightnessCH3_Min; i -= Interval)
                            {
                                F_BrightnessCH3 = (byte)Math.Max(F_BrightnessCH3_Min, F_BrightnessCH3 - Interval);
                                Comm.CH3_SetBrightness(F_BrightnessCH3);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH3 == F_BrightnessCH3_Min)
                                {
                                    Comm.CH3_SetBrightness(F_BrightnessCH3);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH3_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH3 = F_BrightnessCH3_Max;
                                    break;
                                }
                            }
                        }
                        if (checkBox5.Checked == true)
                        {
                            for (int i = F_BrightnessCH4_Max; i >= F_BrightnessCH4_Min; i -= Interval)
                            {
                                F_BrightnessCH4 = (byte)Math.Max(F_BrightnessCH4_Min, F_BrightnessCH4 - Interval);
                                Comm.CH4_SetBrightness(F_BrightnessCH4);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH4 == F_BrightnessCH4_Min)
                                {
                                    Comm.CH4_SetBrightness(F_BrightnessCH4);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH4_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH4 = F_BrightnessCH4_Max;
                                    break;
                                }
                            }
                        }
                        if (checkBox6.Checked == true)
                        {
                            for (int i = F_BrightnessCH5_Max; i >= F_BrightnessCH5_Min; i -= Interval)
                            {
                                F_BrightnessCH5 = (byte)Math.Max(F_BrightnessCH5_Min, F_BrightnessCH5 - Interval);
                                Comm.CH5_SetBrightness(F_BrightnessCH5);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH5 == F_BrightnessCH5_Min)
                                {
                                    Comm.CH5_SetBrightness(F_BrightnessCH5);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH5_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH5 = F_BrightnessCH5_Max;
                                    break;
                                }
                            }
                        }
                        if (checkBox7.Checked == true)
                        {
                            for (int i = F_BrightnessCH6_Max; i >= F_BrightnessCH6_Min; i -= Interval)
                            {
                                F_BrightnessCH6 = (byte)Math.Max(F_BrightnessCH6_Min, F_BrightnessCH6 - Interval);
                                Comm.CH6_SetBrightness(F_BrightnessCH6);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH6 == F_BrightnessCH6_Min)
                                {
                                    Comm.CH6_SetBrightness(F_BrightnessCH6);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH6_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH6 = F_BrightnessCH6_Max;
                                    break;
                                }
                            }
                        }
                        if (checkBox8.Checked == true)
                        {
                            for (int i = F_BrightnessCH7_Max; i >= F_BrightnessCH7_Min; i -= Interval)
                            {
                                F_BrightnessCH7 = (byte)Math.Max(F_BrightnessCH7_Min, F_BrightnessCH7 - Interval);
                                Comm.CH7_SetBrightness(F_BrightnessCH7);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH7 == F_BrightnessCH7_Min)
                                {
                                    Comm.CH7_SetBrightness(F_BrightnessCH7);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH7_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH7 = F_BrightnessCH7_Max;
                                    break;
                                }
                            }
                        }
                        if (checkBox9.Checked == true)
                        {
                            for (int i = F_BrightnessCH8_Max; i >= F_BrightnessCH8_Min; i -= Interval)
                            {
                                F_BrightnessCH8 = (byte)Math.Max(F_BrightnessCH8_Min, F_BrightnessCH8 - Interval);
                                Comm.CH8_SetBrightness(F_BrightnessCH8);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH8 == F_BrightnessCH8_Min)
                                {
                                    Comm.CH8_SetBrightness(F_BrightnessCH8);
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH8_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                    F_BrightnessCH8 = F_BrightnessCH8_Max;
                                    Thread.Sleep(2000);
                                    break;
                                }
                            }
                        }
                        break;
                }

            }
        }

        void Unite_Run_Mode(int channl)
        {
            if(strAutoSetBrightnessMethod == "Increase")
            {
                switch (channl)
                {
                    case 1:
                        F_Brightness_Unit  = (byte)Math.Min(F_BrightnessCH1_Max, F_Brightness_Unit + Interval);
                        Comm.SetUnit1CH(F_Brightness_Unit);
                        Thread.Sleep(AutoSetBrightnessInterval);
                        if (F_Brightness_Unit >= F_BrightnessCH1_Max)
                        {
                            F_Brightness_Unit = F_BrightnessCH1_Min;
                            if (checkBox1.Checked == true)
                            {
                                Comm.SetUnit1CH(0);
                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                            }
                        }
                        break;
                    case 2:
                        F_Brightness_Unit = (byte)Math.Min(F_BrightnessCH1_Max, F_Brightness_Unit + Interval);
                        Comm.SetUnit2CH(F_Brightness_Unit, F_Brightness_Unit);
                        Thread.Sleep(AutoSetBrightnessInterval);
                        if (F_Brightness_Unit >= F_BrightnessCH1_Max)
                        {
                            F_Brightness_Unit = F_BrightnessCH1_Min;
                            if (checkBox1.Checked == true)
                            {
                                Comm.SetUnit2CH(0,0);
                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                            }
                        }
                        break;
                    case 4:
                        F_Brightness_Unit = (byte)Math.Min(F_BrightnessCH1_Max, F_Brightness_Unit + Interval);
                        Comm.SetUnit4CH(F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit);
                        Thread.Sleep(AutoSetBrightnessInterval);
                        if (F_Brightness_Unit >= F_BrightnessCH1_Max)
                        {
                            F_Brightness_Unit = F_BrightnessCH1_Min;
                            if (checkBox1.Checked == true)
                            {
                                Comm.SetUnit4CH(0,0,0,0);
                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                            }
                        }
                        break;
                    case 8:
                        F_Brightness_Unit = (byte)Math.Min(F_BrightnessCH1_Max, F_Brightness_Unit + Interval);
                        Comm.SetUnit8CH(F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit);
                        Thread.Sleep(AutoSetBrightnessInterval);
                        if (F_Brightness_Unit >= F_BrightnessCH1_Max)
                        {
                            F_Brightness_Unit = F_BrightnessCH1_Min;
                            if (checkBox1.Checked == true)
                            {
                                Comm.SetUnit8CH(0, 0, 0, 0, 0, 0, 0, 0);
                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                            }
                        }
                        break;
                }

            }
            if(strAutoSetBrightnessMethod == "Decrease")
            {
                switch (channl)
                {
                    case 1:
                        F_Brightness_Unit = (byte)Math.Max(F_BrightnessCH1_Min, F_Brightness_Unit - Interval);
                        Comm.SetUnit1CH(F_Brightness_Unit);
                        Thread.Sleep(AutoSetBrightnessInterval);
                        if (F_Brightness_Unit <= F_BrightnessCH1_Min)
                        {
                            F_Brightness_Unit = F_BrightnessCH1_Max;
                            if (checkBox1.Checked == true)
                            {
                                Comm.SetUnit1CH(0);
                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                            }
                        }
                        break;
                    case 2:
                        F_Brightness_Unit = (byte)Math.Max(F_BrightnessCH1_Min, F_Brightness_Unit - Interval);
                        Comm.SetUnit2CH(F_Brightness_Unit, F_Brightness_Unit);
                        Thread.Sleep(AutoSetBrightnessInterval);
                        if (F_Brightness_Unit <= F_BrightnessCH1_Min)
                        {
                            F_Brightness_Unit = F_BrightnessCH1_Max;
                            if (checkBox1.Checked == true)
                            {
                                Comm.SetUnit2CH(0, 0);
                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                            }
                        }
                        break;
                    case 4:
                        F_Brightness_Unit = (byte)Math.Max(F_BrightnessCH1_Min, F_Brightness_Unit - Interval);
                        Comm.SetUnit4CH(F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit);
                        Thread.Sleep(AutoSetBrightnessInterval);
                        if (F_Brightness_Unit <= F_BrightnessCH1_Min)
                        {
                            F_Brightness_Unit = F_BrightnessCH1_Max;
                            if (checkBox1.Checked == true)
                            {
                                Comm.SetUnit4CH(0, 0, 0, 0);
                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                            }
                        }
                        break;
                    case 8:
                        F_Brightness_Unit = (byte)Math.Max(F_BrightnessCH1_Min, F_Brightness_Unit - Interval);
                        Comm.SetUnit8CH(F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit, F_Brightness_Unit);
                        Thread.Sleep(AutoSetBrightnessInterval);
                        if (F_Brightness_Unit <= F_BrightnessCH1_Min)
                        {
                            F_Brightness_Unit = F_BrightnessCH1_Max;
                            if (checkBox1.Checked == true)
                            {
                                Comm.SetUnit8CH(0, 0, 0, 0, 0, 0, 0, 0);
                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                            }
                        }
                        break;

                }

            }

        }

        void Random_Run_Mode(int channl)
        {
            int Value;
            Random random = new Random();
            if (strAutoSetBrightnessMethod == "Increase")
            {
                switch (channl)
                {
                    case 1:
                        if (F_BrightnessCH1 >= F_BrightnessCH1_Max)
                        {
                            F_BrightnessCH1 = F_BrightnessCH1_Min;
                        }
                        else
                        {
                            F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                            Comm.CH1_SetBrightness(F_BrightnessCH1);
                            Thread.Sleep(AutoSetBrightnessInterval);
                            if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                            {
                                if (checkBox1.Checked == true)
                                {
                                    Comm.CH1_SetBrightness(0);
                                    Thread.Sleep(AutoSetBrightnessInterval * 50);
                                }
                            }
                        }
                        break;
                    case 2:
                        Value = random.Next(1, channl+1);
                        F_BrightnessCH1 = F_BrightnessCH1_Min;
                        F_BrightnessCH2 = F_BrightnessCH2_Min;
                        switch (Value)
                        {
                            case 1:
                                if (checkBox2.Checked == true)
                                {
                                    for (byte i = 0; i <= F_BrightnessCH1_Max; i++)
                                    {
                                        F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                        Comm.CH1_SetBrightness(F_BrightnessCH1);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH1_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 2:
                                if (checkBox3.Checked == true)
                                {
                                    for (byte i = 0; i <= F_BrightnessCH2_Max; i++)
                                    {
                                        F_BrightnessCH2 = (byte)Math.Min(F_BrightnessCH2_Max, F_BrightnessCH2 + Interval);
                                        Comm.CH2_SetBrightness(F_BrightnessCH2);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH2 == F_BrightnessCH2_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH2_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                    case 4:
                        Value = random.Next(1, channl+1);
                        F_BrightnessCH1 = F_BrightnessCH1_Min;
                        F_BrightnessCH2 = F_BrightnessCH2_Min;
                        F_BrightnessCH3 = F_BrightnessCH3_Min;
                        F_BrightnessCH4 = F_BrightnessCH4_Min;
                        switch (Value)
                        {
                            case 1:
                                if (checkBox2.Checked == true)
                                {
                                    for (byte i = 0; i <= F_BrightnessCH1_Max; i++)
                                    {
                                        F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                        Comm.CH1_SetBrightness(F_BrightnessCH1);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH1_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 2:
                                if (checkBox3.Checked == true)
                                {
                                    for (byte i = 0; i <= F_BrightnessCH2_Max; i++)
                                    {
                                        F_BrightnessCH2 = (byte)Math.Min(F_BrightnessCH2_Max, F_BrightnessCH2 + Interval);
                                        Comm.CH2_SetBrightness(F_BrightnessCH2);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH2 == F_BrightnessCH2_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH2_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 3:
                                if (checkBox4.Checked == true)
                                {
                                    for (byte i = 0; i <= F_BrightnessCH3_Max; i++)
                                    {
                                        F_BrightnessCH3 = (byte)Math.Min(F_BrightnessCH3_Max, F_BrightnessCH3 + Interval);
                                        Comm.CH3_SetBrightness(F_BrightnessCH3);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH3 == F_BrightnessCH3_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH3_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 4:
                                if (checkBox5.Checked == true)
                                {
                                    for (byte i = 0; i <= F_BrightnessCH4_Max; i++)
                                    {
                                        F_BrightnessCH4 = (byte)Math.Min(F_BrightnessCH4_Max, F_BrightnessCH4 + Interval);
                                        Comm.CH4_SetBrightness(F_BrightnessCH4);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH4 == F_BrightnessCH4_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH4_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                    case 8:
                        Value = random.Next(1, channl+1);
                        F_BrightnessCH1 = F_BrightnessCH1_Min;
                        F_BrightnessCH2 = F_BrightnessCH2_Min;
                        F_BrightnessCH3 = F_BrightnessCH3_Min;
                        F_BrightnessCH4 = F_BrightnessCH4_Min;
                        F_BrightnessCH5 = F_BrightnessCH5_Min;
                        F_BrightnessCH6 = F_BrightnessCH6_Min;
                        F_BrightnessCH7 = F_BrightnessCH7_Min;
                        F_BrightnessCH8 = F_BrightnessCH8_Min;
                        switch (Value)
                        {
                            case 1:
                                if (checkBox2.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH1_Min; i <= F_BrightnessCH1_Max; i++)
                                    {
                                        F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                        Comm.CH1_SetBrightness(F_BrightnessCH1);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH1_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 2:
                                if (checkBox3.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH2_Min; i <= F_BrightnessCH2_Max; i++)
                                    {
                                        F_BrightnessCH2 = (byte)Math.Min(F_BrightnessCH2_Max, F_BrightnessCH2 + Interval);
                                        Comm.CH2_SetBrightness(F_BrightnessCH2);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH2 == F_BrightnessCH2_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH2_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 3:
                                if (checkBox4.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH3_Min; i <= F_BrightnessCH3_Max; i++)
                                    {
                                        F_BrightnessCH3 = (byte)Math.Min(F_BrightnessCH3_Max, F_BrightnessCH3 + Interval);
                                        Comm.CH3_SetBrightness(F_BrightnessCH3);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH3 == F_BrightnessCH3_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH3_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 4:
                                if (checkBox5.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH4_Min; i <= F_BrightnessCH4_Max; i++)
                                    {
                                        F_BrightnessCH4 = (byte)Math.Min(F_BrightnessCH4_Max, F_BrightnessCH4 + Interval);
                                        Comm.CH4_SetBrightness(F_BrightnessCH4);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH4 == F_BrightnessCH4_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH4_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 5:
                                if (checkBox6.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH5_Min; i <= F_BrightnessCH5_Max; i++)
                                    {
                                        F_BrightnessCH5 = (byte)Math.Min(F_BrightnessCH5_Max, F_BrightnessCH5 + Interval);
                                        Comm.CH5_SetBrightness(F_BrightnessCH5);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH5 == F_BrightnessCH5_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH5_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 6:
                                if (checkBox7.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH6_Min; i <= F_BrightnessCH6_Max; i++)
                                    {
                                        F_BrightnessCH6 = (byte)Math.Min(F_BrightnessCH6_Max, F_BrightnessCH6 + Interval);
                                        Comm.CH6_SetBrightness(F_BrightnessCH6);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH6 == F_BrightnessCH6_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH6_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 7:
                                if (checkBox8.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH7_Min; i <= F_BrightnessCH7_Max; i++)
                                    {
                                        F_BrightnessCH7 = (byte)Math.Min(F_BrightnessCH7_Max, F_BrightnessCH7 + Interval);
                                        Comm.CH7_SetBrightness(F_BrightnessCH7);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH7 == F_BrightnessCH7_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH7_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 8:
                                if (checkBox9.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH8_Min; i <= F_BrightnessCH8_Max; i++)
                                    {
                                        F_BrightnessCH8 = (byte)Math.Min(F_BrightnessCH8_Max, F_BrightnessCH8 + Interval);
                                        Comm.CH8_SetBrightness(F_BrightnessCH8);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH8 == F_BrightnessCH8_Max)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH8_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                        break;

                }
            }
            if (strAutoSetBrightnessMethod == "Decrease")
            {
                switch (channl)
                {
                    case 1:
                        if (checkBox2.Checked == true)
                        {
                            if (F_BrightnessCH1 < F_BrightnessCH1_Min)
                            {
                                F_BrightnessCH1 = F_BrightnessCH1_Max;
                            }
                            else
                            {
                                F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                Comm.CH1_SetBrightness(F_BrightnessCH1);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                {
                                    if (checkBox1.Checked == true)
                                    {
                                        Comm.CH1_SetBrightness(0);
                                        Thread.Sleep(AutoSetBrightnessInterval * 50);
                                    }
                                }
                            }
                        }
                        break;
                    case 2:
                        Value = random.Next(1, channl + 1);
                        F_BrightnessCH1 = F_BrightnessCH1_Max;
                        F_BrightnessCH2 = F_BrightnessCH2_Max;
                        switch (Value)
                        {
                            case 1:
                                if (checkBox2.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH1_Max; i > F_BrightnessCH1_Min; i--)
                                    {
                                        F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                        Comm.CH1_SetBrightness(F_BrightnessCH1);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH1_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 2:
                                if (checkBox3.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH1_Max; i > F_BrightnessCH2_Min; i--)
                                    {
                                        F_BrightnessCH2 = (byte)Math.Max(F_BrightnessCH2_Min, F_BrightnessCH2 - Interval);
                                        Comm.CH2_SetBrightness(F_BrightnessCH2);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH2 == F_BrightnessCH2_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH2_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                    case 4:
                        Value = random.Next(1, channl + 1);
                        F_BrightnessCH1 = F_BrightnessCH1_Max;
                        F_BrightnessCH2 = F_BrightnessCH2_Max;
                        F_BrightnessCH3 = F_BrightnessCH3_Max;
                        F_BrightnessCH4 = F_BrightnessCH4_Max;
                        switch (Value)
                        {
                            case 1:
                                if (checkBox2.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH1_Max; i > F_BrightnessCH1_Min; i--)
                                    {
                                        F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                        Comm.CH1_SetBrightness(F_BrightnessCH1);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH1_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 2:
                                if (checkBox3.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH2_Max; i > F_BrightnessCH2_Min; i--)
                                    {
                                        F_BrightnessCH2 = (byte)Math.Max(F_BrightnessCH2_Max, F_BrightnessCH2 - Interval);
                                        Comm.CH2_SetBrightness(F_BrightnessCH2);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH2 == F_BrightnessCH2_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH2_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 3:
                                if (checkBox4.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH3_Max; i > F_BrightnessCH3_Min; i--)
                                    {
                                        F_BrightnessCH3 = (byte)Math.Max(F_BrightnessCH3_Max, F_BrightnessCH3 - Interval);
                                        Comm.CH3_SetBrightness(F_BrightnessCH3);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH3 == F_BrightnessCH3_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH3_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 4:
                                if (checkBox5.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH4_Max; i > F_BrightnessCH4_Max; i--)
                                    {
                                        F_BrightnessCH4 = (byte)Math.Max(F_BrightnessCH4_Max, F_BrightnessCH4 - Interval);
                                        Comm.CH4_SetBrightness(F_BrightnessCH4);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH4 == F_BrightnessCH4_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH4_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                    case 8:
                        Value = random.Next(1, channl + 1);
                        F_BrightnessCH1 = F_BrightnessCH1_Max;
                        F_BrightnessCH2 = F_BrightnessCH2_Max;
                        F_BrightnessCH3 = F_BrightnessCH3_Max;
                        F_BrightnessCH4 = F_BrightnessCH4_Max;
                        F_BrightnessCH5 = F_BrightnessCH5_Max;
                        F_BrightnessCH6 = F_BrightnessCH6_Max;
                        F_BrightnessCH7 = F_BrightnessCH7_Max;
                        F_BrightnessCH8 = F_BrightnessCH8_Max;
                        switch (Value)
                        {
                            case 1:
                                if (checkBox2.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH1_Max; i > F_BrightnessCH1_Min; i--)
                                    {
                                        F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                        Comm.CH1_SetBrightness(F_BrightnessCH1);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH1_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 2:
                                if (checkBox3.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH2_Max; i > F_BrightnessCH2_Min; i--)
                                    {
                                        F_BrightnessCH2 = (byte)Math.Max(F_BrightnessCH2_Max, F_BrightnessCH2 - Interval);
                                        Comm.CH2_SetBrightness(F_BrightnessCH2);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH2 == F_BrightnessCH2_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH2_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 3:
                                if (checkBox4.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH3_Max; i > F_BrightnessCH3_Min; i--)
                                    {
                                        F_BrightnessCH3 = (byte)Math.Max(F_BrightnessCH3_Max, F_BrightnessCH3 - Interval);
                                        Comm.CH3_SetBrightness(F_BrightnessCH3);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH3 == F_BrightnessCH3_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH3_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 4:
                                if (checkBox5.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH4_Max; i > F_BrightnessCH4_Max; i--)
                                    {
                                        F_BrightnessCH4 = (byte)Math.Max(F_BrightnessCH4_Max, F_BrightnessCH4 - Interval);
                                        Comm.CH4_SetBrightness(F_BrightnessCH4);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH4 == F_BrightnessCH4_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH4_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 5:
                                if (checkBox6.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH5_Max; i > F_BrightnessCH5_Min; i--)
                                    {
                                        F_BrightnessCH5 = (byte)Math.Max(F_BrightnessCH5_Min, F_BrightnessCH5 - Interval);
                                        Comm.CH5_SetBrightness(F_BrightnessCH5);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH5 == F_BrightnessCH5_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH5_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 6:
                                if (checkBox7.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH6_Max; i > F_BrightnessCH6_Min; i--)
                                    {
                                        F_BrightnessCH6 = (byte)Math.Max(F_BrightnessCH6_Max, F_BrightnessCH6 - Interval);
                                        Comm.CH6_SetBrightness(F_BrightnessCH6);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH6 == F_BrightnessCH6_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH6_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 7:
                                if (checkBox8.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH7_Max; i > F_BrightnessCH7_Min; i--)
                                    {
                                        F_BrightnessCH7 = (byte)Math.Max(F_BrightnessCH7_Max, F_BrightnessCH7 - Interval);
                                        Comm.CH7_SetBrightness(F_BrightnessCH7);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH7 == F_BrightnessCH7_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH7_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                            case 8:
                                if (checkBox9.Checked == true)
                                {
                                    for (byte i = F_BrightnessCH8_Max; i > F_BrightnessCH8_Max; i--)
                                    {
                                        F_BrightnessCH8 = (byte)Math.Max(F_BrightnessCH8_Max, F_BrightnessCH8 - Interval);
                                        Comm.CH8_SetBrightness(F_BrightnessCH8);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                        if (F_BrightnessCH8 == F_BrightnessCH8_Min)
                                        {
                                            if (checkBox1.Checked == true)
                                            {
                                                Comm.CH8_SetBrightness(0);
                                                Thread.Sleep(AutoSetBrightnessInterval * 50);
                                            }
                                        }
                                    }
                                }
                                break;
                        }
                        break;
                }
            }
        }

        bool breathe_circle = true;

        void Breathe_Run_Mode(int channl)
        {
            bool j = true;
            int x;
            if(strAutoSetBrightnessMethod == "Increase")
            {
                switch (channl)
                {
                    case 1:
                        breathe_circle = true;
                        j = true;
                        while (breathe_circle)
                        {
                            if (j)
                            {
                                F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                Comm.CH1_SetBrightness(F_BrightnessCH1);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH1 == F_BrightnessCH1_Max) {
                                   j = false;
                                }
                            }
                            else
                            {
                                F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                Comm.CH1_SetBrightness(F_BrightnessCH1);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if(F_BrightnessCH1 == F_BrightnessCH1_Min)
                                {
                                    breathe_circle = false;
                                }
                            }
                        }
                        break;
                    case 2:
                        breathe_circle = true;
                        j = true;
                        x = 1;            
                        while (breathe_circle)
                        {
                            if (x == 1)
                            {
                                if ((j== true)&&(x==1))
                                {
                                    F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                    if (F_BrightnessCH1 == 0)
                                    {
                                        F_BrightnessCH2 = 1;
                                        Comm.CH2_SetBrightness(F_BrightnessCH2);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 2)
                            {
                                if (j)
                                {
                                    F_BrightnessCH2 = (byte)Math.Min(F_BrightnessCH2_Max, F_BrightnessCH2 + Interval);
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH2 == F_BrightnessCH2_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH2 = (byte)Math.Max(F_BrightnessCH2_Min, F_BrightnessCH2 - Interval);
                                    if (F_BrightnessCH2 == 0)
                                    {
                                        F_BrightnessCH1 = 1;
                                        Comm.CH1_SetBrightness(F_BrightnessCH1);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH2 == F_BrightnessCH2_Min)
                                    {
                                        breathe_circle = false;
                                    }
                                }
                            }
                        }                      
                        break;
                    case 4:
                        breathe_circle = true;
                        j = true;
                        x = 1;
                        while (breathe_circle)
                        {
                            if (x == 1)
                            {
                                if ((j == true) && (x == 1))
                                {
                                    F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                    if (F_BrightnessCH1 == 0)
                                    {
                                        F_BrightnessCH2 = 1;
                                        Comm.CH2_SetBrightness(F_BrightnessCH2);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 2)
                            {
                                if ((j == true) && (x == 2))
                                {
                                    F_BrightnessCH2 = (byte)Math.Min(F_BrightnessCH2_Max, F_BrightnessCH2 + Interval);
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH2 == F_BrightnessCH2_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH2 = (byte)Math.Max(F_BrightnessCH2_Min, F_BrightnessCH2 - Interval);
                                    if (F_BrightnessCH2 == 0)
                                    {
                                        F_BrightnessCH3 = 1;
                                        Comm.CH3_SetBrightness(F_BrightnessCH3);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH2 == F_BrightnessCH2_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 3)
                            {
                                if ((j == true) && (x == 3))
                                {
                                    F_BrightnessCH3 = (byte)Math.Min(F_BrightnessCH3_Max, F_BrightnessCH3 + Interval);
                                    Comm.CH3_SetBrightness(F_BrightnessCH3);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH3 == F_BrightnessCH3_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH3 = (byte)Math.Max(F_BrightnessCH3_Min, F_BrightnessCH3 - Interval);
                                    if (F_BrightnessCH3 == 0)
                                    {
                                        F_BrightnessCH4 = 1;
                                        Comm.CH4_SetBrightness(F_BrightnessCH4);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH3_SetBrightness(F_BrightnessCH3);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH3 == F_BrightnessCH3_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 4)
                            {
                                if ((j == true) && (x == 4))
                                {
                                    F_BrightnessCH4 = (byte)Math.Min(F_BrightnessCH4_Max, F_BrightnessCH4 + Interval);
                                    Comm.CH4_SetBrightness(F_BrightnessCH4);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH4 == F_BrightnessCH4_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH4 = (byte)Math.Max(F_BrightnessCH4_Min, F_BrightnessCH4 - Interval);
                                    if (F_BrightnessCH4 == 0)
                                    {
                                        F_BrightnessCH1 = 1;
                                        Comm.CH1_SetBrightness(F_BrightnessCH1);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH4_SetBrightness(F_BrightnessCH4);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH4 == F_BrightnessCH4_Min)
                                    {
                                        x++;
                                        breathe_circle = false;
                                    }
                                }
                            }
                        }
                        break;
                    case 8:
                        breathe_circle = true;
                        j = true;
                        x = 1;
                        while (breathe_circle)
                        {
                            if (x == 1)
                            {
                                if ((j == true) && (x == 1))
                                {
                                    F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                    if (F_BrightnessCH1 == 0)
                                    {
                                        F_BrightnessCH2 = 1;
                                        Comm.CH2_SetBrightness(F_BrightnessCH2);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 2)
                            {
                                if ((j == true) && (x == 2))
                                {
                                    F_BrightnessCH2 = (byte)Math.Min(F_BrightnessCH2_Max, F_BrightnessCH2 + Interval);
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH2 == F_BrightnessCH2_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH2 = (byte)Math.Max(F_BrightnessCH2_Min, F_BrightnessCH2 - Interval);
                                    if (F_BrightnessCH2 == 0)
                                    {
                                        F_BrightnessCH3 = 1;
                                        Comm.CH3_SetBrightness(F_BrightnessCH3);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH2 == F_BrightnessCH2_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 3)
                            {
                                if ((j == true) && (x == 3))
                                {
                                    F_BrightnessCH3 = (byte)Math.Min(F_BrightnessCH3_Max, F_BrightnessCH3 + Interval);
                                    Comm.CH3_SetBrightness(F_BrightnessCH3);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH3 == F_BrightnessCH3_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH3 = (byte)Math.Max(F_BrightnessCH3_Min, F_BrightnessCH3 - Interval);
                                    if (F_BrightnessCH3 == 0)
                                    {
                                        F_BrightnessCH4 = 1;
                                        Comm.CH4_SetBrightness(F_BrightnessCH4);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH3_SetBrightness(F_BrightnessCH3);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH3 == F_BrightnessCH3_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 4)
                            {
                                if ((j == true) && (x == 4))
                                {
                                    F_BrightnessCH4 = (byte)Math.Min(F_BrightnessCH4_Max, F_BrightnessCH4 + Interval);
                                    Comm.CH4_SetBrightness(F_BrightnessCH4);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH4 == F_BrightnessCH4_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH4 = (byte)Math.Max(F_BrightnessCH4_Min, F_BrightnessCH4 - Interval);
                                    if (F_BrightnessCH4 == 0)
                                    {
                                        F_BrightnessCH5 = 1;
                                        Comm.CH5_SetBrightness(F_BrightnessCH5);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH4_SetBrightness(F_BrightnessCH4);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH4 == F_BrightnessCH4_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 5)
                            {
                                if ((j == true) && (x == 5))
                                {
                                    F_BrightnessCH5 = (byte)Math.Min(F_BrightnessCH5_Max, F_BrightnessCH5 + Interval);
                                    Comm.CH5_SetBrightness(F_BrightnessCH5);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH5 == F_BrightnessCH5_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH5 = (byte)Math.Max(F_BrightnessCH5_Min, F_BrightnessCH5 - Interval);
                                    if (F_BrightnessCH5 == 0)
                                    {
                                        F_BrightnessCH6 = 1;
                                        Comm.CH6_SetBrightness(F_BrightnessCH6);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH5_SetBrightness(F_BrightnessCH5);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH5 == F_BrightnessCH5_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 6)
                            {
                                if ((j == true) && (x == 6))
                                {
                                    F_BrightnessCH6 = (byte)Math.Min(F_BrightnessCH6_Max, F_BrightnessCH6 + Interval);
                                    Comm.CH6_SetBrightness(F_BrightnessCH6);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH6 == F_BrightnessCH6_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH6 = (byte)Math.Max(F_BrightnessCH6_Min, F_BrightnessCH6 - Interval);
                                    if (F_BrightnessCH6 == 0)
                                    {
                                        F_BrightnessCH7 = 1;
                                        Comm.CH7_SetBrightness(F_BrightnessCH7);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH6_SetBrightness(F_BrightnessCH6);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH6 == F_BrightnessCH6_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 7)
                            {
                                if ((j == true) && (x == 7))
                                {
                                    F_BrightnessCH7 = (byte)Math.Min(F_BrightnessCH7_Max, F_BrightnessCH7 + Interval);
                                    Comm.CH7_SetBrightness(F_BrightnessCH7);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH7 == F_BrightnessCH7_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH7 = (byte)Math.Max(F_BrightnessCH7_Min, F_BrightnessCH7 - Interval);
                                    if (F_BrightnessCH7 == 0)
                                    {
                                        F_BrightnessCH8 = 1;
                                        Comm.CH8_SetBrightness(F_BrightnessCH8);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH7_SetBrightness(F_BrightnessCH7);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH7 == F_BrightnessCH7_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 8)
                            {
                                if ((j == true) && (x == 8))
                                {
                                    F_BrightnessCH8 = (byte)Math.Min(F_BrightnessCH8_Max, F_BrightnessCH8 + Interval);
                                    Comm.CH8_SetBrightness(F_BrightnessCH8);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH8 == F_BrightnessCH8_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH8 = (byte)Math.Max(F_BrightnessCH8_Min, F_BrightnessCH8 - Interval);
                                    if (F_BrightnessCH8 == 0)
                                    {
                                        F_BrightnessCH1 = 1;
                                        Comm.CH1_SetBrightness(F_BrightnessCH1);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH8_SetBrightness(F_BrightnessCH8);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH8 == F_BrightnessCH8_Min)
                                    {
                                        x++;
                                        breathe_circle = false;
                                    }
                                }
                            }
                        }
                        break;
                }
            }
            if (strAutoSetBrightnessMethod == "Decrease")
            {
                switch (channl)
                {
                    case 1:
                        breathe_circle = true;
                        j = true;
                        while (breathe_circle)
                        {
                            if (j)
                            {
                                F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                Comm.CH1_SetBrightness(F_BrightnessCH1);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                {
                                    j = false;
                                }
                            }
                            else
                            {
                                F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                Comm.CH1_SetBrightness(F_BrightnessCH1);
                                Thread.Sleep(AutoSetBrightnessInterval);
                                if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                {
                                    breathe_circle = false;
                                }
                            }
                        }
                        break;
                    case 2:
                        breathe_circle = true;
                        j = true;
                        x = 1;
                        while (breathe_circle)
                        {
                            if (x == 1)
                            {
                                if ((j == true) && (x == 1))
                                {
                                    F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                    if (F_BrightnessCH1 == 0)
                                    {
                                        F_BrightnessCH2 = 1;
                                        Comm.CH2_SetBrightness(F_BrightnessCH2);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 2)
                            {
                                if (j)
                                {
                                    F_BrightnessCH2 = (byte)Math.Min(F_BrightnessCH2_Max, F_BrightnessCH2 + Interval);
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH2 == F_BrightnessCH2_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH2 = (byte)Math.Max(F_BrightnessCH2_Min, F_BrightnessCH2 - Interval);
                                    if (F_BrightnessCH2 == 0)
                                    {
                                        F_BrightnessCH1 = 1;
                                        Comm.CH1_SetBrightness(F_BrightnessCH1);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH2 == F_BrightnessCH2_Min)
                                    {
                                        breathe_circle = false;
                                    }
                                }
                            }
                        }
                        break;
                    case 4:
                        breathe_circle = true;
                        j = true;
                        x = 1;
                        while (breathe_circle)
                        {
                            if (x == 1)
                            {
                                if ((j == true) && (x == 1))
                                {
                                    F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                    if (F_BrightnessCH1 == 0)
                                    {
                                        F_BrightnessCH2 = 1;
                                        Comm.CH2_SetBrightness(F_BrightnessCH2);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 2)
                            {
                                if ((j == true) && (x == 2))
                                {
                                    F_BrightnessCH2 = (byte)Math.Min(F_BrightnessCH2_Max, F_BrightnessCH2 + Interval);
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH2 == F_BrightnessCH2_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH2 = (byte)Math.Max(F_BrightnessCH2_Min, F_BrightnessCH2 - Interval);
                                    if (F_BrightnessCH2 == 0)
                                    {
                                        F_BrightnessCH3 = 1;
                                        Comm.CH3_SetBrightness(F_BrightnessCH3);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH2 == F_BrightnessCH2_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 3)
                            {
                                if ((j == true) && (x == 3))
                                {
                                    F_BrightnessCH3 = (byte)Math.Min(F_BrightnessCH3_Max, F_BrightnessCH3 + Interval);
                                    Comm.CH3_SetBrightness(F_BrightnessCH3);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH3 == F_BrightnessCH3_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH3 = (byte)Math.Max(F_BrightnessCH3_Min, F_BrightnessCH3 - Interval);
                                    if (F_BrightnessCH3 == 0)
                                    {
                                        F_BrightnessCH4 = 1;
                                        Comm.CH4_SetBrightness(F_BrightnessCH4);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH3_SetBrightness(F_BrightnessCH3);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH3 == F_BrightnessCH3_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 4)
                            {
                                if ((j == true) && (x == 4))
                                {
                                    F_BrightnessCH4 = (byte)Math.Min(F_BrightnessCH4_Max, F_BrightnessCH4 + Interval);
                                    Comm.CH4_SetBrightness(F_BrightnessCH4);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH4 == F_BrightnessCH4_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH4 = (byte)Math.Max(F_BrightnessCH4_Min, F_BrightnessCH4 - Interval);
                                    if (F_BrightnessCH4 == 0)
                                    {
                                        F_BrightnessCH1 = 1;
                                        Comm.CH1_SetBrightness(F_BrightnessCH1);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH4_SetBrightness(F_BrightnessCH4);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH4 == F_BrightnessCH4_Min)
                                    {
                                        x++;
                                        breathe_circle = false;
                                    }
                                }
                            }
                        }
                        break;
                    case 8:
                        breathe_circle = true;
                        j = true;
                        x = 1;
                        while (breathe_circle)
                        {
                            if (x == 1)
                            {
                                if ((j == true) && (x == 1))
                                {
                                    F_BrightnessCH1 = (byte)Math.Min(F_BrightnessCH1_Max, F_BrightnessCH1 + Interval);
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH1 == F_BrightnessCH1_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH1 = (byte)Math.Max(F_BrightnessCH1_Min, F_BrightnessCH1 - Interval);
                                    if (F_BrightnessCH1 == 0)
                                    {
                                        F_BrightnessCH2 = 1;
                                        Comm.CH2_SetBrightness(F_BrightnessCH2);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH1_SetBrightness(F_BrightnessCH1);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH1 == F_BrightnessCH1_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 2)
                            {
                                if ((j == true) && (x == 2))
                                {
                                    F_BrightnessCH2 = (byte)Math.Min(F_BrightnessCH2_Max, F_BrightnessCH2 + Interval);
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH2 == F_BrightnessCH2_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH2 = (byte)Math.Max(F_BrightnessCH2_Min, F_BrightnessCH2 - Interval);
                                    if (F_BrightnessCH2 == 0)
                                    {
                                        F_BrightnessCH3 = 1;
                                        Comm.CH3_SetBrightness(F_BrightnessCH3);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH2_SetBrightness(F_BrightnessCH2);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH2 == F_BrightnessCH2_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 3)
                            {
                                if ((j == true) && (x == 3))
                                {
                                    F_BrightnessCH3 = (byte)Math.Min(F_BrightnessCH3_Max, F_BrightnessCH3 + Interval);
                                    Comm.CH3_SetBrightness(F_BrightnessCH3);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH3 == F_BrightnessCH3_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH3 = (byte)Math.Max(F_BrightnessCH3_Min, F_BrightnessCH3 - Interval);
                                    if (F_BrightnessCH3 == 0)
                                    {
                                        F_BrightnessCH4 = 1;
                                        Comm.CH4_SetBrightness(F_BrightnessCH4);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH3_SetBrightness(F_BrightnessCH3);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH3 == F_BrightnessCH3_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 4)
                            {
                                if ((j == true) && (x == 4))
                                {
                                    F_BrightnessCH4 = (byte)Math.Min(F_BrightnessCH4_Max, F_BrightnessCH4 + Interval);
                                    Comm.CH4_SetBrightness(F_BrightnessCH4);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH4 == F_BrightnessCH4_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH4 = (byte)Math.Max(F_BrightnessCH4_Min, F_BrightnessCH4 - Interval);
                                    if (F_BrightnessCH4 == 0)
                                    {
                                        F_BrightnessCH5 = 1;
                                        Comm.CH5_SetBrightness(F_BrightnessCH5);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH4_SetBrightness(F_BrightnessCH4);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH4 == F_BrightnessCH4_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 5)
                            {
                                if ((j == true) && (x == 5))
                                {
                                    F_BrightnessCH5 = (byte)Math.Min(F_BrightnessCH5_Max, F_BrightnessCH5 + Interval);
                                    Comm.CH5_SetBrightness(F_BrightnessCH5);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH5 == F_BrightnessCH5_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH5 = (byte)Math.Max(F_BrightnessCH5_Min, F_BrightnessCH5 - Interval);
                                    if (F_BrightnessCH5 == 0)
                                    {
                                        F_BrightnessCH6 = 1;
                                        Comm.CH6_SetBrightness(F_BrightnessCH6);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH5_SetBrightness(F_BrightnessCH5);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH5 == F_BrightnessCH5_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 6)
                            {
                                if ((j == true) && (x == 6))
                                {
                                    F_BrightnessCH6 = (byte)Math.Min(F_BrightnessCH6_Max, F_BrightnessCH6 + Interval);
                                    Comm.CH6_SetBrightness(F_BrightnessCH6);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH6 == F_BrightnessCH6_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH6 = (byte)Math.Max(F_BrightnessCH6_Min, F_BrightnessCH6 - Interval);
                                    if (F_BrightnessCH6 == 0)
                                    {
                                        F_BrightnessCH7 = 1;
                                        Comm.CH7_SetBrightness(F_BrightnessCH7);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH6_SetBrightness(F_BrightnessCH6);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH6 == F_BrightnessCH6_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 7)
                            {
                                if ((j == true) && (x == 7))
                                {
                                    F_BrightnessCH7 = (byte)Math.Min(F_BrightnessCH7_Max, F_BrightnessCH7 + Interval);
                                    Comm.CH7_SetBrightness(F_BrightnessCH7);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH7 == F_BrightnessCH7_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH7 = (byte)Math.Max(F_BrightnessCH7_Min, F_BrightnessCH7 - Interval);
                                    if (F_BrightnessCH8 == 0)
                                    {
                                        F_BrightnessCH8 = 1;
                                        Comm.CH8_SetBrightness(F_BrightnessCH8);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH7_SetBrightness(F_BrightnessCH7);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH7 == F_BrightnessCH7_Min)
                                    {
                                        x++;
                                        j = true;
                                    }
                                }
                            }
                            if (x == 8)
                            {
                                if ((j == true) && (x == 8))
                                {
                                    F_BrightnessCH8 = (byte)Math.Min(F_BrightnessCH8_Max, F_BrightnessCH8 + Interval);
                                    Comm.CH8_SetBrightness(F_BrightnessCH8);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH8 == F_BrightnessCH8_Max)
                                    {
                                        j = false;
                                    }
                                }
                                else
                                {
                                    F_BrightnessCH8 = (byte)Math.Max(F_BrightnessCH8_Min, F_BrightnessCH8 - Interval);
                                    if (F_BrightnessCH8 == 0)
                                    {
                                        F_BrightnessCH1 = 1;
                                        Comm.CH1_SetBrightness(F_BrightnessCH1);
                                        Thread.Sleep(AutoSetBrightnessInterval);
                                    }
                                    Comm.CH8_SetBrightness(F_BrightnessCH8);
                                    Thread.Sleep(AutoSetBrightnessInterval);
                                    if (F_BrightnessCH8 == F_BrightnessCH8_Min)
                                    {
                                        x++;
                                        breathe_circle = false;
                                    }
                                }
                            }
                        }
                        break;
                }

            }

        }
        void Success_frequency(ulong nubmer)
        {
            if (this.InvokeRequired)
            {
                CountSuccess count = new CountSuccess(Success_frequency);
                this.Invoke(count, new Object[] { nubmer });
            }
            else
            {
                label28.Text = nubmer.ToString();
            }
        }
        void Fail_frequency(ulong nubmer)
        {
            if (this.InvokeRequired)
            {
                CountSuccess count1 = new CountSuccess(Fail_frequency);
                this.Invoke(count1, new Object[] { nubmer });
            }
            else
            {
                label29.Text = nubmer.ToString();
            }
        }
        private void Start_Rinng()
        {
            int i, count=0;

            do
            {
                if (Controller_cycle)
                {
                    i = CHnubmer();
                    run(i);
                    if (Comm.Confirmation() == true)
                    {
                        Success++;
                        Success_frequency(Success);
                    }
                    else{
                        Fail++;
                        Fail_frequency(Fail);
                    }
                }
                if (Controller_random)
                {
                    i = CHnubmer();
                    Random_Run_Mode(i);
                    if (Comm.Confirmation() == true)
                    {
                        Success++;
                        Success_frequency(Success);
                    }
                    else
                    {
                        Fail++;
                        Fail_frequency(Fail);
                    }
                }
                if (Controller_Unite)
                {
                    i = CHnubmer();
                    Unite_Run_Mode(i);
                    if (Comm.Confirmation() == true)
                    {
                        Success++;
                        Success_frequency(Success);
                    }
                    else
                    {
                        Fail++;
                        Fail_frequency(Fail);
                    }
                }
                if (Controller_Breathe)
                {
                    i = CHnubmer();
                    Breathe_Run_Mode(i);
                   if (Comm.Confirmation() == true)
                    {
                        Success++;
                        Success_frequency(Success);
                    }
                    else
                    {
                        Fail++;
                        Fail_frequency(Fail);
                    }
                }
                count++;
                if(count == UartQuantity)
                {
                    Controller_Start = false;
                    button5.BackgroundImage = pic.GetObject("Start") as Image;
                }
            } while (Controller_Start) ;
        }

        void Implement()
        {
            AutoSetBrightnessInterval = Convert.ToInt32(numericUpDown17.Value);
            UartQuantity = Convert.ToInt32(numericUpDown1.Value);
            Interval = (byte)Br_interval.Value;
            comboBox1.Items.Add("Increase");
            comboBox1.Items.Add("Decrease");
            comboBox1.Text = "Increase";
            F_BrightnessCH1 = F_BrightnessCH1_Min;
            F_BrightnessCH2 = F_BrightnessCH2_Min;
            F_BrightnessCH3 = F_BrightnessCH3_Min;
            F_BrightnessCH4 = F_BrightnessCH4_Min;
            F_BrightnessCH5 = F_BrightnessCH5_Min;
            F_BrightnessCH6 = F_BrightnessCH6_Min;
            F_BrightnessCH7 = F_BrightnessCH7_Min;
            F_BrightnessCH8 = F_BrightnessCH8_Min;
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }
        private void button4_Click(object sender, EventArgs e)
        {
            Controller_Unite =! Controller_Unite;
            if (Controller_Unite)
            {
                button4.BackgroundImage = pic.GetObject("Unite_on") as Image;
                button2.Enabled = false;
                button3.Enabled = false;
                brearhe_Button.Enabled = false;
                groupBox4.Enabled = false;
                groupBox5.Enabled = false;
                groupBox6.Enabled = false;
                groupBox7.Enabled = false;
                groupBox8.Enabled = false;
                groupBox9.Enabled = false;
                groupBox10.Enabled = false;
            }
            else
            {
                button4.BackgroundImage = pic.GetObject("Unite") as Image; ;
                button2.Enabled = true;
                button3.Enabled = true;
                brearhe_Button.Enabled = true;
                groupBox4.Enabled = true;
                groupBox5.Enabled = true;
                groupBox6.Enabled = true;
                groupBox7.Enabled = true;
                groupBox8.Enabled = true;
                groupBox9.Enabled = true;
                groupBox10.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Controller_cycle =! Controller_cycle;
            if (Controller_cycle)
            {
                button2.BackgroundImage = pic.GetObject("cycle_on") as Image;
                button4.Enabled = false;
                button3.Enabled = false;
                brearhe_Button.Enabled = false;
            }
            else
            {
                button2.BackgroundImage = pic.GetObject("cycle") as Image;
                button4.Enabled = true;
                button3.Enabled = true;
                brearhe_Button.Enabled = true;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Controller_random =! Controller_random;
            if (Controller_random)
            {
                button3.BackgroundImage = pic.GetObject("random_on") as Image;
                button2.Enabled = false;
                button4.Enabled = false;
                brearhe_Button.Enabled = false;
            }
            else
            {
                button3.BackgroundImage = pic.GetObject("random") as Image;
                button2.Enabled = true;
                button4.Enabled = true;
                brearhe_Button.Enabled = true;
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {

            Controller_Start = !Controller_Start;
            if (Controller_Start)
            {
                Success = 0;
                Fail = 0;
                Success_frequency(Success);
                Fail_frequency(Fail);
                button5.BackgroundImage = pic.GetObject("Stop") as Image;
                ControllerStatusThread = new Thread(new ThreadStart(Start_Rinng));
                ControllerStatusThread.IsBackground = true;
                ControllerStatusThread.Start();
                while (!ControllerStatusThread.IsAlive) ;
            }
            else
            {
                button5.BackgroundImage = pic.GetObject("Start") as Image;
                breathe_circle = false;
            }

        }

        private void CH1_Min_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH1_Min = (byte)CH1_Min.Value;
        }

        private void CH1_Max_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH1_Max =(byte)CH1_Max.Value;
        }

        private void CH2_Min_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH2_Min = (byte)CH2_Min.Value;
        }

        private void CH2_Max_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH2_Max = (byte)CH2_Max.Value;
        }

        private void CH3_Min_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH3_Min = (byte)CH3_Min.Value;
        }

        private void CH3_Max_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH3_Max = (byte)CH3_Max.Value;
        }

        private void CH4_Min_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH4_Min = (byte)CH4_Min.Value;
        }

        private void CH4_Max_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH4_Max = (byte)CH4_Max.Value;
        }

        private void CH5_Min_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH5_Min = (byte)CH5_Min.Value;
        }

        private void CH5_Max_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH5_Max = (byte)CH5_Max.Value;
        }

        private void CH6_Min_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH6_Min =(byte)CH6_Min.Value;
        }

        private void CH6_Max_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH6_Max = (byte)CH6_Max.Value;
        }

        private void CH7_Min_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH7_Min = (byte)CH7_Min.Value;
        }

        private void CH7_Max_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH7_Max = (byte)CH7_Max.Value;
        }

        private void CH8_Min_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH8_Min = (byte)CH8_Min.Value;
        }

        private void Br_interval_ValueChanged(object sender, EventArgs e)
        {
            Interval = (byte)Br_interval.Value;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            strAutoSetBrightnessMethod = comboBox1.Text;
            if (strAutoSetBrightnessMethod == " Increase")
            {
                F_BrightnessCH1 = F_BrightnessCH1_Min;
                F_BrightnessCH2 = F_BrightnessCH2_Min;
                F_BrightnessCH3 = F_BrightnessCH3_Min;
                F_BrightnessCH4 = F_BrightnessCH4_Min;
                F_BrightnessCH5 = F_BrightnessCH5_Min;
                F_BrightnessCH6 = F_BrightnessCH6_Min;
                F_BrightnessCH7 = F_BrightnessCH7_Min;
                F_BrightnessCH8 = F_BrightnessCH8_Min;
            }else{
                F_BrightnessCH1 = F_BrightnessCH1_Max;
                F_BrightnessCH2 = F_BrightnessCH2_Max;
                F_BrightnessCH3 = F_BrightnessCH3_Max;
                F_BrightnessCH4 = F_BrightnessCH4_Max;
                F_BrightnessCH5 = F_BrightnessCH5_Max;
                F_BrightnessCH6 = F_BrightnessCH6_Max;
                F_BrightnessCH7 = F_BrightnessCH7_Max;
                F_BrightnessCH8 = F_BrightnessCH8_Max;
            }
        }

        private void label21_Click(object sender, EventArgs e)
        {

        }

        private void label31_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            UartQuantity = Convert.ToInt32(numericUpDown1.Value);
        }

        private void numericUpDown17_ValueChanged(object sender, EventArgs e)
        {
            AutoSetBrightnessInterval = Convert.ToInt32(numericUpDown17.Value);
        }

        private void brearhe_Button_Click(object sender, EventArgs e)
        {
            Controller_Breathe = !Controller_Breathe;
            if (Controller_Breathe)
            {
                brearhe_Button.BackgroundImage = pic.GetObject("breathe_on") as Image;
                button2.Enabled = false;
                button4.Enabled = false;
                button3.Enabled = false;
                comboBox1.Enabled = false;
            }
            else
            {
                brearhe_Button.BackgroundImage = pic.GetObject("breathe") as Image;
                button2.Enabled = true;
                button4.Enabled = true;
                button3.Enabled = true;
                comboBox1.Enabled = true;
            }
        }

        private void cmbPortName_SelectedIndexChanged(object sender, EventArgs e)
        {
             ComProtNmber = cmbPortName.Text;
        }

        private void CH8_Max_ValueChanged(object sender, EventArgs e)
        {
            F_BrightnessCH8_Max = (byte)CH8_Max.Value;
        }
    }
}
