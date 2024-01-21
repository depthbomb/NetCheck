using Microsoft.Web.WebView2.WinForms;

namespace NetCheck.Forms;

partial class MainForm
{
    /// <summary>
    ///  Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;
    
    private WebView2 _webView2;

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
        this.components    = new System.ComponentModel.Container();
        this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        this.ClientSize    = new System.Drawing.Size(800, 450);
        this.Text          = "NetCheck";
        this.MinimumSize   = new Size(256, 128);

        this._webView2                    = new WebView2();
        this._webView2.AllowExternalDrop  = false;
        this._webView2.CreationProperties = null;
        this._webView2.Dock               = DockStyle.Fill;
        this._webView2.Location           = new Point(0, 0);
        this._webView2.Margin             = new Padding(0);
        this._webView2.Name               = "_WebView";
        this._webView2.TabIndex           = 0;
        this._webView2.ZoomFactor         = 1D;

        this.Controls.Add(_webView2);

        this.Closing += OnClosing;
    }
    #endregion
}
