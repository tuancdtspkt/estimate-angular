namespace WindowsFormsApplication1
{
    partial class Form_3Dcuboid
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.simpleOpenGlControl = new Tao.Platform.Windows.SimpleOpenGlControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.DrawLabel = new System.Windows.Forms.LinkLabel();
            this.Ax = new System.Windows.Forms.Label();
            this.Ay = new System.Windows.Forms.Label();
            this.Az = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.DataAx = new System.Windows.Forms.Label();
            this.DatAx = new System.Windows.Forms.Label();
            this.DatAy = new System.Windows.Forms.Label();
            this.DatAz = new System.Windows.Forms.Label();
            this.DatGx = new System.Windows.Forms.Label();
            this.DatGy = new System.Windows.Forms.Label();
            this.DatGz = new System.Windows.Forms.Label();
            this.lab2 = new System.Windows.Forms.Label();
            this.lab3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.DatZ = new System.Windows.Forms.Label();
            this.DatY = new System.Windows.Forms.Label();
            this.DatX = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // simpleOpenGlControl
            // 
            this.simpleOpenGlControl.AccumBits = ((byte)(0));
            this.simpleOpenGlControl.AutoCheckErrors = false;
            this.simpleOpenGlControl.AutoFinish = false;
            this.simpleOpenGlControl.AutoMakeCurrent = true;
            this.simpleOpenGlControl.AutoSwapBuffers = true;
            this.simpleOpenGlControl.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.simpleOpenGlControl.ColorBits = ((byte)(32));
            this.simpleOpenGlControl.DepthBits = ((byte)(16));
            this.simpleOpenGlControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.simpleOpenGlControl.ForeColor = System.Drawing.SystemColors.ActiveBorder;
            this.simpleOpenGlControl.Location = new System.Drawing.Point(0, 0);
            this.simpleOpenGlControl.Name = "simpleOpenGlControl";
            this.simpleOpenGlControl.Size = new System.Drawing.Size(491, 298);
            this.simpleOpenGlControl.StencilBits = ((byte)(0));
            this.simpleOpenGlControl.TabIndex = 0;
            this.simpleOpenGlControl.Load += new System.EventHandler(this.simpleOpenGlControl_Load);
            this.simpleOpenGlControl.SizeChanged += new System.EventHandler(this.simpleOpenGlControl_SizeChanged);
            this.simpleOpenGlControl.Paint += new System.Windows.Forms.PaintEventHandler(this.simpleOpenGlControl_Paint);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // DrawLabel
            // 
            this.DrawLabel.AutoSize = true;
            this.DrawLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawLabel.ForeColor = System.Drawing.Color.White;
            this.DrawLabel.LinkColor = System.Drawing.SystemColors.ButtonHighlight;
            this.DrawLabel.Location = new System.Drawing.Point(164, 272);
            this.DrawLabel.Name = "DrawLabel";
            this.DrawLabel.Size = new System.Drawing.Size(118, 17);
            this.DrawLabel.TabIndex = 1;
            this.DrawLabel.TabStop = true;
            this.DrawLabel.Text = "NAME SYSTEM";
            // 
            // Ax
            // 
            this.Ax.AutoSize = true;
            this.Ax.BackColor = System.Drawing.Color.Black;
            this.Ax.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ax.ForeColor = System.Drawing.Color.White;
            this.Ax.Location = new System.Drawing.Point(-2, 9);
            this.Ax.Name = "Ax";
            this.Ax.Size = new System.Drawing.Size(60, 17);
            this.Ax.TabIndex = 2;
            this.Ax.Text = "Ax(g) =";
            // 
            // Ay
            // 
            this.Ay.AutoSize = true;
            this.Ay.BackColor = System.Drawing.Color.Black;
            this.Ay.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Ay.ForeColor = System.Drawing.Color.White;
            this.Ay.Location = new System.Drawing.Point(-2, 29);
            this.Ay.Name = "Ay";
            this.Ay.Size = new System.Drawing.Size(66, 17);
            this.Ay.TabIndex = 3;
            this.Ay.Text = "Ay(g) = ";
            // 
            // Az
            // 
            this.Az.AutoSize = true;
            this.Az.BackColor = System.Drawing.Color.Black;
            this.Az.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Az.ForeColor = System.Drawing.Color.White;
            this.Az.Location = new System.Drawing.Point(-2, 49);
            this.Az.Name = "Az";
            this.Az.Size = new System.Drawing.Size(66, 17);
            this.Az.TabIndex = 4;
            this.Az.Text = "Az(g) = ";
            this.Az.Click += new System.EventHandler(this.Az_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(-2, 220);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Gx(rad/s) = ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(-2, 237);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Gy(rad/s) =";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Black;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(-2, 254);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "Gz(rad/s)= ";
            // 
            // DataAx
            // 
            this.DataAx.AutoSize = true;
            this.DataAx.BackColor = System.Drawing.Color.Black;
            this.DataAx.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataAx.ForeColor = System.Drawing.Color.White;
            this.DataAx.Location = new System.Drawing.Point(62, 9);
            this.DataAx.Name = "DataAx";
            this.DataAx.Size = new System.Drawing.Size(0, 20);
            this.DataAx.TabIndex = 8;
            // 
            // DatAx
            // 
            this.DatAx.AutoSize = true;
            this.DatAx.BackColor = System.Drawing.Color.Black;
            this.DatAx.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatAx.ForeColor = System.Drawing.Color.White;
            this.DatAx.Location = new System.Drawing.Point(60, 9);
            this.DatAx.Name = "DatAx";
            this.DatAx.Size = new System.Drawing.Size(25, 17);
            this.DatAx.TabIndex = 9;
            this.DatAx.Text = "Ax";
            // 
            // DatAy
            // 
            this.DatAy.AutoSize = true;
            this.DatAy.BackColor = System.Drawing.Color.Black;
            this.DatAy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatAy.ForeColor = System.Drawing.Color.White;
            this.DatAy.Location = new System.Drawing.Point(59, 29);
            this.DatAy.Name = "DatAy";
            this.DatAy.Size = new System.Drawing.Size(26, 17);
            this.DatAy.TabIndex = 10;
            this.DatAy.Text = "Ay";
            // 
            // DatAz
            // 
            this.DatAz.AutoSize = true;
            this.DatAz.BackColor = System.Drawing.Color.Black;
            this.DatAz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatAz.ForeColor = System.Drawing.Color.White;
            this.DatAz.Location = new System.Drawing.Point(59, 49);
            this.DatAz.Name = "DatAz";
            this.DatAz.Size = new System.Drawing.Size(26, 17);
            this.DatAz.TabIndex = 11;
            this.DatAz.Text = "Az";
            // 
            // DatGx
            // 
            this.DatGx.AutoSize = true;
            this.DatGx.BackColor = System.Drawing.Color.Black;
            this.DatGx.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatGx.ForeColor = System.Drawing.Color.White;
            this.DatGx.Location = new System.Drawing.Point(86, 220);
            this.DatGx.Name = "DatGx";
            this.DatGx.Size = new System.Drawing.Size(27, 17);
            this.DatGx.TabIndex = 12;
            this.DatGx.Text = "Gx";
            // 
            // DatGy
            // 
            this.DatGy.AutoSize = true;
            this.DatGy.BackColor = System.Drawing.Color.Black;
            this.DatGy.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatGy.ForeColor = System.Drawing.Color.White;
            this.DatGy.Location = new System.Drawing.Point(86, 237);
            this.DatGy.Name = "DatGy";
            this.DatGy.Size = new System.Drawing.Size(28, 17);
            this.DatGy.TabIndex = 13;
            this.DatGy.Text = "Gy";
            // 
            // DatGz
            // 
            this.DatGz.AutoSize = true;
            this.DatGz.BackColor = System.Drawing.Color.Black;
            this.DatGz.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatGz.ForeColor = System.Drawing.Color.White;
            this.DatGz.Location = new System.Drawing.Point(85, 254);
            this.DatGz.Name = "DatGz";
            this.DatGz.Size = new System.Drawing.Size(28, 17);
            this.DatGz.TabIndex = 14;
            this.DatGz.Text = "Gz";
            // 
            // lab2
            // 
            this.lab2.AutoSize = true;
            this.lab2.BackColor = System.Drawing.Color.Black;
            this.lab2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab2.ForeColor = System.Drawing.Color.White;
            this.lab2.Location = new System.Drawing.Point(331, 9);
            this.lab2.Name = "lab2";
            this.lab2.Size = new System.Drawing.Size(76, 17);
            this.lab2.TabIndex = 15;
            this.lab2.Text = "X(deg) = ";
            // 
            // lab3
            // 
            this.lab3.AutoSize = true;
            this.lab3.BackColor = System.Drawing.Color.Black;
            this.lab3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lab3.ForeColor = System.Drawing.Color.White;
            this.lab3.Location = new System.Drawing.Point(331, 29);
            this.lab3.Name = "lab3";
            this.lab3.Size = new System.Drawing.Size(76, 17);
            this.lab3.TabIndex = 16;
            this.lab3.Text = "Y(deg) = ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Black;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(331, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Z(deg) = ";
            // 
            // DatZ
            // 
            this.DatZ.AutoSize = true;
            this.DatZ.BackColor = System.Drawing.Color.Black;
            this.DatZ.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatZ.ForeColor = System.Drawing.Color.White;
            this.DatZ.Location = new System.Drawing.Point(398, 49);
            this.DatZ.Name = "DatZ";
            this.DatZ.Size = new System.Drawing.Size(18, 17);
            this.DatZ.TabIndex = 18;
            this.DatZ.Text = "Z";
            // 
            // DatY
            // 
            this.DatY.AutoSize = true;
            this.DatY.BackColor = System.Drawing.Color.Black;
            this.DatY.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatY.ForeColor = System.Drawing.Color.White;
            this.DatY.Location = new System.Drawing.Point(398, 29);
            this.DatY.Name = "DatY";
            this.DatY.Size = new System.Drawing.Size(18, 17);
            this.DatY.TabIndex = 19;
            this.DatY.Text = "Y";
            // 
            // DatX
            // 
            this.DatX.AutoSize = true;
            this.DatX.BackColor = System.Drawing.Color.Black;
            this.DatX.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DatX.ForeColor = System.Drawing.Color.White;
            this.DatX.Location = new System.Drawing.Point(398, 9);
            this.DatX.Name = "DatX";
            this.DatX.Size = new System.Drawing.Size(18, 17);
            this.DatX.TabIndex = 20;
            this.DatX.Text = "X";
            // 
            // Form_3Dcuboid
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(491, 298);
            this.Controls.Add(this.DatX);
            this.Controls.Add(this.DatY);
            this.Controls.Add(this.DatZ);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lab3);
            this.Controls.Add(this.lab2);
            this.Controls.Add(this.DatGz);
            this.Controls.Add(this.DatGy);
            this.Controls.Add(this.DatGx);
            this.Controls.Add(this.DatAz);
            this.Controls.Add(this.DatAy);
            this.Controls.Add(this.DatAx);
            this.Controls.Add(this.DataAx);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.Az);
            this.Controls.Add(this.Ay);
            this.Controls.Add(this.Ax);
            this.Controls.Add(this.DrawLabel);
            this.Controls.Add(this.simpleOpenGlControl);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Form_3Dcuboid";
            this.Text = "3D Cuboid";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form_3Dcuboid_FormClosing);
            this.Load += new System.EventHandler(this.Form_3Dcuboid_Load);
            this.VisibleChanged += new System.EventHandler(this.Form_3Dcuboid_VisibleChanged);
            this.ResumeLayout(false);
            this.PerformLayout();
            

        }

        #endregion

        private Tao.Platform.Windows.SimpleOpenGlControl simpleOpenGlControl;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.LinkLabel DrawLabel;
        private System.Windows.Forms.Label Ax;
        private System.Windows.Forms.Label Ay;
        private System.Windows.Forms.Label Az;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label DataAx;
        private System.Windows.Forms.Label DatAx;
        private System.Windows.Forms.Label DatAy;
        private System.Windows.Forms.Label DatAz;
        private System.Windows.Forms.Label DatGx;
        private System.Windows.Forms.Label DatGy;
        private System.Windows.Forms.Label DatGz;
        private System.Windows.Forms.Label lab2;
        private System.Windows.Forms.Label lab3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label DatZ;
        private System.Windows.Forms.Label DatY;
        private System.Windows.Forms.Label DatX;
    }
}