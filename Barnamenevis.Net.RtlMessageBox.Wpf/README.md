# Barnamenevis.Net.RtlMessageBox.Wpf

A pure WPF implementation of RTL-enabled MessageBox with Persian language support, custom theming, and automatic Persian button text translation. This library provides a drop-in replacement for WPF MessageBox with enhanced RTL layout support and Persian localization.

## ✨ Features

### 🌐 RTL Language Support
- **Automatic Persian Button Text**: OK→تایید, Cancel→انصراف, Yes→بله, No→خیر
- **Right-to-Left Layout**: Proper RTL text flow and UI alignment
- **Persian Font Integration**: Automatic detection and use of installed Persian fonts
- **Unicode Support**: Full support for Persian, and other RTL languages

### 🎨 Advanced UI Features
- **Themed MessageBox**: Modern appearance that respects system themes
- **Custom Title Bars**: Optional client-area title bars with Persian fonts
- **System Sound Integration**: Appropriate sounds for Error, Warning, Information, and Question dialogs
- **Responsive Design**: Auto-sizing based on content length and screen resolution
- **Focus Management**: Proper keyboard navigation and focus indicators
- **Owner Window Support**: Modal dialog behavior with proper parent window relationships

### 🔧 Technical Features
- **Drop-in Replacement**: Compatible with standard WPF MessageBox API
- **No XAML**: Entirely code-based for easy distribution
- **Theme Aware**: Respects system colors and themes
- **Memory Efficient**: Proper resource cleanup and disposal
- **Thread Safe**: Can be called from UI thread

### 🎯 Supported Icon Types
- `MessageBoxImage.Error` / `Hand` / `Stop` - Red X icon with error sound
- `MessageBoxImage.Warning` / `Exclamation` - Yellow triangle with warning sound  
- `MessageBoxImage.Information` / `Asterisk` - Blue "i" icon (silent)
- `MessageBoxImage.Question` - Blue "?" icon (silent)
- `MessageBoxImage.None` - No icon

### 🔘 Button Mappings
- `MessageBoxButton.OK` → "تایید"
- `MessageBoxButton.OKCancel` → "تایید", "انصراف"
- `MessageBoxButton.YesNo` → "بله", "خیر"
- `MessageBoxButton.YesNoCancel` → "بله", "خیر", "انصراف"

## ⚙️ Requirements

- **.NET 6.0, 7.0, 8.0, or 9.0 (Windows)**
- **WPF Application** target framework
- **Windows 10/11** (for system icon loading)

## 🛠️ Integration

### Add to Your WPF Project

1. **Add Project Reference**:
   ```xml
   <ProjectReference Include="path\to\Barnamenevis.Net.RtlMessageBox.Wpf\Barnamenevis.Net.RtlMessageBox.Wpf.csproj" />
   ```

2. **Update your code**:
   ```csharp
   // Replace this:
   MessageBox.Show("Hello", "Title");
   
   // With this:
   RtlMessageBox.Show("سلام", "عنوان");
   ```

3. **Configure fonts** (optional):
   ```csharp
   // In App.xaml.cs or MainWindow constructor
   RtlMessageBox.PreferredFontName = "Vazirmatn FD";
   RtlMessageBox.PreferredFontPointSize = 11;
   ```

## 🚀 Quick Start Examples

### Basic Usage
```csharp
using Barnamenevis.Net.RtlMessageBox.Wpf;

// Simple information message
var result = RtlMessageBox.Show("عملیات با موفقیت انجام شد.", "موفقیت");

// Error message
var result = RtlMessageBox.Show(
    "خطا در اتصال به پایگاه داده رخ داد.",
    "خطا",
    MessageBoxButton.OK,
    MessageBoxImage.Error);

// Confirmation dialog
var result = RtlMessageBox.Show(
    "آیا مطمئن هستید که می‌خواهید این فایل را حذف کنید؟",
    "تأیید حذف",
    MessageBoxButton.YesNo,
    MessageBoxImage.Question);

if (result == MessageBoxResult.Yes)
{
    // Perform delete operation
    DeleteFile();
}
```

### Advanced Configuration
```csharp
// In App.xaml.cs
protected override void OnStartup(StartupEventArgs e)
{
    // Configure Persian fonts
    RtlMessageBox.PreferredFontName = "Vazirmatn FD";
    RtlMessageBox.PreferredFontPointSize = 12;
    RtlMessageBox.UseCustomTitleBar = true;
    RtlMessageBox.ApplyCustomFont = true;
    
    base.OnStartup(e);
}
```

## 📖 Best Practices

1. **Font Setup**: Use Persian fonts like Vazirmatn for optimal typography
2. **Owner Windows**: Always specify owner windows for modal behavior
3. **Icon Usage**: Use appropriate icons for message types
4. **Text Length**: Keep messages concise; the dialog will auto-size
5. **Default Buttons**: Let the library handle default button selection for better UX

## 🔗 Integration with FontInstaller

This library works seamlessly with `Barnamenevis.Net.Tools.FontInstaller`:

```csharp
// In App.xaml.cs
protected override void OnStartup(StartupEventArgs e)
{
    // Install fonts first
    var installedCount = FontInstaller.InstallApplicationFonts();
    if (installedCount > 0)
    {
        Console.WriteLine($"✅ نصب شد {installedCount} فونت جدید");
    }
    
    // Configure RtlMessageBox to use installed font
    RtlMessageBox.PreferredFontName = "Vazirmatn FD";
    RtlMessageBox.ApplyCustomFont = true;
    
    base.OnStartup(e);
}
```

## 🎨 Customization Options

### Font Customization
```csharp
// Set Persian font
RtlMessageBox.PreferredFontName = "Vazirmatn FD";
RtlMessageBox.PreferredFontPointSize = 11;

// Enable custom title bar
RtlMessageBox.UseCustomTitleBar = true;

// Apply font to all elements
RtlMessageBox.ApplyCustomFont = true;
```

### Theme Integration
```csharp
// The MessageBox automatically adapts to system theme
// Dark mode, light mode, and high contrast themes are supported
var result = RtlMessageBox.Show(
    "این پیغام با تم سیستم سازگار است.",
    "سازگاری تم",
    MessageBoxButton.OK,
    MessageBoxImage.Information);
```

## 🏗️ Technical Architecture

### Component Structure
- **RtlMessageBox**: Main static class with Show methods
- **RtlMessageBoxWindow**: Custom WPF Window implementation
- **PersianButtonManager**: Handles button text translation
- **FontManager**: Manages font detection and application
- **SoundManager**: Handles system sound integration

### Memory Management
- Automatic resource cleanup
- Proper disposal of font resources
- Efficient window lifecycle management
- No memory leaks in repeated usage

## 📚 See Also

- [Barnamenevis.Net.Tools Documentation](../Barnamenevis.Net.Tools/README.md) - Font installation utilities
- [Windows Forms RTL MessageBox](../Barnamenevis.Net.RtlMessageBox.WindowsForms/README.md) - Windows Forms version
- [WPF Demo Application](../Barnamenevis.Net.RtlMessageBox.Wpf.Demo/README.md) - Complete usage examples