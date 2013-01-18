using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
//using System.Windows.Forms;
using MatrixLibrary;
using System.Data;
using System.Windows.Forms;
//using System.Threading;
using System.Timers;
using myIIRfilter;
namespace WindowsFormsApplication1
{
    class AcquisitionThread
    {
        //
        //variables for 3D cuboid
        //
        const float Gyro_sensity = (3.0f * 1000 / 1023 / 3.33f) * (float)Math.PI / 180.00f;
        const float Acc_sensity = 3.0f * 1000 / 1023 / 800.00f;
        const int gyroMeasError = 40;
        double beta = Math.Sqrt ( 3.0 / 4.0 ) * ( Math.PI * ( gyroMeasError / 180.0 ) );   // compute beta
        //static AHRS.MadgwickAHRS AHRS = new AHRS.MadgwickAHRS ( 0.01f, 0.0001f );
        static AHRS.MahonyAHRS AHRS = new AHRS.MahonyAHRS ( 0.01f,8.0f );//0.003,3,0

        static WindowsFormsApplication1.KalmanFilter Kalman = new WindowsFormsApplication1.KalmanFilter();

        MatrixLibrary.Matrix Q = new MatrixLibrary.Matrix ( 1, 4 );
        MatrixLibrary.Matrix Q2 = new MatrixLibrary.Matrix ( 4, 1 );
        MatrixLibrary.Matrix ret = new MatrixLibrary.Matrix ( 1, 9 );
        MatrixLibrary.Matrix angle = new MatrixLibrary.Matrix ( 3, 1 );
        float[] rotation = new float[9];
        float[] rotation2 = new float[9];
        Plot plotForm = new Plot();
        Form_3Dcuboid form_3DcuboidA = new Form_3Dcuboid ();
        Form_3Dcuboid form_3DcuboidC = new Form_3Dcuboid ();
        Form_3Dcuboid form_3DcuboidB = new Form_3Dcuboid ( new string[] { "Form_3Dcuboid/RightInv.png", "Form_3Dcuboid/LeftInv.png", "Form_3Dcuboid/BackInv.png", "Form_3Dcuboid/FrontInv.png", "Form_3Dcuboid/TopInv.png", "Form_3Dcuboid/BottomInv.png" } );
       // public float SamplePeriod { get; set; }
        //
        //IMU elements
        //
        public UInt32[] ACC = new UInt32[3];
        public UInt32[] GYRO = new UInt32[3];
        
        //public UInt32[] tMAG = new UInt32[3];
        public Int32[] MAG = new Int32[3];
        public UInt32[] ACC_OFFSET = new UInt32[3];
        public UInt32[] GYRO_OFFSET = new UInt32[3];
        public float[] ERROR = new float[4];
        public float[] LIST = new float[6];
        //ENCODER
        //
        public UInt32[] ENCODER = new UInt32[3];
        //
        // Euler Angel
        //
        public float[] En_Angle = new float[3]; //radian
        public float[] Pre_En_Angle = new float[3]; //radian
        public float[] En_Angle_Old = new float[3]; //radian
        public float IMU_Angle_X, IMU_Angle_Y, IMU_Angle_z;//radian
        public double[] En_Angle2 = new double[3]; //radian
        public float[] Quaternion = new float[4]{1.0f,0.0f,0.0f,0.0f};
        //
        //variables for plot form
        //
        //
        float gx, gy, gz, ax, ay, az, mx, my, mz;
        float[] RwEst = new float[3];
         float[] RwGyro =new float[3];
         float [] Awz = new float[2];  
         const float wGyro  = 10.0f; 
         float [] RwAcc = new float[3]; 
        int first_time =1;
        //
        //
        //

        public byte[] Input = new byte[49];
        public string s ;
        public byte Data_flag ;


        //
        //
        //
        double pre_pitch  = 0, pre_roll =0 ;
        private  System.Timers.Timer  DataUpdateTimer;
        private IIRfilter HPfilterp = new IIRfilter ( new double[] { 0.999975456909767, -0.999975456909767 }, new double[] { 1, -0.999950913819534 } );      // 0.001Hz 1st order HP filter
        private IIRfilter HPfilterq = new IIRfilter ( new double[] { 0.999975456909767, -0.999975456909767 }, new double[] { 1, -0.999950913819534 } );
        private IIRfilter HPfilterr = new IIRfilter ( new double[] { 0.999975456909767, -0.999975456909767 }, new double[] { 1, -0.999950913819534 } );
      
