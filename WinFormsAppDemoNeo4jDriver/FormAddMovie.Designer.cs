namespace WinFormsAppDemoNeo4jDriver
{
    partial class FormAddMovie
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
            textBoxTitle = new TextBox();
            label1 = new Label();
            textBoxTagline = new TextBox();
            label2 = new Label();
            textBoxReleased = new TextBox();
            label3 = new Label();
            dataGridView1 = new DataGridView();
            PersonCol = new DataGridViewComboBoxColumn();
            ActionCol = new DataGridViewComboBoxColumn();
            button1 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            SuspendLayout();
            // 
            // textBoxTitle
            // 
            textBoxTitle.Location = new Point(90, 6);
            textBoxTitle.Name = "textBoxTitle";
            textBoxTitle.Size = new Size(239, 27);
            textBoxTitle.TabIndex = 3;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(12, 9);
            label1.Name = "label1";
            label1.Size = new Size(41, 20);
            label1.TabIndex = 2;
            label1.Text = "Title:";
            // 
            // textBoxTagline
            // 
            textBoxTagline.Location = new Point(90, 48);
            textBoxTagline.Name = "textBoxTagline";
            textBoxTagline.Size = new Size(239, 27);
            textBoxTagline.TabIndex = 5;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(12, 51);
            label2.Name = "label2";
            label2.Size = new Size(59, 20);
            label2.TabIndex = 4;
            label2.Text = "Tagline:";
            // 
            // textBoxReleased
            // 
            textBoxReleased.Location = new Point(90, 92);
            textBoxReleased.Name = "textBoxReleased";
            textBoxReleased.Size = new Size(239, 27);
            textBoxReleased.TabIndex = 7;
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 95);
            label3.Name = "label3";
            label3.Size = new Size(72, 20);
            label3.TabIndex = 6;
            label3.Text = "Released:";
            // 
            // dataGridView1
            // 
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { PersonCol, ActionCol });
            dataGridView1.Location = new Point(12, 134);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.RowHeadersWidth = 51;
            dataGridView1.RowTemplate.Height = 29;
            dataGridView1.Size = new Size(776, 304);
            dataGridView1.TabIndex = 8;
            // 
            // PersonCol
            // 
            PersonCol.HeaderText = "Person";
            PersonCol.MinimumWidth = 6;
            PersonCol.Name = "PersonCol";
            // 
            // ActionCol
            // 
            ActionCol.HeaderText = "Action";
            ActionCol.Items.AddRange(new object[] { "Acted_In", "Directed", "Follows", "Produced", "Reviewed", "Wrote" });
            ActionCol.MinimumWidth = 6;
            ActionCol.Name = "ActionCol";
            // 
            // button1
            // 
            button1.Location = new Point(694, 12);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 9;
            button1.Text = "Lưu";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // FormAddMovie
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(button1);
            Controls.Add(dataGridView1);
            Controls.Add(textBoxReleased);
            Controls.Add(label3);
            Controls.Add(textBoxTagline);
            Controls.Add(label2);
            Controls.Add(textBoxTitle);
            Controls.Add(label1);
            Name = "FormAddMovie";
            Text = "FormAddMovie";
            Load += FormAddMovie_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private TextBox textBoxTitle;
        private Label label1;
        private TextBox textBoxTagline;
        private Label label2;
        private TextBox textBoxReleased;
        private Label label3;
        private DataGridView dataGridView1;
        private Button button1;
        private DataGridViewComboBoxColumn PersonCol;
        private DataGridViewComboBoxColumn ActionCol;
    }
}