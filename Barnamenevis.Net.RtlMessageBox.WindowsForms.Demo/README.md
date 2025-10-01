# Barnamenevis.Net.RtlMessageBox.WindowsForms.Demo

A Windows Forms demonstration application showcasing the RTL MessageBox library with designer-based UI, Persian language support, and comprehensive MessageBox scenarios.

## 📋 Overview

This demo application demonstrates the `Barnamenevis.Net.RtlMessageBox.WindowsForms` library capabilities including:
- Native Win32 MessageBox with RTL support and Persian button text
- Designer-based Windows Forms UI with proper separation of concerns
- Various MessageBox scenarios (Error, Warning, Information, Question)
- Custom font integration with Persian typography
- Best practices for RTL Windows Forms development

## ✨ Features Demonstrated

### 🌐 RTL MessageBox Scenarios
- **Simple OK Dialog**: Basic information display with Persian text
- **OK/Cancel + Warning**: Warning prompts with dual options
- **Yes/No + Question**: User confirmation dialogs
- **Yes/No/Cancel + Error**: Error handling with multiple response options
- **Information Dialog**: Standard information messages
- **Cancel/Retry + Stop**: Operation control scenarios

### 🔧 Technical Features
- **Designer-Generated UI**: Proper separation of UI design and business logic
- **Win32 Hook Integration**: Real-time button text translation during MessageBox display
- **Custom Font Application**: Applies Persian fonts to all dialog elements
- **RTL Layout Options**: Automatic RTL reading and right-align options
- **Focus Management**: Proper default button selection and keyboard navigation

### 🎨 UI Architecture
- **MainForm.Designer.cs**: Contains all UI element declarations and layout
- **MainForm.cs**: Contains business logic and event handlers
- **Designer Integration**: Fully compatible with Visual Studio Windows Forms Designer

## 🚀 Running the Demo

### Prerequisites
- **.NET 6.0, 7.0, 8.0, or 9.0 SDK**
- **Windows 10/11**
- **Visual Studio 2022** (recommended for designer support)

### Quick Start

1. **Clone and Build**:
   ```bash
   git clone https://github.com/delphiassistant/Barnamenevis.Net.Tools.git
   cd Barnamenevis.Net.Tools
   dotnet build
   ```

2. **Run the Demo**:
   ```bash
   dotnet run --project Barnamenevis.Net.RtlMessageBox.WindowsForms.Demo
   ```

3. **Alternative - Visual Studio**:
   - Open the solution in Visual Studio
   - Set as startup project
   - Press F5 or click Start

## 🌟 Demo Application Features

### Main Window Layout
The demo features a clean, grid-based layout with:
- **Title Bar**: Descriptive header explaining the demo purpose
- **Button Grid**: 6 demo buttons arranged in a 2x3 TableLayoutPanel
- **Persian-Friendly Design**: RTL-aware layout and typography

### Available Test Scenarios

#### 1. OK Dialog
```csharp
var result = RtlMessageBox.Show(
    "این یک پیام نمونه است.", 
    "پیغام", 
    MessageBoxButton.OK, 
    MessageBoxImage.None);
```

#### 2. OK/Cancel + Warning
```csharp
var result = RtlMessageBox.Show(
    "آیا ادامه می‌دهید؟", 
    "هشدار", 
    MessageBoxButton.OKCancel, 
    MessageBoxImage.Warning);
```

#### 3. Yes/No + Question
```csharp
var result = RtlMessageBox.Show(
    "آیا با شرایط موافقید؟", 
    "سوال", 
    MessageBoxButton.YesNo, 
    MessageBoxImage.Question);
```

#### 4. Yes/No/Cancel + Error
```csharp
var result = RtlMessageBox.Show(
    "مشکلی رخ داده است. چه کاری انجام می‌دهید؟", 
    "خطا", 
    MessageBoxButton.YesNoCancel, 
    MessageBoxImage.Error);
```

#### 5. Info Dialog
```csharp
var result = RtlMessageBox.Show(
    "اطلاعات مهم", 
    "اطلاع", 
    MessageBoxButton.OK, 
    MessageBoxImage.Information);
```

#### 6. Cancel/Retry + Stop
```csharp
var result = RtlMessageBox.Show(
    "عملیات متوقف شد", 
    "توقف", 
    MessageBoxButton.OKCancel, 
    MessageBoxImage.Stop);
```

## 📁 Project Structure

