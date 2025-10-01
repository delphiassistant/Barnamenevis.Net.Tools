using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace Barnamenevis.Net.Tools
{
    /// <summary>
    /// Utility class to install fonts from a local folder to Windows for the current user only
    /// </summary>
    public static class FontInstaller
    {
        // Win32 API for font installation
        [DllImport("gdi32.dll", EntryPoint = "AddFontResourceEx", SetLastError = true)]
        private static extern int AddFontResourceEx([In][MarshalAs(UnmanagedType.LPStr)] string lpszFilename,
            uint fl, IntPtr pdv);

        [DllImport("gdi32.dll", EntryPoint = "RemoveFontResourceEx", SetLastError = true)]
        private static extern bool RemoveFontResourceEx([In][MarshalAs(UnmanagedType.LPStr)] string lpFileName,
            uint fl, IntPtr pdv);

        [DllImport("user32.dll", EntryPoint = "SendMessage", SetLastError = true)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        private const int HWND_BROADCAST = 0xFFFF;
        private const uint WM_FONTCHANGE = 0x001D;
        private const uint FR_PRIVATE = 0x10;      // Font is private to the process
        private const uint FR_NOT_ENUM = 0x20;     // Font is not enumerable

        /// <summary>
        /// Scans the specified fonts directory and installs all supported font files
        /// for the current user only (no admin privileges required)
        /// </summary>
        /// <param name="fontsDirectoryPath">Path to the directory containing font files</param>
        /// <returns>Number of fonts that were newly installed</returns>
        public static int InstallFontsFromDirectory(string fontsDirectoryPath)
        {
            if (string.IsNullOrWhiteSpace(fontsDirectoryPath))
            {
                throw new ArgumentException("Fonts directory path cannot be null or empty", nameof(fontsDirectoryPath));
            }

            if (!Directory.Exists(fontsDirectoryPath))
            {
                return 0;
            }

            var supportedExtensions = new[] { ".ttf", ".otf", ".woff", ".woff2", ".eot" };
            int installedCount = 0;

            try
            {
                var fontFiles = Directory.GetFiles(fontsDirectoryPath, "*.*", SearchOption.AllDirectories)
                    .Where(file => supportedExtensions.Contains(Path.GetExtension(file).ToLowerInvariant()));

                foreach (var fontFile in fontFiles)
                {
                    try
                    {
                        if (InstallFont(fontFile))
                        {
                            installedCount++;
                        }
                    }
                    catch (Exception)
                    {
                        // Silent failure for individual fonts
                    }
                }

                // Notify all applications that the font list has changed
                if (installedCount > 0)
                {
                    SendMessage((IntPtr)HWND_BROADCAST, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
                }
            }
            catch (Exception)
            {
                // Silent failure
            }

            return installedCount;
        }

        /// <summary>
        /// Installs a single font file for the current user if it's not already installed
        /// </summary>
        /// <param name="fontFilePath">Path to the font file</param>
        /// <returns>True if the font was newly installed, false if already installed or failed</returns>
        private static bool InstallFont(string fontFilePath)
        {
            if (!File.Exists(fontFilePath))
                return false;

            var fileName = Path.GetFileName(fontFilePath);
            var fileNameWithoutExt = Path.GetFileNameWithoutExtension(fontFilePath);

            // Check if font is already installed by looking in the user-specific registry
            if (IsFontInstalled(fileNameWithoutExt, fileName))
            {
                return false; // Already installed
            }

            // Get user's local fonts directory
            var userFontsDir = GetUserFontsDirectory();
            Directory.CreateDirectory(userFontsDir); // Ensure directory exists
            var destinationPath = Path.Combine(userFontsDir, fileName);

            try
            {
                // Copy the font file to user's fonts directory
                if (!File.Exists(destinationPath))
                {
                    File.Copy(fontFilePath, destinationPath, overwrite: false);
                }

                // Register the font with Windows for current user only
                int result = AddFontResourceEx(destinationPath, FR_PRIVATE, IntPtr.Zero);

                if (result > 0)
                {
                    // Add registry entry for the font in current user's registry
                    RegisterFontInUserRegistry(fileNameWithoutExt, fileName);
                    return true;
                }
                else
                {
                    // Clean up the copied file if registration failed
                    try { File.Delete(destinationPath); } catch { }
                }
            }
            catch (Exception)
            {
                // Silent failure
            }

            return false;
        }

        /// <summary>
        /// Gets the user-specific fonts directory (creates if not exists)
        /// </summary>
        /// <returns>Path to user's fonts directory</returns>
        private static string GetUserFontsDirectory()
        {
            // Use LocalApplicationData for user-specific fonts
            var localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            return Path.Combine(localAppData, "Microsoft", "Windows", "Fonts");
        }

        /// <summary>
        /// Checks if a font is already installed by examining the user's fonts registry
        /// </summary>
        /// <param name="fontName">Font name without extension</param>
        /// <param name="fileName">Font file name with extension</param>
        /// <returns>True if the font is already installed</returns>
        private static bool IsFontInstalled(string fontName, string fileName)
        {
            try
            {
                // Check user-specific fonts registry first
                using var userKey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");
                if (userKey != null)
                {
                    foreach (var valueName in userKey.GetValueNames())
                    {
                        var value = userKey.GetValue(valueName)?.ToString();
                        if (!string.IsNullOrEmpty(value) &&
                            (value.Equals(fileName, StringComparison.OrdinalIgnoreCase) ||
                             valueName.Contains(fontName, StringComparison.OrdinalIgnoreCase)))
                        {
                            return true;
                        }
                    }
                }

                // Also check if font exists in user's fonts directory
                var userFontsDir = GetUserFontsDirectory();
                var fontPath = Path.Combine(userFontsDir, fileName);
                if (File.Exists(fontPath))
                {
                    return true;
                }
            }
            catch (Exception)
            {
                // Silent failure
            }

            return false;
        }

        /// <summary>
        /// Registers the font in the current user's registry
        /// </summary>
        /// <param name="fontName">Font name without extension</param>
        /// <param name="fileName">Font file name with extension</param>
        private static void RegisterFontInUserRegistry(string fontName, string fileName)
        {
            try
            {
                using var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", true);
                if (key != null)
                {
                    // Use full path for user fonts
                    var userFontsDir = GetUserFontsDirectory();
                    var fullPath = Path.Combine(userFontsDir, fileName);
                    
                    // Use a reasonable font display name  
                    var fontDisplayName = fontName + " (TrueType)";
                    key.SetValue(fontDisplayName, fullPath);
                }
                else
                {
                    // Create the key if it doesn't exist
                    using var newKey = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts");
                    if (newKey != null)
                    {
                        var userFontsDir = GetUserFontsDirectory();
                        var fullPath = Path.Combine(userFontsDir, fileName);
                        var fontDisplayName = fontName + " (TrueType)";
                        newKey.SetValue(fontDisplayName, fullPath);
                    }
                }
            }
            catch (Exception)
            {
                // Silent failure
            }
        }

        /// <summary>
        /// Installs fonts from the default "Fonts" subdirectory of the application
        /// </summary>
        /// <returns>Number of fonts that were newly installed</returns>
        public static int InstallApplicationFonts()
        {
            var appDir = AppDomain.CurrentDomain.BaseDirectory;
            var fontsDir = Path.Combine(appDir, "Fonts");
            return InstallFontsFromDirectory(fontsDir);
        }

        /// <summary>
        /// Uninstalls a font for the current user
        /// </summary>
        /// <param name="fontFileName">Font file name</param>
        /// <returns>True if successfully uninstalled</returns>
        public static bool UninstallFont(string fontFileName)
        {
            try
            {
                var userFontsDir = GetUserFontsDirectory();
                var fontPath = Path.Combine(userFontsDir, fontFileName);

                if (File.Exists(fontPath))
                {
                    // Remove from Windows font resources
                    RemoveFontResourceEx(fontPath, FR_PRIVATE, IntPtr.Zero);

                    // Remove registry entry
                    using var key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts", true);
                    if (key != null)
                    {
                        var valuesToRemove = key.GetValueNames()
                            .Where(name => key.GetValue(name)?.ToString()?.Contains(fontFileName, StringComparison.OrdinalIgnoreCase) == true)
                            .ToList();
                        
                        foreach (var value in valuesToRemove)
                        {
                            key.DeleteValue(value);
                        }
                    }

                    // Delete the font file
                    File.Delete(fontPath);

                    // Notify applications
                    SendMessage((IntPtr)HWND_BROADCAST, WM_FONTCHANGE, IntPtr.Zero, IntPtr.Zero);
                    return true;
                }
            }
            catch (Exception)
            {
                // Silent failure
            }

            return false;
        }
    }
}