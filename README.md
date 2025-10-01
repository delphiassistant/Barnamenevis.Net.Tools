# Barnamenevis.Net.Tools - RTL MessageBox Solutions

A comprehensive collection of RTL (Right-to-Left) enabled MessageBox implementations for .NET applications with built-in Persian/Arabic language support and automatic font installation capabilities.

## 📋 Overview

This solution provides RTL-aware MessageBox replacements for both **WPF** and **Windows Forms** applications, specifically designed for Persian, Arabic, and other RTL languages. The libraries feature automatic Persian button text translation, proper RTL layout, and integrated font installation for optimal typography.

The solution includes the **Barnamenevis.Net.Tools** library, which currently focuses on font installation utilities but is designed to be expanded with additional Persian/Arabic development tools in future releases.

## 📁 Projects Structure

### Core Libraries

| Project | Description | Target Frameworks |
|---------|-------------|-------------------|
| **[Barnamenevis.Net.RtlMessageBox.Wpf](./Barnamenevis.Net.RtlMessageBox.Wpf/)** | Pure WPF implementation with themed UI | .NET 6.0, 7.0, 8.0, 9.0 (Windows) |
| **[Barnamenevis.Net.RtlMessageBox.WindowsForms](./Barnamenevis.Net.RtlMessageBox.WindowsForms/)** | Win32 MessageBox wrapper with RTL support | .NET 6.0, 7.0, 8.0, 9.0 (Windows) |
| **[Barnamenevis.Net.Tools](./Barnamenevis.Net.FontInstaller/)** | Comprehensive toolkit for Persian development<br/>*Currently: Font installation utilities*<br/>*Future: Text processing, RTL helpers, Persian calendar, and more* | .NET 6.0, 7.0, 8.0, 9.0 |

### Demo Applications

| Project | Description | Features |
|---------|-------------|----------|
| **[Barnamenevis.Net.RtlMessageBox.Wpf.Demo](./Barnamenevis.Net.RtlMessageBox.Wpf.Demo/)** | WPF demo with font auto-installation | Themed UI, Custom title bars |
| **[Barnamenevis.Net.RtlMessageBox.WindowsForms.Demo](./Barnamenevis.Net.RtlMessageBox.WindowsForms.Demo/)** | Windows Forms demo application | Designer-based UI, Standard controls |

## ✨ Key Features

### 🌐 RTL Language Support
- **Automatic Persian Button Text**: OK→تایید, Cancel→انصراف, Yes→بله, No→خیر
- **Proper RTL Layout**: Right-to-left text flow and UI alignment
- **Custom Font Integration**: Automatic installation and usage of Persian fonts
- **Drop-in Replacement**: Compatible with standard MessageBox API

### 🎨 Advanced UI Features (WPF)
- **Themed MessageBox**: Modern, customizable appearance
- **Custom Title Bars**: Optional client-area title bars with preferred fonts
- **System Sound Integration**: Appropriate sounds for warnings and errors
- **Focus Management**: Proper keyboard navigation and focus indicators

### 📝 Font Management (Barnamenevis.Net.Tools)
- **Automatic Font Installation**: Per-user font installation (no admin rights required)
- **Multiple Format Support**: TTF, OTF, WOFF, WOFF2, EOT
- **Runtime Font Detection**: Automatically uses installed Persian fonts
- **Font Persistence**: Fonts remain available after application closes

### 🚀 Future Tools (Coming Soon in Barnamenevis.Net.Tools)
- **Persian Text Processing**: Utilities for Persian text manipulation
- **RTL Layout Helpers**: Custom control development assistance
- **Persian Date/Calendar**: Shamsi calendar utilities
- **Keyboard Layout Management**: Persian keyboard handling
- **Persian Number Formatting**: Number-to-text conversion
- **Cultural Localization**: Persian/Arabic localization helpers

## 🚀 Quick Start

### WPF Applications

```csharp
using Barnamenevis.Net.RtlMessageBox.Wpf;

// Simple usage
var result = RtlMessageBox.Show("سلام دنیا!", "پیغام");

// With custom options
var result = RtlMessageBox.Show(
    "آیا مطمئن هستید؟", 
    "تأیید", 
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
var result = RtlMessageBox.Show("سلام دنیا!", "پیغام");
```

### Font Installation (Barnamenevis.Net.Tools)

```csharp
using Barnamenevis.Net.Tools;

// Install fonts from application's Fonts directory
int installedCount = FontInstaller.InstallApplicationFonts();
Console.WriteLine($"نصب شد {installedCount} فونت جدید");

// Install fonts from custom directory
int installedCount = FontInstaller.InstallFontsFromDirectory(@"C:\MyFonts");
```

