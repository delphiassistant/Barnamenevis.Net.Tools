using System.Windows;

namespace Barnamenevis.Net.RtlMessageBox.Wpf.Demo
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Configure custom font for themed dialogs - use Vazirmatn which is included
            RtlMessageBox.PreferredFontName = "Vazirmatn FD";
            RtlMessageBox.PreferredFontPointSize = 13;
            RtlMessageBox.ApplyCustomFont = true;
            RtlMessageBox.UseCustomTitleBar = false;
        }

        // WPF RtlMessageBox demos (pure WPF)
        private void BtnThOk_Click(object sender, RoutedEventArgs e)
        {
            var r = RtlMessageBox.Show("این یک پیام WPF است.", "پیغام");
            ResultText.Text = $"Result: {r}";
        }

        private void BtnThOkCancel_Click(object sender, RoutedEventArgs e)
        {
            var r = RtlMessageBox.Show(this, "این یک پیام WPF با اطلاعات است.", "اطلاع", MessageBoxButton.OKCancel, MessageBoxImage.Information, MessageBoxResult.OK);
            ResultText.Text = $"Result: {r}";
        }

        private void BtnThYesNo_Click(object sender, RoutedEventArgs e)
        {
            var r = RtlMessageBox.Show("آیا با شرایط موافقید؟", "سوال", MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
            ResultText.Text = $"Result: {r}";
        }

        private void BtnThYesNoCancel_Click(object sender, RoutedEventArgs e)
        {
            var r = RtlMessageBox.Show(this, "تغییرات ذخیره شود؟", "هشدار", MessageBoxButton.YesNoCancel, MessageBoxImage.Warning, MessageBoxResult.Yes);
            ResultText.Text = $"Result: {r}";
        }

        private void BtnThError_Click(object sender, RoutedEventArgs e)
        {
            var r = RtlMessageBox.Show(this, "خطای جدی رخ داده است!", "خطای سیستم", MessageBoxButton.OK, MessageBoxImage.Error);
            ResultText.Text = $"Result: {r}";
        }
    }
}