```
Barnamenevis.Net.RtlMessageBox.WindowsForms.Demo/
├── MainForm.cs                           # Business logic and event handlers
├── MainForm.Designer.cs                  # Designer-generated UI code
├── MainForm.resx                         # Form resources
├── Program.cs                            # Application entry point
└── Barnamenevis.Net.RtlMessageBox.WindowsForms.Demo.csproj
```

## 🔧 Implementation Details

### MainForm.Designer.cs - UI Layout
The designer file contains all UI element definitions:

```csharp
private void InitializeComponent()
{
    this.mainPanel = new TableLayoutPanel();
    this.titleLabel = new Label();
    this.btnOkDialog = new Button();
    this.btnOkCancelWarning = new Button();
    // ... more button definitions
    
    // Layout configuration
    this.mainPanel.ColumnCount = 2;
    this.mainPanel.RowCount = 4;
    this.mainPanel.Dock = DockStyle.Fill;
    
    // Button properties
    this.btnOkDialog.Text = "OK Dialog";
    this.btnOkDialog.Dock = DockStyle.Fill;
    // ... more configuration
}
```

### MainForm.cs - Business Logic
The main form contains event handlers and RtlMessageBox configuration:

```csharp
public partial class MainForm : Form
{
    public MainForm()
    {
        InitializeComponent();

        // Configure RtlMessageBox for Persian fonts
        RtlMessageBox.PreferredFontName = "Vazirmatn FD";
        RtlMessageBox.PreferredFontPointSize = 10;
        RtlMessageBox.ApplyCustomFont = true;

        // Wire up event handlers
        btnOkDialog.Click += btnOkDialog_Click;
        btnOkCancelWarning.Click += btnOkCancelWarning_Click;
        // ... more event handlers
    }

    private void btnOkDialog_Click(object sender, EventArgs e)
    {
        var result = RtlMessageBox.Show(
            "این یک پیام نمونه است.", 
            "پیغام", 
            MessageBoxButton.OK, 
            MessageBoxImage.None);
        
        // Show result using standard MessageBox for comparison
        System.Windows.Forms.MessageBox.Show(
            $"Result: {result}", "Demo Result", 
            MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
```

## 📚 Learning Objectives

This demo helps developers understand:

### 1. **Designer Integration**
- How to properly separate UI design from business logic
- Using Visual Studio Windows Forms Designer with RTL libraries
- Best practices for designer-generated code management

### 2. **RTL MessageBox Implementation**
- Win32 hook-based button text translation
- Custom font application to native dialogs
- RTL layout options and behavior

### 3. **Windows Forms Best Practices**
- Event handler patterns for designer-created controls
- Form initialization and component configuration
- Resource management and disposal

### 4. **Persian UI Development**
- Configuring Persian fonts in Windows Forms applications
- Handling RTL text and layout considerations
- Cultural considerations for Persian users

## 🎨 Customization Examples

### Custom Font Configuration
```csharp
public MainForm()
{
    InitializeComponent();

    // Configure different Persian font
    RtlMessageBox.PreferredFontName = "IranSansX";
    RtlMessageBox.PreferredFontPointSize = 11;
    RtlMessageBox.ApplyCustomFont = true;
}
```

### Global Configuration in Program.cs
```csharp
static void Main()
{
    Application.EnableVisualStyles();
    Application.SetCompatibleTextRenderingDefault(false);

    // Configure RTL MessageBox globally
    RtlMessageBox.PreferredFontName = "Vazirmatn FD";
    RtlMessageBox.PreferredFontPointSize = 9;
    RtlMessageBox.ApplyCustomFont = true;

    Application.Run(new MainForm());
}
```

### Adding New Demo Scenarios
Add new buttons in the designer and corresponding event handlers:

```csharp
// In MainForm.Designer.cs (via designer)
this.btnCustomScenario = new Button();
this.btnCustomScenario.Text = "Custom Scenario";
this.btnCustomScenario.Dock = DockStyle.Fill;

// In MainForm.cs constructor
btnCustomScenario.Click += btnCustomScenario_Click;

// Event handler
private void btnCustomScenario_Click(object sender, EventArgs e)
{
    var result = RtlMessageBox.Show(
        "آیا می‌خواهید فایل را حذف کنید؟\nاین عملیات قابل بازگشت نیست.",
        "تأیید حذف",
        MessageBoxButton.YesNo,
        MessageBoxImage.Warning);

    if (result == MessageBoxResult.Yes)
    {
        // Perform delete operation
        RtlMessageBox.Show(
            "فایل با موفقیت حذف شد.",
            "موفقیت",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }
}
```

