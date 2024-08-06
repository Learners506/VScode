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
            textBox1 = new TextBox();
            label1 = new Label();
            groupBox2 = new GroupBox();
            textBox14 = new TextBox();
            label14 = new Label();
            textBox13 = new TextBox();
            label13 = new Label();
            textBox5 = new TextBox();
            label5 = new Label();
            textBox6 = new TextBox();
            label6 = new Label();
            textBox7 = new TextBox();
            label7 = new Label();
            textBox8 = new TextBox();
            label8 = new Label();
            groupBox3 = new GroupBox();
            textBox10 = new TextBox();
            label10 = new Label();
            textBox11 = new TextBox();
            label11 = new Label();
            textBoxFx = new TextBox();
            label12 = new Label();
            groupBox4 = new GroupBox();
            textBox15 = new TextBox();
            label15 = new Label();
            textBox16 = new TextBox();
            label16 = new Label();
            textBox17 = new TextBox();
            label17 = new Label();
            groupBox5 = new GroupBox();
            textBox9 = new TextBox();
            label18 = new Label();
            textBox2 = new TextBox();
            label2 = new Label();
            textBox3 = new TextBox();
            label3 = new Label();
            textBox4 = new TextBox();
            label4 = new Label();
            label9 = new Label();
            groupBox6 = new GroupBox();
            textBox18 = new TextBox();
            label19 = new Label();
            textBox19 = new TextBox();
            label20 = new Label();
            textBox20 = new TextBox();
            label21 = new Label();
            textBox21 = new TextBox();
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
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label1);
            groupBox1.Location = new Point(12, 427);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(235, 61);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "稳定性复核";
            groupBox1.Enter += groupBox1_Enter;
            // 
            // textBox1
            // 
            textBox1.Location = new Point(115, 26);
            textBox1.Name = "textBox1";
            textBox1.Size = new Size(100, 23);
            textBox1.TabIndex = 1;
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
            groupBox2.Controls.Add(textBox14);
            groupBox2.Controls.Add(label14);
            groupBox2.Controls.Add(textBox13);
            groupBox2.Controls.Add(label13);
            groupBox2.Controls.Add(textBox5);
            groupBox2.Controls.Add(label5);
            groupBox2.Controls.Add(textBox6);
            groupBox2.Controls.Add(label6);
            groupBox2.Controls.Add(textBox7);
            groupBox2.Controls.Add(label7);
            groupBox2.Controls.Add(textBox8);
            groupBox2.Controls.Add(label8);
            groupBox2.Location = new Point(12, 62);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(235, 211);
            groupBox2.TabIndex = 2;
            groupBox2.TabStop = false;
            groupBox2.Text = "基础相关";
            groupBox2.Enter += groupBox1_Enter;
            // 
            // textBox14
            // 
            textBox14.Location = new Point(115, 171);
            textBox14.Name = "textBox14";
            textBox14.Size = new Size(100, 23);
            textBox14.TabIndex = 1;
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
            // textBox13
            // 
            textBox13.Location = new Point(115, 142);
            textBox13.Name = "textBox13";
            textBox13.Size = new Size(100, 23);
            textBox13.TabIndex = 1;
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
            // textBox5
            // 
            textBox5.Location = new Point(115, 113);
            textBox5.Name = "textBox5";
            textBox5.Size = new Size(100, 23);
            textBox5.TabIndex = 1;
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
            // textBox6
            // 
            textBox6.Location = new Point(115, 84);
            textBox6.Name = "textBox6";
            textBox6.Size = new Size(100, 23);
            textBox6.TabIndex = 1;
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
            // textBox7
            // 
            textBox7.Location = new Point(115, 55);
            textBox7.Name = "textBox7";
            textBox7.Size = new Size(100, 23);
            textBox7.TabIndex = 1;
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
            // textBox8
            // 
            textBox8.Location = new Point(115, 26);
            textBox8.Name = "textBox8";
            textBox8.Size = new Size(100, 23);
            textBox8.TabIndex = 1;
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
            groupBox3.Controls.Add(textBox10);
            groupBox3.Controls.Add(label10);
            groupBox3.Controls.Add(textBox11);
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
            // textBox10
            // 
            textBox10.Location = new Point(115, 84);
            textBox10.Name = "textBox10";
            textBox10.Size = new Size(100, 23);
            textBox10.TabIndex = 1;
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
            // textBox11
            // 
            textBox11.Location = new Point(115, 55);
            textBox11.Name = "textBox11";
            textBox11.Size = new Size(100, 23);
            textBox11.TabIndex = 1;
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
            groupBox4.Controls.Add(textBox15);
            groupBox4.Controls.Add(label15);
            groupBox4.Controls.Add(textBox16);
            groupBox4.Controls.Add(label16);
            groupBox4.Controls.Add(textBox17);
            groupBox4.Controls.Add(label17);
            groupBox4.Location = new Point(288, 62);
            groupBox4.Name = "groupBox4";
            groupBox4.Size = new Size(254, 112);
            groupBox4.TabIndex = 2;
            groupBox4.TabStop = false;
            groupBox4.Text = "抗拔稳定验算";
            groupBox4.Enter += groupBox1_Enter;
            // 
            // textBox15
            // 
            textBox15.Location = new Point(137, 84);
            textBox15.Name = "textBox15";
            textBox15.Size = new Size(100, 23);
            textBox15.TabIndex = 1;
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
            // textBox16
            // 
            textBox16.Location = new Point(137, 55);
            textBox16.Name = "textBox16";
            textBox16.Size = new Size(100, 23);
            textBox16.TabIndex = 1;
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
            // textBox17
            // 
            textBox17.Location = new Point(137, 26);
            textBox17.Name = "textBox17";
            textBox17.Size = new Size(100, 23);
            textBox17.TabIndex = 1;
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
            groupBox5.Controls.Add(textBox9);
            groupBox5.Controls.Add(label18);
            groupBox5.Controls.Add(textBox2);
            groupBox5.Controls.Add(label2);
            groupBox5.Controls.Add(textBox3);
            groupBox5.Controls.Add(label3);
            groupBox5.Controls.Add(textBox4);
            groupBox5.Controls.Add(label4);
            groupBox5.Location = new Point(288, 190);
            groupBox5.Name = "groupBox5";
            groupBox5.Size = new Size(254, 146);
            groupBox5.TabIndex = 2;
            groupBox5.TabStop = false;
            groupBox5.Text = "抗滑移稳定验算";
            groupBox5.Enter += groupBox1_Enter;
            // 
            // textBox9
            // 
            textBox9.Location = new Point(137, 112);
            textBox9.Name = "textBox9";
            textBox9.Size = new Size(100, 23);
            textBox9.TabIndex = 1;
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
            // textBox2
            // 
            textBox2.Location = new Point(137, 84);
            textBox2.Name = "textBox2";
            textBox2.Size = new Size(100, 23);
            textBox2.TabIndex = 1;
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
            // textBox3
            // 
            textBox3.Location = new Point(137, 55);
            textBox3.Name = "textBox3";
            textBox3.Size = new Size(100, 23);
            textBox3.TabIndex = 1;
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
            // textBox4
            // 
            textBox4.Location = new Point(137, 26);
            textBox4.Name = "textBox4";
            textBox4.Size = new Size(100, 23);
            textBox4.TabIndex = 1;
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
            groupBox6.Controls.Add(textBox18);
            groupBox6.Controls.Add(label19);
            groupBox6.Controls.Add(textBox19);
            groupBox6.Controls.Add(label20);
            groupBox6.Controls.Add(textBox20);
            groupBox6.Controls.Add(label21);
            groupBox6.Controls.Add(textBox21);
            groupBox6.Controls.Add(label22);
            groupBox6.Location = new Point(288, 342);
            groupBox6.Name = "groupBox6";
            groupBox6.Size = new Size(254, 146);
            groupBox6.TabIndex = 2;
            groupBox6.TabStop = false;
            groupBox6.Text = "抗倾覆稳定验算";
            groupBox6.Enter += groupBox1_Enter;
            // 
            // textBox18
            // 
            textBox18.Location = new Point(137, 112);
            textBox18.Name = "textBox18";
            textBox18.Size = new Size(100, 23);
            textBox18.TabIndex = 1;
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
            // textBox19
            // 
            textBox19.Location = new Point(137, 84);
            textBox19.Name = "textBox19";
            textBox19.Size = new Size(100, 23);
            textBox19.TabIndex = 1;
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
            // textBox20
            // 
            textBox20.Location = new Point(137, 55);
            textBox20.Name = "textBox20";
            textBox20.Size = new Size(100, 23);
            textBox20.TabIndex = 1;
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
            // textBox21
            // 
            textBox21.Location = new Point(137, 26);
            textBox21.Name = "textBox21";
            textBox21.Size = new Size(100, 23);
            textBox21.TabIndex = 1;
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
            // 
            // button3
            // 
            button3.Location = new Point(261, 510);
            button3.Name = "button3";
            button3.Size = new Size(75, 30);
            button3.TabIndex = 4;
            button3.Text = "导出结果";
            button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            button4.Location = new Point(141, 510);
            button4.Name = "button4";
            button4.Size = new Size(75, 30);
            button4.TabIndex = 4;
            button4.Text = "开始验算";
            button4.UseVisualStyleBackColor = true;
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
        private TextBox textBox1;
        private Label label1;
        private GroupBox groupBox2;
        private TextBox textBox5;
        private Label label5;
        private TextBox textBox6;
        private Label label6;
        private TextBox textBox7;
        private Label label7;
        private TextBox textBox8;
        private Label label8;
        private GroupBox groupBox3;
        private TextBox textBox10;
        private Label label10;
        private TextBox textBox11;
        private Label label11;
        private TextBox textBoxFx;
        private Label label12;
        private TextBox textBox14;
        private Label label14;
        private TextBox textBox13;
        private Label label13;
        private GroupBox groupBox4;
        private TextBox textBox15;
        private Label label15;
        private TextBox textBox16;
        private Label label16;
        private TextBox textBox17;
        private Label label17;
        private GroupBox groupBox5;
        private TextBox textBox2;
        private Label label2;
        private TextBox textBox3;
        private Label label3;
        private TextBox textBox4;
        private Label label4;
        private Label label9;
        private TextBox textBox9;
        private Label label18;
        private GroupBox groupBox6;
        private TextBox textBox18;
        private Label label19;
        private TextBox textBox19;
        private Label label20;
        private TextBox textBox20;
        private Label label21;
        private TextBox textBox21;
        private Label label22;
        private Button button2;
        private Button button3;
        private Button button4;
    }
}
