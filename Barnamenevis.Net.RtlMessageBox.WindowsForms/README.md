# Barnamenevis.Net.RtlMessageBox.WindowsForms

A Windows Forms-compatible RTL MessageBox implementation that wraps the native Win32 MessageBox with automatic Persian button text translation and custom font support.

## ?? Overview

This library provides a drop-in replacement for the standard Windows Forms MessageBox with full RTL (Right-to-Left) support, automatic Persian button translation, and custom font integration. It uses Win32 hooks to modify the native MessageBox at runtime, ensuring compatibility with all Windows MessageBox features.

## ? Key Features

### ?? RTL & Persian Support
- **Automatic Persian Button Text**: OK?ÊÇ??Ï, Cancel?ÇäÕÑÇÝ, Yes?Èáå, No?Î?Ñ, etc.
- **RTL Layout Options**: Proper right-to-left reading and alignment
- **Win32 Integration**: Uses native Windows MessageBox with custom modifications
- **Complete API Compatibility**: Drop-in replacement for System.Windows.MessageBox

### ?? Font Management
- **Custom Font Application**: Apply Persian fonts to all dialog elements
- **Runtime Font Configuration**: Configure font family and size
- **Automatic Font Detection**: Works with installed Persian fonts
- **Focus Management**: Proper keyboard navigation and default button handling

### ?? Visual Features
- **Native Windows Theming**: Respects system theme and appearance
- **System Icon Support**: Standard Windows icons with proper scaling
- **Focus Indicators**: Clear visual focus cues for keyboard navigation
- **Default Button Logic**: Smart default button selection (OK/Yes preferred)

## ?? Quick Start

### Basic Usage

```csharp
using Barnamenevis.Net.RtlMessageBox.WindowsForms;

// Simple message
var result = RtlMessageBox.Show("ÓáÇã Ïä?Ç!");

// With title
var result = RtlMessageBox.Show("?ÛÇã ÔãÇ ÇÑÓÇá ÔÏ.", "ãæÝÞ?Ê");

// Yes/No question
var result = RtlMessageBox.Show(
    "Â?Ç ãØãÆä åÓÊ?Ï ˜å ã?ÎæÇå?Ï ÇÏÇãå Ïå?Ï¿",
    "ÊÃ??Ï Úãá?ÇÊ",
    MessageBoxButton.YesNo,
    MessageBoxImage.Question);
```

### Font Configuration

```csharp
// Configure font settings (typically in Form constructor or Main method)
RtlMessageBox.PreferredFontName = "Vazirmatn FD";
RtlMessageBox.PreferredFontPointSize = 10.0;
RtlMessageBox.ApplyCustomFont = true;

// Now all MessageBoxes will use the custom font
var result = RtlMessageBox.Show("Ç?ä ãÊä ÈÇ ÝæäÊ ÏáÎæÇå äãÇ?Ô ÏÇÏå ã?ÔæÏ.");
```

## ?? API Reference

### Static Properties

| Property | Type | Default | Description |
|----------|------|---------|-------------|
| `PreferredFontName` | `string` | `"Vazirmatn FD"` | Font family name for dialog text |
| `PreferredFontPointSize` | `double` | `10.0` | Font size in points |
| `ApplyCustomFont` | `bool` | `true` | Enable/disable custom font application |

### Show Methods

All standard WPF MessageBox.Show overloads are supported for compatibility:

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

## ?? Technical Implementation

### Win32 Hook Mechanism
The library uses a **CBT (Computer-Based Training) Hook** to intercept MessageBox creation:

