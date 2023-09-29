namespace homework_3
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
            button1 = new Button();
            wordListView = new ListView();
            单词 = new ColumnHeader();
            数量 = new ColumnHeader();
            LineCountLabel = new Label();
            WordCountLabel = new Label();
            fLineCountLabel = new Label();
            fWordCountLabel = new Label();
            SuspendLayout();
            // 
            // button1
            // 
            button1.Location = new Point(646, 159);
            button1.Name = "button1";
            button1.Size = new Size(94, 29);
            button1.TabIndex = 0;
            button1.Text = "选择文件";
            button1.UseVisualStyleBackColor = true;
            button1.Click += button1_Click;
            // 
            // wordListView
            // 
            wordListView.Columns.AddRange(new ColumnHeader[] { 单词, 数量 });
            wordListView.Location = new Point(104, 355);
            wordListView.Name = "wordListView";
            wordListView.Size = new Size(253, 223);
            wordListView.TabIndex = 1;
            wordListView.UseCompatibleStateImageBehavior = false;
            wordListView.View = View.Details;
            // 
            // 单词
            // 
            单词.Text = "单词";
            // 
            // 数量
            // 
            数量.Text = "出现次数";
            // 
            // LineCountLabel
            // 
            LineCountLabel.AutoSize = true;
            LineCountLabel.Location = new Point(121, 168);
            LineCountLabel.Name = "LineCountLabel";
            LineCountLabel.Size = new Size(84, 20);
            LineCountLabel.TabIndex = 2;
            LineCountLabel.Text = "原始行数：";
            // 
            // WordCountLabel
            // 
            WordCountLabel.AutoSize = true;
            WordCountLabel.Location = new Point(309, 168);
            WordCountLabel.Name = "WordCountLabel";
            WordCountLabel.Size = new Size(99, 20);
            WordCountLabel.TabIndex = 3;
            WordCountLabel.Text = "原始单词数：";
            // 
            // fLineCountLabel
            // 
            fLineCountLabel.AutoSize = true;
            fLineCountLabel.Location = new Point(121, 225);
            fLineCountLabel.Name = "fLineCountLabel";
            fLineCountLabel.Size = new Size(114, 20);
            fLineCountLabel.TabIndex = 4;
            fLineCountLabel.Text = "格式化后行数：";
            // 
            // fWordCountLabel
            // 
            fWordCountLabel.AutoSize = true;
            fWordCountLabel.Location = new Point(309, 225);
            fWordCountLabel.Name = "fWordCountLabel";
            fWordCountLabel.Size = new Size(129, 20);
            fWordCountLabel.TabIndex = 5;
            fWordCountLabel.Text = "格式化后单词数：";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1158, 668);
            Controls.Add(fWordCountLabel);
            Controls.Add(fLineCountLabel);
            Controls.Add(WordCountLabel);
            Controls.Add(LineCountLabel);
            Controls.Add(wordListView);
            Controls.Add(button1);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button button1;
        private ListView wordListView;
        private Label LineCountLabel;
        private Label WordCountLabel;
        private Label fLineCountLabel;
        private Label fWordCountLabel;
        private ColumnHeader 单词;
        private ColumnHeader 数量;
    }
}