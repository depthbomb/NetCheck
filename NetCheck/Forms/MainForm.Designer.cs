namespace NetCheck.Forms;

partial class MainForm
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
        c_MainTableLayout = new TableLayoutPanel();
        c_MainTabControl = new TabControl();
        c_ProbeInfoTabPage = new TabPage();
        c_InfoTableLayout = new TableLayoutPanel();
        c_LastPingTitleLabel = new Label();
        c_LastPingValueLabel = new Label();
        c_AveragePingTitleLabel = new Label();
        c_AveragePingValueLabel = new Label();
        c_NetworkTypeTitleLabel = new Label();
        c_NetworkTypeValueLabel = new Label();
        c_ProbeLogTabPage = new TabPage();
        c_ProbeLogListView = new ListView();
        c_MainTableLayout.SuspendLayout();
        c_MainTabControl.SuspendLayout();
        c_ProbeInfoTabPage.SuspendLayout();
        c_InfoTableLayout.SuspendLayout();
        c_ProbeLogTabPage.SuspendLayout();
        SuspendLayout();
        // 
        // c_MainTableLayout
        // 
        c_MainTableLayout.ColumnCount = 1;
        c_MainTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        c_MainTableLayout.Controls.Add(c_MainTabControl, 0, 1);
        c_MainTableLayout.Dock = DockStyle.Fill;
        c_MainTableLayout.Location = new Point(0, 0);
        c_MainTableLayout.Margin = new Padding(0);
        c_MainTableLayout.Name = "c_MainTableLayout";
        c_MainTableLayout.RowCount = 2;
        c_MainTableLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 64F));
        c_MainTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        c_MainTableLayout.Size = new Size(353, 295);
        c_MainTableLayout.TabIndex = 0;
        // 
        // c_MainTabControl
        // 
        c_MainTabControl.Controls.Add(c_ProbeInfoTabPage);
        c_MainTabControl.Controls.Add(c_ProbeLogTabPage);
        c_MainTabControl.Dock = DockStyle.Fill;
        c_MainTabControl.Location = new Point(3, 67);
        c_MainTabControl.Name = "c_MainTabControl";
        c_MainTabControl.SelectedIndex = 0;
        c_MainTabControl.Size = new Size(347, 225);
        c_MainTabControl.SizeMode = TabSizeMode.Fixed;
        c_MainTabControl.TabIndex = 1;
        c_MainTabControl.TabStop = false;
        // 
        // c_ProbeInfoTabPage
        // 
        c_ProbeInfoTabPage.Controls.Add(c_InfoTableLayout);
        c_ProbeInfoTabPage.Location = new Point(4, 24);
        c_ProbeInfoTabPage.Name = "c_ProbeInfoTabPage";
        c_ProbeInfoTabPage.Padding = new Padding(3);
        c_ProbeInfoTabPage.Size = new Size(339, 197);
        c_ProbeInfoTabPage.TabIndex = 0;
        c_ProbeInfoTabPage.Text = "Info";
        c_ProbeInfoTabPage.UseVisualStyleBackColor = true;
        // 
        // c_InfoTableLayout
        // 
        c_InfoTableLayout.ColumnCount = 2;
        c_InfoTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        c_InfoTableLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        c_InfoTableLayout.Controls.Add(c_LastPingTitleLabel, 0, 0);
        c_InfoTableLayout.Controls.Add(c_LastPingValueLabel, 1, 0);
        c_InfoTableLayout.Controls.Add(c_AveragePingTitleLabel, 0, 1);
        c_InfoTableLayout.Controls.Add(c_AveragePingValueLabel, 1, 1);
        c_InfoTableLayout.Controls.Add(c_NetworkTypeTitleLabel, 0, 2);
        c_InfoTableLayout.Controls.Add(c_NetworkTypeValueLabel, 1, 2);
        c_InfoTableLayout.Dock = DockStyle.Fill;
        c_InfoTableLayout.Location = new Point(3, 3);
        c_InfoTableLayout.Margin = new Padding(0);
        c_InfoTableLayout.Name = "c_InfoTableLayout";
        c_InfoTableLayout.RowCount = 4;
        c_InfoTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 17.0212936F));
        c_InfoTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 17.0212955F));
        c_InfoTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 17.0212936F));
        c_InfoTableLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 48.9361153F));
        c_InfoTableLayout.Size = new Size(333, 191);
        c_InfoTableLayout.TabIndex = 1;
        // 
        // c_LastPingTitleLabel
        // 
        c_LastPingTitleLabel.Dock = DockStyle.Fill;
        c_LastPingTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        c_LastPingTitleLabel.Location = new Point(3, 3);
        c_LastPingTitleLabel.Margin = new Padding(3);
        c_LastPingTitleLabel.Name = "c_LastPingTitleLabel";
        c_LastPingTitleLabel.Padding = new Padding(3);
        c_LastPingTitleLabel.Size = new Size(160, 26);
        c_LastPingTitleLabel.TabIndex = 0;
        c_LastPingTitleLabel.Text = "Last Ping";
        c_LastPingTitleLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // c_LastPingValueLabel
        // 
        c_LastPingValueLabel.Dock = DockStyle.Fill;
        c_LastPingValueLabel.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        c_LastPingValueLabel.Location = new Point(169, 3);
        c_LastPingValueLabel.Margin = new Padding(3);
        c_LastPingValueLabel.Name = "c_LastPingValueLabel";
        c_LastPingValueLabel.Padding = new Padding(3, 0, 3, 0);
        c_LastPingValueLabel.Size = new Size(161, 26);
        c_LastPingValueLabel.TabIndex = 1;
        c_LastPingValueLabel.Text = "???";
        c_LastPingValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // c_AveragePingTitleLabel
        // 
        c_AveragePingTitleLabel.Dock = DockStyle.Fill;
        c_AveragePingTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        c_AveragePingTitleLabel.Location = new Point(3, 35);
        c_AveragePingTitleLabel.Margin = new Padding(3);
        c_AveragePingTitleLabel.Name = "c_AveragePingTitleLabel";
        c_AveragePingTitleLabel.Padding = new Padding(3);
        c_AveragePingTitleLabel.Size = new Size(160, 26);
        c_AveragePingTitleLabel.TabIndex = 2;
        c_AveragePingTitleLabel.Text = "Average Ping";
        c_AveragePingTitleLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // c_AveragePingValueLabel
        // 
        c_AveragePingValueLabel.Dock = DockStyle.Fill;
        c_AveragePingValueLabel.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        c_AveragePingValueLabel.Location = new Point(169, 35);
        c_AveragePingValueLabel.Margin = new Padding(3);
        c_AveragePingValueLabel.Name = "c_AveragePingValueLabel";
        c_AveragePingValueLabel.Padding = new Padding(3, 0, 3, 0);
        c_AveragePingValueLabel.Size = new Size(161, 26);
        c_AveragePingValueLabel.TabIndex = 3;
        c_AveragePingValueLabel.Text = "???";
        c_AveragePingValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // c_NetworkTypeTitleLabel
        // 
        c_NetworkTypeTitleLabel.Dock = DockStyle.Fill;
        c_NetworkTypeTitleLabel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
        c_NetworkTypeTitleLabel.Location = new Point(3, 67);
        c_NetworkTypeTitleLabel.Margin = new Padding(3);
        c_NetworkTypeTitleLabel.Name = "c_NetworkTypeTitleLabel";
        c_NetworkTypeTitleLabel.Padding = new Padding(3);
        c_NetworkTypeTitleLabel.Size = new Size(160, 26);
        c_NetworkTypeTitleLabel.TabIndex = 4;
        c_NetworkTypeTitleLabel.Text = "Network Interface";
        c_NetworkTypeTitleLabel.TextAlign = ContentAlignment.MiddleRight;
        // 
        // c_NetworkTypeValueLabel
        // 
        c_NetworkTypeValueLabel.Dock = DockStyle.Fill;
        c_NetworkTypeValueLabel.Font = new Font("Consolas", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 0);
        c_NetworkTypeValueLabel.Location = new Point(169, 67);
        c_NetworkTypeValueLabel.Margin = new Padding(3);
        c_NetworkTypeValueLabel.Name = "c_NetworkTypeValueLabel";
        c_NetworkTypeValueLabel.Padding = new Padding(3, 0, 3, 0);
        c_NetworkTypeValueLabel.Size = new Size(161, 26);
        c_NetworkTypeValueLabel.TabIndex = 5;
        c_NetworkTypeValueLabel.Text = "Unknown";
        c_NetworkTypeValueLabel.TextAlign = ContentAlignment.MiddleLeft;
        // 
        // c_ProbeLogTabPage
        // 
        c_ProbeLogTabPage.Controls.Add(c_ProbeLogListView);
        c_ProbeLogTabPage.Location = new Point(4, 24);
        c_ProbeLogTabPage.Margin = new Padding(0);
        c_ProbeLogTabPage.Name = "c_ProbeLogTabPage";
        c_ProbeLogTabPage.Size = new Size(339, 175);
        c_ProbeLogTabPage.TabIndex = 1;
        c_ProbeLogTabPage.Text = "Log";
        c_ProbeLogTabPage.UseVisualStyleBackColor = true;
        // 
        // c_ProbeLogListView
        // 
        c_ProbeLogListView.Dock = DockStyle.Fill;
        c_ProbeLogListView.GridLines = true;
        c_ProbeLogListView.HeaderStyle = ColumnHeaderStyle.None;
        c_ProbeLogListView.LabelWrap = false;
        c_ProbeLogListView.Location = new Point(0, 0);
        c_ProbeLogListView.Margin = new Padding(0);
        c_ProbeLogListView.Name = "c_ProbeLogListView";
        c_ProbeLogListView.Size = new Size(339, 175);
        c_ProbeLogListView.TabIndex = 0;
        c_ProbeLogListView.UseCompatibleStateImageBehavior = false;
        c_ProbeLogListView.View = View.Details;
        // 
        // MainForm
        // 
        AutoScaleDimensions = new SizeF(7F, 15F);
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(353, 295);
        Controls.Add(c_MainTableLayout);
        FormBorderStyle = FormBorderStyle.FixedSingle;
        HelpButton = true;
        MaximizeBox = false;
        MinimizeBox = false;
        MinimumSize = new Size(256, 128);
        Name = "MainForm";
        SizeGripStyle = SizeGripStyle.Hide;
        Text = "NetCheck";
        Closing += OnClosing;
        c_MainTableLayout.ResumeLayout(false);
        c_MainTabControl.ResumeLayout(false);
        c_ProbeInfoTabPage.ResumeLayout(false);
        c_InfoTableLayout.ResumeLayout(false);
        c_ProbeLogTabPage.ResumeLayout(false);
        ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel c_MainTableLayout;
    private TabControl c_MainTabControl;
    private TabPage c_ProbeInfoTabPage;
    private TabPage c_ProbeLogTabPage;
    private TableLayoutPanel c_InfoTableLayout;
    private Label c_LastPingTitleLabel;
    private Label c_LastPingValueLabel;
    private Label c_AveragePingTitleLabel;
    private Label c_AveragePingValueLabel;
    private Label c_NetworkTypeTitleLabel;
    private Label c_NetworkTypeValueLabel;
    private ListView c_ProbeLogListView;
}
