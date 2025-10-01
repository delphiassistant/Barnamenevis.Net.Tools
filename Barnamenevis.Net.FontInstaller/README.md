# Barnamenevis.Net.Tools (FontInstaller)

A utility library for installing fonts programmatically in Windows applications. Provides per-user font installation without requiring administrator privileges, perfect for bundling custom fonts with your applications.

## ?? Overview

The FontInstaller class allows .NET applications to automatically install fonts for the current user, making it easy to distribute applications with custom Persian, Arabic, or other specialized fonts. The installation is per-user only, requires no admin privileges, and fonts persist after the application closes.

## ? Key Features

### ?? Font Installation
- **Per-User Installation**: Fonts installed only for current user (no admin rights required)
- **Multiple Format Support**: TTF, OTF, WOFF, WOFF2, EOT
- **Automatic Detection**: Scans directories recursively for font files
- **Duplicate Prevention**: Skips fonts that are already installed
- **Registry Management**: Properly registers fonts in Windows Registry

### ??? Safety & Reliability
- **Silent Operation**: No user prompts or UI interruptions
- **Error Handling**: Graceful failure handling for individual fonts
- **Memory Management**: Proper cleanup of Win32 resources
- **Thread Safe**: Can be called from any thread

### ?? Flexible Usage
- **Directory Scanning**: Install all fonts from a directory
- **Application Integration**: Install fonts from app's `Fonts` subdirectory
- **Custom Paths**: Install fonts from any specified location
- **Uninstall Support**: Remove previously installed fonts

## ?? Quick Start

### Basic Usage

```csharp
using Barnamenevis.Net.Tools;

// Install fonts from application's "Fonts" subdirectory
int installedCount = FontInstaller.InstallApplicationFonts();
Console.WriteLine($"Installed {installedCount} new fonts");

// Install fonts from a custom directory
int count = FontInstaller.InstallFontsFromDirectory(@"C:\MyFonts");

// Install from relative path
int count = FontInstaller.InstallFontsFromDirectory("./Resources/Fonts");
```

### Application Startup Integration

```csharp
// In Program.cs (Console/WinForms) or App.xaml.cs (WPF)
static void Main() // or protected override void OnStartup(StartupEventArgs e)
{
    try
    {
        // Install fonts at application startup
        int installedCount = FontInstaller.InstallApplicationFonts();
        
        if (installedCount > 0)
        {
            Console.WriteLine($"? Successfully installed {installedCount} new font(s)");
            
            // Configure your UI libraries to use the installed fonts
            // Example for RtlMessageBox:
            RtlMessageBox.PreferredFontName = "Vazirmatn FD";
            RtlMessageBox.ApplyCustomFont = true;
        }
        else
        {
            Console.WriteLine("?? No new fonts to install (may already be installed)");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"?? Font installation failed: {ex.Message}");
        // Application continues with system fonts
    }
    
    // Continue with normal application startup
    Application.Run(new MainForm()); // WinForms
    // or base.OnStartup(e); // WPF
}
```

## ?? API Reference

### Static Methods

#### `InstallApplicationFonts()`
Installs fonts from the application's default "Fonts" subdirectory.

```csharp
public static int InstallApplicationFonts()
```

**Returns**: Number of fonts that were newly installed (0 if none or already installed)

**Example**:
```csharp
int count = FontInstaller.InstallApplicationFonts();
// Looks for fonts in: [AppDirectory]/Fonts/
```

#### `InstallFontsFromDirectory(string)`
Installs fonts from a specified directory path.

```csharp
public static int InstallFontsFromDirectory(string fontsDirectoryPath)
```

**Parameters**:
- `fontsDirectoryPath`: Path to directory containing font files

**Returns**: Number of fonts that were newly installed

**Throws**: `ArgumentException` if path is null/empty

**Example**:
```csharp
// Absolute path
int count = FontInstaller.InstallFontsFromDirectory(@"C:\MyProject\Assets\Fonts");

// Relative path
int count = FontInstaller.InstallFontsFromDirectory("./Resources/Fonts");

// Network path
int count = FontInstaller.InstallFontsFromDirectory(@"\\server\share\Fonts");
```

#### `UninstallFont(string)`
Removes a previously installed font for the current user.

```csharp
public static bool UninstallFont(string fontFileName)
```

**Parameters**:
- `fontFileName`: Name of the font file (e.g., "Vazirmatn-FD-Regular.ttf")

**Returns**: `true` if successfully uninstalled, `false` otherwise

**Example**:
```csharp
bool success = FontInstaller.UninstallFont("Vazirmatn-FD-Regular.ttf");
```

