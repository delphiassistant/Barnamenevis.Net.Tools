namespace Barnamenevis.Net.RtlMessageBox.WindowsForms.Demo;

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
        this.mainPanel = new TableLayoutPanel();
        this.titleLabel = new Label();
        this.btnOkDialog = new Button();
        this.btnOkCancelWarning = new Button();
        this.btnYesNoQuestion = new Button();
        this.btnYesNoCancelError = new Button();
        this.btnInfoDialog = new Button();
        this.btnCancelRetryStop = new Button();
        this.mainPanel.SuspendLayout();
        this.SuspendLayout();
        // 
        // mainPanel
        // 
        this.mainPanel.ColumnCount = 2;
        this.mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        this.mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
        this.mainPanel.Controls.Add(this.btnOkDialog, 0, 0);
        this.mainPanel.Controls.Add(this.btnOkCancelWarning, 1, 0);
        this.mainPanel.Controls.Add(this.btnYesNoQuestion, 0, 1);
        this.mainPanel.Controls.Add(this.btnYesNoCancelError, 1, 1);
        this.mainPanel.Controls.Add(this.btnInfoDialog, 0, 2);
        this.mainPanel.Controls.Add(this.btnCancelRetryStop, 1, 2);
        this.mainPanel.Dock = DockStyle.Fill;
        this.mainPanel.Location = new Point(0, 40);
        this.mainPanel.Name = "mainPanel";
        this.mainPanel.Padding = new Padding(20);
        this.mainPanel.RowCount = 4;
        this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 25F));
        this.mainPanel.Size = new Size(800, 410);
        this.mainPanel.TabIndex = 0;
        // 
        // titleLabel
        // 
        this.titleLabel.BackColor = Color.LightBlue;
        this.titleLabel.Dock = DockStyle.Top;
        this.titleLabel.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
        this.titleLabel.Location = new Point(0, 0);
        this.titleLabel.Name = "titleLabel";
        this.titleLabel.Size = new Size(800, 40);
        this.titleLabel.TabIndex = 1;
        this.titleLabel.Text = "RtlMessageBox Demo - Win32 MessageBox with RTL + Persian Support";
        this.titleLabel.TextAlign = ContentAlignment.MiddleCenter;
        // 
        // btnOkDialog
        // 
        this.btnOkDialog.Dock = DockStyle.Fill;
        this.btnOkDialog.Location = new Point(30, 30);
        this.btnOkDialog.Margin = new Padding(10);
        this.btnOkDialog.Name = "btnOkDialog";
        this.btnOkDialog.Size = new Size(360, 82);
        this.btnOkDialog.TabIndex = 0;
        this.btnOkDialog.Text = "OK Dialog";
        this.btnOkDialog.UseVisualStyleBackColor = true;
        // 
        // btnOkCancelWarning
        // 
        this.btnOkCancelWarning.Dock = DockStyle.Fill;
        this.btnOkCancelWarning.Location = new Point(410, 30);
        this.btnOkCancelWarning.Margin = new Padding(10);
        this.btnOkCancelWarning.Name = "btnOkCancelWarning";
        this.btnOkCancelWarning.Size = new Size(360, 82);
        this.btnOkCancelWarning.TabIndex = 1;
        this.btnOkCancelWarning.Text = "OK/Cancel + Warning";
        this.btnOkCancelWarning.UseVisualStyleBackColor = true;
        // 
        // btnYesNoQuestion
        // 
        this.btnYesNoQuestion.Dock = DockStyle.Fill;
        this.btnYesNoQuestion.Location = new Point(30, 132);
        this.btnYesNoQuestion.Margin = new Padding(10);
        this.btnYesNoQuestion.Name = "btnYesNoQuestion";
        this.btnYesNoQuestion.Size = new Size(360, 82);
        this.btnYesNoQuestion.TabIndex = 2;
        this.btnYesNoQuestion.Text = "Yes/No + Question";
        this.btnYesNoQuestion.UseVisualStyleBackColor = true;
        // 
        // btnYesNoCancelError
        // 
        this.btnYesNoCancelError.Dock = DockStyle.Fill;
        this.btnYesNoCancelError.Location = new Point(410, 132);
        this.btnYesNoCancelError.Margin = new Padding(10);
        this.btnYesNoCancelError.Name = "btnYesNoCancelError";
        this.btnYesNoCancelError.Size = new Size(360, 82);
        this.btnYesNoCancelError.TabIndex = 3;
        this.btnYesNoCancelError.Text = "Yes/No/Cancel + Error";
        this.btnYesNoCancelError.UseVisualStyleBackColor = true;
        // 
        // btnInfoDialog
        // 
        this.btnInfoDialog.Dock = DockStyle.Fill;
        this.btnInfoDialog.Location = new Point(30, 234);
        this.btnInfoDialog.Margin = new Padding(10);
        this.btnInfoDialog.Name = "btnInfoDialog";
        this.btnInfoDialog.Size = new Size(360, 82);
        this.btnInfoDialog.TabIndex = 4;
        this.btnInfoDialog.Text = "Info Dialog";
        this.btnInfoDialog.UseVisualStyleBackColor = true;
        // 
        // btnCancelRetryStop
        // 
        this.btnCancelRetryStop.Dock = DockStyle.Fill;
        this.btnCancelRetryStop.Location = new Point(410, 234);
        this.btnCancelRetryStop.Margin = new Padding(10);
        this.btnCancelRetryStop.Name = "btnCancelRetryStop";
        this.btnCancelRetryStop.Size = new Size(360, 82);
        this.btnCancelRetryStop.TabIndex = 5;
        this.btnCancelRetryStop.Text = "Cancel/Retry + Stop";
        this.btnCancelRetryStop.UseVisualStyleBackColor = true;
        // 
        // MainForm
        // 
        this.AutoScaleMode = AutoScaleMode.Font;
        this.ClientSize = new Size(800, 450);
        this.Controls.Add(this.mainPanel);
        this.Controls.Add(this.titleLabel);
        this.Name = "MainForm";
        this.StartPosition = FormStartPosition.CenterScreen;
        this.Text = "RtlMessageBox Windows Forms Demo";
        this.mainPanel.ResumeLayout(false);
        this.ResumeLayout(false);
    }

    #endregion

    private TableLayoutPanel mainPanel;
    private Label titleLabel;
    private Button btnOkDialog;
    private Button btnOkCancelWarning;
    private Button btnYesNoQuestion;
    private Button btnYesNoCancelError;
    private Button btnInfoDialog;
    private Button btnCancelRetryStop;
}
