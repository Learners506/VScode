namespace WinFormsApp1
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            buttonimport = new Button();
            comboBox1 = new ComboBox();
            groupBox1 = new GroupBox();
            textBoxWeight = new TextBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            textBoxZJJ = new TextBox();
            label14 = new Label();
            textBoxLZJ = new TextBox();
            label13 = new Label();
            textBoxJC = new TextBox();
            label5 = new Label();
            textBoxJK = new TextBox();
            label6 = new Label();
            textBoxJG = new TextBox();
            label7 = new Label();
            textBoxMC = new TextBox();
            label8 = new Label();
            groupBox3 = new GroupBox();
            textBoxM = new TextBox();
            label10 = new Label();
            textBoxFy = new TextBox();
            label11 = new Label();
            textBoxFx = new TextBox();
            label12 = new Label();
            groupBox4 = new GroupBox();
            textBoxBR = new TextBox();
            label15 = new Label();
            textBoxKBXS = new TextBox();
            label16 = new Label();
            textBoxZBL = new TextBox();
            label17 = new Label();
            groupBox5 = new GroupBox();
            textBoxHR = new TextBox();
            label18 = new Label();
            textBoxHYXS = new TextBox();
            label2 = new Label();
            textBoxHDL = new TextBox();
            label3 = new Label();
            textBoxKHL = new TextBox();
            label4 = new Label();
            label9 = new Label();
            groupBox6 = new GroupBox();
            textBoxQR = new TextBox();
            label19 = new Label();
            textBoxQFXS = new TextBox();
            label20 = new Label();
            textBoxQFL = new TextBox();
            label21 = new Label();
            textBoxKQF = new TextBox();
            label22 = new Label();
            button2 = new Button();
            button3 = new Button();
            button4 = new Button();
            groupBox1.SuspendLayout();
            groupBox2.SuspendLayout();
            groupBox3.SuspendLayout();
            groupBox4.SuspendLayout();
            groupBox5.SuspendLayout();
            groupBox6.SuspendLayout();
            SuspendLayout();
            // 
            // buttonimport
            // 
            buttonimport.Location = new Point(382, 18);
            buttonimport.Name = "buttonimport";
            buttonimport.Size = new Size(103, 25);
            buttonimport.TabIndex = 0;
            buttonimport.Text = "导入文件";
            buttonimport.UseVisualStyleBackColor = true;
            buttonimport.Click += button1_Click;
            // 
            // comboBox1
            // 
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new Point(101, 19);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new Size(235, 25);
            comboBox1.TabIndex = 1;
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(textBoxWeight);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 427);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(235, 61);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "稳定性复核";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // textBoxWeight
            // 
            textBoxWeight.Location = new Point(115, 26);
            textBoxWeight.Name = "textBoxWeight";
            textBoxWeight.Size = new Size(100, 23);
            textBoxWeight.TabIndex = 1;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(22, 29);
            label1.Name = "label1";
            label1.Size = new Size(80, 17);
            label1.TabIndex = 0;
            label1.Text = "条形基础自重";
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(textBoxZJJ);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(textBoxLZJ);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(textBoxJC);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(textBoxJK);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(textBoxJG);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(textBoxMC);
            groupBox2.Controls.Add(label8);
            groupBox2.Location = new Point(12, 62);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(235, 211);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "基础相关";
            groupBox2.Enter += groupBox1_Enter;
            // 
            // textBoxZJJ
            // 
            textBoxZJJ.Location = new Point(115, 171);
            textBoxZJJ.Name = "textBoxZJJ";
            textBoxZJJ.Size = new Size(100, 23);
            textBoxZJJ.TabIndex = 1;
            // 
            // label14
            // 
            label14.AutoSize = true;
            label14.Location = new Point(22, 174);
            label14.Name = "label14";
            label14.Size = new Size(80, 17);
            label14.TabIndex = 0;
            label14.Text = "支架纵向间距";
            // 
            // textBoxLZJ
            // 
            textBoxLZJ.Location = new Point(115, 142);
            textBoxLZJ.Name = "textBoxLZJ";
            textBoxLZJ.Size = new Size(100, 23);
            textBoxLZJ.TabIndex = 1;
            // 
            // label13
            // 
            label13.AutoSize = true;
            label13.Location = new Point(22, 145);
            label13.Name = "label13";
            label13.Size = new Size(80, 17);
            label13.TabIndex = 0;
            label13.Text = "前后立柱间距";
            // 
            // textBoxJC
            // 
            textBoxJC.Location = new Point(115, 113);
            textBoxJC.Name = "textBoxJC";
            textBoxJC.Size = new Size(100, 23);
            textBoxJC.TabIndex = 1;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(22, 116);
            label5.Name = "label5";
            label5.Size = new Size(80, 17);
            label5.TabIndex = 0;
            label5.Text = "矩形基础长度";
            // 
            // textBoxJK
            // 
            textBoxJK.Location = new Point(115, 84);
            textBoxJK.Name = "textBoxJK";
            textBoxJK.Size = new Size(100, 23);
            textBoxJK.TabIndex = 1;
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(22, 87);
            label6.Name = "label6";
            label6.Size = new Size(80, 17);
            label6.TabIndex = 0;
            label6.Text = "矩形基础宽度";
            // 
            // textBoxJG
            // 
            textBoxJG.Location = new Point(115, 55);
            textBoxJG.Name = "textBoxJG";
            textBoxJG.Size = new Size(100, 23);
            textBoxJG.TabIndex = 1;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(22, 58);
            label7.Name = "label7";
            label7.Size = new Size(80, 17);
            label7.TabIndex = 0;
            label7.Text = "矩形基础高度";
            // 
            // textBoxMC
            // 
            textBoxMC.Location = new Point(115, 26);
            textBoxMC.Name = "textBoxMC";
            textBoxMC.Size = new Size(100, 23);
            textBoxMC.TabIndex = 1;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(22, 29);
            label8.Name = "label8";
            label8.Size = new Size(80, 17);
            label8.TabIndex = 0;
            label8.Text = "底面摩擦系数";
            // 
            // groupBox3
            // 
            groupBox3.Controls.Add(textBoxM);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(textBoxFy);
            groupBox3.Controls.Add(label11);
            groupBox3.Controls.Add(textBoxFx);
            groupBox3.Controls.Add(label12);
            groupBox3.Location = new Point(12, 279);
            groupBox3.Name = "groupBox3";
            groupBox3.Size = new Size(235, 128);
            groupBox3.TabIndex = 2;
            groupBox3.TabStop = false;
            groupBox3.Text = "柱脚荷载";
            groupBox3.Enter += groupBox1_Enter;
            // 
            // textBoxM
            // 
            textBoxM.Location = new Point(115, 84);
            textBoxM.Name = "textBoxM";
            textBoxM.Size = new Size(100, 23);
            textBoxM.TabIndex = 1;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new Point(22, 87);
            label10.Name = "label10";
            label10.Size = new Size(32, 17);
            label10.TabIndex = 0;
            label10.Text = "弯矩";
            // 
            // textBoxFy
            // 
            textBoxFy.Location = new Point(115, 55);
            textBoxFy.Name = "textBoxFy";
            textBoxFy.Size = new Size(100, 23);
            textBoxFy.TabIndex = 1;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Location = new Point(22, 58);
            label11.Name = "label11";
            label11.Size = new Size(44, 17);
            label11.TabIndex = 0;
            label11.Text = "拔力Fy";
            // 
            // textBoxFx
            // 
            textBoxFx.Location = new Point(115, 26);
            textBoxFx.Name = "textBoxFx";
            textBoxFx.Size = new Size(100, 23);
            textBoxFx.TabIndex = 1;
            // 
            // label12
            // 
            label12.AutoSize = true;
            label12.Location = new Point(22, 29);
            label12.Name = "label12";
            label12.Size = new Size(56, 17);
            label12.TabIndex = 0;
            label12.Text = "水平力Fx";
            // 
            // groupBox4
            // 
            groupBox4.Controls.Add(textBoxBR);
            groupBox4.Controls.Add(label15);
            groupBox4.Controls.Add(textBoxKBXS);
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(textBoxZBL);
            groupBox4.Controls.Add(label17);
            groupBox4.Location = new Point(288, 62);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(254, 112);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            groupBox4.Text = "抗拔稳定验算";
            groupBox4.Enter += groupBox1_Enter;
            // 
            // textBoxBR
            // 
            textBoxBR.Location = new Point(137, 84);
            textBoxBR.Name = "textBoxBR";
            textBoxBR.Size = new Size(100, 23);
            textBoxBR.TabIndex = 1;
            // 
            // label15
            // 
            label15.AutoSize = true;
            label15.Location = new Point(22, 87);
            label15.Name = "label15";
            label15.Size = new Size(56, 17);
            label15.TabIndex = 0;
            label15.Text = "是否满足";
            // 
            // textBoxKBXS
            // 
            textBoxKBXS.Location = new Point(137, 55);
            textBoxKBXS.Name = "textBoxKBXS";
            textBoxKBXS.Size = new Size(100, 23);
            textBoxKBXS.TabIndex = 1;
            // 
            // label16
            // 
            label16.AutoSize = true;
            label16.Location = new Point(22, 58);
            label16.Name = "label16";
            label16.Size = new Size(80, 17);
            label16.TabIndex = 0;
            label16.Text = "抗拔稳定系数";
            // 
            // textBoxZBL
            // 
            textBoxZBL.Location = new Point(137, 26);
            textBoxZBL.Name = "textBoxZBL";
            textBoxZBL.Size = new Size(100, 23);
            textBoxZBL.TabIndex = 1;
            // 
            // label17
            // 
            label17.AutoSize = true;
            label17.Location = new Point(22, 29);
            label17.Name = "label17";
            label17.Size = new Size(44, 17);
            label17.TabIndex = 0;
            label17.Text = "总拔力";
            // 
            // groupBox5
            // 
            groupBox5.Controls.Add(textBoxHR);
            groupBox5.Controls.Add(label18);
            groupBox5.Controls.Add(textBoxHYXS);
            groupBox5.Controls.Add(label2);
            groupBox5.Controls.Add(textBoxHDL);
            groupBox5.Controls.Add(label3);
            groupBox5.Controls.Add(textBoxKHL);
            groupBox5.Controls.Add(label4);
            groupBox5.Location = new Point(288, 190);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(254, 146);
            groupBox5.TabIndex = 2;
            groupBox5.TabStop = false;
            groupBox5.Text = "抗滑移稳定验算";
            groupBox5.Enter += groupBox1_Enter;
            // 
            // textBoxHR
            // 
            textBoxHR.Location = new Point(137, 112);
            textBoxHR.Name = "textBoxHR";
            textBoxHR.Size = new Size(100, 23);
            textBoxHR.TabIndex = 1;
            // 
            // label18
            // 
            label18.AutoSize = true;
            label18.Location = new Point(22, 115);
            label18.Name = "label18";
            label18.Size = new Size(56, 17);
            label18.TabIndex = 0;
            label18.Text = "是否满足";
            // 
            // textBoxHYXS
            // 
            textBoxHYXS.Location = new Point(137, 84);
            textBoxHYXS.Name = "textBoxHYXS";
            textBoxHYXS.Size = new Size(100, 23);
            textBoxHYXS.TabIndex = 1;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(22, 87);
            label2.Name = "label2";
            label2.Size = new Size(92, 17);
            label2.TabIndex = 0;
            label2.Text = "抗滑移稳定系数";
            // 
            // textBoxHDL
            // 
            textBoxHDL.Location = new Point(137, 55);
            textBoxHDL.Name = "textBoxHDL";
            textBoxHDL.Size = new Size(100, 23);
            textBoxHDL.TabIndex = 1;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(22, 58);
            label3.Name = "label3";
            label3.Size = new Size(56, 17);
            label3.TabIndex = 0;
            label3.Text = "总滑动力";
            // 
            // textBoxKHL
            // 
            textBoxKHL.Location = new Point(137, 26);
            textBoxKHL.Name = "textBoxKHL";
            textBoxKHL.Size = new Size(100, 23);
            textBoxKHL.TabIndex = 1;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(22, 29);
            label4.Name = "label4";
            label4.Size = new Size(56, 17);
            label4.TabIndex = 0;
            label4.Text = "总抗滑力";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new Point(33, 23);
            label9.Name = "label9";
            label9.Size = new Size(32, 17);
            label9.TabIndex = 3;
            label9.Text = "工况";
            // 
            // groupBox6
            // 
            groupBox6.Controls.Add(textBoxQR);
            groupBox6.Controls.Add(label19);
            groupBox6.Controls.Add(textBoxQFXS);
            groupBox6.Controls.Add(label20);
            groupBox6.Controls.Add(textBoxQFL);
            groupBox6.Controls.Add(label21);
            groupBox6.Controls.Add(textBoxKQF);
            groupBox6.Controls.Add(label22);
            groupBox6.Location = new Point(288, 342);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(254, 146);
            groupBox6.TabIndex = 2;
            groupBox6.TabStop = false;
            groupBox6.Text = "抗倾覆稳定验算";
            groupBox6.Enter += groupBox1_Enter;
            // 
            // textBoxQR
            // 
            textBoxQR.Location = new Point(137, 112);
            textBoxQR.Name = "textBoxQR";
            textBoxQR.Size = new Size(100, 23);
            textBoxQR.TabIndex = 1;
            // 
            // label19
            // 
            label19.AutoSize = true;
            label19.Location = new Point(22, 115);
            label19.Name = "label19";
            label19.Size = new Size(56, 17);
            label19.TabIndex = 0;
            label19.Text = "是否满足";
            // 
            // textBoxQFXS
            // 
            textBoxQFXS.Location = new Point(137, 84);
            textBoxQFXS.Name = "textBoxQFXS";
            textBoxQFXS.Size = new Size(100, 23);
            textBoxQFXS.TabIndex = 1;
            // 
            // label20
            // 
            label20.AutoSize = true;
            label20.Location = new Point(22, 87);
            label20.Name = "label20";
            label20.Size = new Size(92, 17);
            label20.TabIndex = 0;
            label20.Text = "抗倾覆稳定系数";
            // 
            // textBoxQFL
            // 
            textBoxQFL.Location = new Point(137, 55);
            textBoxQFL.Name = "textBoxQFL";
            textBoxQFL.Size = new Size(100, 23);
            textBoxQFL.TabIndex = 1;
            // 
            // label21
            // 
            label21.AutoSize = true;
            label21.Location = new Point(22, 58);
            label21.Name = "label21";
            label21.Size = new Size(56, 17);
            label21.TabIndex = 0;
            label21.Text = "总倾覆力";
            // 
            // textBoxKQF
            // 
            textBoxKQF.Location = new Point(137, 26);
            textBoxKQF.Name = "textBoxKQF";
            textBoxKQF.Size = new Size(100, 23);
            textBoxKQF.TabIndex = 1;
            // 
            // label22
            // 
            label22.AutoSize = true;
            label22.Location = new Point(22, 29);
            label22.Name = "label22";
            label22.Size = new Size(68, 17);
            label22.TabIndex = 0;
            label22.Text = "总抗倾覆力";
            // 
            // button2
            // 
            button2.Location = new Point(15, 510);
            button2.Name = "button2";
            button2.Size = new Size(75, 30);
            button2.TabIndex = 4;
            button2.Text = "缺省参数";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // button3
            // 
            button3.Location = new Point(261, 510);
            button3.Name = "button3";
            button3.Size = new Size(75, 30);
            button3.TabIndex = 4;
            button3.Text = "导出结果";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button4
            // 
            button4.Location = new Point(141, 510);
            button4.Name = "button4";
            button4.Size = new Size(75, 30);
            button4.TabIndex = 4;
            button4.Text = "开始验算";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 17F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(568, 561);
            Controls.Add(button3);
            Controls.Add(button4);
            Controls.Add(button2);
            Controls.Add(label9);
            Controls.Add(groupBox3);
            Controls.Add(groupBox2);
            Controls.Add(groupBox6);
            Controls.Add(groupBox5);
            Controls.Add(groupBox4);
            Controls.Add(groupBox1);
            Controls.Add(comboBox1);
            Controls.Add(buttonimport);
            Name = "Form1";
            Text = "单桩矩形桥墩基础稳定验算";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            groupBox2.ResumeLayout(false);
            groupBox2.PerformLayout();
            groupBox3.ResumeLayout(false);
            groupBox3.PerformLayout();
            groupBox4.ResumeLayout(false);
            groupBox4.PerformLayout();
            groupBox5.ResumeLayout(false);
            groupBox5.PerformLayout();
            groupBox6.ResumeLayout(false);
            groupBox6.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button buttonimport;
        private ComboBox comboBox1;
        private GroupBox groupBox1;
        private TextBox textBoxWeight;
        private Label label1;
        private GroupBox groupBox2;
        private TextBox textBoxJC;
        private Label label5;
        private TextBox textBoxJK;
        private Label label6;
        private TextBox textBoxJG;
        private Label label7;
        private TextBox textBoxMC;
        private Label label8;
        private GroupBox groupBox3;
        private TextBox textBoxM;
        private Label label10;
        private TextBox textBoxFy;
        private Label label11;
        private TextBox textBoxFx;
        private Label label12;
        private TextBox textBoxZJJ;
        private Label label14;
        private TextBox textBoxLZJ;
        private Label label13;
        private GroupBox groupBox4;
        private TextBox textBoxBR;
        private Label label15;
        private TextBox textBoxKBXS;
        private Label label16;
        private TextBox textBoxZBL;
        private Label label17;
        private GroupBox groupBox5;
        private TextBox textBoxHYXS;
        private Label label2;
        private TextBox textBoxHDL;
        private Label label3;
        private TextBox textBoxKHL;
        private Label label4;
        private Label label9;
        private TextBox textBoxHR;
        private Label label18;
        private GroupBox groupBox6;
        private TextBox textBoxQR;
        private Label label19;
        private TextBox textBoxQFXS;
        private Label label20;
        private TextBox textBoxQFL;
        private Label label21;
        private TextBox textBoxKQF;
        private Label label22;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