1. **Hook Installation**: Temporarily installs a thread-specific hook
2. **Dialog Detection**: Identifies MessageBox windows by class name (#32770)
3. **Button Translation**: Locates and retitles buttons with Persian text
4. **Font Application**: Applies custom fonts to all child controls
5. **Focus Management**: Sets proper default button and focus
6. **Cleanup**: Removes hook and cleans up resources

### Font Creation Process
```csharp
// Creates a custom font using Win32 CreateFontIndirect
private static IntPtr CreateDialogFont(string faceName, double pointSize)
{
    // Calculate proper font height based on screen DPI
    // Create LOGFONT structure with Persian font settings
    // Return font handle for use with WM_SETFONT
}
```

### Button Mapping Table
| Button ID | Persian Text | English Equivalent |
|-----------|-------------|-------------------|
| IDOK (1) | ÊÇ??Ï | OK |
| IDCANCEL (2) | ÇäÕÑÇÝ | Cancel |
| IDYES (6) | Èáå | Yes |
| IDNO (7) | Î?Ñ | No |
| IDRETRY (4) | ÊáÇÔ ãÌÏÏ | Retry |
| IDIGNORE (5) | äÇÏ?Ïå ÑÝÊä | Ignore |
| IDABORT (3) | ÞØÚ | Abort |
| IDTRYAGAIN (10) | ÊáÇÔ ãÌÏÏ | Try Again |
| IDCONTINUE (11) | ÇÏÇãå | Continue |

## ?? Usage Examples

### Error Handling
```csharp
try
{
    // Some operation that might fail
    PerformOperation();
}
catch (Exception ex)
{
    RtlMessageBox.Show(
        $"ÎØÇ ÏÑ ÇäÌÇã Úãá?ÇÊ: {ex.Message}",
        "ÎØÇ",
        MessageBoxButton.OK,
        MessageBoxImage.Error);
}
```

### User Confirmation
```csharp
private void DeleteButton_Click(object sender, EventArgs e)
{
    var result = RtlMessageBox.Show(
        "Â?Ç ãØãÆä åÓÊ?Ï ˜å ã?ÎæÇå?Ï Ç?ä ÝÇ?á ÑÇ ÍÐÝ ˜ä?Ï¿\nÇ?ä Úãá?ÇÊ ÞÇÈá ÈÇÒÔÊ ä?ÓÊ.",
        "ÊÃ??Ï ÍÐÝ",
        MessageBoxButton.YesNo,
        MessageBoxImage.Warning);
    
    if (result == MessageBoxResult.Yes)
    {
        DeleteFile();
    }
}
```

### Save Changes Prompt
```csharp
private void FormClosing_Handler(object sender, FormClosingEventArgs e)
{
    if (HasUnsavedChanges)
    {
        var result = RtlMessageBox.Show(
            "ÊÛ??ÑÇÊ ÐÎ?Ñå äÔÏåÇ? æÌæÏ ÏÇÑÏ. Â?Ç ã?ÎæÇå?Ï ÂäåÇ ÑÇ ÐÎ?Ñå ˜ä?Ï¿",
            "ÐÎ?Ñå ÊÛ??ÑÇÊ",
            MessageBoxButton.YesNoCancel,
            MessageBoxImage.Question);
        
        switch (result)
        {
            case MessageBoxResult.Yes:
                SaveChanges();
                break;
            case MessageBoxResult.No:
                // Continue closing without saving
                break;
            case MessageBoxResult.Cancel:
                e.Cancel = true; // Cancel the close operation
                break;
        }
    }
}
```

### Information Display
```csharp
private void ShowAbout()
{
    RtlMessageBox.Show(
        "äÑãÇÝÒÇÑ ãÏ?Ñ?Ê ÝÇ?á\näÓÎå 1.0.0\n\nÊæÓÚå ?ÇÝÊå ÊæÓØ Ê?ã ÈÑäÇãåäæ?Ó",
        "ÏÑÈÇÑå ÈÑäÇãå",
        MessageBoxButton.OK,
        MessageBoxImage.Information);
}
```

## ??? Integration with Windows Forms

### Form Designer Integration
This library works perfectly with Windows Forms Designer-created applications:

```csharp
public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();
        
        // Configure RTL MessageBox
        RtlMessageBox.PreferredFontName = "Vazirmatn FD";
        RtlMessageBox.ApplyCustomFont = true;
    }
    
    private void saveButton_Click(object sender, EventArgs e)
    {
        // Use RTL MessageBox instead of standard MessageBox
        RtlMessageBox.Show("ÝÇ?á ÈÇ ãæÝÞ?Ê ÐÎ?Ñå ÔÏ.", "ãæÝÞ?Ê");
    }
}
```

### Global Configuration
Set up font configuration once in your application:

```csharp
// In Program.cs or Main method
static void Main()
{
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);
    
    // Configure RTL MessageBox globally
    RtlMessageBox.PreferredFontName = "Vazirmatn FD";
    RtlMessageBox.PreferredFontPointSize = 9.0;
    RtlMessageBox.ApplyCustomFont = true;
    
    Application.Run(new MainForm());
}
```

## ?? Requirements

- **.NET 8.0-windows** or later
- **Windows Forms** application
- **Windows 10/11** (Win32 API dependencies)

## ?? Advanced Features

### Custom Default Button Logic
The library implements smart default button selection:
- **OK buttons** are preferred over Cancel
- **Yes buttons** are preferred over No
- **Focus and visual state** are properly managed
- **Keyboard navigation** follows RTL patterns

### Memory Management
- **Font handles** are properly created and destroyed
- **Hook cleanup** prevents memory leaks
- **Thread-safe** hook management
- **Exception handling** prevents crashes

### RTL Layout Options
The library automatically applies RTL options to the native MessageBox:
```csharp
private static MessageBoxOptions AddRtl(MessageBoxOptions options)
{
    return options | MessageBoxOptions.RtlReading | MessageBoxOptions.RightAlign;
}
```

## ?? Integration with FontInstaller

Works seamlessly with the FontInstaller utility:

```csharp
// In Program.cs
static void Main()
{
    // Install fonts at application startup
    try
    {
        var installedCount = FontInstaller.InstallApplicationFonts();
        if (installedCount > 0)
        {
            // Configure RTL MessageBox to use newly installed fonts
            RtlMessageBox.PreferredFontName = "Vazirmatn FD";
        }
    }
    catch
    {
        // Fall back to system fonts if installation fails
    }
    
    Application.Run(new MainForm());
}
```

## ?? Important Notes

1. **Thread Safety**: The hook is installed per thread, making it safe for multi-threaded applications
2. **Font Requirements**: Custom fonts must be installed on the system or installed via FontInstaller
3. **Compatibility**: Works with all Windows versions that support the Win32 MessageBox API
4. **Performance**: Minimal overhead - hooks are only active during MessageBox display

## ?? See Also

- [FontInstaller Documentation](../Barnamenevis.Net.FontInstaller/README.md) - Font installation utilities
- [WPF RTL MessageBox](../Barnamenevis.Net.RtlMessageBox.Wpf/README.md) - WPF version with additional theming