# Fonts Directory - Persian Typography for RTL MessageBox

This directory is automatically scanned at application startup to install fonts for the **current user only** (no administrator privileges required). It serves as the central location for Persian and Arabic fonts used by the RtlMessageBox demonstrations.

## ?? Overview

The Fonts directory provides:
- **Automatic Font Detection**: Scanned recursively for all supported font formats
- **Per-User Installation**: Fonts installed only for current user account
- **Seamless Integration**: Automatically configures RtlMessageBox libraries
- **Fallback Support**: Graceful degradation if fonts cannot be installed

## ?? Supported Font Formats

| Extension | Format | Description | Support Level |
|-----------|--------|-------------|---------------|
| `.ttf` | TrueType Font | Most common desktop font format | Full ? |
| `.otf` | OpenType Font | Advanced typography features | Full ? |
| `.woff` | Web Open Font Format | Web-optimized compression | Limited ?? |
| `.woff2` | Web Open Font Format 2 | Modern web font format | Limited ?? |
| `.eot` | Embedded OpenType | Legacy web font format | Limited ?? |

**Recommendation**: Use `.ttf` or `.otf` formats for best compatibility with Windows applications.

## ?? Per-User Installation Details

### Installation Location
Fonts are installed to the current user's local directory:
```
%LOCALAPPDATA%\Microsoft\Windows\Fonts\
```
**Example**: `C:\Users\YourName\AppData\Local\Microsoft\Windows\Fonts\`

### Registry Integration
Font registrations are stored in:
```
HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Fonts
```

### Key Benefits
- ? **No Administrator Privileges Required**
- ? **No UAC Prompts or Elevation**
- ? **Fonts Persist After Application Closes**
- ? **Available to Current User's Applications**
- ? **Other Users on Same Computer Unaffected**

## ?? Usage Instructions

### 1. Adding Fonts to Your Project

**Download Persian Fonts** (recommended: Vazirmatn):
- Visit: https://rastikerdar.github.io/vazirmatn/
- Download the latest release ZIP file
- Extract the TTF files

**Place fonts in this directory**:
```
Barnamenevis.Net.RtlMessageBox.Wpf.Demo/
??? Fonts/
    ??? Vazirmatn-FD-Regular.ttf
    ??? Vazirmatn-FD-Bold.ttf
    ??? Vazirmatn-FD-Light.ttf
    ??? Vazirmatn-FD-Medium.ttf
    ??? Subdirectories/          # Also scanned recursively
        ??? MoreFonts.otf
```

### 2. Automatic Installation Process

When you run the application:

1. **Directory Scanning**: FontInstaller scans this directory recursively
2. **Format Detection**: Identifies supported font files by extension
3. **Duplicate Check**: Skips fonts that are already installed
4. **File Copy**: Copies new fonts to user's local fonts directory
5. **Registration**: Registers fonts with Windows using Win32 APIs
6. **Notification**: Notifies system that font list has changed
7. **Configuration**: Automatically configures RtlMessageBox to use installed fonts

### 3. Expected Console Output

```
=== WPF MessageBox Application Starting ===
Application directory: C:\...\bin\Debug\net8.0-windows\
Fonts directory: C:\...\bin\Debug\net8.0-windows\Fonts
User fonts directory: C:\Users\[YourName]\AppData\Local\Microsoft\Windows\Fonts

--- Starting Font Installation ---
FontInstaller: Scanning fonts directory: C:\...\Fonts
FontInstaller: Found font file: Vazirmatn-FD-Regular.ttf
FontInstaller: Found font file: Vazirmatn-FD-Bold.ttf
FontInstaller: Installed font for current user: Vazirmatn-FD-Regular.ttf
FontInstaller: Installed font for current user: Vazirmatn-FD-Bold.ttf
? Successfully installed 2 new font(s) for current user
--- Font Installation Complete ---

