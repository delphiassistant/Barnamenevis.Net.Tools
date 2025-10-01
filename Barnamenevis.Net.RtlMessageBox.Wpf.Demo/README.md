# Barnamenevis.Net.RtlMessageBox.Wpf.Demo

A comprehensive WPF demonstration application showcasing the RtlMessageBox library with automatic font installation, Persian UI, and various MessageBox scenarios.

## 📋 Overview

This demo application demonstrates the full capabilities of the `Barnamenevis.Net.RtlMessageBox.Wpf` library including:
- Automatic Persian font installation at startup
- RTL-enabled MessageBox dialogs with Persian button text
- Various MessageBox types (Error, Warning, Information, Question)
- Custom theming and font integration
- Best practices for Persian/RTL UI development

## ✨ Features Demonstrated

### 🌐 RTL MessageBox Scenarios
- **Information Dialog**: Simple information display with Persian text
- **Error Dialog**: Error messages with appropriate icons and sounds
- **Warning Dialog**: Warning prompts with Yes/No options
- **Question Dialog**: User confirmation dialogs
- **Multi-button Options**: Yes/No/Cancel combinations

### 🔧 Technical Features
- **Automatic Font Installation**: Downloads and installs Persian fonts at startup
- **Custom Title Bars**: Demonstrates themed title bars with Persian fonts
- **System Sound Integration**: Appropriate system sounds for different message types
- **Focus Management**: Proper keyboard navigation and focus indicators
- **Owner Window Demo**: Modal dialog behavior with parent windows

### 📝 Font Management
- **Bundled Fonts**: Includes Persian fonts in the application package
- **Runtime Installation**: Installs fonts for current user without admin privileges
- **Font Detection**: Automatically detects and uses installed Persian fonts
- **Fallback Handling**: Graceful degradation to system fonts if installation fails

## 🚀 Running the Demo

### Prerequisites
- **.NET 6.0, 7.0, 8.0, or 9.0 SDK**
- **Windows 10/11**
- **Visual Studio 2022** or VS Code (optional, for development)

### Quick Start

1. **Clone and Build**:
   ```bash
   git clone https://github.com/delphiassistant/Barnamenevis.Net.Tools.git
   cd Barnamenevis.Net.Tools
   dotnet build
   ```

2. **Run the Demo**:
   ```bash
   dotnet run --project Barnamenevis.Net.RtlMessageBox.Wpf.Demo
   ```

3. **Expected Output**:
   ```
   === WPF MessageBox Application Starting ===
   Application directory: C:\...\bin\Debug\net9.0-windows\
   Fonts directory: C:\...\bin\Debug\net9.0-windows\Fonts
   
   --- Starting Font Installation ---
   FontInstaller: Scanning fonts directory: C:\...\Fonts
   FontInstaller: Installed font for current user: Vazirmatn-FD-Regular.ttf
   ✅ Successfully installed 1 new font(s) for current user
   --- Font Installation Complete ---
   
   🎉 Application ready - you can now test the message boxes!
   ```

### Adding Persian Fonts

1. **Download Vazirmatn Font**:
   - Visit: https://rastikerdar.github.io/vazirmatn/
   - Download the latest release ZIP file
   - Extract TTF files

2. **Add to Project**:
   ```
   Barnamenevis.Net.RtlMessageBox.Wpf.Demo/
   └── Fonts/
       ├── Vazirmatn-FD-Regular.ttf
       ├── Vazirmatn-FD-Bold.ttf
       ├── Vazirmatn-FD-Light.ttf
       └── ...
   ```

3. **Automatic Copy** (already configured):
   The `.csproj` file includes:
   ```xml
   <ItemGroup>
     <None Include="Fonts\**\*.*">
       <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
     </None>
   </ItemGroup>
   ```

## 🎯 Demo Application Features