## ?? Project Setup

### Required .csproj Configuration

To copy font files to your application's output directory, add this to your `.csproj` file:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <!-- Your existing PropertyGroup -->
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
  </PropertyGroup>

  <!-- Copy Fonts folder to output directory -->
  <ItemGroup>
    <None Include="Fonts\**\*.*">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>
  
  <!-- Reference to FontInstaller -->
  <ItemGroup>
    <ProjectReference Include="path\to\Barnamenevis.Net.Tools.csproj" />
  </ItemGroup>
</Project>
```

### Directory Structure

```
YourApplication/
??? YourApp.csproj
??? Program.cs
??? Fonts/                    # Font files directory
?   ??? Vazirmatn-FD-Regular.ttf
?   ??? Vazirmatn-FD-Bold.ttf
?   ??? Subdirectory/        # Subdirectories are scanned too
?       ??? AnotherFont.otf
??? bin/Debug/net8.0-windows/
    ??? Fonts/               # Copied by build process
        ??? Vazirmatn-FD-Regular.ttf
        ??? ...
```

## ?? Usage Examples

### Console Application with Fonts

```csharp
using Barnamenevis.Net.Tools;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== Font Installation Demo ===");
        
        // Show current directory info
        var appDir = AppDomain.CurrentDomain.BaseDirectory;
        var fontsDir = Path.Combine(appDir, "Fonts");
        
        Console.WriteLine($"Application directory: {appDir}");
        Console.WriteLine($"Fonts directory: {fontsDir}");
        
        if (Directory.Exists(fontsDir))
        {
            var fontFiles = Directory.GetFiles(fontsDir, "*.*", SearchOption.AllDirectories)
                .Where(f => new[] { ".ttf", ".otf", ".woff", ".woff2", ".eot" }
                    .Contains(Path.GetExtension(f).ToLowerInvariant()));
            
            Console.WriteLine($"Found {fontFiles.Count()} font files to install");
            
            // Install fonts
            var installedCount = FontInstaller.InstallApplicationFonts();
            
            if (installedCount > 0)
            {
                Console.WriteLine($"? Successfully installed {installedCount} new font(s)");
            }
            else
            {
                Console.WriteLine("?? No new fonts installed (may already exist)");
            }
        }
        else
        {
            Console.WriteLine("?? Fonts directory not found");
        }
        
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }
}
```

### WPF Application with Font Integration

```csharp
// App.xaml.cs
using System.Windows;
using Barnamenevis.Net.Tools;

namespace MyWpfApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Install fonts silently at startup
            try
            {
                var installedCount = FontInstaller.InstallApplicationFonts();
                
                // Configure UI to use installed Persian font
                if (installedCount > 0 || IsFontInstalled("Vazirmatn FD"))
                {
                    // Font is available - configure your UI libraries
                    SetPersianFontConfiguration();
                }
            }
            catch
            {
                // Silent failure - app continues with system fonts
            }
            
            base.OnStartup(e);
        }
        
        private void SetPersianFontConfiguration()
        {
            // Configure RtlMessageBox to use Persian font
            Barnamenevis.Net.RtlMessageBox.Wpf.RtlMessageBox.PreferredFontName = "Vazirmatn FD";
            Barnamenevis.Net.RtlMessageBox.Wpf.RtlMessageBox.ApplyCustomFont = true;
            
            // Set application-wide font
            this.Resources["DefaultFontFamily"] = new FontFamily("Vazirmatn FD");
        }
        
        private bool IsFontInstalled(string fontName)
        {
            return Fonts.SystemFontFamilies.Any(f => 
                f.Source.Contains(fontName, StringComparison.OrdinalIgnoreCase));
        }
    }
}
```

### Custom Font Directory Installation

```csharp
public class CustomFontManager
{
    private readonly string[] _fontDirectories = 
    {
        "./Assets/Fonts",
        "./Resources/Typography", 
        @"C:\CompanyFonts"
    };
    