?? Application ready - you can now test the message boxes!
```

## ?? Testing Font Installation

### Testing with Real Fonts

1. **Download a Persian font** (like Vazirmatn):
   - Visit: https://rastikerdar.github.io/vazirmatn/
   - Download the latest release
   - Extract the TTF files

2. **Place fonts in the Fonts directory**:
   - Copy `.ttf` files to: `WpfMessageBox\Fonts\`
   - Example: `Vazirmatn-FD-Regular.ttf`, `Vazirmatn-FD-Bold.ttf`

3. **Run the application**:
   ```bash
   dotnet run
   ```

4. **Check installation results**:
   - Console will show installation progress
   - Check Windows Settings > Personalization > Fonts
   - Look for fonts with your username or in the user section

### Verification Steps

1. **Check Windows Settings**:
   - Open Windows Settings > Personalization > Fonts
   - Search for your font name (e.g., "Vazirmatn FD")
   - The font should appear in the list
   - Look for fonts with your username or in the user section

2. **Test in Applications**:
   - Open Microsoft Word or other text editor
   - Look for the installed font in the font list
   - The font should be immediately available

3. **Verify RtlMessageBox Usage**:
   - Run the demo application
   - Open any MessageBox dialog
   - Text should appear in the custom Persian font

## ??? Project Configuration

### Required .csproj Settings

The project must be configured to copy font files to the output directory:

```xml
<Project Sdk="Microsoft.NET.Sdk">
  <PropertyGroup>
    <OutputType>WinExe</OutputType>
    <TargetFramework>net8.0-windows</TargetFramework>
    <UseWPF>true</UseWPF>
  </PropertyGroup>

  <!-- Essential: Copy Fonts folder to output directory -->
  <ItemGroup>
    <None Include="Fonts\**\*.*">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>

  <ItemGroup>    
    <ProjectReference Include="..\Barnamenevis.Net.FontInstaller\Barnamenevis.Net.Tools.csproj" />    
    <ProjectReference Include="..\Barnamenevis.Net.RtlMessageBox.Wpf\Barnamenevis.Net.RtlMessageBox.Wpf.csproj" />
  </ItemGroup>
</Project>
```

**Important**: The `<None Include="Fonts\**\*.*">` directive ensures all font files are copied to the output directory during build.

### Build Process Integration

```
Source:     Fonts/Vazirmatn-FD-Regular.ttf
            ? (Build Process)
Output:     bin/Debug/net8.0-windows/Fonts/Vazirmatn-FD-Regular.ttf
            ? (Runtime Installation)
System:     %LOCALAPPDATA%/Microsoft/Windows/Fonts/Vazirmatn-FD-Regular.ttf
```

## ?? Font Recommendations

### Primary Recommendation: Vazirmatn FD
- **Source**: https://rastikerdar.github.io/vazirmatn/
- **License**: SIL Open Font License 1.1
- **Features**: Comprehensive Persian/Arabic support, multiple weights
- **Files**: Vazirmatn-FD-Regular.ttf, Vazirmatn-FD-Bold.ttf, etc.

### Alternative Persian Fonts
- **IranSansX**: Modern, clean Persian typography
- **Samim**: Traditional Persian appearance
- **Shabnam**: Elegant Persian font family
- **Tanha**: Minimalist Persian design

### Font Selection Criteria
- ? **Unicode Support**: Complete Persian/Arabic character coverage
- ? **Readability**: Clear at small sizes (9-12pt)
- ? **License**: Compatible with application distribution
- ? **File Size**: Reasonable for bundling with applications
- ? **Maintenance**: Actively maintained and updated

## ?? Advanced Configuration

### Custom Font Directory Structure
```
Fonts/
??? Persian/
?   ??? Vazirmatn-FD-Regular.ttf
?   ??? Vazirmatn-FD-Bold.ttf
?   ??? Vazirmatn-FD-Light.ttf
??? Arabic/
?   ??? NotoSansArabic-Regular.ttf
??? Fallback/
    ??? DejaVuSans.ttf
```

### Font Configuration in Code
```csharp
// Configure primary font
RtlMessageBox.PreferredFontName = "Vazirmatn FD";
RtlMessageBox.PreferredFontPointSize = 11;
RtlMessageBox.ApplyCustomFont = true;