        //***********************************************
        //
        // data proccessing
        
        //**********************************************
         public void Data_proccessing (int n)
        {

          /*  if (s.Length == 48)
            {

                Int32.TryParse(s.Substring(1, 3), out MAG[0]);
                Int32.TryParse(s.Substring(5, 3), out MAG[1]);
                Int32.TryParse(s.Substring(9, 3), out MAG[2]);

                UInt32.TryParse(s.Substring(12, 4), out ACC[0]);
                UInt32.TryParse(s.Substring(16, 4), out ACC[1]);
                UInt32.TryParse(s.Substring(20, 4), out ACC[2]);

                UInt32.TryParse(s.Substring(24, 4), out GYRO[0]);
                UInt32.TryParse(s.Substring(28, 4), out GYRO[1]);
                UInt32.TryParse(s.Substring(32, 4), out GYRO[2]);

                UInt32.TryParse(s.Substring(36, 4), out ENCODER[0]);
                UInt32.TryParse(s.Substring(40, 4), out ENCODER[1]);
                UInt32.TryParse(s.Substring(44, 4), out ENCODER[2]);

                if (s.Substring(0, 1) == "-")
                {
                    MAG[0] = -MAG[0];
                }
                if (s.Substring(4, 1) == "-")
                {
                    MAG[1] = -MAG[1];
                }
                if (s.Substring(8, 1) == "-")
                {
                    MAG[2] = -MAG[2];
                }
            }*/
                MAG[0] = (Int32)((Input[1] - 0x30) * 100 + (Input[2] - 0x30) * 10 + (Input[3] - 0x30) );
                MAG[1] = (Int32)((Input[5] - 0x30) * 100 + (Input[6] - 0x30) * 10 +  (Input[7] - 0x30));
                MAG[2] = (Int32)((Input[9] - 0x30) * 100 + (Input[10] - 0x30) * 10 + (Input[11] - 0x30));

                ACC[0] =  (UInt32) ( (Input[12] - 0x30) * 1000 + (Input[13] - 0x30) * 100 + (Input[14] - 0x30) * 10 + (Input[15] - 0x30));
                ACC[1] = (UInt32)((Input[16] - 0x30) * 1000 + (Input[17] - 0x30) * 100 + (Input[18] - 0x30) * 10 + (Input[19] - 0x30));
                ACC[2] = (UInt32)((Input[20] - 0x30) * 1000 + (Input[21] - 0x30) * 100 + (Input[22] - 0x30) * 10 + (Input[23] - 0x30));


                GYRO[0] = (UInt32)((Input[24] - 0x30) * 1000 + (Input[25] - 0x30) * 100 + (Input[26] - 0x30) * 10 + (Input[27] - 0x30));
                GYRO[1] = (UInt32)((Input[28] - 0x30) * 1000 + (Input[29] - 0x30) * 100 + (Input[30] - 0x30) * 10 + (Input[31] - 0x30));
                GYRO[2] = (UInt32)((Input[32] - 0x30) * 1000 + (Input[33] - 0x30) * 100 + (Input[34] - 0x30) * 10 + (Input[35] - 0x30));


                ENCODER[0] = (UInt32)((Input[36] - 0x30) * 1000 + (Input[37] - 0x30) * 100 + (Input[38] - 0x30) * 10 + (Input[39] - 0x30));
                ENCODER[1] = (UInt32)((Input[40] - 0x30) * 1000 + (Input[41] - 0x30) * 100 + (Input[42] - 0x30) * 10 + (Input[43] - 0x30));
                ENCODER[2] = (UInt32)((Input[44] - 0x30) * 1000 + (Input[45] - 0x30) * 100 + (Input[46] - 0x30) * 10 + (Input[47] - 0x30));

            if (Input[0] == '-')
            {
                MAG[0] =  -MAG[0];
            }
            if (Input[4] == '-')
            {
                MAG[1] = -MAG[1];
            }
            if (Input[8] == '-')
            {
                MAG[2] = -MAG[2];
            }

           // }
            //calculate to real value for filter
            int a = MAG[0];
            MAG[0] =- MAG[1];
            MAG[1] = a;
            MAG[2] = MAG[2];

            this.form_3DcuboidB.X = (float)MAG[0];
            this.form_3DcuboidB.Y = (float)MAG[1];
            this.form_3DcuboidB.Z = (float)MAG[2];
           
            ax =  ( (float)ACC[0] - (float)ACC_OFFSET[0] ) * Acc_sensity;// ax real
            ay =  ( (float)ACC[1] - (float)ACC_OFFSET[1] ) * Acc_sensity; // ay real
            az =  ( (float)ACC[2] - (float)ACC_OFFSET[2] ) * Acc_sensity;

            gx = - ( (float)GYRO[0] - (float)GYRO_OFFSET[0] ) *  Gyro_sensity;
            gy =  ( (float)GYRO[1] - (float)GYRO_OFFSET[1] ) * Gyro_sensity;
            gz =  ( (float)GYRO[2] - (float)GYRO_OFFSET[2] ) * Gyro_sensity;

           

           // float norm = (float)Math.Sqrt ( ax * ax + ay * ay + az * az );
         //   ax /= norm;
          //  ay /= norm;
           // az /= norm;

            

            
            //
            //
            //
            En_Angle[1] = (float)( ( ENCODER[0] / 4095.0f ) * 2.0f * Math.PI );
            En_Angle[0] = (float)( ( ENCODER[1] / 4095.0f ) * 2.0f * Math.PI );
            En_Angle[2] = (float)( ( ENCODER[2] / (6395.0f ) ) * 2.0f * Math.PI );
            if(En_Angle[0]>Math.PI)
                En_Angle[0] -= (float)Math.PI *2;
            if (En_Angle[1] > Math.PI)
                En_Angle[1] -= (float)Math.PI * 2;
            if (En_Angle[2] > Math.PI)
                En_Angle[2] -= (float)Math.PI * 2;
         /*   if(( En_Angle[0]==0)&& (Pre_En_Angle[0]==0))
            {   
                En_Angle[0]=0;
            }
            if( ( En_Angle[0]==0) & (Pre_En_Angle[0] != 0))
            {
                En_Angle[0]=Pre_En_Angle[0];
            }
            if (( En_Angle[0] != 0 ))
            {
                Pre_En_Angle[0] = En_Angle[0];
            }
            if (( En_Angle[1] == 0 ) & ( Pre_En_Angle[1] == 0 ))
            {
                En_Angle[1] = 0;
            }
             if (( En_Angle[1] == 0 ) & ( Pre_En_Angle[1] != 0 ))
            {
                En_Angle[1] = Pre_En_Angle[1];
            }
            if (( En_Angle[1] != 0 ) )
            {
                Pre_En_Angle[1] = En_Angle[1];
            }*/
            //
            //
            //this for Encoder
            //
            // En_Angle[0] = 1;
            // En_Angle[1] = 1;
            // En_Angle[2] = 1;
            // float[] Quatemp = MyQuaternion.getQuaternionFromAngles3(En_Angle);
            //En_Angle = MyQuaternion.getAnglesFromQuaternion2(Quatemp);
            this.form_3DcuboidA.RotationMatrix = MyQuaternion.getRotaionMatrixFromAngle ( En_Angle );
       //     this.form_3DcuboidA.RotationMatrix =  MyQuaternion.getRotaionMatrixFromAngle ( En_Angle );
            //MatrixLibrary.Matrix mat = new MatrixLibrary.Matrix(3,3);
            //mat[0,0] = rotation[0];
           // mat[0, 1] = rotation[1];
            //mat[0, 2] = rotation[2];
            //mat[1, 0] = rotation[3];
            //mat[1, 1] = rotation[4];
            //mat[1, 2] = rotation[5];
            //mat[2, 0] = rotation[6];
            //mat[2, 1] = rotation[7];
            //mat[2, 2] = rotation[8];
            //double[] dtemp = MyQuaternion.getAngleFromRotation(mat);
            //float[] ftemp = new float[3];
            //ftemp[0] = (float)dtemp[0];
            //ftemp[1] = (float)dtemp[1];
            //ftemp[2] = (float)dtemp[2];
            //this.form_3DcuboidC.RotationMatrix = MyQuaternion.getRotaionMatrixFromAngle ( ftemp );
            //gx =(float) HPfilterp.step ((double) gx );                       // 16-bit uint ADC gyrocope values to rad/s (HP filtered)
            //gy = (float)HPfilterp.step ( (double)gy);
            //gz = (float)HPfilterp.step ( (double)gz);   
             //
            //update
            //
            // AHRS.Update(gx,gy,gz,ax,ay,az);
           //  Quaternion = AHRS.Quaternion;
           //  this.form_3DcuboidA.RotationMatrix = MyQuaternion.getRotationMatrixFromQuaternion3 ( Quatemp );
           

            //
            //send data to 3D form
            //
            this.form_3DcuboidB.Accx = (float)ax;
            this.form_3DcuboidB.Accy = (float)ay;
            this.form_3DcuboidB.Accz = (float)az;
            this.form_3DcuboidB.Gyrox = (float)gx;
            this.form_3DcuboidB.Gyroy = (float)gy;
            this.form_3DcuboidB.Gyroz = (float)gz;

            //
            //for accellero only
           // AHRS.Update((float)gx, (float)gy, (float)gz, (float)ax, (float)ay, (float)az);
            if (n ==1)
            {
                Kalman.filterStep((double)gx * 180.00f / Math.PI, (double)gy * 180.00f / Math.PI, (double)gz * 180.00f / Math.PI, (double)ax, (double)ay, (double)az, (double)MAG[0], (double)MAG[1], (double)MAG[2]);
                // Kalman.filterStep((double)gx , (double)gy, (double)gz, (double)ax, (double)ay, (double)az, (double)MAG[0], (double)MAG[1], (double)MAG[2]);
                Quaternion[0] = Kalman.q_filt1;
                Quaternion[1] = Kalman.q_filt2;
                Quaternion[2] = Kalman.q_filt3;
                Quaternion[3] = Kalman.q_filt4;
            }
            else
            {
                
                AHRS.Update((float)gx, (float)gy, (float)gz, (float)ax, (float)ay, (float)az, (float)MAG[0], (float)MAG[1], (float)MAG[2]);//
                Quaternion = AHRS.Quaternion;
            }
            //  Quaternion[0] = 1.0f;
            // Quaternion[1] = 0.5f;
            // Quaternion[2] = 0.0f;
            //Quaternion[3] = 0.0f;

            //float[]  Angle3 = MyQuaternion.getAnglesFromQuaternion2(Quaternion);
            //this.form_3DcuboidB.X = (float)Angle3[0];
            //this.form_3DcuboidB.Y = (float)Angle3[1];
            //this.form_3DcuboidB.Z = (float)Angle3[2];
            //  float[] temp1 = MyQuaternion.getRotationMatrixFromQuaternion3(Quaternion);
            // MatrixLibrary.Matrix mat = new MatrixLibrary.Matrix ( 3, 3 );
            float[] temp1 = MyQuaternion.getRotationMatrixFromQuaternion3(Quaternion);
           float[] dtemp = MyQuaternion.getAnglesFromQuaternion2(Quaternion);
            /*    mat[0, 0] = temp1[0];
                mat[0, 1] = temp1[1];
                mat[0, 2] = temp1[2];
                mat[1, 0] = temp1[3];
                mat[1, 1] = temp1[4];
                mat[1, 2] = temp1[5];
                mat[2, 0] = temp1[6];
                mat[2, 1] = temp1[7];
                mat[2, 2] = temp1[8];*/
            //   double[] dtemp = MyQuaternion.getAngleFromRotation ( mat );
            //m   double[] Angle5 = new double[3];
            //m   Angle5[0] = En_Angle2[0];
            //m   Angle5[1] = En_Angle2[1];
            //m  Angle5[2] = dtemp[2];
            //  dtemp[2] = En_Angle[2];

            //   this.form_3DcuboidB.Gyrox = (float)(dtemp[0] * 180 / Math.PI) ;//Angle5[0];
            // this.form_3DcuboidB.Gyroy = (float)(dtemp[1] * 180 / Math.PI);//Angle5[1];
            //  this.form_3DcuboidB.Gyroz = (float)(dtemp[2] * 180 / Math.PI);//]Angle5[2];
            //      float[] temp3 = MyQuaternion.getRotaionMatrixFromAngle2(dtemp);//Angle5 
            this.form_3DcuboidB.RotationMatrix = temp1;
            //
            //Error angle
            //
            /*m      ERROR[0] = (En_Angle[0] -(float)Angle5[0])*180 / (float)Math.PI;
                  ERROR[1] = En_Angle[1] - (float)Angle5[1] * 180 / (float)Math.PI;*/
            // ERROR[0] = (float)(dtemp[0] * 180 / Math.PI); ;
            //ERROR[1] = (float)(En_Angle[0] * 180 / Math.PI);
            // ERROR[2] = (float)(dtemp[0] * 180 / Math.PI);
            //ERROR[3] = 0;
             
            //
            LIST[0] = (float)(dtemp[0] * 180 / Math.PI);
            LIST[1] = (float)(En_Angle[0] * 180 / Math.PI);

            LIST[2] = (float)(dtemp[1] * 180 / Math.PI);
            LIST[3] = (float)(En_Angle[1] * 180 / Math.PI);

            LIST[4] = (float)(dtemp[2] * 180 / Math.PI);
            LIST[5] = (float)(En_Angle[2] * 180 / Math.PI);
            plotForm.add_graph = LIST;
            //
            //
            //
            //
            /*MatrixLibrary.Matrix Quaternion12 = new MatrixLibrary.Matrix ( 4, 1 );
            MatrixLibrary.Matrix Angle4 = new MatrixLibrary.Matrix ( 3, 1 );
            Quaternion12[0,0] =  1/Math.Sqrt(4);
            Quaternion12[1, 0] =- 1 / Math.Sqrt ( 4);
            Quaternion12[2, 0] = -1 / Math.Sqrt ( 4 ); ;
            Quaternion12[3, 0] = -1 / Math.Sqrt ( 4 );

            Angle4 = MyQuaternion.getAnglesFromQuaternion ( Quaternion12 );
            MatrixLibrary.Matrix Temp_AN = new MatrixLibrary.Matrix ( 3, 1 );
            MatrixLibrary.Matrix QuaRe = new MatrixLibrary.Matrix ( 4, 1 );
            QuaRe = MyQuaternion.getQuaternionFromAngles2 ( Angle4 );
            QuaRe = MyQuaternion.getQuaternionFromAngles ( Angle4 );
             * */

            //this.form_3DcuboidB.RotationMatrix = temp1;
            //QuaRe = MyQuaternion.getQuaternionFromAngles ( Angle4 );
            
        }
        public double[] computeAnglesFromAcc( float ax, float ay, float az )
        {
            double gamma = 0;
            double phi = 0;
            double psi = 0;
            double pitch =0, roll = 0, yaw =0;
            if ( (Math.Abs ( ax ) > 0.99)||(Math.Abs ( ax ) > 0.99)||(Math.Abs ( ax ) > 0.99))
            {
                    return new double[] {pre_roll, pre_pitch,yaw};
            }
            else
            {

                   // phi = Math.Asin ( ax );
                    //gamma = Math.Asin ( ay);
                   // psi = Math.Asin(az);
                  
                    pitch = Math.Atan( ax / Math.Sqrt(ay*ay +az*az));
                    roll  = Math.Atan ( ay / Math.Sqrt ( az * az + ax * ax ) );
                    if(pitch> Math.PI /2)
                    {   
                       pitch = Math.PI /2 ;
                    }
                    if(pitch < (-Math.PI/2))
                    {
                        pitch = -Math.PI/2;
                    }
                    if(roll> Math.PI /2)
                    {   
                       roll = Math.PI /2 ;
                    }
                    if(roll< (-Math.PI/2))
                    {
                        roll= -Math.PI/2;
                    }
                    pre_roll = roll;
                    pre_pitch = pitch;
                    // double bx = magn[0] * Math.Cos ( phi ) + magn[2] * Math.Sin ( -phi );
                    // double by = magn[0] * Math.Sin ( gamma ) * Math.Sin ( -phi ) + magn[1] * Math.Cos ( gamma ) - magn[2] * Math.Sin ( gamma ) * Math.Cos ( phi );

                    // psi = Math.Atan2 ( ( by * 1.0672 + 0.1075 ), ( bx + 0.3159 ) );
                }


            //gamma = gamma * 180 / Math.PI;
            //phi = phi * 180 / Math.PI;
            // psi = psi * 180 / Math.PI;
            return new double[] { roll,pitch, yaw };
        }
        public double[] computeEulerFromGyro( double[] w, double[] anglesEuler1 )
        {

            Matrix m = new Matrix ( 3, 3 );

            m[0, 0] = 0;
            m[0, 1] = Math.Sin ( anglesEuler1[0] );
            m[0, 2] = Math.Cos ( anglesEuler1[0] );

            m[1, 0] = 0;
            m[1, 1] = Math.Cos ( anglesEuler1[0] ) * Math.Cos ( anglesEuler1[1] );
            m[1, 2] = -Math.Sin ( anglesEuler1[0] ) * Math.Cos ( anglesEuler1[1] );

            m[2, 0] = Math.Cos ( anglesEuler1[1] );
            m[2, 1] = Math.Sin ( anglesEuler1[0] ) * Math.Sin ( anglesEuler1[1] );
            m[2, 2] = Math.Cos ( anglesEuler1[0] ) * Math.Sin ( anglesEuler1[1] );

            Matrix omega = new Matrix ( 3, 1 );
            omega[0, 0] = w[0];
            omega[1, 0] = w[1];
            omega[2, 0] = w[2];
            Matrix dAnglesM = Matrix.ScalarMultiply ( 1 / Math.Cos ( anglesEuler1[1] ), Matrix.Multiply ( m, omega ) );

            double[] dAngles = new double[3];

            dAngles[0] = dAnglesM[2, 0];
            dAngles[1] = dAnglesM[1, 0];
            dAngles[2] = dAnglesM[0, 0];

            //for (int i = 0; i < anglesEuler.Length; i++)
            //{
            //    anglesEuler1[i] = anglesEuler1[i] * 180 / Math.PI;
            //}

            return dAngles;
        }
        public void DATA_Initialize()
        {
           // for( int temp =0 ; temp <49 ; temp++)
           // {
            //    Input[temp]= 0x30;
           // }
         
            BackgroundWorker backgroundWorkerA = new BackgroundWorker ();
            BackgroundWorker backgroundWorkerB = new BackgroundWorker ();
            BackgroundWorker backgroundWorkerC = new BackgroundWorker ();
            BackgroundWorker myplotForm = new BackgroundWorker ();
            backgroundWorkerA.DoWork += new DoWorkEventHandler ( delegate { form_3DcuboidA.ShowDialog (); } );
            backgroundWorkerB.DoWork += new DoWorkEventHandler ( delegate { form_3DcuboidB.ShowDialog (); } );
            backgroundWorkerC.DoWork += new DoWorkEventHandler ( delegate { form_3DcuboidC.ShowDialog (); } );
            myplotForm.DoWork += new DoWorkEventHandler ( delegate { plotForm.Show (); } );
            backgroundWorkerA.RunWorkerAsync ();
            backgroundWorkerB.RunWorkerAsync ();
            backgroundWorkerC.RunWorkerAsync ();
            //myplotForm.RunWorkerAsync ();
            form_3DcuboidA.label_showA = (string)" REFERENCE SYSTEM";
            form_3DcuboidB.label_showA =  " IMU MEASUREMENT";
            form_3DcuboidC.label_showA = " IMU MEASUREMENT";
            //form_3DcuboidB.Text = "   IMU MEASUREMENT GYRO + ACCELEROMETER";
            //form_3DcuboidA.Text = "  REFERENCE SYSTEM";
            //form_3DcuboidC.Text = "  IMU MEASUREMENT ACCELEROMETER";
            DataUpdateTimer = new    System.Timers.Timer() ;
            DataUpdateTimer.Enabled = true;
            DataUpdateTimer.Interval = 10; 
            DataUpdateTimer.Elapsed += new ElapsedEventHandler(DataUpdateTimer_Tick);
            //DataUpdateTimer.Start ();
            this.form_3DcuboidB.MouseClick += new System.Windows.Forms.MouseEventHandler(this.form_3DcuboidB_MouseClick);

            ACC_OFFSET[0] = 573;// 573;// 2086;//610;
            ACC_OFFSET[1] = 610;// 607;// 2170;// 585;
            ACC_OFFSET[2] = 410;// 430;// 1438;// 510;
            GYRO_OFFSET[0] = 438;// 1580;// MAG[0];
            GYRO_OFFSET[1] = 438;//1580;// MAG[1];
            GYRO_OFFSET[2] = 438;//1580;//
            //plotForm = new Plot();
            //plotForm.Show ();
            //plotForm=new Plot();
          

           
        }
        private void form_3DcuboidB_MouseClick(object sender, MouseEventArgs e)
        {
            this.form_3DcuboidB.RotationMatrix[0] = 1;
            this.form_3DcuboidB.RotationMatrix[0] = 0;
            this.form_3DcuboidB.RotationMatrix[0] = 0;
            this.form_3DcuboidB.RotationMatrix[0] = 0;
            this.form_3DcuboidB.RotationMatrix[0] = 1;
            this.form_3DcuboidB.RotationMatrix[0] = 0;
            this.form_3DcuboidB.RotationMatrix[0] = 0;
            this.form_3DcuboidB.RotationMatrix[0] = 0;
            this.form_3DcuboidB.RotationMatrix[0] = 1;  
            

        }
        private void DataUpdateTimer_Tick( object sender, ElapsedEventArgs e )
        {
            //Data_flag  =1;
           // if(Data_flag == 1 )
           // {
           // Data_proccessing ();
           // Data_flag =0;
           // }
        }
        private void Dataupdate_New(float ax, float ay, float az, float gx, float gy, float gz)
      
