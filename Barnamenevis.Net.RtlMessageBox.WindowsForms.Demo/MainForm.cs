using System.Windows;

namespace Barnamenevis.Net.RtlMessageBox.WindowsForms.Demo
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();

            // Configure RtlMessageBox for Persian fonts
            RtlMessageBox.PreferredFontName = "Vazirmatn FD";
            RtlMessageBox.PreferredFontPointSize = 8;
            RtlMessageBox.ApplyCustomFont = true;

            // Wire up event handlers
            btnOkDialog.Click += btnOkDialog_Click;
            btnOkCancelWarning.Click += btnOkCancelWarning_Click;
            btnYesNoQuestion.Click += btnYesNoQuestion_Click;
            btnYesNoCancelError.Click += btnYesNoCancelError_Click;
            btnInfoDialog.Click += btnInfoDialog_Click;
            btnCancelRetryStop.Click += btnCancelRetryStop_Click;
        }

        private void btnOkDialog_Click(object sender, EventArgs e)
        {
            var result = RtlMessageBox.Show(
                "این یک پیام نمونه است.",
                "پیغام",
                MessageBoxButton.OK,
                MessageBoxImage.None);
            System.Windows.Forms.MessageBox.Show($"Result: {result}", "Demo Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnOkCancelWarning_Click(object sender, EventArgs e)
        {
            var result = RtlMessageBox.Show(
                "آیا ادامه می‌دهید؟",
                "هشدار",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Warning);
            System.Windows.Forms.MessageBox.Show($"Result: {result}", "Demo Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnYesNoQuestion_Click(object sender, EventArgs e)
        {
            var result = RtlMessageBox.Show(
                "آیا با شرایط موافقید؟",
                "سوال",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);
            System.Windows.Forms.MessageBox.Show($"Result: {result}", "Demo Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnYesNoCancelError_Click(object sender, EventArgs e)
        {
            var result = RtlMessageBox.Show(
                "مشکلی رخ داده است. چه کاری انجام می‌دهید؟ مشکلی رخ داده است. چه کاری انجام می‌دهید؟ مشکلی رخ داده است. چه کاری انجام می‌دهید؟",
                "خطا",
                MessageBoxButton.YesNoCancel,
                MessageBoxImage.Error);
            System.Windows.Forms.MessageBox.Show($"Result: {result}", "Demo Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnInfoDialog_Click(object sender, EventArgs e)
        {
            var result = RtlMessageBox.Show(
                "اطلاعات مهم",
                "اطلاع",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            System.Windows.Forms.MessageBox.Show($"Result: {result}", "Demo Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnCancelRetryStop_Click(object sender, EventArgs e)
        {
            var result = RtlMessageBox.Show(
                "عملیات متوقف شد",
                "توقف",
                MessageBoxButton.OKCancel,
                MessageBoxImage.Stop);
            System.Windows.Forms.MessageBox.Show($"Result: {result}", "Demo Result", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