    public int InstallAllFonts()
    {
        int totalInstalled = 0;
        
        foreach (var directory in _fontDirectories)
        {
            if (Directory.Exists(directory))
            {
                try
                {
                    int count = FontInstaller.InstallFontsFromDirectory(directory);
                    totalInstalled += count;
                    Console.WriteLine($"Installed {count} fonts from {directory}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to install from {directory}: {ex.Message}");
                }
            }
        }
        
        return totalInstalled;
    }
}
```

## ?? Technical Details

### Installation Process

1. **Directory Scanning**: Recursively scans for supported font files
2. **Duplicate Check**: Verifies font isn't already installed (registry + file check)
3. **File Copy**: Copies font to user's local fonts directory
4. **Font Registration**: Registers font with Windows using `AddFontResourceEx`
5. **Registry Entry**: Adds entry to user's font registry
6. **Notification**: Notifies system that font list has changed

### File Locations

#### User Fonts Directory
Fonts are installed to:
```
%LOCALAPPDATA%\Microsoft\Windows\Fonts\
```
Example: `C:\Users\YourName\AppData\Local\Microsoft\Windows\Fonts\`

#### Registry Location
Font registrations are stored in:
```
HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts
```

### Supported Font Formats

| Extension | Format | Support |
|-----------|--------|---------|
| `.ttf` | TrueType Font | Full |
| `.otf` | OpenType Font | Full |
| `.woff` | Web Open Font Format | Limited |
| `.woff2` | Web Open Font Format 2 | Limited |
| `.eot` | Embedded OpenType | Limited |

**Note**: WOFF/WOFF2/EOT formats have limited Windows support and are primarily for web use.

### Win32 API Integration

The library uses several Win32 APIs:
- `AddFontResourceEx` - Register font with Windows
- `RemoveFontResourceEx` - Unregister font
- `SendMessage(WM_FONTCHANGE)` - Notify applications of font changes
- `Registry APIs` - Manage font registry entries

## ?? Important Notes

### User vs System Installation
- **Per-User Only**: Fonts are installed only for the current user account
- **No Admin Rights**: No UAC prompts or administrator privileges required
- **Persistence**: Fonts remain available after application closes
- **Isolation**: Other users on the same computer won't see these fonts

### Error Handling
- **Silent Failures**: Individual font installation failures don't crash the application
- **Graceful Degradation**: Applications continue with system fonts if installation fails
- **Return Values**: Methods return counts/booleans to indicate success

### Performance Considerations
- **Fast Operation**: Font installation is typically very quick
- **Startup Integration**: Suitable for application startup (non-blocking)
- **Caching**: Already-installed fonts are detected and skipped quickly

## ?? Requirements

- **.NET 8.0** or later
- **Windows 10/11** (Win32 font APIs)
- **File System Access** to application directory and user local data

## ??? Integration Examples

### With RtlMessageBox Libraries

```csharp
// Install fonts and configure RTL MessageBox in one step
public static void SetupPersianUI()
{
    var installedCount = FontInstaller.InstallApplicationFonts();
    
    // Configure WPF RTL MessageBox
    Barnamenevis.Net.RtlMessageBox.Wpf.RtlMessageBox.PreferredFontName = "Vazirmatn FD";
    Barnamenevis.Net.RtlMessageBox.Wpf.RtlMessageBox.ApplyCustomFont = true;
    
    // Configure Windows Forms RTL MessageBox  
    Barnamenevis.Net.RtlMessageBox.WindowsForms.RtlMessageBox.PreferredFontName = "Vazirmatn FD";
    Barnamenevis.Net.RtlMessageBox.WindowsForms.RtlMessageBox.ApplyCustomFont = true;
    
    Console.WriteLine($"Persian UI configured with {installedCount} new fonts");
}
```

### Font Download Integration

```csharp
public class PersianFontSetup
{
    private const string VAZIRMATN_URL = "https://github.com/rastikerdar/vazirmatn/releases/download/v33.003/vazirmatn-font-v33.003.zip";
    
    public async Task<bool> SetupPersianFontsAsync()
    {
        try
        {
            // Try installing existing fonts first
            var existingCount = FontInstaller.InstallApplicationFonts();
            if (existingCount > 0)
            {
                return true; // Success with existing fonts
            }
            
            // Download and install fonts if none found
            await DownloadAndInstallVazirmamtnAsync();
            
            var newCount = FontInstaller.InstallApplicationFonts();
            return newCount > 0;
        }
        catch
        {
            return false; // Fall back to system fonts
        }
    }
    
    private async Task DownloadAndInstallVazirmamtnAsync()
    {
        // Implementation for downloading and extracting fonts
        // This is just an example - implement based on your needs
    }
}
```

## ?? See Also

- [WPF RTL MessageBox](../Barnamenevis.Net.RtlMessageBox.Wpf/README.md) - WPF MessageBox with font integration
- [Windows Forms RTL MessageBox](../Barnamenevis.Net.RtlMessageBox.WindowsForms/README.md) - Windows Forms MessageBox with font integration
- [Vazirmatn Font](https://rastikerdar.github.io/vazirmatn/) - Recommended Persian font family