// Configure fallback behavior
if (!IsFontInstalled("Vazirmatn FD"))
{
    RtlMessageBox.PreferredFontName = "Tahoma"; // System fallback
}
```

### Conditional Font Loading
```csharp
public static void ConfigureFonts()
{
    var installedCount = FontInstaller.InstallApplicationFonts();
    
    if (installedCount > 0)
    {
        Console.WriteLine($"? Installed {installedCount} Persian fonts");
        
        // Primary configuration
        RtlMessageBox.PreferredFontName = "Vazirmatn FD";
        RtlMessageBox.UseCustomTitleBar = true;
    }
    else if (IsFontInstalled("Vazirmatn FD"))
    {
        Console.WriteLine("?? Using previously installed Vazirmatn FD");
        RtlMessageBox.PreferredFontName = "Vazirmatn FD";
    }
    else
    {
        Console.WriteLine("?? No Persian fonts available, using system fonts");
        RtlMessageBox.PreferredFontName = "Tahoma";
        RtlMessageBox.UseCustomTitleBar = false;
    }
}
```

## ?? Troubleshooting

### Common Issues and Solutions

#### No Fonts Found
```
FontInstaller: No font files found in directory
```
**Causes & Solutions**:
- Font files not in Fonts directory ? Add .ttf/.otf files
- Build not copying files ? Check .csproj configuration
- Incorrect file extensions ? Ensure files are .ttf, .otf, etc.

#### Permission Errors
```
Access denied when installing fonts
```
**Solution**: FontInstaller uses per-user installation - no admin rights should be needed. Check user profile access.

#### Font Not Applied in UI
```
MessageBox still shows system font
```
**Causes & Solutions**:
- Font name mismatch ? Check exact font family name
- `ApplyCustomFont = false` ? Set to `true`
- Font installation failed ? Check console output for errors

#### Registry Issues
```
Font installed but not appearing in applications
```
**Solution**: 
- Restart the application
- Check Windows font cache
- Verify registry entries in `HKEY_CURRENT_USER\...\Fonts`

#### No fonts installed
- Ensure `.ttf` files are in the `Fonts` directory
- Check .csproj file has proper copy configuration
- Verify build process copies files to output directory

#### Permission errors
- Per-user installation shouldn't need admin rights
- Check user profile permissions
- Verify %LOCALAPPDATA% is accessible

#### Font not visible
- Restart applications or check user-specific font registry
- Check font name spelling in configuration
- Verify font actually installed successfully

### Debug Information

Enable verbose logging by checking console output:
```
FontInstaller: Scanning fonts directory: C:\...\Fonts
FontInstaller: Found font file: example.ttf
FontInstaller: Font already installed: example.ttf (skipped)
FontInstaller: Installed font for current user: newFont.ttf
```

## ?? Best Practices

### Development
1. **Include Sample Fonts**: Provide at least one Persian font for testing
2. **Test Without Fonts**: Ensure graceful fallback when fonts unavailable
3. **Build Verification**: Verify fonts are copied to output directory
4. **Version Control**: Consider font file size when committing to git

### Deployment
1. **Font Licensing**: Verify font licenses allow redistribution
2. **File Size**: Balance font quality with application size
3. **User Experience**: Install fonts silently without prompts
4. **Error Handling**: Handle font installation failures gracefully

### Maintenance
1. **Font Updates**: Keep fonts updated for security and feature improvements
2. **Compatibility**: Test fonts on different Windows versions
3. **Cleanup**: Provide uninstall functionality if needed
4. **Documentation**: Keep font sources and licenses documented

## ?? Related Resources

- **Vazirmatn Font Family**: https://rastikerdar.github.io/vazirmatn/
- **Persian Typography**: https://github.com/Persian-Typographic
- **SIL Open Font License**: https://scripts.sil.org/OFL
- **Windows Font Installation**: https://docs.microsoft.com/en-us/windows/win32/api/wingdi/nf-wingdi-addfontresource
- **FontInstaller Documentation**: [../Barnamenevis.Net.FontInstaller/README.md](../Barnamenevis.Net.FontInstaller/README.md)

---

## ?? Quick Reference

### Essential Commands
```bash
# Download Vazirmatn (example)
curl -L https://github.com/rastikerdar/vazirmatn/releases/latest/download/vazirmatn-font-latest.zip -o fonts.zip

# Build and run
dotnet build
dotnet run --project Barnamenevis.Net.RtlMessageBox.Wpf.Demo
```

### Essential .csproj Entry
```xml
<ItemGroup>
  <None Include="Fonts\**\*.*">
    <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
  </None>
</ItemGroup>
```

### Font Configuration
```csharp
FontInstaller.InstallApplicationFonts();
RtlMessageBox.PreferredFontName = "Vazirmatn FD";
RtlMessageBox.ApplyCustomFont = true;
```