### Main Window
The demo application presents a clean, Persian-friendly interface with:
- **RTL Layout**: Proper right-to-left text alignment
- **Persian Fonts**: Uses installed Vazirmatn or falls back to system fonts
- **Test Buttons**: Various scenarios to test MessageBox functionality

### Available Test Scenarios

#### 1. Information Message
```csharp
RtlMessageBox.Show(
    "عملیات با موفقیت انجام شد.",
    "اطلاع",
    MessageBoxButton.OK,
    MessageBoxImage.Information);
```

#### 2. Error Dialog
```csharp
RtlMessageBox.Show(
    "خطایی در پردازش اطلاعات رخ داده است.",
    "خطا",
    MessageBoxButton.OK,
    MessageBoxImage.Error);
```

#### 3. Warning Confirmation
```csharp
var result = RtlMessageBox.Show(
    "این عملیات قابل بازگشت نیست. ادامه می‌دهید؟",
    "هشدار",
    MessageBoxButton.YesNo,
    MessageBoxImage.Warning);
```

#### 4. Question Dialog
```csharp
var result = RtlMessageBox.Show(
    "آیا می‌خواهید تغییرات را ذخیره کنید؟",
    "ذخیره تغییرات",
    MessageBoxButton.YesNoCancel,
    MessageBoxImage.Question);
```

#### 5. Custom Font Demo
```csharp
// Configure custom font
RtlMessageBox.PreferredFontName = "Vazirmatn FD";
RtlMessageBox.PreferredFontPointSize = 12;
RtlMessageBox.UseCustomTitleBar = true;

RtlMessageBox.Show(
    "این پیام با فونت سفارشی نمایش داده می‌شود.",
    "فونت سفارشی");
```

## 📁 Project Structure

```
Barnamenevis.Net.RtlMessageBox.Wpf.Demo/
├── App.xaml                              # Application definition
├── App.xaml.cs                           # Font installation logic
├── MainWindow.xaml                       # Main demo window UI
├── MainWindow.xaml.cs                    # Demo button event handlers
├── Fonts/                                # Font files directory
│   ├── README.md                         # Font usage documentation
│   ├── TESTING.md                        # Testing instructions
│   └── [Persian font files]             # TTF/OTF files (add manually)
└── Barnamenevis.Net.RtlMessageBox.Wpf.Demo.csproj
```

## 🔧 Implementation Details

### App.xaml.cs - Font Installation
```csharp
public partial class App : Application
{
    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            // Install Persian fonts at application startup
            var installedCount = FontInstaller.InstallApplicationFonts();
            
            if (installedCount > 0)
            {
                Console.WriteLine($"✅ نصب شد {installedCount} فونت جدید");
                
                // Configure RtlMessageBox to use installed font
                RtlMessageBox.PreferredFontName = "Vazirmatn FD";
                RtlMessageBox.ApplyCustomFont = true;
                RtlMessageBox.UseCustomTitleBar = true;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"⚠️ نصب فونت با خطا مواجه شد: {ex.Message}");
            // Application continues with system fonts
        }
        
        base.OnStartup(e);
    }
}
```

### MainWindow.xaml.cs - Demo Logic
```csharp
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        
        // Configure window for RTL
        FlowDirection = FlowDirection.RightToLeft;
        
        // Set Persian font if available
        if (IsPersianFontAvailable())
        {
            FontFamily = new FontFamily("Vazirmatn FD");
        }
    }
    
    private void ShowInfoDialog_Click(object sender, RoutedEventArgs e)
    {
        RtlMessageBox.Show(
            this, // owner window
            "این یک پیام اطلاع‌رسانی نمونه است.",
            "اطلاع",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
    
    // More demo methods...
}
```

## 📚 Learning Objectives

This demo helps developers understand:

### 1. **Font Integration**
- How to bundle fonts with WPF applications
- Automatic font installation at startup
- Configuring UI libraries to use custom fonts