## 📚 Project Details

### [Barnamenevis.Net.RtlMessageBox.Wpf](./Barnamenevis.Net.RtlMessageBox.Wpf/)
Pure WPF implementation featuring:
- Themed MessageBox with modern appearance
- Optional custom title bars using preferred fonts
- Built-in system sound integration
- Proper RTL layout and Persian button text
- Focus management and keyboard navigation

### [Barnamenevis.Net.RtlMessageBox.WindowsForms](./Barnamenevis.Net.RtlMessageBox.WindowsForms/)
Win32 MessageBox wrapper featuring:
- Hook-based button text translation
- Custom font application to all dialog elements
- RTL reading and right-align options
- Compatible with all standard MessageBox overloads
- Automatic focus management

### [Barnamenevis.Net.Tools](./Barnamenevis.Net.FontInstaller/) - Persian/Arabic Development Toolkit
**Current Features (v1.0):**
- **FontInstaller**: Per-user font installation (no admin privileges required)
- Support for multiple font formats (TTF, OTF, WOFF, WOFF2, EOT)
- Automatic font detection and duplicate prevention
- Registry management for font persistence
- Silent operation with error handling

**Planned Features (Future Versions):**
- Persian text processing utilities
- RTL layout helpers for custom controls
- Persian Date/Calendar utilities
- Keyboard layout management tools
- Persian number formatting utilities
- Cultural localization helpers

*The Barnamenevis.Net.Tools library is designed to become a comprehensive toolkit for Persian and Arabic .NET application development. We welcome contributions and feature suggestions!*

## ⚙️ Requirements

- **.NET 6.0, 7.0, 8.0, or 9.0**
- **Windows 10/11** (Windows-specific font installation APIs)
- **WPF/Windows Forms** target frameworks as appropriate

## 🛠️ Installation & Setup

### 1. Clone the Repository
```bash
git clone https://github.com/delphiassistant/Barnamenevis.Net.Tools.git
cd Barnamenevis.Net.Tools
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

**For WPF Projects:**
```xml
<ProjectReference Include="path\to\Barnamenevis.Net.RtlMessageBox.Wpf\Barnamenevis.Net.RtlMessageBox.Wpf.csproj" />
```

**For Windows Forms Projects:**
```xml
<ProjectReference Include="path\to\Barnamenevis.Net.RtlMessageBox.WindowsForms\Barnamenevis.Net.RtlMessageBox.WindowsForms.csproj" />
```

**For Persian/Arabic Development Tools:**
```xml
<ProjectReference Include="path\to\Barnamenevis.Net.FontInstaller\Barnamenevis.Net.Tools.csproj" />
```

## 🎯 Font Setup for Development

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

## 📖 Documentation

Each project includes comprehensive documentation:

- **[WPF MessageBox Documentation](./Barnamenevis.Net.RtlMessageBox.Wpf/README.md)** - Complete WPF implementation guide
- **[Windows Forms MessageBox Documentation](./Barnamenevis.Net.RtlMessageBox.WindowsForms/README.md)** - Win32 wrapper implementation guide  
- **[Barnamenevis.Net.Tools Documentation](./Barnamenevis.Net.FontInstaller/README.md)** - Persian/Arabic development toolkit guide
- **[WPF Demo Documentation](./Barnamenevis.Net.RtlMessageBox.Wpf.Demo/README.md)** - WPF demo application guide
- **[Windows Forms Demo Documentation](./Barnamenevis.Net.RtlMessageBox.WindowsForms.Demo/README.md)** - Windows Forms demo guide

## 🤝 Contributing

Contributions are welcome! Please feel free to submit pull requests, report issues, or suggest improvements.

**Special Interest Areas:**
- Additional utilities for **Barnamenevis.Net.Tools** library
- Persian/Arabic text processing algorithms
- RTL layout helpers and custom controls
- Persian calendar and date utilities
- Localization and cultural adaptation tools

## 📄 License

This project is open source. Please refer to the license file for details.

## 🔗 Related Resources

- [Vazirmatn Font Family](https://rastikerdar.github.io/vazirmatn/) - Recommended Persian font
- [Persian Typography Guidelines](https://github.com/Persian-Typographic) - Best practices for Persian text
- [.NET Globalization Documentation](https://docs.microsoft.com/en-us/dotnet/core/extensions/globalization) - Internationalization resources