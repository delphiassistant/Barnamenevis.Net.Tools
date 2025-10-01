# Barnamenevis.Net.RtlMessageBox.Wpf

A pure WPF implementation of RTL-enabled MessageBox with modern theming, Persian language support, and advanced customization options.

## ?? Overview

This library provides a complete replacement for the standard WPF MessageBox with full RTL (Right-to-Left) support, automatic Persian button text translation, custom theming, and advanced features like custom title bars and system sound integration.

## ? Key Features

### ?? RTL & Persian Support
- **Automatic Persian Button Text**: OK? «??œ, Cancel?«‰’—«›, Yes?»·Â, No?Œ?—
- **Full RTL Layout**: Proper right-to-left text flow and UI alignment
- **Persian Font Integration**: Configurable Persian font support (default: IranSansX)
- **Bi-directional Text**: Handles mixed Persian/English content correctly

### ?? Advanced UI Features
- **Themed Appearance**: Modern, customizable design
- **Custom Title Bars**: Optional client-area title bars with preferred fonts
- **System Sound Integration**: Appropriate sounds for warnings and errors (non-intrusive)
- **Focus Management**: Proper keyboard navigation with visible focus indicators
- **Icon Support**: Standard system icons (Error, Warning, Information, Question)

### ?? Customization Options
- **Font Configuration**: Specify custom fonts and sizes
- **Title Bar Style**: Choose between system or custom title bars
- **Sound Control**: Enable/disable system sounds
- **Owner Window Support**: Proper modal dialog behavior

## ?? Quick Start

### Basic Usage

```csharp
using Barnamenevis.Net.RtlMessageBox.Wpf;

// Simple message
var result = RtlMessageBox.Show("”·«„ œ‰?«!");

// With title
var result = RtlMessageBox.Show("Å?€«„ ‘„« «—”«· ‘œ.", "„Ê›ﬁ? ");

// Yes/No question
var result = RtlMessageBox.Show(
    "¬?« „ÿ„∆‰ Â” ?œ òÂ „?ùŒÊ«Â?œ «œ«„Â œÂ?œø",
    " √??œ ⁄„·?« ",
    MessageBoxButton.YesNo,
    MessageBoxImage.Question);

// With owner window
var result = RtlMessageBox.Show(
    this, // owner window
    "Œÿ«?? œ— Å—œ«“‘ —Œ œ«œÂ «” .",
    "Œÿ«",
    MessageBoxButton.OK,
    MessageBoxImage.Error);
```

### Configuration

```csharp
// Configure font settings (before showing dialogs)
RtlMessageBox.PreferredFontName = "Vazirmatn FD";
RtlMessageBox.PreferredFontPointSize = 12.0;
RtlMessageBox.ApplyCustomFont = true;

// Configure title bar style
RtlMessageBox.UseCustomTitleBar = true; // Custom themed title bar
// or
RtlMessageBox.UseCustomTitleBar = false; // System title bar
```

## ?? API Reference

### Static Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `PreferredFontName` | `string` | `"IranSansX"` | Font family name for all dialog text |
| `PreferredFontPointSize` | `double` | `12.0` | Font size in points |
| `ApplyCustomFont` | `bool` | `true` | Enable/disable custom font usage |
| `UseCustomTitleBar` | `bool` | `true` | Use themed title bar vs system title bar |

### Show Methods

All standard WPF MessageBox.Show overloads are supported:

#### Ownerless Overloads
```csharp
MessageBoxResult Show(string messageBoxText)
MessageBoxResult Show(string messageBoxText, string caption)
MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
```

#### Owner-Aware Overloads
```csharp
MessageBoxResult Show(Window owner, string messageBoxText)
MessageBoxResult Show(Window owner, string messageBoxText, string caption)
MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
```

## ?? Visual Features

### Custom Title Bar
When `UseCustomTitleBar = true`:
- Themed title bar using your preferred font
- Close button with Persian tooltip («‰’—«›)
- Drag-to-move functionality
- Consistent with dialog theming

### System Sounds
- **Error/Stop**: Plays system error sound
- **Warning/Exclamation**: Plays system warning sound
- **Information/Question**: Silent (non-intrusive)

### Focus Management
- Automatically focuses OK/Yes buttons
- Visible focus indicators (dotted border)
- Proper keyboard navigation
- Tab order follows RTL layout

## ?? Usage Examples

