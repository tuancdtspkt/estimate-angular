using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using MatrixLibrary;
namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        //********************************************
        //
        // variables for COMPORT
        //
        //********************************************
        public SerialPort myComPort = new SerialPort();
        static string myComPortName;
        public string ex_myComPortName
        {
            get { return myComPortName; }
        }
         public int baudrate;
        public string parity;
        public byte bits;
        public string stopBit;
        string[] portname = null;
        int numPort = 0;
        public byte[] RS232_Tx_Buf = new byte[32];
        public byte[] RS232_Rx_Buf = new byte[49];
        static int flag=0;
        int temp,count;
        static int ahrs;
        //
        //
        int first_flag = 0;
        string s;
        static AcquisitionThread  DATA_PROCCESSING = new AcquisitionThread();
      
        //
        //variables for DATA PROCCESSING
        //
        const  byte    MASTER_ID  = 0x00;
        const  byte    SLAVE_ID =  0x01;
        const  byte    MOTOR_ID  = 0x02;
        const  byte    RF_ID    =  0x03;
        const  byte    IMU_ID    = 0x04;
        const  byte    ENABLE   =  0x05;
        const  byte    DISABLE   =  0x06;
        const  byte    EN_ID     = 0x07;
        const  byte    EN_RESET  = 0x08;
        const  byte    SET_SAMPLE_PREIOD = 0x09;
        const  byte    FINISH = 0x10;
        const  UInt16  Sample_Period  =  10000;// ms
        const  byte    Out_Buf_Length  = 5 ;
       // const  byte    In_Buf_Length  = 5 ;
        const byte START   = 0x10;
        const byte STOP    = 0x11;
        const byte ALL  =    0x12;
        private void Form2_Load(object sender, EventArgs e)
        {
           int i;
           //
           // initializing for COM PORT
           //
           portname = SerialPort.GetPortNames();
           // cur_name = SerialPort.GetPortNames();
           numPort = portname.Length;
           Array.Sort(portname);
           for (i = 0; i < portname.Length; i++)
               combCom.Items.Add(portname[i]);
          //
          //other items
          //
          // combMode.Items.AddRange(new object[] { "MODE1 ( X-Y-Z)", "MODE2 (Z)", "MODE3 ( X-Y-Z Full)" });
         
           Setting_Status2.Text = " Device not found";
           testtimer.Enabled =false;
           //
           //
           //

        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            
            int count;
            //Setting_Status2.Text = "CONNECTING....";
            if (combCom.Text == "")
            {
                MessageBox.Show("SELECT PORT NAME", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk);
                return;

            }
                myComPortName = combCom.Text;
                if (cmdConnect.Text == "CONNECT")
                {
                    Setting_Status2.Text = "CONNECTING....";
                    cmdConnect.Text = "DISCONNECT";
                    myComPort = new SerialPort(myComPortName, 115200, Parity.None, 8, StopBits.One);
                    myComPort.ReceivedBytesThreshold = 1;
                    proConnect.Value = 1;
                    proConnect.Minimum = 1;
                    proConnect.Value = proConnect.Minimum;
                    myComPort.ReadTimeout = 1;
                    proConnect.PerformStep();
                   
                    myComPort.Close();
                    if (myComPort.IsOpen)
                    {
                        myComPort.Close();
                        for (count = 0; count < 1000; count++)
                            proConnect.PerformStep();
                    }
                    myComPort.Open();
                    proConnect.PerformStep();
                    proConnect.Value = proConnect.Maximum;
                   
                    //testtimer.Start();
                    myComPort.DataReceived += new SerialDataReceivedEventHandler(serialPort1_DataReceived);
                    Setting_Status2.Text = "CONNECTED";
                }
                else
                {

                    cmdConnect.Text = "CONNECT";
                    proConnect.Maximum = 1000;
                    proConnect.Minimum = 0;
                    if (myComPort.IsOpen == true)
                        myComPort.Close();
                    proConnect.Value = proConnect.Minimum;
                    Setting_Status2.Text = "DISCONNECTED";
                }
        }
        //********************************************
        //
        //data receiving event
        //
        //*****************************************
        private void serialPort1_DataReceived(object sender, SerialDataReceivedEventArgs e)

        {           
            try
             {
                 temp = myComPort.ReadByte();
              }
                      catch
                        {
                          return;
                         }

                      if (flag==0)
                      {
                          if (temp == 0x73)
                          {
                              flag = 1;
                              count = 0;
                          }
                          else
                          return;
                      }
                      else
                          {
                              RS232_Rx_Buf[count++] = (byte)temp;
                              if (count == 49)
                              {
                                  if (RS232_Rx_Buf[48] == 0x0A)
                                  DATA_PROCCESSING.Input = RS232_Rx_Buf;
                                  myComPort.DiscardInBuffer();
                                  flag = 0;
                                  DATA_PROCCESSING.Data_proccessing(ahrs);
                                  //DATA_PROCCESSING.Data_flag = 1; 
                              }

                           }
                 
                
                         // myComPort.DiscardInBuffer();
            }
      

        private void button1_Click(object sender, EventArgs e)

        {
            if (MessageBox.Show("Do you want to exit?", "IMU PC INTERFACE",
   MessageBoxButtons.YesNo, MessageBoxIcon.Question)
   == DialogResult.Yes)
            {
                if (myComPort.IsOpen)
                    myComPort.Close();
                Application.Exit();
            }
        }

        private void Plot_FormStart()
        {
            Application.Run(new WindowsFormsApplication1.Plot());
        }

        private void cmdStart_Click(object sender, EventArgs e)
        {
         

            if (myComPort.IsOpen == false)
            {
                //myComPort.Open ();
                MessageBox.Show ( "COMPORT NOT OPEN", "ERROR",
                     MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk );
                return;

            }
         //   if( first_flag ==0)
           // {
                //
                //plot form
                //
                Thread Plot_Form = new Thread ( new ThreadStart ( Plot_FormStart ) );
                if(comboGraph.Text=="ENABLE")
                Plot_Form.Start ();
                Thread DATA = new Thread ( new ThreadStart ( DATA_PROCCESSING.DATA_Initialize ) );
                DATA.Start (); 
                if(combFilter.Text=="AHRS")
                ahrs=0;
                else
                ahrs=1;

              //  first_flag =1;
          //  }
            /*form_3DcuboidB.Text += "   REFERENCE SYSTEM";
            form_3DcuboidA.Text += "   IMU MEASUREMENT";
            BackgroundWorker backgroundWorkerA = new BackgroundWorker();
            BackgroundWorker backgroundWorkerB = new BackgroundWorker();
            backgroundWorkerA.DoWork += new DoWorkEventHandler(delegate { form_3DcuboidA.ShowDialog(); });
            backgroundWorkerB.DoWork += new DoWorkEventHandler(delegate { form_3DcuboidB.ShowDialog(); });
            backgroundWorkerA.RunWorkerAsync();
            backgroundWorkerB.RunWorkerAsync();
            form_3DcuboidB.label_show(" REFERENCE SYSTEM", 10);
            form_3DcuboidA.label_show(" IMU MEASUREMENT", 10);
             */
            //
            //timer
            //
           // testtimer.Enabled = true;
           // testtimer.Start();
            
            //
            //send command to start system
            //
            //System_Config();
            //
            //
            //packet_data ( SLAVE_ID, RF_ID, ENABLE, 0 );
            //Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
        }
        private void testtimer_Tick_1(object sender, EventArgs e)
        {

            try
            {
                s= myComPort.ReadTo("\n");
               
               // myComPort.Read(RS232_Rx_Buf, 0, 49);

                //if (RS232_Rx_Buf[48] == '\n')
                if(s.Length==48)
                {
                    //Encoding.ASCII.GetBytes(input);
                    DATA_PROCCESSING.Input = Encoding.ASCII.GetBytes(s);
                    DATA_PROCCESSING.Data_flag = (byte)1;
                }
            }
            catch
            {
                return;
            }
            myComPort.DiscardInBuffer();
        }

        private void testtimer_Tick_11(object sender, EventArgs e)
        {
           // angle[1, 0] += 0.001;
           // angle[0, 0] += 0.001;
            //angle[2, 0]  += 0.001;
            //Q[0,0] = 1.0f / (double)Math.Sqrt(2);
            //Q[0,1] = 1.0f / (double)Math.Sqrt(2);
            //Q[0,2] = 0.0f;
            // Q[0,3] = 0.0f;
            //Q2 = MyQuaternion.getQuaternionFromAngles(angle);
            //Q[0, 0] = (float)Q2[0, 0];
            //Q[0, 1] = (float)Q2[1, 0];
            ///Q[0, 2] = (float)Q2[2, 0];
           // Q[0, 3] = (float)Q2[3, 0];
           // ret = MyQuaternion.getRotationMatrixFromQuaternion2(Q);
            //double[] ret2 = MyQuaternion.getRotaionMatrixFromAngle(angle);
            //angle = MyQuaternion.getAnglesFromQuaternion(Q2);
            //for (int i = 0; i < 9; i++)
            //{
            //    rotation[i] = (float) ret[0,i];
                //rotation2[i] = (float)ret2[i]; 
            //}

           // this.form_3DcuboidA.RotationMatrix = rotation;
            //this.form_3DcuboidB.RotationMatrix = rotation2;
            //plotForm = new Plot();
            //plotForm.AddDataToGraph ( 1, 0, 0, 0);
           
            Delay ( 10000 );
            packet_data ( MASTER_ID, ALL, START, 0 );
            Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
            Delay ( 10000 );
        }

        static float deg2rad(float degrees)
        {
            return (float)(Math.PI / 180) * degrees;
        }
        //*******************************************
        //
        // sending data to device
        //
        //******************************************
        public void Send_Data(byte[] Output, int length)
        {
            myComPort.Write(Output, 0, length);
        }
        //******************************************
        //
        // package data
        //
        //******************************************
        public void packet_data(byte  ID, byte Module,byte Command,UInt16 Data)
        {
            RS232_Tx_Buf[0] = ID;
            RS232_Tx_Buf[1] = Module;
            RS232_Tx_Buf[2] = Command;
            RS232_Tx_Buf[3] = (byte)(Data >>8);
            RS232_Tx_Buf[4] = (byte)(Data & 0x00FF);
        }
        //****************************************
        //
        //Delay a short time
        //
        //****************************************
        private void Delay(UInt64 num)
        {
            while (num-- !=0)
            {
            }
        }
        //****************************************
        //
        // System Run
        //
        //***************************************
    /*   private void  System_Config()
       {

           if (myComPort.IsOpen == false)
           {
               //myComPort.Open ();
               MessageBox.Show ( "COMPORT NOT OPEN", "ERROR",
                    MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk );
               return;
                
           }
           else{
           if (combMode.Text == "MODE1(X-Y-Z)")
           {
           //
           // Slave enable
           //
           packet_data(SLAVE_ID,IMU_ID,ENABLE,Sample_Period);
           Send_Data(RS232_Tx_Buf,Out_Buf_Length);
           Delay(1000);
           packet_data(SLAVE_ID,EN_ID,ENABLE,Sample_Period);
           Send_Data(RS232_Tx_Buf,Out_Buf_Length);
           Delay(10000);
           packet_data ( SLAVE_ID, RF_ID, ENABLE, Sample_Period );
           Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
           Delay ( 1000 );
           //
           //Master enable
           //
          // packet_data(MASTER_ID,MOTOR_ID,DISABLE,0);
          // Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
           Delay ( 1000 );
           packet_data (MASTER_ID, EN_ID, ENABLE, Sample_Period );
           Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
           Delay ( 10000 );
           packet_data ( MASTER_ID, RF_ID, ENABLE, Sample_Period );
           Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
           Delay ( 10000 );
           }
           else if (combMode.Text == "MODE2(X-Y-Z-MOTOR)")
           {
               //
               // Slave enable
               //
               packet_data ( SLAVE_ID, IMU_ID, ENABLE, Sample_Period );
               Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
               Delay ( 1000 );
               packet_data ( SLAVE_ID, EN_ID, ENABLE, Sample_Period );
               Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
               Delay ( 10000 );
               packet_data ( SLAVE_ID, RF_ID, ENABLE, Sample_Period );
               Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
               Delay ( 1000 );
               //
               //Master enable
               //
               packet_data ( MASTER_ID, MOTOR_ID, ENABLE, 0 );
               Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
               Delay ( 1000 );
               packet_data ( MASTER_ID, EN_ID, ENABLE, Sample_Period );
               Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
               Delay ( 10000 );
               packet_data ( MASTER_ID, RF_ID, ENABLE, Sample_Period );
               Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
               Delay ( 10000 );
               
           }
           else
           {
               //myComPort.Open ();
               MessageBox.Show ( "SELECT MODE", "ERROR",
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk );
               return;
           }
           }
       }*/
       //**************************************************
       //
       //Stop all device
       //
       //**************************************************
       private void  System_Stop()
       {

           if (myComPort.IsOpen == false)
           {
               //myComPort.Open ();
               MessageBox.Show ( "COMPORT NOT OPEN", "ERROR",
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk );
               return;
           }
           packet_data(SLAVE_ID,ALL,STOP,0);
           Send_Data(RS232_Tx_Buf,Out_Buf_Length);
           Delay(10000);
           packet_data(MASTER_ID,ALL,STOP,0);
           Send_Data(RS232_Tx_Buf,Out_Buf_Length);
           Delay(10000);
           //
       }
       //****************************************************
       //
       //Start to get data from device
       //
       //****************************************************
       private void System_Start()
       {

           if (myComPort.IsOpen == false)
           {
               //myComPort.Open ();
               MessageBox.Show ( "COMPORT NOT OPEN", "ERROR",
                   MessageBoxButtons.OKCancel, MessageBoxIcon.Asterisk );
               return;
           }
          
           //
       }

       private void cmdStop_Click( object sender, EventArgs e )
       {
           packet_data ( SLAVE_ID, IMU_ID, DISABLE, 0 );
           Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
           packet_data ( SLAVE_ID, RF_ID, DISABLE, 0 );
           Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
           packet_data ( MASTER_ID, MOTOR_ID, DISABLE, 0 );
           Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
           packet_data ( MASTER_ID, EN_ID, DISABLE, 0 );
           Send_Data ( RS232_Tx_Buf, Out_Buf_Length );
       }

       private void Form2_FormClosing( object sender, FormClosingEventArgs e )
       {
           if (myComPort.IsOpen)
               myComPort.Close ();
           Application.Exit ();
       }

       private void label7_Click(object sender, EventArgs e)
       {

       }
       
    }
}