## 🎯 UI Design Guidelines

### Designer Best Practices

1. **Layout Management**:
   ```csharp
   // Use TableLayoutPanel for grid-based layouts
   this.mainPanel.ColumnCount = 2;
   this.mainPanel.RowCount = 3;
   this.mainPanel.Dock = DockStyle.Fill;
   
   // Set column and row styles
   this.mainPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));
   this.mainPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 33.33F));
   ```

2. **Button Configuration**:
   ```csharp
   // Consistent button styling
   this.btnExample.Dock = DockStyle.Fill;
   this.btnExample.Margin = new Padding(10);
   this.btnExample.UseVisualStyleBackColor = true;
   ```

3. **RTL Considerations**:
   ```csharp
   // Form-level RTL setup
   this.RightToLeft = RightToLeft.Yes;
   this.RightToLeftLayout = true;
   ```

### Event Handler Patterns
```csharp
// Consistent event handler pattern
private void ButtonName_Click(object sender, EventArgs e)
{
    // Show RTL MessageBox
    var result = RtlMessageBox.Show(
        "Persian message text",
        "Persian caption",
        MessageBoxButton.YesNo,
        MessageBoxImage.Question);
    
    // Handle result
    switch (result)
    {
        case MessageBoxResult.Yes:
            // Handle Yes
            break;
        case MessageBoxResult.No:
            // Handle No
            break;
    }
    
    // Optional: Show result for demo purposes
    System.Windows.Forms.MessageBox.Show(
        $"User selected: {result}", 
        "Demo Result",
        MessageBoxButtons.OK, 
        MessageBoxIcon.Information);
}
```

## 💡 Development Tips

### 1. **Designer Integration**
- Use Visual Studio Designer for layout creation
- Keep business logic separate from designer code
- Use meaningful names for controls (btnOkDialog, not button1)

### 2. **Event Handler Management**
- Wire up events in constructor, not designer
- Use descriptive event handler names
- Group related handlers together

### 3. **Font Integration**
- Configure fonts early in form lifecycle
- Test with and without Persian fonts installed
- Provide fallback options for missing fonts

### 4. **RTL Testing**
- Test with longer Persian text
- Verify button order and layout
- Check keyboard navigation patterns

## 🔍 Troubleshooting

### Common Issues

#### Designer Issues
```
Error: Designer cannot load MainForm
```
**Solution**: Ensure all referenced assemblies are built successfully

#### Font Not Applied
```
RtlMessageBox shows system font instead of Persian font
```
**Solution**: 
- Verify font name spelling
- Ensure `ApplyCustomFont = true`
- Check if font is installed on system

#### Event Handlers Not Working
```
Button clicks don't trigger MessageBox
```
**Solution**: Verify event handlers are wired up in constructor:
```csharp
btnExample.Click += btnExample_Click;
```

#### RTL Layout Issues
**Solution**: Check form RTL properties:
```csharp
this.RightToLeft = RightToLeft.Yes;
this.RightToLeftLayout = true;
```

### Designer File Corruption
If `MainForm.Designer.cs` becomes corrupted:
1. Close Visual Studio
2. Delete `MainForm.Designer.cs`
3. Open Visual Studio
4. Open the form in designer
5. Visual Studio will regenerate the designer file

## 🏗️ Architecture Notes

### Separation of Concerns
- **MainForm.Designer.cs**: UI layout, control properties, visual design
- **MainForm.cs**: Business logic, event handlers, initialization
- **Program.cs**: Application startup, global configuration

### Event Flow
1. User clicks button (UI event)
2. Event handler called (business logic)
3. RtlMessageBox.Show() called (library integration)
4. Win32 hook installs (library internal)
5. Native MessageBox displays with Persian buttons
6. User selects option
7. Result returned to event handler
8. Optional demo result display

## 📚 See Also

- [RtlMessageBox.WindowsForms Documentation](../Barnamenevis.Net.RtlMessageBox.WindowsForms/README.md) - Main library documentation
- [Barnamenevis.Net.Tools Documentation](../Barnamenevis.Net.Tools/README.md) - Font installation utilities  
- [WPF Demo](../Barnamenevis.Net.RtlMessageBox.Wpf.Demo/README.md) - WPF version comparison