### Error Dialog
```csharp
RtlMessageBox.Show(
    "›«?· „Ê—œ ‰Ÿ— ?«›  ‰‘œ.",
    "Œÿ«",
    MessageBoxButton.OK,
    MessageBoxImage.Error);
```

### Confirmation Dialog
```csharp
var result = RtlMessageBox.Show(
    "¬?« „?ùŒÊ«Â?œ  €??—«  —« –Œ?—Â ò‰?œø",
    "–Œ?—Â  €??—« ",
    MessageBoxButton.YesNoCancel,
    MessageBoxImage.Question);

if (result == MessageBoxResult.Yes)
{
    // Save changes
}
else if (result == MessageBoxResult.No)
{
    // Discard changes
}
// Cancel = do nothing
```

### Information Dialog
```csharp
RtlMessageBox.Show(
    "⁄„·?«  »« „Ê›ﬁ?  «‰Ã«„ ‘œ.",
    "«ÿ·«⁄",
    MessageBoxButton.OK,
    MessageBoxImage.Information);
```

### Warning Dialog
```csharp
var result = RtlMessageBox.Show(
    "«?‰ ⁄„·?«  ﬁ«»· »«“ê‘  ‰?” . «œ«„Â „?ùœÂ?œø",
    "Â‘œ«—",
    MessageBoxButton.OKCancel,
    MessageBoxImage.Warning);
```

## ?? Technical Details

### Implementation Features
- **Pure WPF**: No dependencies on Windows Forms or Win32 APIs (except for icons)
- **No XAML**: Entirely code-based for easy distribution
- **Theme Aware**: Respects system colors and themes
- **Memory Efficient**: Proper resource cleanup and disposal
- **Thread Safe**: Can be called from UI thread

### Supported Icon Types
- `MessageBoxImage.Error` / `Hand` / `Stop` - Red X icon with error sound
- `MessageBoxImage.Warning` / `Exclamation` - Yellow triangle with warning sound  
- `MessageBoxImage.Information` / `Asterisk` - Blue "i" icon (silent)
- `MessageBoxImage.Question` - Blue "?" icon (silent)
- `MessageBoxImage.None` - No icon

### Button Mappings
- `MessageBoxButton.OK` ? " «??œ"
- `MessageBoxButton.OKCancel` ? " «??œ", "«‰’—«›"
- `MessageBoxButton.YesNo` ? "»·Â", "Œ?—"
- `MessageBoxButton.YesNoCancel` ? "»·Â", "Œ?—", "«‰’—«›"

## ?? Requirements

- **.NET 8.0-windows** or later
- **WPF Application** target framework
- **Windows 10/11** (for system icon loading)

## ??? Integration

### Add to Your WPF Project

1. **Add Project Reference**:
   ```xml
   <ProjectReference Include="path\to\Barnamenevis.Net.RtlMessageBox.Wpf.csproj" />
   ```

2. **Update your code**:
   ```csharp
   // Replace this:
   MessageBox.Show("Hello", "Title");
   
   // With this:
   RtlMessageBox.Show("”·«„", "⁄‰Ê«‰");
   ```

3. **Configure fonts** (optional):
   ```csharp
   // In App.xaml.cs or MainWindow constructor
   RtlMessageBox.PreferredFontName = "Vazirmatn FD";
   RtlMessageBox.PreferredFontPointSize = 11;
   ```

## ?? Best Practices

1. **Font Setup**: Use Persian fonts like Vazirmatn for optimal typography
2. **Owner Windows**: Always specify owner windows for modal behavior
3. **Icon Usage**: Use appropriate icons for message types
4. **Text Length**: Keep messages concise; the dialog will auto-size
5. **Default Buttons**: Let the library handle default button selection for better UX

## ?? Integration with FontInstaller

This library works seamlessly with `Barnamenevis.Net.Tools.FontInstaller`:

```csharp
// In App.xaml.cs
protected override void OnStartup(StartupEventArgs e)
{
    // Install fonts first
    FontInstaller.InstallApplicationFonts();
    
    // Configure RtlMessageBox to use installed font
    RtlMessageBox.PreferredFontName = "Vazirmatn FD";
    RtlMessageBox.ApplyCustomFont = true;
    
    base.OnStartup(e);
}
```

## ?? See Also

- [FontInstaller Documentation](../Barnamenevis.Net.FontInstaller/README.md) - Font installation utilities
- [Windows Forms RTL MessageBox](../Barnamenevis.Net.RtlMessageBox.WindowsForms/README.md) - Windows Forms version