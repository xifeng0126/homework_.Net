using System;
using System.Windows.Forms;

public class InputDialog : Form
{
    private TextBox textBox;
    private Button okButton;

    public string InputText { get; private set; }

    public InputDialog(string prompt, string defaultText = "")
    {

        Label promptLabel = new Label
        {
            Text = prompt,
            Location = new System.Drawing.Point(10, 10),
            AutoSize = true
        };
        Controls.Add(promptLabel);

        textBox = new TextBox
        {
            Text = defaultText,
            Location = new System.Drawing.Point(10, promptLabel.Bottom + 10),
            Width = 200
        };
        Controls.Add(textBox);

        okButton = new Button
        {
            Text = "确定",
            Location = new System.Drawing.Point(50, textBox.Bottom + 20),
            DialogResult = DialogResult.OK
            
        };
        okButton.Click += OkButton_Click;
        Controls.Add(okButton);

        AcceptButton = okButton;
    }


    private void OkButton_Click(object sender, EventArgs e)
    {
        InputText = textBox.Text;
    }

    private void InitializeComponent()
    {
            this.SuspendLayout();
            // 
            // InputDialog
            // 
            this.ClientSize = new System.Drawing.Size(443, 383);
            this.Name = "InputDialog";
            this.ResumeLayout(false);

    }
}
