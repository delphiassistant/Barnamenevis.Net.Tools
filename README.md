# RtlMessageBox Solutions (.NET 8)

A comprehensive collection of RTL (Right-to-Left) enabled MessageBox implementations for .NET applications with built-in Persian/Arabic language support and automatic font installation capabilities.

## ?? Overview

This solution provides RTL-aware MessageBox replacements for both **WPF** and **Windows Forms** applications, specifically designed for Persian, Arabic, and other RTL languages. The libraries feature automatic Persian button text translation, proper RTL layout, and integrated font installation for optimal typography.

## ?? Projects Structure

### Core Libraries

| Project | Description | Target Framework |
|---------|-------------|------------------|
| **Barnamenevis.Net.RtlMessageBox.Wpf** | Pure WPF implementation with themed UI | .NET 8.0-windows |
| **Barnamenevis.Net.RtlMessageBox.WindowsForms** | Win32 MessageBox wrapper with RTL support | .NET 8.0-windows |
| **Barnamenevis.Net.Tools** | Font installation utilities | .NET 8.0 |

### Demo Applications

| Project | Description | Features |
|---------|-------------|----------|
| **Barnamenevis.Net.RtlMessageBox.Wpf.Demo** | WPF demo with font auto-installation | Themed UI, Custom title bars |
| **Barnamenevis.Net.RtlMessageBox.WindowsForms.Demo** | Windows Forms demo application | Designer-based UI, Standard controls |

## ? Key Features

### ?? RTL Language Support
- **Automatic Persian Button Text**: OK? «??œ, Cancel?«‰’—«›, Yes?»·Â, No?Œ?—
- **Proper RTL Layout**: Right-to-left text flow and UI alignment
- **Custom Font Integration**: Automatic installation and usage of Persian fonts
- **Drop-in Replacement**: Compatible with standard MessageBox API

### ?? Advanced UI Features (WPF)
- **Themed MessageBox**: Modern, customizable appearance
- **Custom Title Bars**: Optional client-area title bars with preferred fonts
- **System Sound Integration**: Appropriate sounds for warnings and errors
- **Focus Management**: Proper keyboard navigation and focus indicators

### ?? Font Management
- **Automatic Font Installation**: Per-user font installation (no admin rights required)
- **Multiple Format Support**: TTF, OTF, WOFF, WOFF2, EOT
- **Runtime Font Detection**: Automatically uses installed Persian fonts
- **Font Persistence**: Fonts remain available after application closes

## ?? Quick Start

### WPF Applications

```csharp
using Barnamenevis.Net.RtlMessageBox.Wpf;

// Simple usage
var result = RtlMessageBox.Show("”·«„ œ‰?«!", "Å?€«„");

// With custom options
var result = RtlMessageBox.Show(
    "¬?« „ÿ„∆‰ Â” ?œø", 
    " √??œ", 
    MessageBoxButton.YesNo, 
    MessageBoxImage.Question);
```

### Windows Forms Applications

```csharp
using Barnamenevis.Net.RtlMessageBox.WindowsForms;

// Configure font (optional)
RtlMessageBox.PreferredFontName = "Vazirmatn FD";
RtlMessageBox.ApplyCustomFont = true;

// Simple usage
var result = RtlMessageBox.Show("”·«„ œ‰?«!", "Å?€«„");
```

### Font Installation

```csharp
using Barnamenevis.Net.Tools;

// Install fonts from application's Fonts directory
int installedCount = FontInstaller.InstallApplicationFonts();

// Install fonts from custom directory
int installedCount = FontInstaller.InstallFontsFromDirectory(@"C:\MyFonts");
```

## ?? Project Details

### Barnamenevis.Net.RtlMessageBox.Wpf
Pure WPF implementation featuring:
- Themed MessageBox with modern appearance
- Optional custom title bars using preferred fonts
- Built-in system sound integration
- Proper RTL layout and Persian button text
- Focus management and keyboard navigation

### Barnamenevis.Net.RtlMessageBox.WindowsForms
Win32 MessageBox wrapper featuring:
- Hook-based button text translation
- Custom font application to all dialog elements
- RTL reading and right-align options
- Compatible with all standard MessageBox overloads
- Automatic focus management

### Barnamenevis.Net.Tools (FontInstaller)
Font management utility featuring:
- Per-user font installation (no admin privileges required)
- Support for multiple font formats (TTF, OTF, WOFF, WOFF2, EOT)
- Automatic font detection and duplicate prevention
- Registry management for font persistence
- Silent operation with error handling

## ?? Requirements

- **.NET 8.0** or later
- **Windows 10/11** (Windows-specific font installation APIs)
- **WPF/Windows Forms** target frameworks as appropriate

## ??? Installation & Setup

### 1. Clone the Repository
```bash
git clone <repository-url>
cd RtlMessageBox
```

### 2. Build the Solution
```bash
dotnet build
```

### 3. Run Demo Applications
```bash
# WPF Demo
dotnet run --project Barnamenevis.Net.RtlMessageBox.Wpf.Demo

# Windows Forms Demo  
dotnet run --project Barnamenevis.Net.RtlMessageBox.WindowsForms.Demo
```

### 4. Using in Your Projects
Reference the appropriate project and start using the RTL-enabled MessageBox:
```xml
<ProjectReference Include="path\to\Barnamenevis.Net.RtlMessageBox.Wpf.csproj" />
```

## ?? Font Setup for Development

1. **Download Persian Fonts** (recommended: [Vazirmatn](https://rastikerdar.github.io/vazirmatn/))
2. **Place fonts in `Fonts` directory** of demo applications
3. **Configure project to copy fonts**:
   ```xml
   <ItemGroup>
     <None Include="Fonts\**\*.*">
       <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
     </None>
   </ItemGroup>
   ```

## ?? Contributing

Contributions are welcome! Please feel free to submit pull requests, report issues, or suggest improvements.

## ?? License

This project is open source. Please refer to the license file for details.

## ?? Related Resources

- [Vazirmatn Font Family](https://rastikerdar.github.io/vazirmatn/) - Recommended Persian font
- [Persian Typography Guidelines](https://github.com/Persian-Typographic) - Best practices for Persian text
- [.NET Globalization Documentation](https://docs.microsoft.com/en-us/dotnet/core/extensions/globalization) - Internationalization resources