using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IMUdemo
{
    // computes differnce equation output a_0*y*z^-0 = b_0*x*z^-0 + b_1*x*z^-1 + ... - a_1*y*z^-1 - a_2*y*z^-2 - ...
    // where a_0 is normalised.

    class IIRfilter
    {
        private double[] a;         // denominator coefficents (a[0] = a_0)
        private double[] b;         // numerator coefficents (b[0] = b_0)
        private double[] x;         // past inputs (x[0] = xz^-0)
        private double[] y;         // past outputs (y[0] = yz^-0)

        public IIRfilter(double[] bCoef, double[] aCoef)
        {
            a = aCoef;                                                      // store filter coefficents
            b = bCoef;
            y = new double[a.Length];                                       // create arrays
            x = new double[b.Length];
            for (int i = 0; i < b.Length; i++) b[i] = bCoef[i] / aCoef[0];  // normalise numerator coefficents
            for (int i = 0; i < a.Length; i++) a[i] = aCoef[i] / aCoef[0];  // normalise denominator coefficents
        }

        public double step(double input)
        {
            double output = 0;                      // initialise running sum output
            for (int i = x.Length - 1; i > 0; i--)
            {
                x[i] = x[i - 1];
                output += b[i] * x[i];
            }
            x[0] = input;
            output += b[0] * x[0];
            for (int i = y.Length - 1; i > 0; i--)
            {
                y[i] = y[i - 1];
                output -= a[i] * y[i];
            }
            y[0] = output;
            return output;
        }
    }
}