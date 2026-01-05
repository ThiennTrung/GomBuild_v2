using System.Windows.Forms;

namespace GomBuild_v2
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolTip1 = new ToolTip(components);
            comboBox1 = new ComboBox();
            cbo_BuildVer = new ComboBox();
            cbo_Project = new ComboBox();
            tabPage2 = new TabPage();
            textBox8 = new TextBox();
            label11 = new Label();
            label8 = new Label();
            checkBox2 = new CheckBox();
            textBox4 = new TextBox();
            textBox3 = new TextBox();
            textBox2 = new TextBox();
            textBox1 = new TextBox();
            button3 = new Button();
            button2 = new Button();
            comboBox3 = new ComboBox();
            label5 = new Label();
            checkBox1 = new CheckBox();
            button1 = new Button();
            comboBox2 = new ComboBox();
            label4 = new Label();
            button4 = new Button();
            dataGridView1 = new DataGridView();
            NAME = new DataGridViewTextBoxColumn();
            EXTEND = new DataGridViewTextBoxColumn();
            TYPE = new DataGridViewComboBoxColumn();
            FILENAME = new DataGridViewTextBoxColumn();
            Status = new DataGridViewTextBoxColumn();
            Column2 = new DataGridViewTextBoxColumn();
            Column5 = new DataGridViewCheckBoxColumn();
            Column3 = new DataGridViewCheckBoxColumn();
            Column4 = new DataGridViewCheckBoxColumn();
            Column1 = new DataGridViewButtonColumn();
            label3 = new Label();
            label2 = new Label();
            label1 = new Label();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            groupBox2 = new GroupBox();
            textBox7 = new TextBox();
            groupBox1 = new GroupBox();
            checkBox5 = new CheckBox();
            checkBox4 = new CheckBox();
            checkBox3 = new CheckBox();
            button6 = new Button();
            button5 = new Button();
            textBox6 = new TextBox();
            label10 = new Label();
            textBox5 = new TextBox();
            label9 = new Label();
            tabPage3 = new TabPage();
            dataGridView2 = new DataGridView();
            label7 = new Label();
            label6 = new Label();
            Txt_SerchKey = new TextBox();
            tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            groupBox1.SuspendLayout();
            tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).BeginInit();
            SuspendLayout();
            // 
            // comboBox1
            // 
            comboBox1.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
            comboBox1.FormattingEnabled = true;
            comboBox1.Location = new System.Drawing.Point(67, 83);
            comboBox1.Margin = new Padding(3, 4, 3, 4);
            comboBox1.Name = "comboBox1";
            comboBox1.Size = new System.Drawing.Size(137, 28);
            comboBox1.TabIndex = 30;
            toolTip1.SetToolTip(comboBox1, "Không biết build nào thì hỏi OanhDH2 ");
            comboBox1.SelectedIndexChanged += comboBox1_SelectedIndexChanged_1;
            // 
            // cbo_BuildVer
            // 
            cbo_BuildVer.Enabled = false;
            cbo_BuildVer.FormattingEnabled = true;
            cbo_BuildVer.Location = new System.Drawing.Point(274, 15);
            cbo_BuildVer.Margin = new Padding(3, 4, 3, 4);
            cbo_BuildVer.Name = "cbo_BuildVer";
            cbo_BuildVer.Size = new System.Drawing.Size(171, 28);
            cbo_BuildVer.TabIndex = 32;
            toolTip1.SetToolTip(cbo_BuildVer, "Không biết build nào thì hỏi OanhDH2 ");
            // 
            // cbo_Project
            // 
            cbo_Project.FormattingEnabled = true;
            cbo_Project.Location = new System.Drawing.Point(66, 15);
            cbo_Project.Margin = new Padding(3, 4, 3, 4);
            cbo_Project.Name = "cbo_Project";
            cbo_Project.Size = new System.Drawing.Size(137, 28);
            cbo_Project.TabIndex = 34;
            toolTip1.SetToolTip(cbo_Project, "Không biết build nào thì hỏi OanhDH2 ");
            cbo_Project.SelectedValueChanged += cbo_Project_SelectedValueChanged;
            // 
            // tabPage2
            // 
            tabPage2.Controls.Add(textBox8);
            tabPage2.Controls.Add(label11);
            tabPage2.Controls.Add(label8);
            tabPage2.Controls.Add(checkBox2);
            tabPage2.Controls.Add(textBox4);
            tabPage2.Controls.Add(textBox3);
            tabPage2.Controls.Add(textBox2);
            tabPage2.Controls.Add(textBox1);
            tabPage2.Controls.Add(button3);
            tabPage2.Controls.Add(button2);
            tabPage2.Controls.Add(comboBox3);
            tabPage2.Controls.Add(label5);
            tabPage2.Controls.Add(checkBox1);
            tabPage2.Controls.Add(button1);
            tabPage2.Controls.Add(comboBox2);
            tabPage2.Controls.Add(comboBox1);
            tabPage2.Controls.Add(label4);
            tabPage2.Controls.Add(button4);
            tabPage2.Controls.Add(dataGridView1);
            tabPage2.Controls.Add(label3);
            tabPage2.Controls.Add(label2);
            tabPage2.Controls.Add(label1);
            tabPage2.Location = new System.Drawing.Point(4, 29);
            tabPage2.Margin = new Padding(3, 4, 3, 4);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3, 4, 3, 4);
            tabPage2.Size = new System.Drawing.Size(1133, 648);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "Commit";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // textBox8
            // 
            textBox8.Location = new System.Drawing.Point(590, 48);
            textBox8.Margin = new Padding(3, 4, 3, 4);
            textBox8.Multiline = true;
            textBox8.Name = "textBox8";
            textBox8.PlaceholderText = "Code";
            textBox8.Size = new System.Drawing.Size(269, 101);
            textBox8.TabIndex = 44;
            // 
            // label11
            // 
            label11.AutoSize = true;
            label11.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point);
            label11.ForeColor = System.Drawing.Color.Red;
            label11.Location = new System.Drawing.Point(19, 155);
            label11.Name = "label11";
            label11.Size = new System.Drawing.Size(0, 32);
            label11.TabIndex = 43;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.ForeColor = System.Drawing.Color.Red;
            label8.Location = new System.Drawing.Point(293, 164);
            label8.Name = "label8";
            label8.Size = new System.Drawing.Size(0, 20);
            label8.TabIndex = 42;
            // 
            // checkBox2
            // 
            checkBox2.AutoSize = true;
            checkBox2.Location = new System.Drawing.Point(211, 51);
            checkBox2.Margin = new Padding(3, 4, 3, 4);
            checkBox2.Name = "checkBox2";
            checkBox2.Size = new System.Drawing.Size(77, 24);
            checkBox2.TabIndex = 41;
            checkBox2.Text = "No Jira";
            checkBox2.UseVisualStyleBackColor = true;
            checkBox2.CheckedChanged += checkBox2_CheckedChanged;
            // 
            // textBox4
            // 
            textBox4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            textBox4.Location = new System.Drawing.Point(866, 48);
            textBox4.Margin = new Padding(3, 4, 3, 4);
            textBox4.Multiline = true;
            textBox4.Name = "textBox4";
            textBox4.PlaceholderText = "Add new tham số, mỗi tham số 1 dòng";
            textBox4.Size = new System.Drawing.Size(255, 101);
            textBox4.TabIndex = 39;
            // 
            // textBox3
            // 
            textBox3.Location = new System.Drawing.Point(286, 48);
            textBox3.Margin = new Padding(3, 4, 3, 4);
            textBox3.Multiline = true;
            textBox3.Name = "textBox3";
            textBox3.PlaceholderText = "Nội dung chỉnh sửa";
            textBox3.Size = new System.Drawing.Size(298, 101);
            textBox3.TabIndex = 32;
            // 
            // textBox2
            // 
            textBox2.BackColor = System.Drawing.Color.FromArgb(255, 255, 192);
            textBox2.Location = new System.Drawing.Point(67, 48);
            textBox2.Margin = new Padding(3, 4, 3, 4);
            textBox2.Name = "textBox2";
            textBox2.Size = new System.Drawing.Size(137, 27);
            textBox2.TabIndex = 26;
            // 
            // textBox1
            // 
            textBox1.Enabled = false;
            textBox1.Location = new System.Drawing.Point(67, 9);
            textBox1.Margin = new Padding(3, 4, 3, 4);
            textBox1.Name = "textBox1";
            textBox1.Size = new System.Drawing.Size(679, 27);
            textBox1.TabIndex = 24;
            // 
            // button3
            // 
            button3.Location = new System.Drawing.Point(143, 159);
            button3.Margin = new Padding(3, 4, 3, 4);
            button3.Name = "button3";
            button3.Size = new System.Drawing.Size(61, 31);
            button3.TabIndex = 38;
            button3.Text = "Clear";
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // button2
            // 
            button2.Location = new System.Drawing.Point(211, 156);
            button2.Margin = new Padding(3, 4, 3, 4);
            button2.Name = "button2";
            button2.Size = new System.Drawing.Size(73, 35);
            button2.TabIndex = 37;
            button2.Text = "Commit";
            button2.UseVisualStyleBackColor = true;
            button2.Click += button2_Click;
            // 
            // comboBox3
            // 
            comboBox3.FormattingEnabled = true;
            comboBox3.Location = new System.Drawing.Point(67, 120);
            comboBox3.Margin = new Padding(3, 4, 3, 4);
            comboBox3.Name = "comboBox3";
            comboBox3.Size = new System.Drawing.Size(137, 28);
            comboBox3.TabIndex = 36;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(5, 125);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(34, 20);
            label5.TabIndex = 35;
            label5.Text = "Site";
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.Location = new System.Drawing.Point(211, 85);
            checkBox1.Margin = new Padding(3, 4, 3, 4);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new System.Drawing.Size(74, 24);
            checkBox1.TabIndex = 34;
            checkBox1.Text = "HotFix";
            checkBox1.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            button1.Location = new System.Drawing.Point(211, 120);
            button1.Margin = new Padding(3, 4, 3, 4);
            button1.Name = "button1";
            button1.Size = new System.Drawing.Size(73, 31);
            button1.TabIndex = 33;
            button1.Text = "Browser";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // comboBox2
            // 
            comboBox2.FormattingEnabled = true;
            comboBox2.Location = new System.Drawing.Point(810, 5);
            comboBox2.Margin = new Padding(3, 4, 3, 4);
            comboBox2.Name = "comboBox2";
            comboBox2.Size = new System.Drawing.Size(100, 28);
            comboBox2.TabIndex = 31;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(759, 11);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(49, 20);
            label4.TabIndex = 29;
            label4.Text = "Name";
            // 
            // button4
            // 
            button4.Location = new System.Drawing.Point(1054, 4);
            button4.Margin = new Padding(3, 4, 3, 4);
            button4.Name = "button4";
            button4.Size = new System.Drawing.Size(69, 31);
            button4.TabIndex = 40;
            button4.Text = "Xuất BC";
            button4.UseVisualStyleBackColor = true;
            button4.Click += button4_Click;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { NAME, EXTEND, TYPE, FILENAME, Status, Column2, Column5, Column3, Column4, Column1 });
            dataGridView1.Location = new System.Drawing.Point(9, 197);
            dataGridView1.Margin = new Padding(3, 4, 3, 4);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 20;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new System.Drawing.Size(1113, 439);
            dataGridView1.TabIndex = 28;
            // 
            // NAME
            // 
            NAME.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            NAME.DataPropertyName = "NAME";
            NAME.HeaderText = "Name";
            NAME.MinimumWidth = 6;
            NAME.Name = "NAME";
            NAME.ReadOnly = true;
            // 
            // EXTEND
            // 
            EXTEND.DataPropertyName = "EXTEND";
            EXTEND.HeaderText = "Extend";
            EXTEND.MinimumWidth = 6;
            EXTEND.Name = "EXTEND";
            EXTEND.ReadOnly = true;
            EXTEND.Resizable = DataGridViewTriState.True;
            EXTEND.Visible = false;
            EXTEND.Width = 50;
            // 
            // TYPE
            // 
            TYPE.DataPropertyName = "TYPE";
            TYPE.DisplayStyle = DataGridViewComboBoxDisplayStyle.ComboBox;
            TYPE.HeaderText = "Type";
            TYPE.MinimumWidth = 6;
            TYPE.Name = "TYPE";
            TYPE.Width = 120;
            // 
            // FILENAME
            // 
            FILENAME.DataPropertyName = "FILENAME";
            FILENAME.HeaderText = "FILENAME";
            FILENAME.MinimumWidth = 6;
            FILENAME.Name = "FILENAME";
            FILENAME.ReadOnly = true;
            FILENAME.Visible = false;
            FILENAME.Width = 125;
            // 
            // Status
            // 
            Status.DataPropertyName = "Status";
            Status.HeaderText = "Status";
            Status.MinimumWidth = 6;
            Status.Name = "Status";
            Status.ReadOnly = true;
            Status.Width = 65;
            // 
            // Column2
            // 
            Column2.HeaderText = "Path Build";
            Column2.MinimumWidth = 6;
            Column2.Name = "Column2";
            Column2.Width = 200;
            // 
            // Column5
            // 
            Column5.DataPropertyName = "ISDUP";
            Column5.HeaderText = "Column5";
            Column5.MinimumWidth = 6;
            Column5.Name = "Column5";
            Column5.Visible = false;
            Column5.Width = 125;
            // 
            // Column3
            // 
            Column3.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            Column3.HeaderText = "Override";
            Column3.MinimumWidth = 6;
            Column3.Name = "Column3";
            Column3.ToolTipText = "File trùng mặc định cột này = true";
            Column3.Width = 72;
            // 
            // Column4
            // 
            Column4.AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader;
            Column4.HeaderText = "New Version";
            Column4.MinimumWidth = 6;
            Column4.Name = "Column4";
            Column4.Width = 97;
            // 
            // Column1
            // 
            Column1.HeaderText = "Detail";
            Column1.MinimumWidth = 6;
            Column1.Name = "Column1";
            Column1.Resizable = DataGridViewTriState.False;
            Column1.Width = 45;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(5, 88);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(67, 20);
            label3.TabIndex = 27;
            label3.Text = "Build ver";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(5, 52);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(62, 20);
            label2.TabIndex = 25;
            label2.Text = "Mã JIRA";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(5, 15);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(51, 20);
            label1.TabIndex = 23;
            label1.Text = "Folder";
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage2);
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Controls.Add(tabPage3);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new System.Drawing.Point(0, 0);
            tabControl1.Margin = new Padding(3, 4, 3, 4);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new System.Drawing.Size(1141, 681);
            tabControl1.TabIndex = 23;
            tabControl1.SelectedIndexChanged += tabControl1_SelectedIndexChanged;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(groupBox2);
            tabPage1.Controls.Add(textBox7);
            tabPage1.Controls.Add(groupBox1);
            tabPage1.Location = new System.Drawing.Point(4, 29);
            tabPage1.Margin = new Padding(3, 4, 3, 4);
            tabPage1.Name = "tabPage1";
            tabPage1.Size = new System.Drawing.Size(1133, 648);
            tabPage1.TabIndex = 3;
            tabPage1.Text = "Tính năng ❤️";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            groupBox2.Location = new System.Drawing.Point(11, 213);
            groupBox2.Margin = new Padding(3, 4, 3, 4);
            groupBox2.Name = "groupBox2";
            groupBox2.Padding = new Padding(3, 4, 3, 4);
            groupBox2.Size = new System.Drawing.Size(1111, 133);
            groupBox2.TabIndex = 34;
            groupBox2.TabStop = false;
            groupBox2.Text = "Có trick gì thì gắn vô sau";
            // 
            // textBox7
            // 
            textBox7.Location = new System.Drawing.Point(11, 531);
            textBox7.Margin = new Padding(3, 4, 3, 4);
            textBox7.Multiline = true;
            textBox7.Name = "textBox7";
            textBox7.Size = new System.Drawing.Size(1110, 101);
            textBox7.TabIndex = 33;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(checkBox5);
            groupBox1.Controls.Add(checkBox4);
            groupBox1.Controls.Add(checkBox3);
            groupBox1.Controls.Add(button6);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(textBox6);
            groupBox1.Controls.Add(label10);
            groupBox1.Controls.Add(textBox5);
            groupBox1.Controls.Add(label9);
            groupBox1.Location = new System.Drawing.Point(11, 16);
            groupBox1.Margin = new Padding(3, 4, 3, 4);
            groupBox1.Name = "groupBox1";
            groupBox1.Padding = new Padding(3, 4, 3, 4);
            groupBox1.Size = new System.Drawing.Size(1111, 165);
            groupBox1.TabIndex = 27;
            groupBox1.TabStop = false;
            groupBox1.Text = "Replace duplicate name in form 👍";
            // 
            // checkBox5
            // 
            checkBox5.AutoSize = true;
            checkBox5.Checked = true;
            checkBox5.CheckState = CheckState.Checked;
            checkBox5.Location = new System.Drawing.Point(768, 135);
            checkBox5.Margin = new Padding(3, 4, 3, 4);
            checkBox5.Name = "checkBox5";
            checkBox5.Size = new System.Drawing.Size(131, 24);
            checkBox5.TabIndex = 38;
            checkBox5.Text = "Generate event";
            checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox4
            // 
            checkBox4.AutoSize = true;
            checkBox4.Checked = true;
            checkBox4.CheckState = CheckState.Checked;
            checkBox4.Location = new System.Drawing.Point(768, 101);
            checkBox4.Margin = new Padding(3, 4, 3, 4);
            checkBox4.Name = "checkBox4";
            checkBox4.Size = new System.Drawing.Size(138, 24);
            checkBox4.TabIndex = 37;
            checkBox4.Text = "Generate model";
            checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            checkBox3.AutoSize = true;
            checkBox3.Checked = true;
            checkBox3.CheckState = CheckState.Checked;
            checkBox3.Location = new System.Drawing.Point(768, 68);
            checkBox3.Margin = new Padding(3, 4, 3, 4);
            checkBox3.Name = "checkBox3";
            checkBox3.Size = new System.Drawing.Size(151, 24);
            checkBox3.TabIndex = 36;
            checkBox3.Text = "Open folder result";
            checkBox3.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            button6.Location = new System.Drawing.Point(843, 29);
            button6.Margin = new Padding(3, 4, 3, 4);
            button6.Name = "button6";
            button6.Size = new System.Drawing.Size(73, 31);
            button6.TabIndex = 35;
            button6.Text = "Execute";
            button6.UseVisualStyleBackColor = true;
            button6.Click += button6_Click;
            // 
            // button5
            // 
            button5.Location = new System.Drawing.Point(763, 29);
            button5.Margin = new Padding(3, 4, 3, 4);
            button5.Name = "button5";
            button5.Size = new System.Drawing.Size(73, 31);
            button5.TabIndex = 34;
            button5.Text = "Browser";
            button5.UseVisualStyleBackColor = true;
            button5.Click += button5_Click;
            // 
            // textBox6
            // 
            textBox6.Enabled = false;
            textBox6.Location = new System.Drawing.Point(81, 81);
            textBox6.Margin = new Padding(3, 4, 3, 4);
            textBox6.Name = "textBox6";
            textBox6.Size = new System.Drawing.Size(679, 27);
            textBox6.TabIndex = 30;
            // 
            // label10
            // 
            label10.AutoSize = true;
            label10.Location = new System.Drawing.Point(18, 87);
            label10.Name = "label10";
            label10.Size = new System.Drawing.Size(55, 20);
            label10.TabIndex = 29;
            label10.Text = "Output";
            // 
            // textBox5
            // 
            textBox5.Enabled = false;
            textBox5.Location = new System.Drawing.Point(81, 31);
            textBox5.Margin = new Padding(3, 4, 3, 4);
            textBox5.Name = "textBox5";
            textBox5.Size = new System.Drawing.Size(679, 27);
            textBox5.TabIndex = 28;
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Location = new System.Drawing.Point(18, 36);
            label9.Name = "label9";
            label9.Size = new System.Drawing.Size(43, 20);
            label9.TabIndex = 27;
            label9.Text = "Input";
            // 
            // tabPage3
            // 
            tabPage3.Controls.Add(dataGridView2);
            tabPage3.Controls.Add(cbo_Project);
            tabPage3.Controls.Add(label7);
            tabPage3.Controls.Add(cbo_BuildVer);
            tabPage3.Controls.Add(label6);
            tabPage3.Controls.Add(Txt_SerchKey);
            tabPage3.Location = new System.Drawing.Point(4, 29);
            tabPage3.Margin = new Padding(3, 4, 3, 4);
            tabPage3.Name = "tabPage3";
            tabPage3.Size = new System.Drawing.Size(1133, 648);
            tabPage3.TabIndex = 2;
            tabPage3.Text = "Search";
            tabPage3.UseVisualStyleBackColor = true;
            // 
            // dataGridView2
            // 
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dataGridView2.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView2.Location = new System.Drawing.Point(9, 53);
            dataGridView2.Margin = new Padding(3, 4, 3, 4);
            dataGridView2.Name = "dataGridView2";
            dataGridView2.RowHeadersWidth = 20;
            dataGridView2.RowTemplate.Height = 25;
            dataGridView2.Size = new System.Drawing.Size(1113, 580);
            dataGridView2.TabIndex = 35;
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(9, 20);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(55, 20);
            label7.TabIndex = 33;
            label7.Text = "Project";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(211, 20);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(67, 20);
            label6.TabIndex = 31;
            label6.Text = "Build ver";
            // 
            // Txt_SerchKey
            // 
            Txt_SerchKey.Location = new System.Drawing.Point(453, 15);
            Txt_SerchKey.Margin = new Padding(3, 4, 3, 4);
            Txt_SerchKey.Name = "Txt_SerchKey";
            Txt_SerchKey.Size = new System.Drawing.Size(669, 27);
            Txt_SerchKey.TabIndex = 26;
            Txt_SerchKey.TextChanged += Txt_SerchKey_TextChanged;
            // 
            // Form1
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1141, 681);
            Controls.Add(tabControl1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "GOM BUILD";
            Load += Form1_Load;
            tabPage2.ResumeLayout(false);
            tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            tabPage1.PerformLayout();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            tabPage3.ResumeLayout(false);
            tabPage3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dataGridView2).EndInit();
            ResumeLayout(false);
        }

        #endregion
        private DataGridViewTextBoxColumn DirectoryName;
        private ToolTip toolTip1;
        private TabPage tabPage2;
        private CheckBox checkBox2;
        private TextBox textBox4;
        private TextBox textBox3;
        private TextBox textBox2;
        private TextBox textBox1;
        private Button button3;
        private Button button2;
        private ComboBox comboBox3;
        private Label label5;
        private CheckBox checkBox1;
        private Button button1;
        private ComboBox comboBox2;
        private ComboBox comboBox1;
        private Label label4;
        private Button button4;
        private DataGridView dataGridView1;
        private DataGridViewTextBoxColumn NAME;
        private DataGridViewTextBoxColumn EXTEND;
        private DataGridViewComboBoxColumn TYPE;
        private DataGridViewTextBoxColumn FILENAME;
        private DataGridViewTextBoxColumn Status;
        private DataGridViewTextBoxColumn Column2;
        private DataGridViewCheckBoxColumn Column5;
        private DataGridViewCheckBoxColumn Column3;
        private DataGridViewCheckBoxColumn Column4;
        private DataGridViewButtonColumn Column1;
        private Label label3;
        private Label label2;
        private Label label1;
        private TabControl tabControl1;
        private TabPage tabPage3;
        private ComboBox cbo_Project;
        private Label label7;
        private ComboBox cbo_BuildVer;
        private Label label6;
        private TextBox Txt_SerchKey;
        private DataGridView dataGridView2;
        private Label label8;
        private TabPage tabPage1;
        private GroupBox groupBox1;
        private TextBox textBox6;
        private Label label10;
        private TextBox textBox5;
        private Label label9;
        private Button button5;
        private TextBox textBox7;
        private Button button6;
        private CheckBox checkBox5;
        private CheckBox checkBox4;
        private CheckBox checkBox3;
        private Label label11;
        private TextBox textBox8;
        private GroupBox groupBox2;
    }
}