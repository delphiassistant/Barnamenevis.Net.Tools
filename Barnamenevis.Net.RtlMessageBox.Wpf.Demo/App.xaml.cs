using System.Windows;
using Barnamenevis.Net.Tools;

namespace Barnamenevis.Net.RtlMessageBox.Wpf.Demo
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Install fonts from the Fonts subdirectory at application startup (silent)
            try
            {
                var installedCount = FontInstaller.InstallApplicationFonts();
                if (installedCount > 0)
                {
                    // Update RtlMessageBox to use installed Persian font
                    global::Barnamenevis.Net.RtlMessageBox.Wpf.RtlMessageBox.PreferredFontName = "Vazirmatn FD";
                }
                else
                {
                    // Still configure the font in case it was previously installed
                    global::Barnamenevis.Net.RtlMessageBox.Wpf.RtlMessageBox.PreferredFontName = "Vazirmatn FD";
                }
            }
            catch (System.Exception)
            {
                // Silent failure - don't crash the app if font installation fails
                // Fall back to default system fonts
            }

            base.OnStartup(e);
        }
    }
}
