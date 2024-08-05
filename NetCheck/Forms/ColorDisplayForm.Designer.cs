using System.ComponentModel;

namespace NetCheck.Forms;

partial class ColorDisplayForm
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private IContainer components = null;

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
        ComponentResourceManager resources = new ComponentResourceManager(typeof(ColorDisplayForm));
        c_StatusLabel = new Label();
        SuspendLayout();
        // 
        // c_StatusLabel
        // 
        c_StatusLabel.Dock = DockStyle.Fill;
        c_StatusLabel.Font = new Font("Consolas", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        c_StatusLabel.Location = new Point(0, 0);
        c_StatusLabel.Margin = new Padding(0);
        c_StatusLabel.Name = "c_StatusLabel";
        c_StatusLabel.Size = new Size(800, 450);
        c_StatusLabel.TabIndex = 0;
        c_StatusLabel.Text = "Probing...\r\n";
        c_StatusLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // ColorDisplayForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BackColor = Color.FromArgb(71, 71, 71);
        ClientSize = new Size(800, 450);
        Controls.Add(c_StatusLabel);
        ForeColor = Color.White;
        Icon = (Icon)resources.GetObject("$this.Icon");
        Name = "ColorDisplayForm";
        StartPosition = FormStartPosition.CenterParent;
        Text = "NetCheck Status Color Display";
        ResumeLayout(false);
    }

    #endregion

    private Label c_StatusLabel;
}

