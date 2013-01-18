using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.IO;
//using System.Drawing.Imaging;
//using Tao.OpenGl;

namespace WindowsFormsApplication1
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
           
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Setting_Form()
        {
            Application.Run(new Form2());
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            //Form2 Set_form = new Form2();
            Thread Set_Form_Run = new Thread ( new ThreadStart (Setting_Form) );
            Set_Form_Run.Start();
            this.Close();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
        
    }
}
