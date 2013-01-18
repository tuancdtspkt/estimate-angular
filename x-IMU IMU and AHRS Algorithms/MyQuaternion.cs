using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WindowsFormsApplication1
{
    class MyQuaternion
    {
        private double q1;
        private double q2;
        private double q3;
        private double q4;

        public MyQuaternion(double real, double i, double j, double k)
        {
            this.q1 = real;
            this.q2 = i;
            this.q3 = j;
            this.q4 = k;
        }

        public MatrixLibrary.Matrix getQuaternionAsVector()
        {
            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(4, 1);
            result[0, 0] = q1;
            result[1, 0] = q2;
            result[2, 0] = q3;
            result[3, 0] = q4;
            return result;
        }

        public static MatrixLibrary.Matrix quaternionProduct(MatrixLibrary.Matrix quaternion, MatrixLibrary.Matrix matrix)
        {
            double a1 = quaternion[0, 0];
            double a2 = quaternion[1, 0];
            double a3 = quaternion[2, 0];
            double a4 = quaternion[3, 0];
            double b1 = matrix[0, 0];
            double b2 = matrix[1, 0];
            double b3 = matrix[2, 0];
            double b4 = matrix[3, 0];

            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(4, 1);
            result[0, 0] = a1 * b1 - a2 * b2 - a3 * b3 - a4 * b4;
            result[1, 0] = a1 * b2 + a2 * b1 + a3 * b4 - a4 * b3;
            result[2, 0] = a1 * b3 - a2 * b4 + a3 * b1 + a4 * b2;
            result[3, 0] = a1 * b4 + a2 * b3 - a3 * b2 + a4 * b1;

            return result;
        }

        public static MatrixLibrary.Matrix quaternionProduct(MyQuaternion q_a, MyQuaternion q_b)
        {
            MatrixLibrary.Matrix v1 = q_a.getQuaternionAsVector();
            MatrixLibrary.Matrix v2 = q_b.getQuaternionAsVector();
            return quaternionProduct(v1, v2);
        }

        public double getNorm()
        {
            double norm = Math.Sqrt((this.q1 * this.q1) + (this.q2 * this.q2) + (this.q3 * this.q3) + (this.q4 * this.q4));
            return norm;
        }

        public double get(int index)
        {
            if (index == 0)
                return q1;
            else
                if (index == 1)
                    return q2;
                else
                    if (index == 2)
                        return q3;
                    else
                        return q4;
        }

        public MatrixLibrary.Matrix getConjugate()
        {
            return new MyQuaternion(this.q1, -this.q2, -this.q3, -this.q4).getQuaternionAsVector();
        }

        public static MatrixLibrary.Matrix getAnglesFromQuaternion(MatrixLibrary.Matrix q)
        {
            double q0 = q[0, 0];
            double q1 = q[1, 0];
            double q2 = q[2, 0];
            double q3 = q[3, 0];

            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(3, 1);

            result[0, 0] = Math.Atan2((2 * q2 * q3 + 2 * q0 * q1), (1 - 2 * q1 * q1 - 2 * q2 * q2)) * 180.0 / Math.PI;
            result[1, 0] = Math.Asin(-2 * q1 * q3 + 2 * q0 * q2) * 180 / Math.PI;
            result[2, 0] = Math.Atan2((2 * q1 * q2 + 2 * q0 * q3), (1 - 2 * (q2 * q2 + q3 * q3))) * 180.0 / Math.PI;
            return result;
        }
        //
        //
        // input none Unit
        // output Radian
        //

        public static float[] getAnglesFromQuaternion2( float[] q )
        {
            double q0 = q[0];
            double q1 = q[1];
            double q2 = q[2];
            double q3 = q[3];

            float[] result = new float[3];

            result[0] = (float)-(Math.Atan2 ( ( 2 * q2 * q3 + 2 * q0 * q1 ), ( 1 - 2 * q1 * q1 - 2 * q2 * q2 ) ));
            result[1] = (float)-(Math.Asin ( -2 * q1 * q3 + 2 * q0 * q2 ) );
            result[2] = (float)-(( Math.Atan2 ( ( 2 * q1 * q2 + 2 * q0 * q3 ), ( 1 - 2 * ( q2 * q2 + q3 * q3 ) ) ) ) );
           // for(int temp =0 ;temp<3; temp++)
           // {
            //    result[temp] %= (float) 2* Math.PI;
            //}
            return result;
        }
        public static MatrixLibrary.Matrix getQuaternionFromAngles(MatrixLibrary.Matrix angle)
        {
            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(4, 1);
            angle = (angle * Math.PI) / 180.0;
            double c1 = Math.Cos(angle[0, 0] / 2);
            double c2 = Math.Cos(angle[1, 0] / 2);
            double c3 = Math.Cos(angle[2, 0] / 2);

            double s1 = Math.Sin(angle[0, 0] / 2);
            double s2 = Math.Sin(angle[1, 0] / 2);
            double s3 = Math.Sin(angle[2, 0] / 2);

            result[0, 0] = c1 * c2 * c3 + s1 * s2 * s3;
            result[1, 0] = ( s1 * c2 * c3 - c1 * s2 * s3);
            result[2, 0] = c1 * s2 * c3 + s1 * c2 * s3;
            result[3, 0] = c1 * c2 * s3 - s1 * s2 * c3;

            return result;
        }
        public static MatrixLibrary.Matrix getRotationMatrixFromQuaternion(MatrixLibrary.Matrix Quaternion)
        {
            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(1, 9);
            result[0, 0] = 1.0f - 2.0f * Quaternion[0, 2] * Quaternion[0, 2] - 2.0f * Quaternion[0, 3] * Quaternion[0, 3];
            result[0, 1] = 2.0f * Quaternion[0, 1] * Quaternion[0, 2] - 2.0f * Quaternion[0, 3] * Quaternion[0, 0];
            result[0, 2] = 2.0f * Quaternion[0, 1] * Quaternion[0, 3] + 2.0f * Quaternion[0, 2] * Quaternion[0, 0];
            result[0, 3] = 2.0f * Quaternion[0, 1] * Quaternion[0, 2] + 2.0f * Quaternion[0, 3] * Quaternion[0, 0];
            result[0, 8] = 1.0f - 2.0f * Quaternion[0, 0] * Quaternion[0, 0] - 2.0f * Quaternion[0, 3] * Quaternion[0, 3];
            result[0, 7] = 2.0f * Quaternion[0, 2] * Quaternion[0, 3]  - 2.0f * Quaternion[0, 1] * Quaternion[0, 0];
            result[0, 6] = 2.0f * Quaternion[0, 1] * Quaternion[0, 3] - 2.0f * Quaternion[0, 2] * Quaternion[0, 0];
            result[0, 5] = 2.0f * Quaternion[0, 2] * Quaternion[0, 3] + 2.0f * Quaternion[0, 1] * Quaternion[0, 0];
            result[0, 4] = 1.0f - 2.0f * Quaternion[0, 0] * Quaternion[0, 0] - 2.0f * Quaternion[0, 2] * Quaternion[0, 2];
            return result;
        }


        //
        //Iput Radian
        //

        public static float[] getRotaionMatrixFromAngle(float[] angel)
        {
            float[] mat = new float[9];
            float angle_x = angel[0];
            float angle_y = -angel[1];
            float angle_z = angel[2];

            float A =(float) Math.Cos ( angle_x );
            float B =(float) Math.Sin ( angle_x );
            float C = (float)Math.Cos ( angle_y );
            float D = (float)Math.Sin ( angle_y );
            float E = (float)Math.Cos ( angle_z );
            float F = (float)Math.Sin ( angle_z );

            float AD = A * D;
            float BD = B * D;

            mat[0] = C * E;
            mat[1] = -C * F;
            mat[2] = -D;
            mat[3] = -BD * E + A * F;
            mat[4] = BD * F + A * E;
            mat[5] = -B * C;
            mat[6] = AD * E + B * F;
            mat[7] = -AD * F + B * E;
            mat[8] = A * C;
            return mat;

        }
        public static float[] getRotaionMatrixFromAngle2( double[] angel )
        {
            float[] mat = new float[9];
            double angle_x = angel[0];
            double angle_y = -angel[1];
            double angle_z = angel[2];

            double A = (double)Math.Cos ( angle_x );
            double B = (double)Math.Sin ( angle_x );
            double C = (double)Math.Cos ( angle_y );
            double D = (double)Math.Sin ( angle_y );
            double E = (double)Math.Cos ( angle_z );
            double F = (double)Math.Sin ( angle_z );

            double AD = A * D;
            double BD = B * D;

            mat[0] = (float) (C * E);
            mat[1] = (float)(-C * F);
            mat[2] = (float)-D;
            mat[3] = (float)(-BD * E + A * F);
            mat[4] = (float)(BD * F + A * E);
            mat[5] = (float)(-B * C);
            mat[6] = (float)(AD * E + B * F);
            mat[7] = (float)(-AD * F + B * E);
            mat[8] = (float)(A * C);
            return  mat;

        }
        public static float[] getRotaionMatrixFromAngle3( double[] angel )
        {
            float[] mat = new float[9];
            double angle_x = angel[0]*Math.PI/180;
            double angle_y = -angel[1]*angel[0]*Math.PI/180;
            double angle_z = angel[2]*angel[0]*Math.PI/180;

            double A = (double)Math.Cos ( angle_x );
            double B = (double)Math.Sin ( angle_x );
            double C = (double)Math.Cos ( angle_y );
            double D = (double)Math.Sin ( angle_y );
            double E = (double)Math.Cos ( angle_z );
            double F = (double)Math.Sin ( angle_z );

            double AD = A * D;
            double BD = B * D;

            mat[0] = (float)( C * E );
            mat[1] = (float)( -C * F );
            mat[2] = (float)-D;
            mat[3] = (float)( -BD * E + A * F );
            mat[4] = (float)( BD * F + A * E );
            mat[5] = (float)( -B * C );
            mat[6] = (float)( AD * E + B * F );
            mat[7] = (float)( -AD * F + B * E );
            mat[8] = (float)( A * C );
            return mat;

        }
        public static MatrixLibrary.Matrix getQuaternionFromAngles2(MatrixLibrary.Matrix angle)
        {
            /*
             Example
            we take the 90 degree rotation from this: 		to this: 	

                As shown here the axis angle for this rotation is:

                heading = 0 degrees
                bank = 90 degrees
                attitude = 0 degrees

                so substituting this in the above formula gives:

                  c1 = cos(heading / 2) = 1
                 c2 = cos(attitude / 2) = 1
              c3 = cos(bank / 2) = 0.7071
                 s1 = sin(heading / 2) = 0
                 s2 = sin(attitude / 2) = 0
                s3 = sin(bank / 2) = 0.7071

            w = c1 c2 c3 - s1 s2 s3 = 0.7071	
                x = s1 s2 c3 +c1 c2 s3 = 0.7071
                y = s1 c2 c3 + c1 s2 s3 = 0
                z = c1 s2 c3 - s1 c2 s3 = 0

                which gives the quaternion 0.7071 + i 0.7071
             */
            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(4, 1);
            angle = (angle * Math.PI) / 180.0;
            double c1 = Math.Cos(angle[2, 0] / 2);
            double c2 = Math.Cos(angle[1, 0] / 2);
            double c3 = Math.Cos(angle[0, 0] / 2);

            double s1 = Math.Sin(angle[2, 0] / 2);
            double s2 = Math.Sin(angle[1, 0] / 2);
            double s3 = Math.Sin(angle[0, 0] / 2);

            result[0, 0] = c1 * c2 * c3 - s1 * s2 * s3;
            result[1, 0] = s1 * s2 * c3 + c1 * c2 * s3;
            result[2, 0] = s1 * c2 * c3 + c1 * s2 * s3;
            result[3, 0] = c1 * s2 * c3 - s1 * c2 * s3;

            return result;
        }
        public static MatrixLibrary.Matrix getRotationMatrixFromQuaternion2(MatrixLibrary.Matrix Quaternion)
        {
            MatrixLibrary.Matrix result = new MatrixLibrary.Matrix(1, 9);
            result[0, 0] = -1.0f + 2.0f * Quaternion[0, 0] * Quaternion[0, 0] +2.0f * Quaternion[0, 1] * Quaternion[0, 1];
            result[0, 1] = 2.0f * Quaternion[0, 1] * Quaternion[0, 2] +  2.0f * Quaternion[0, 3] * Quaternion[0, 0];
            result[0, 2] = 2.0f * Quaternion[0, 1] * Quaternion[0, 3] - 2.0f * Quaternion[0, 2] * Quaternion[0, 0];
            result[0, 3] = 2.0f * Quaternion[0, 1] * Quaternion[0, 2] - 2.0f * Quaternion[0, 3] * Quaternion[0, 0];
            result[0, 4] = -1.0f +2.0f * Quaternion[0, 0] * Quaternion[0, 0] + 2.0f * Quaternion[0, 2] * Quaternion[0, 2];
            result[0, 5] = 2.0f * Quaternion[0, 2] * Quaternion[0, 3] + 2.0f * Quaternion[0, 1] * Quaternion[0, 0];
            result[0, 6] = 2.0f * Quaternion[0, 1] * Quaternion[0, 3] + 2.0f * Quaternion[0, 2] * Quaternion[0, 0];
            result[0, 7] = 2.0f * Quaternion[0, 2] * Quaternion[0, 3] - 2.0f * Quaternion[0, 1] * Quaternion[0, 0];
            result[0, 8] = -1.0f +2.0f * Quaternion[0, 0] * Quaternion[0, 0] + 2.0f * Quaternion[0, 3] * Quaternion[0,3 ];
            return result;
        }
        public static float[] getRotationMatrixFromQuaternion3( float[] Quaternion )
        {
            float[]  result = new float[9];
            result[ 0] = -1.0f + 2.0f * Quaternion[ 0] * Quaternion[ 0] + 2.0f * Quaternion[1] * Quaternion[1];
            result[1] = 2.0f * Quaternion[ 1] * Quaternion[ 2] + 2.0f * Quaternion[3] * Quaternion[ 0];
            result[ 2] = 2.0f * Quaternion[ 1] * Quaternion[ 3] - 2.0f * Quaternion[2] * Quaternion[ 0];
            result[3] = 2.0f * Quaternion[1] * Quaternion[2] - 2.0f * Quaternion[ 3] * Quaternion[0];
            result[4] = -1.0f + 2.0f * Quaternion[ 0] * Quaternion[0] + 2.0f * Quaternion[2] * Quaternion[ 2];
            result[5] = 2.0f * Quaternion[2] * Quaternion[3] + 2.0f * Quaternion[1] * Quaternion[0];
            result[6] = 2.0f * Quaternion[ 1] * Quaternion[ 3] + 2.0f * Quaternion[2] * Quaternion[0];
            result[7] = 2.0f * Quaternion[2] * Quaternion[3] - 2.0f * Quaternion[1] * Quaternion[0];
            result[8] = -1.0f + 2.0f * Quaternion[ 0] * Quaternion[0] + 2.0f * Quaternion[ 3] * Quaternion[ 3];
            return result;
        }

        //
        //
        //
        //Input Radian
        //
        public static float[] getQuaternionFromAngles3( float[] angle )
        {
            /*
             Example
            we take the 90 degree rotation from this: 		to this: 	

                As shown here the axis angle for this rotation is:

                heading = 0 degrees
                bank = 90 degrees
                attitude = 0 degrees

                so substituting this in the above formula gives:

                  c1 = cos(heading / 2) = 1
                 c2 = cos(attitude / 2) = 1
              c3 = cos(bank / 2) = 0.7071
                 s1 = sin(heading / 2) = 0
                 s2 = sin(attitude / 2) = 0
                s3 = sin(bank / 2) = 0.7071

            w = c1 c2 c3 - s1 s2 s3 = 0.7071	
                x = s1 s2 c3 +c1 c2 s3 = 0.7071
                y = s1 c2 c3 + c1 s2 s3 = 0
                z = c1 s2 c3 - s1 c2 s3 = 0

                which gives the quaternion 0.7071 + i 0.7071
             */
            float[] result = new float[4];
            angle[0] = ( angle[0] );
            angle[1] = -( angle[1] );
            angle[2] = -( angle[2] );
            float c1 = (float)Math.Cos ( angle[1] / 2 );
            float c2 = (float)Math.Cos ( angle[2] / 2 );
            float c3 = (float)Math.Cos ( angle[0] / 2 );

            double s1 = Math.Sin ( angle[1] / 2 );
            double s2 = Math.Sin ( angle[2] / 2 );
            double s3 = Math.Sin ( angle[0] / 2 );

            result[0] = (float)(c1 * c2 * c3 - s1 * s2 * s3);
            result[1] =(float)( s1 * s2 * c3 - c1 * c2 * s3);
            result[2] = (float)(s1 * c2 * c3 + c1 * s2 * s3);
            result[3] = (float)(c1 * s2 * c3 - s1 * c2 * s3);
            return result;
        }
        public static double[] getAngleFromRotation( MatrixLibrary.Matrix m )
        {
            /** this conversion uses conventions as described on page:
*   http://www.euclideanspace.com/maths/geometry/rotations/euler/index.htm
*   Coordinate System: right hand
*   Positive angle: right hand
*   Order of euler angles: heading first, then attitude, then bank
*   matrix row column ordering:
*   [m[0,0] m[0,1] m[0,2]]
*   [m[1,0] m[1,1] m[1,2]]
*   [m[2,0] m[2,1] m[2,2]]*/
    // Assuming the angles are in radians.
    double yaw=0, pitch=0, roll=0 ;
	if (m[1,0] > 0.998) { // singularity at north pole
		yaw = Math.Atan2(m[0,2],m[2,2]);
		pitch= Math.PI/2;
		roll = 0;
        return new double[] { roll, pitch, yaw };
	}
	if (m[1,0] < -0.998) { // singularity at south pole
		yaw= Math.Atan2(m[0,2],m[2,2]);
		pitch = -Math.PI/2;
		roll = 0;
		return new double[] { roll, pitch, yaw};
	}
	   yaw = Math.Atan2(-m[2,0],m[0,0]);
	   roll = Math.Atan2(-m[1,2],m[1,1]);
	   pitch= Math.Asin(m[1,0]);
       return new double[] { roll, yaw, pitch};
    }
    
}
}
