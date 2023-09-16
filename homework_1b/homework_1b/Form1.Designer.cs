namespace homework_1b
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.labelQuestion = new System.Windows.Forms.Label();
            this.startButton = new System.Windows.Forms.Button();
            this.submitButton = new System.Windows.Forms.Button();
            this.textBoxAnswer = new System.Windows.Forms.TextBox();
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.timetext = new System.Windows.Forms.Label();
            this.scoreText = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelQuestion
            // 
            this.labelQuestion.AutoSize = true;
            this.labelQuestion.Font = new System.Drawing.Font("宋体", 18F);
            this.labelQuestion.Location = new System.Drawing.Point(283, 269);
            this.labelQuestion.Name = "labelQuestion";
            this.labelQuestion.Size = new System.Drawing.Size(58, 30);
            this.labelQuestion.TabIndex = 0;
            this.labelQuestion.Text = "1+1";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(785, 362);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(134, 41);
            this.startButton.TabIndex = 1;
            this.startButton.Text = "开始测试";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.startButton_Click);
            // 
            // submitButton
            // 
            this.submitButton.Location = new System.Drawing.Point(376, 413);
            this.submitButton.Name = "submitButton";
            this.submitButton.Size = new System.Drawing.Size(123, 41);
            this.submitButton.TabIndex = 2;
            this.submitButton.Text = "提交答案";
            this.submitButton.UseVisualStyleBackColor = true;
            this.submitButton.Click += new System.EventHandler(this.buttonSubmit_Click);
            // 
            // textBoxAnswer
            // 
            this.textBoxAnswer.Location = new System.Drawing.Point(508, 274);
            this.textBoxAnswer.Name = "textBoxAnswer";
            this.textBoxAnswer.Size = new System.Drawing.Size(100, 25);
            this.textBoxAnswer.TabIndex = 3;
            this.textBoxAnswer.TextChanged += new System.EventHandler(this.textBoxAnswer_TextChanged);
            // 
            // timer
            // 
            this.timer.Interval = 1000;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // timetext
            // 
            this.timetext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.timetext.AutoSize = true;
            this.timetext.Font = new System.Drawing.Font("宋体", 22F);
            this.timetext.ForeColor = System.Drawing.Color.Crimson;
            this.timetext.Location = new System.Drawing.Point(385, 39);
            this.timetext.Name = "timetext";
            this.timetext.Size = new System.Drawing.Size(202, 37);
            this.timetext.TabIndex = 4;
            this.timetext.Text = "剩余时间：";
            // 
            // scoreText
            // 
            this.scoreText.AutoSize = true;
            this.scoreText.Font = new System.Drawing.Font("宋体", 15F);
            this.scoreText.Location = new System.Drawing.Point(782, 61);
            this.scoreText.Name = "scoreText";
            this.scoreText.Size = new System.Drawing.Size(219, 31);
            this.scoreText.TabIndex = 5;
            this.scoreText.Text = "当前得分：0分";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(785, 254);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(134, 45);
            this.button1.TabIndex = 6;
            this.button1.Text = "规则说明";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1012, 693);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.scoreText);
            this.Controls.Add(this.timetext);
            this.Controls.Add(this.textBoxAnswer);
            this.Controls.Add(this.submitButton);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.labelQuestion);
            this.Name = "Form1";
            this.Text = "加减法测试";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelQuestion;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.Button submitButton;
        private System.Windows.Forms.TextBox textBoxAnswer;
        private System.Windows.Forms.Timer timer;
        private int countdown = 30;
        private System.Windows.Forms.Label timetext;
        private System.Windows.Forms.Label scoreText;
        private System.Windows.Forms.Button button1;
    }
}