        {
                 int w;
                 float tmpf = 0.0f;
                 float[] Gyro= new float[3];
                 int signRzGyro;
                 Gyro[0] = gx;
                 Gyro[1] = gy;
                 Gyro[2] = gz;
                 RwAcc[0] = ax;
                 RwAcc[1] = ay;
                 RwAcc[2] = az;
                 const float PI = (float)Math.PI;
                //evaluate RwGyro vector
                if (Math.Abs ( RwEst[2] )  < 0.1)
                {
                   
                    //Rz is too small and because it is used as reference for computing Axz, Ayz it's error fluctuations will amplify leading to bad results
                    //in this case skip the gyro data and just use previous estimate
                    for (w = 0; w <= 2; w++)
                    {
                        RwGyro[w] = RwEst[w];
                    }
                }
                else
                {
                    //get angles between projection of R on ZX/ZY plane and Z axis, based on last RwEst
                    for (w = 0; w <= 1; w++)
                    {
                        tmpf = Gyro[w];                        //get current gyro rate in deg/s
                        tmpf *= 0.01f;                      //get angle change in deg
                        Awz[w] = (float)Math.Atan2( RwEst[w], RwEst[2] ) * 180 / (float)Math.PI;   //get angle and convert to degrees 
                        Awz[w] += tmpf;             //get updated angle according to gyro movement
                    }

                    //estimate sign of RzGyro by looking in what qudrant the angle Axz is, 
                    //RzGyro is pozitive if  Axz in range -90 ..90 => cos(Awz) >= 0
                     signRzGyro = ( Math.Cos ( Awz[0] * (float)Math.PI / 180 ) >= 0 ) ? 1 : -1;

                    //reverse calculation of RwGyro from Awz angles, for formulas deductions see  http://starlino.com/imu_guide.html
                  //  for (w = 0; w <= 1; w++)
                   // {
                        RwGyro[0] = (float)Math.Sin ( Awz[0] * PI / 180 );
                        RwGyro[0] /= (float)Math.Sqrt ( 1 +  ( Math.Cos ( Awz[0] * PI / 180 ) ) *( Math.Cos ( Awz[0] * PI / 180 ) )*  ( Math.Tan( Awz[1] * PI / 180 ) )* ( Math.Tan( Awz[1] * PI / 180 ) ) );
                        RwGyro[1] =(float) Math.Sin ( Awz[1] * PI / 180 );
                        RwGyro[1] /= (float)Math.Sqrt ( 1 + ( Math.Cos ( Awz[1] * PI / 180 ) ) *( Math.Cos ( Awz[1] * PI / 180 ) )*  (Math.Tan ( Awz[0] * PI / 180 ) )*(Math.Tan ( Awz[0] * PI / 180 ) ) );
                   // }

                    RwGyro[2] = signRzGyro * (float)Math.Sqrt( 1 -  ( RwGyro[0] )* ( RwGyro[0] ) - ( RwGyro[1] )*( RwGyro[1] ) );
                }

                //combine Accelerometer and gyro readings
                for (w = 0; w <= 2; w++) 
                    RwEst[w] = ( RwAcc[w] + wGyro * RwGyro[w] ) / ( 1 + wGyro );

               // normalize3DVec ( RwEst );
                // normalise the accelerometer measurement
                float norm = (float)Math.Sqrt ( RwEst[0] * RwEst[0] + RwEst[1] * RwEst[1] + RwEst[2] * RwEst[2] );
                RwEst[0] /= norm;
                RwEst[1] /= norm;
                RwEst[2] /= norm;

        }
       
    }
}
