# Barnamenevis.Net.Tools

A comprehensive utility library for .NET applications providing tools for Persian application development, starting with advanced font management capabilities.

## 📋 Overview

**Barnamenevis.Net.Tools** is designed to be a comprehensive toolkit for Persian .NET application development. Currently, the library focuses on **FontInstaller** functionality, with additional developer tools and utilities planned for future releases.

### 🔧 Current Features (v1.0)

The library currently provides the **FontInstaller** class, which allows .NET applications to automatically install fonts for the current user, making it easy to distribute applications with custom Persian, or other specialized fonts. The installation is per-user only, requires no admin privileges, and fonts persist after the application closes.

### 🚀 Future Roadmap

**Barnamenevis.Net.Tools** will be expanded with additional utility classes and tools for Persian application development, including:
- **Persian Text Processing** utilities
- **RTL Layout Helpers** for custom controls
- **Persian Date/Calendar** utilities  
- **Keyboard Layout** management tools
- **Persian Number Formatting** utilities
- **Cultural Localization** helpers

*Stay tuned for upcoming releases with these additional features!*

## ✨ Current Key Features (FontInstaller)

### 📝 Font Installation
- **Per-User Installation**: Fonts installed only for current user (no admin rights required)
- **Multiple Format Support**: TTF, OTF, WOFF, WOFF2, EOT
- **Automatic Detection**: Scans directories recursively for font files
- **Duplicate Prevention**: Skips fonts that are already installed
- **Registry Management**: Properly registers fonts in Windows Registry

### 🛡️ Safety & Reliability
- **Silent Operation**: No user prompts or UI interruptions
- **Error Handling**: Graceful failure handling for individual fonts
- **Memory Management**: Proper cleanup of Win32 resources
- **Thread Safe**: Can be called from any thread

### 🔧 Flexible Usage
- **Directory Scanning**: Install all fonts from a directory
- **Application Integration**: Install fonts from app's `Fonts` subdirectory
- **Custom Paths**: Install fonts from any specified location
- **Uninstall Support**: Remove previously installed fonts

## 🚀 Quick Start

### Basic Usage

```csharp
using Barnamenevis.Net.Tools;

// Install fonts from application's "Fonts" subdirectory
int installedCount = FontInstaller.InstallApplicationFonts();
Console.WriteLine($"نصب شد {installedCount} فونت جدید");

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
            Console.WriteLine($"✅ با موفقیت {installedCount} فونت جدید نصب شد");
            
            // Configure your UI libraries to use the installed fonts
            // Example for RtlMessageBox:
            RtlMessageBox.PreferredFontName = "Vazirmatn FD";
            RtlMessageBox.ApplyCustomFont = true;
        }
        else
        {
            Console.WriteLine("ℹ️ فونت جدیدی برای نصب وجود ندارد (احتمالاً قبلاً نصب شده)");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"⚠️ نصب فونت با خطا مواجه شد: {ex.Message}");
        // Application continues with system fonts
    }
    
    // Continue with normal application startup
    Application.Run(new MainForm()); // WinForms
    // or base.OnStartup(e); // WPF
}
```

## ⚙️ Requirements

- **.NET 6.0, 7.0, 8.0, or 9.0**
- **Windows 10/11** (Win32 font APIs)
- **File System Access** to application directory and user local data

## 📖 API Reference

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

## 🔗 Integration with RTL MessageBox Libraries

This library works seamlessly with the RTL MessageBox libraries in this solution:

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
    
    Console.WriteLine($"رابط کاربری فارسی با {installedCount} فونت جدید پیکربندی شد");
}
```

## 📚 See Also

- [WPF RTL MessageBox](../Barnamenevis.Net.RtlMessageBox.Wpf/README.md) - WPF MessageBox with font integration
- [Windows Forms RTL MessageBox](../Barnamenevis.Net.RtlMessageBox.WindowsForms/README.md) - Windows Forms MessageBox with font integration
- [Vazirmatn Font](https://rastikerdar.github.io/vazirmatn/) - Recommended Persian font family

## 🤝 Contributing

We welcome contributions! If you have ideas for additional tools and utilities that would benefit Persian .NET developers, please:

1. Open an issue to discuss your ideas
2. Submit pull requests for new features
3. Report bugs or suggest improvements
4. Help with documentation and examples

Together, we can build a comprehensive toolkit for Persian .NET application development!