### 2. **RTL UI Design**
- Proper RTL layout in WPF applications
- Persian text handling and display
- Cultural considerations for Persian UI

### 3. **MessageBox Best Practices**
- When to use different MessageBox types
- Proper button combinations for Persian users
- Sound and visual feedback considerations

### 4. **Error Handling**
- Graceful degradation when fonts aren't available
- Silent font installation with fallback options
- User experience during font loading

## 🎨 Customization Examples

### Custom Font Configuration
```csharp
// In App.xaml.cs or MainWindow constructor
RtlMessageBox.PreferredFontName = "IranSansX";        // Different Persian font
RtlMessageBox.PreferredFontPointSize = 14;           // Larger text
RtlMessageBox.UseCustomTitleBar = false;             // System title bar
RtlMessageBox.ApplyCustomFont = true;                // Enable custom fonts
```

### Application-Wide Persian Setup
```csharp
public static class PersianUIConfig
{
    public static void ConfigureApplication()
    {
        // Install fonts
        FontInstaller.InstallApplicationFonts();
        
        // Configure RtlMessageBox
        RtlMessageBox.PreferredFontName = "Vazirmatn FD";
        RtlMessageBox.ApplyCustomFont = true;
        RtlMessageBox.UseCustomTitleBar = true;
        
        // Set application-wide defaults
        FrameworkElement.LanguageProperty.OverrideMetadata(
            typeof(FrameworkElement),
            new FrameworkPropertyMetadata(
                XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));
    }
}
```

### Custom Demo Scenarios
Add your own test scenarios:

```csharp
private void CustomScenario_Click(object sender, RoutedEventArgs e)
{
    // File save confirmation
    var result = RtlMessageBox.Show(
        this,
        "فایل تغییر کرده است. آیا می‌خواهید آن را ذخیره کنید؟",
        "ذخیره فایل",
        MessageBoxButton.YesNoCancel,
        MessageBoxImage.Question);
    
    switch (result)
    {
        case MessageBoxResult.Yes:
            // Save file logic
            RtlMessageBox.Show(this, "فایل ذخیره شد.", "موفقیت", 
                MessageBoxButton.OK, MessageBoxImage.Information);
            break;
        case MessageBoxResult.No:
            // Don't save, continue
            break;
        case MessageBoxResult.Cancel:
            // Cancel the operation
            break;
    }
}
```

## 💡 Development Tips

### 1. **Font Testing**
- Test with and without Persian fonts installed
- Verify fallback to system fonts works correctly
- Check font rendering on different Windows versions

### 2. **RTL Layout**
- Always test RTL layout with longer Persian text
- Verify button order follows Persian conventions
- Check alignment and spacing with Persian fonts

### 3. **User Experience**
- Keep messages concise and clear in Persian
- Use appropriate icons for Persian cultural context  
- Test keyboard navigation and accessibility

### 4. **Deployment**
- Include font files in your distribution
- Test font installation on fresh Windows installations
- Verify per-user installation works correctly

## 🔍 Troubleshooting

### Common Issues

#### Fonts Not Installing
```
⚠️ FontInstaller: No fonts found in directory
```
**Solution**: Ensure font files are in the `Fonts` directory and copied to output

#### Font Not Applied  
**Solution**: Check font name spelling and ensure `ApplyCustomFont = true`

#### RTL Layout Issues
**Solution**: Set `FlowDirection = FlowDirection.RightToLeft` on containers

#### Permission Errors
**Solution**: FontInstaller uses per-user installation - no admin rights needed

## 📚 See Also

- [RtlMessageBox.Wpf Documentation](../Barnamenevis.Net.RtlMessageBox.Wpf/README.md) - Main library documentation  
- [Barnamenevis.Net.Tools Documentation](../Barnamenevis.Net.Tools/README.md) - Font installation utilities
- [Vazirmatn Font](https://rastikerdar.github.io/vazirmatn/) - Recommended Persian font