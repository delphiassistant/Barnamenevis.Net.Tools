using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Barnamenevis.Net.RtlMessageBox.Wpf
{
    // Pure WPF, themed, RTL-aware MessageBox replacement with no XAML
    public static class RtlMessageBox
    {
        // Configuration
        public static string PreferredFontName { get; set; } = "IranSansX";
        public static double PreferredFontPointSize { get; set; } = 12.0;
        public static bool ApplyCustomFont { get; set; } = true;
        // If true, render a client-area title bar using PreferredFontName for the caption
        public static bool UseCustomTitleBar { get; set; } = true;

        // Persian captions
        private const string TextOk = "تایید";
        private const string TextCancel = "انصراف";
        private const string TextYes = "بله";
        private const string TextNo = "خیر";

        // Win32 API for system sounds
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern bool MessageBeep(uint uType);

        // System sound types
        private const uint MB_ICONHAND = 0x00000010;        // Error sound
        private const uint MB_ICONEXCLAMATION = 0x00000030; // Warning sound

        // Public overloads (ownerless)
        public static MessageBoxResult Show(string messageBoxText) =>
            ShowCore(null, messageBoxText, DefaultCaption(), MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(string messageBoxText, string caption) =>
            ShowCore(null, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button) =>
            ShowCore(null, messageBoxText, caption, button, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon) =>
            ShowCore(null, messageBoxText, caption, button, icon, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult) =>
            ShowCore(null, messageBoxText, caption, button, icon, defaultResult, MessageBoxOptions.None);

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options) =>
            ShowCore(null, messageBoxText, caption, button, icon, defaultResult, options);

        // Owner-aware overloads
        public static MessageBoxResult Show(Window owner, string messageBoxText) =>
            ShowCore(owner, messageBoxText, string.IsNullOrWhiteSpace(owner?.Title) ? DefaultCaption() : owner!.Title, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption) =>
            ShowCore(owner, messageBoxText, caption, MessageBoxButton.OK, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button) =>
            ShowCore(owner, messageBoxText, caption, button, MessageBoxImage.None, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon) =>
            ShowCore(owner, messageBoxText, caption, button, icon, MessageBoxResult.None, MessageBoxOptions.None);

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult) =>
            ShowCore(owner, messageBoxText, caption, button, icon, defaultResult, MessageBoxOptions.None);

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options) =>
            ShowCore(owner, messageBoxText, caption, button, icon, defaultResult, options);

        /// <summary>
        /// Plays the appropriate Windows system sound based on MessageBoxImage
        /// Only plays sounds for Warning and Error icons to avoid being intrusive
        /// </summary>
        /// <param name="icon">The MessageBoxImage that determines which sound to play</param>
        private static void PlaySystemSound(MessageBoxImage icon)
        {
            try
            {
                uint? soundType = icon switch
                {
                    MessageBoxImage.Error or MessageBoxImage.Hand or MessageBoxImage.Stop => MB_ICONHAND,
                    MessageBoxImage.Warning or MessageBoxImage.Exclamation => MB_ICONEXCLAMATION,
                    // Don't play sounds for Information, Question, or None - they're not critical
                    _ => null
                };

                if (soundType.HasValue)
                {
                    MessageBeep(soundType.Value);
                }
            }
            catch
            {
                // Silent failure if sound cannot be played
            }
        }

        private static MessageBoxResult ShowCore(Window? owner, string text, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, MessageBoxOptions options)
        {
            // Play system sound before showing dialog
            PlaySystemSound(icon);

            var window = new Window
            {
                Title = caption ?? string.Empty,
                ShowInTaskbar = false,
                WindowStartupLocation = owner != null ? WindowStartupLocation.CenterOwner : WindowStartupLocation.CenterScreen,
                SizeToContent = SizeToContent.WidthAndHeight,
                ResizeMode = ResizeMode.NoResize,
                WindowStyle = UseCustomTitleBar ? WindowStyle.None : WindowStyle.SingleBorderWindow,
                FlowDirection = FlowDirection.RightToLeft,
                MinWidth = 300,
                MaxWidth = 600,
                Topmost = owner?.Topmost ?? false
            };

            if (ApplyCustomFont && !string.IsNullOrWhiteSpace(PreferredFontName))
            {
                window.FontFamily = new FontFamily(PreferredFontName);
                window.FontSize = PreferredFontPointSize;
            }

            if (owner != null)
            {
                window.Owner = owner;
            }

            // OUTER chrome (adds a border for WindowStyle=None)
            var outer = new Border
            {
                BorderBrush = SystemColors.ActiveBorderBrush,
                BorderThickness = UseCustomTitleBar ? new Thickness(1) : new Thickness(0),
                Background = SystemColors.WindowBrush
            };

            // Root layout
            var root = new Grid { Margin = new Thickness(UseCustomTitleBar ? 12 : 16) };
            if (UseCustomTitleBar)
            {
                root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Title bar
                root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Content
                root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Buttons
            }
            else
            {
                root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Content
                root.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto }); // Buttons
            }

            // Optional custom title bar using PreferredFontName
            if (UseCustomTitleBar)
            {
                var titleGrid = new Grid
                {
                    Height = 34,
                    Background = SystemColors.InactiveSelectionHighlightBrush,
                    FlowDirection = FlowDirection.RightToLeft
                };
                titleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                titleGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });

                var titleText = new TextBlock
                {
                    Text = caption ?? string.Empty,
                    VerticalAlignment = VerticalAlignment.Center,
                    Margin = new Thickness(8, 0, 8, 0),
                    FontFamily = new FontFamily(PreferredFontName),
                    FontSize = PreferredFontPointSize,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = SystemColors.WindowTextBrush,
                    TextTrimming = TextTrimming.CharacterEllipsis
                };
                Grid.SetColumn(titleText, 0);

                var closeBtn = new Button
                {
                    Content = "×",
                    Width = 32,
                    Height = 24,
                    Margin = new Thickness(4),
                    Padding = new Thickness(0),
                    HorizontalAlignment = HorizontalAlignment.Right,
                    VerticalAlignment = VerticalAlignment.Center,
                    ToolTip = TextCancel
                };
                Grid.SetColumn(closeBtn, 1);
                closeBtn.Click += (_, __) =>
                {
                    // Close maps to Cancel if available, else No for Yes/No, else OK
                    var r = button switch
                    {
                        MessageBoxButton.OKCancel => MessageBoxResult.Cancel,
                        MessageBoxButton.YesNoCancel => MessageBoxResult.Cancel,
                        MessageBoxButton.YesNo => MessageBoxResult.No,
                        _ => MessageBoxResult.OK
                    };
                    window.Tag = r; // stash result
                    window.DialogResult = true;
                };

                titleGrid.Children.Add(titleText);
                titleGrid.Children.Add(closeBtn);

                // Drag window by title bar
                titleGrid.MouseLeftButtonDown += (s, e) =>
                {
                    try { window.DragMove(); } catch { }
                };

                Grid.SetRow(titleGrid, 0);
                root.Children.Add(titleGrid);
            }

            // Content area (icon + text)
            var contentGrid = new Grid { FlowDirection = FlowDirection.RightToLeft };
            contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = GridLength.Auto });
            contentGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            // Extra spacing below the custom title bar
            if (UseCustomTitleBar)
            {
                contentGrid.Margin = new Thickness(0, 12, 0, 0);
            }

            Image? iconImage = CreateIconImage(icon);
            if (iconImage != null)
            {
                iconImage.Margin = new Thickness(0, 0, 12, 0);
                Grid.SetColumn(iconImage, 0);
                contentGrid.Children.Add(iconImage);
            }

            var textBlock = new TextBlock
            {
                Text = text ?? string.Empty,
                TextWrapping = TextWrapping.Wrap,
                MaxWidth = 520,
                FlowDirection = FlowDirection.RightToLeft,
                TextAlignment = TextAlignment.Left,
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Center
            };
            Grid.SetColumn(textBlock, 1);
            contentGrid.Children.Add(textBlock);

            Grid.SetRow(contentGrid, UseCustomTitleBar ? 1 : 0);
            root.Children.Add(contentGrid);

            // Buttons row
            var buttonsPanel = new StackPanel
            {
                Orientation = Orientation.Horizontal,
                HorizontalAlignment = HorizontalAlignment.Right,
                Margin = new Thickness(0, 16, 0, 0),
                FlowDirection = FlowDirection.RightToLeft
            };

            MessageBoxResult result = MessageBoxResult.None;
            Button? defaultButton = null;
            Button? okOrYesButton = null;

            // Reusable dotted focus style for buttons
            var dottedFocusStyle = CreateDottedFocusStyle();

            void AddButton(string captionText, MessageBoxResult res, bool isDefault = false, bool isCancel = false)
            {
                var btn = new Button
                {
                    Content = captionText,
                    MinWidth = 88,
                    Margin = new Thickness(6, 0, 0, 0),
                    IsDefault = isDefault,
                    IsCancel = isCancel,
                    FocusVisualStyle = dottedFocusStyle
                };
                btn.Click += (_, __) => { result = res; window.DialogResult = true; };
                buttonsPanel.Children.Add(btn);
                if (isDefault) defaultButton = btn;
                if (res == MessageBoxResult.OK || res == MessageBoxResult.Yes) okOrYesButton = btn;
            }

            switch (button)
            {
                case MessageBoxButton.OK:
                    AddButton(TextOk, MessageBoxResult.OK, isDefault: true);
                    break;
                case MessageBoxButton.OKCancel:
                    AddButton(TextOk, MessageBoxResult.OK, isDefault: true);
                    AddButton(TextCancel, MessageBoxResult.Cancel, isCancel: true, isDefault: false);
                    break;
                case MessageBoxButton.YesNo:
                    AddButton(TextYes, MessageBoxResult.Yes, isDefault: true);
                    AddButton(TextNo, MessageBoxResult.No, isDefault: false);
                    break;
                case MessageBoxButton.YesNoCancel:
                    AddButton(TextYes, MessageBoxResult.Yes, isDefault: true);
                    AddButton(TextNo, MessageBoxResult.No, isDefault: false);
                    AddButton(TextCancel, MessageBoxResult.Cancel, isCancel: true, isDefault: false);
                    break;
                default:
                    AddButton(TextOk, MessageBoxResult.OK, isDefault: true);
                    break;
            }

            // If caller explicitly asked for a different default, switch the default mark
            if (defaultResult != MessageBoxResult.None && defaultResult != MessageBoxResult.OK && defaultResult != MessageBoxResult.Yes)
            {
                foreach (var child in buttonsPanel.Children)
                {
                    if (child is Button b)
                    {
                        bool match = (defaultResult == MessageBoxResult.Cancel && (string)b.Content == TextCancel) ||
                                     (defaultResult == MessageBoxResult.No && (string)b.Content == TextNo);
                        if (match)
                        {
                            if (defaultButton != null) defaultButton.IsDefault = false;
                            b.IsDefault = true;
                            defaultButton = b;
                            break;
                        }
                    }
                }
            }

            // Focus OK/Yes (or the chosen default) after first render and show focus cues
            window.ContentRendered += (_, __) =>
            {
                var target = okOrYesButton ?? defaultButton;
                if (target != null)
                {
                    window.Dispatcher.BeginInvoke(DispatcherPriority.Input, new Action(() =>
                    {
                        FocusManager.SetFocusedElement(window, target);
                        Keyboard.Focus(target);
                        target.Focus();
                        ForceShowFocusCues(window);
                    }));
                }
            };

            Grid.SetRow(buttonsPanel, UseCustomTitleBar ? 2 : 1);
            root.Children.Add(buttonsPanel);

            outer.Child = root;
            window.Content = outer;

            window.ShowDialog();

            // If closed via custom title bar close button, return mapped result
            if (window.Tag is MessageBoxResult mapped)
                return mapped;

            return result == MessageBoxResult.None ? InferDefaultResult(button, defaultResult) : result;
        }

        private static Style CreateDottedFocusStyle()
        {
            // Simple dotted rectangle shown by focus adorner
            var style = new Style(typeof(Control));
            var template = new ControlTemplate(typeof(Control));
            var rect = new FrameworkElementFactory(typeof(Rectangle));
            rect.SetValue(Rectangle.StrokeProperty, SystemColors.ControlTextBrush);
            rect.SetValue(Rectangle.StrokeThicknessProperty, 1.0);
            rect.SetValue(Rectangle.StrokeDashArrayProperty, new DoubleCollection { 1, 1 });
            rect.SetValue(FrameworkElement.MarginProperty, new Thickness(3));
            rect.SetValue(Panel.IsHitTestVisibleProperty, false);
            template.VisualTree = rect;
            style.Setters.Add(new Setter(Control.TemplateProperty, template));
            return style;
        }

        private static void ForceShowFocusCues(Window window)
        {
            const int WM_CHANGEUISTATE = 0x0127;
            const int UIS_CLEAR = 2;
            const int UISF_HIDEFOCUS = 0x1;
            const int UISF_HIDEACCEL = 0x2;
            var hwnd = new WindowInteropHelper(window).Handle;
            if (hwnd == IntPtr.Zero) return;
            IntPtr wParam = (IntPtr)(UIS_CLEAR | ((UISF_HIDEFOCUS | UISF_HIDEACCEL) << 16));
            SendMessage(hwnd, WM_CHANGEUISTATE, wParam, IntPtr.Zero);
        }

        private static MessageBoxResult InferDefaultResult(MessageBoxButton button, MessageBoxResult defaultResult)
        {
            if (defaultResult != MessageBoxResult.None)
                return defaultResult;
            return button switch
            {
                MessageBoxButton.OK => MessageBoxResult.OK,
                MessageBoxButton.OKCancel => MessageBoxResult.OK,
                MessageBoxButton.YesNo => MessageBoxResult.Yes,
                MessageBoxButton.YesNoCancel => MessageBoxResult.Yes,
                _ => MessageBoxResult.OK
            };
        }

        // Win32 interop for stock system icons (avoid System.Drawing)
        private const int IMAGE_ICON = 1;
        private const int LR_DEFAULTSIZE = 0x00000040;
        private const int LR_SHARED = 0x00008000;

        private const int IDI_APPLICATION = 32512;
        private const int IDI_ERROR = 32513;
        private const int IDI_QUESTION = 32514;
        private const int IDI_WARNING = 32515;
        private const int IDI_INFORMATION = 32516;
        private const int IDI_SHIELD = 32518;

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern IntPtr LoadImage(IntPtr hinst, IntPtr lpszName, uint uType, int cxDesired, int cyDesired, uint fuLoad);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CopyIcon(IntPtr hIcon);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern bool DestroyIcon(IntPtr hIcon);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        // Shell stock icons (modern, Fluent on recent Windows)
        private enum SHSTOCKICONID : uint
        {
            SIID_HELP = 23,
            SIID_WARNING = 78,
            SIID_INFO = 79,
            SIID_ERROR = 80
        }

        [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private struct SHSTOCKICONINFO
        {
            public uint cbSize;
            public IntPtr hIcon;
            public int iSysImageIndex;
            public int iIcon;
            [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szPath;
        }

        private const uint SHGSI_ICON = 0x000000100;
        private const uint SHGSI_SMALLICON = 0x000000001;
        private const uint SHGSI_LARGEICON = 0x000000000;
        private const uint SHGSI_SHELLICONSIZE = 0x000000004;

        [System.Runtime.InteropServices.DllImport("Shell32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        private static extern int SHGetStockIconInfo(SHSTOCKICONID siid, uint uFlags, ref SHSTOCKICONINFO psii);

        private static Image? CreateIconImage(MessageBoxImage icon)
        {
            // Try modern shell stock icons first
            SHSTOCKICONID? siid = icon switch
            {
                MessageBoxImage.Error or MessageBoxImage.Hand or MessageBoxImage.Stop => SHSTOCKICONID.SIID_ERROR,
                MessageBoxImage.Warning or MessageBoxImage.Exclamation => SHSTOCKICONID.SIID_WARNING,
                MessageBoxImage.Information or MessageBoxImage.Asterisk => SHSTOCKICONID.SIID_INFO,
                MessageBoxImage.Question => SHSTOCKICONID.SIID_HELP,
                _ => null
            };

            if (siid.HasValue)
            {
                var sii = new SHSTOCKICONINFO { cbSize = (uint)System.Runtime.InteropServices.Marshal.SizeOf<SHSTOCKICONINFO>() };
                // large current shell size icon to get crisp visuals on modern Windows
                int hr = SHGetStockIconInfo(siid.Value, SHGSI_ICON | SHGSI_LARGEICON | SHGSI_SHELLICONSIZE, ref sii);
                if (hr == 0 && sii.hIcon != IntPtr.Zero)
                {
                    try
                    {
                        var imageSource = Imaging.CreateBitmapSourceFromHIcon(sii.hIcon, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                        imageSource.Freeze();
                        return new Image { Source = imageSource, Width = 32, Height = 32 };
                    }
                    finally
                    {
                        DestroyIcon(sii.hIcon);
                    }
                }
            }

            // Fallback to legacy user32 stock icons if shell lookup failed
            int resId = icon switch
            {
                MessageBoxImage.Error or MessageBoxImage.Hand or MessageBoxImage.Stop => IDI_ERROR,
                MessageBoxImage.Warning or MessageBoxImage.Exclamation => IDI_WARNING,
                MessageBoxImage.Information or MessageBoxImage.Asterisk => IDI_INFORMATION,
                MessageBoxImage.Question => IDI_QUESTION,
                _ => 0
            };
            if (resId == 0) return null;

            IntPtr hIconShared = LoadImage(IntPtr.Zero, (IntPtr)resId, IMAGE_ICON, 0, 0, LR_DEFAULTSIZE | LR_SHARED);
            if (hIconShared == IntPtr.Zero) return null;
            IntPtr hIcon = CopyIcon(hIconShared);
            if (hIcon == IntPtr.Zero) return null;

            try
            {
                var imageSource = Imaging.CreateBitmapSourceFromHIcon(hIcon, System.Windows.Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                imageSource.Freeze();
                return new Image { Source = imageSource, Width = 32, Height = 32 };
            }
            finally
            {
                DestroyIcon(hIcon);
            }
        }

        private static string DefaultCaption()
        {
            var appTitle = Application.Current?.MainWindow?.Title;
            if (!string.IsNullOrWhiteSpace(appTitle)) return appTitle!;
            var entry = System.Reflection.Assembly.GetEntryAssembly() ?? System.Reflection.Assembly.GetExecutingAssembly();
            var titleAttr = entry.GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), false);
            if (titleAttr.Length > 0 && titleAttr[0] is System.Reflection.AssemblyTitleAttribute at && !string.IsNullOrWhiteSpace(at.Title))
                return at.Title!;
            return entry.GetName().Name ?? string.Empty;
        }
    }
}