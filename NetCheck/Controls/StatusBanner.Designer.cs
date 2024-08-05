using System.ComponentModel;

namespace NetCheck.Controls;

partial class StatusBanner
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
        c_StatusLabel = new Label();
        SuspendLayout();
        // 
        // c_StatusLabel
        // 
        c_StatusLabel.Dock = DockStyle.Fill;
        c_StatusLabel.Font = new Font("Consolas", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
        c_StatusLabel.ForeColor = Color.White;
        c_StatusLabel.Location = new Point(0, 0);
        c_StatusLabel.Margin = new Padding(3);
        c_StatusLabel.Name = "c_StatusLabel";
        c_StatusLabel.Padding = new Padding(3);
        c_StatusLabel.Size = new Size(736, 112);
        c_StatusLabel.TabIndex = 1;
        c_StatusLabel.Text = "Probing...";
        c_StatusLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // StatusBanner
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        BorderStyle = BorderStyle.FixedSingle;
        Controls.Add(c_StatusLabel);
        Cursor = Cursors.Hand;
        Name = "StatusBanner";
        Size = new Size(736, 112);
        ResumeLayout(false);
    }

    #endregion

    private Label c_StatusLabel;
}

