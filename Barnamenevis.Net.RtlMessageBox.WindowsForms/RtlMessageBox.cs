using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

namespace Barnamenevis.Net.RtlMessageBox.WindowsForms
{
    // A drop-in RTL-enabled MessageBox wrapper mirroring WPF MessageBox.Show overloads
    public static class RtlMessageBox
    {
        // Configuration: set a preferred font for all dialog elements (must be installed on the system)
        public static string PreferredFontName { get; set; } = "Vazirmatn FD";
        public static double PreferredFontPointSize { get; set; } = 10.0; // points
        public static bool ApplyCustomFont { get; set; } = true;

        // New configuration: allow soft wrap insertion for long uninterrupted tokens (e.g., URLs, #selection)
        public static bool InsertSoftWrapsForLongTokens { get; set; } = true;
        public static int LongTokenWrapThreshold { get; set; } = 24;

        // Win32 interop to retitle MessageBox buttons to Persian without resources and to apply custom font
        private const int WH_CBT = 5;
        private const int HCBT_DESTROYWND = 4;
        private const int HCBT_ACTIVATE = 5;
        private const int IDOK = 1;
        private const int IDCANCEL = 2;
        private const int IDABORT = 3;
        private const int IDRETRY = 4;
        private const int IDIGNORE = 5;
        private const int IDYES = 6;
        private const int IDNO = 7;
        private const int IDTRYAGAIN = 10;
        private const int IDCONTINUE = 11;

        private const int WM_SETFONT = 0x0030;
        private const int LOGPIXELSY = 90;
        private const int DM_SETDEFID = 0x0401;
        private const int BM_SETSTYLE = 0x00F4;
        private const int BS_DEFPUSHBUTTON = 0x00000001;

        private const int WM_CHANGEUISTATE = 0x0127;
        private const int UIS_CLEAR = 2;
        private const int UISF_HIDEFOCUS = 0x1;
        private const int UISF_HIDEACCEL = 0x2;

        private delegate IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam);
        private static HookProc? _cbtProc; // keep delegate alive

        private delegate bool EnumChildProc(IntPtr hWnd, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetClassName(IntPtr hWnd, System.Text.StringBuilder lpClassName, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr GetDlgItem(IntPtr hDlg, int nIDDlgItem);

        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        private static extern bool SetWindowText(IntPtr hWnd, string lpString);

        [DllImport("user32.dll")]
        private static extern bool EnumChildWindows(IntPtr hWndParent, EnumChildProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll")]
        private static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr CreateFontIndirect(ref LOGFONT lplf);

        [DllImport("gdi32.dll")]
        private static extern bool DeleteObject(IntPtr hObject);

        [DllImport("user32.dll")]
        private static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct LOGFONT
        {
            public int lfHeight;
            public int lfWidth;
            public int lfEscapement;
            public int lfOrientation;
            public int lfWeight;
            public byte lfItalic;
            public byte lfUnderline;
            public byte lfStrikeOut;
            public byte lfCharSet;        // DEFAULT_CHARSET = 1
            public byte lfOutPrecision;
            public byte lfClipPrecision;
            public byte lfQuality;        // CLEARTYPE_QUALITY = 5
            public byte lfPitchAndFamily;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
            public string lfFaceName;
        }

        private static readonly Dictionary<IntPtr, IntPtr> s_dialogFonts = new(); // hwnd -> HFONT to cleanup on destroy

        private static IDisposable InstallPersianButtonsHook()
        {
            _cbtProc ??= CbtHookProc;
            var threadId = GetCurrentThreadId();
            IntPtr hook = SetWindowsHookEx(WH_CBT, _cbtProc, IntPtr.Zero, threadId);
            return new Unhooker(hook);
        }

        private sealed class Unhooker : IDisposable
        {
            private IntPtr _hook;
            public Unhooker(IntPtr hook) => _hook = hook;
            public void Dispose()
            {
                var h = _hook;
                _hook = IntPtr.Zero;
                if (h != IntPtr.Zero)
                {
                    UnhookWindowsHookEx(h);
                }
            }
        }

        private static void ClearFocusUIState(IntPtr hDlg)
        {
            // MAKEWPARAM(UIS_CLEAR, UISF_HIDEFOCUS | UISF_HIDEACCEL)
            IntPtr wParam = (IntPtr)(UIS_CLEAR | ((UISF_HIDEFOCUS | UISF_HIDEACCEL) << 16));
            SendMessage(hDlg, WM_CHANGEUISTATE, wParam, IntPtr.Zero);
            EnumChildWindows(hDlg, static (child, lp) =>
            {
                SendMessage(child, WM_CHANGEUISTATE, lp, IntPtr.Zero);
                return true;
            }, wParam);
        }

        private static IntPtr CbtHookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code == HCBT_ACTIVATE)
            {
                var hwnd = wParam;
                // Verify it's a standard dialog (#32770)
                var cls = new System.Text.StringBuilder(256);
                if (GetClassName(hwnd, cls, cls.Capacity) > 0 && cls.ToString() == "#32770")
                {
                    // Set Persian captions for known button IDs if present
                    TrySetButton(hwnd, IDOK, "تایید");
                    TrySetButton(hwnd, IDCANCEL, "انصراف");
                    TrySetButton(hwnd, IDYES, "بله");
                    TrySetButton(hwnd, IDNO, "خیر");
                    TrySetButton(hwnd, IDRETRY, "تلاش مجدد");
                    TrySetButton(hwnd, IDIGNORE, "نادیده گرفتن");
                    TrySetButton(hwnd, IDABORT, "قطع");
                    TrySetButton(hwnd, IDTRYAGAIN, "تلاش مجدد");
                    TrySetButton(hwnd, IDCONTINUE, "ادامه");

                    // Apply custom font if enabled
                    if (ApplyCustomFont && !string.IsNullOrWhiteSpace(PreferredFontName))
                    {
                        var hFont = CreateDialogFont(PreferredFontName, PreferredFontPointSize);
                        if (hFont != IntPtr.Zero)
                        {
                            s_dialogFonts[hwnd] = hFont;
                            
                            // Apply font to all child controls (buttons and static text)
                            EnumChildWindows(hwnd, (child, lp) =>
                            {
                                var clsName = new System.Text.StringBuilder(32);
                                GetClassName(child, clsName, clsName.Capacity);
                                var isButton = clsName.ToString() == "Button";
                                var isStatic = clsName.ToString() == "Static";

                                if (isButton || isStatic)
                                {
                                    SendMessage(child, WM_SETFONT, lp, (IntPtr)1);
                                }
                                return true;
                            }, hFont);
                        }
                    }

                    // Prefer OK/YES default and focus with visual default state
                    var hOk = GetDlgItem(hwnd, IDOK);
                    var hYes = GetDlgItem(hwnd, IDYES);
                    var target = hOk != IntPtr.Zero ? (ID: IDOK, Handle: hOk) : (hYes != IntPtr.Zero ? (ID: IDYES, Handle: hYes) : (ID: 0, Handle: IntPtr.Zero));
                    if (target.Handle != IntPtr.Zero)
                    {
                        // Bring dialog to foreground, mark default, and set focus
                        SetForegroundWindow(hwnd);
                        SendMessage(hwnd, DM_SETDEFID, (IntPtr)target.ID, IntPtr.Zero);
                        SendMessage(target.Handle, BM_SETSTYLE, (IntPtr)BS_DEFPUSHBUTTON, (IntPtr)1);
                        SetFocus(target.Handle);

                        // Ensure focus cues (focus rectangle) are visible
                        ClearFocusUIState(hwnd);
                    }
                }
            }
            else if (code == HCBT_DESTROYWND)
            {
                var hwnd = wParam;
                if (s_dialogFonts.TryGetValue(hwnd, out var hFont))
                {
                    s_dialogFonts.Remove(hwnd);
                    if (hFont != IntPtr.Zero)
                    {
                        DeleteObject(hFont);
                    }
                }
            }
            return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        private static IntPtr CreateDialogFont(string faceName, double pointSize)
        {
            try
            {
                var hdc = GetDC(IntPtr.Zero);
                int dpi = hdc != IntPtr.Zero ? GetDeviceCaps(hdc, LOGPIXELSY) : 96;
                if (hdc != IntPtr.Zero) ReleaseDC(IntPtr.Zero, hdc);
                int height = -(int)Math.Round(pointSize * dpi / 72.0);
                var lf = new LOGFONT
                {
                    lfHeight = height,
                    lfWidth = 0,
                    lfEscapement = 0,
                    lfOrientation = 0,
                    lfWeight = 400, // normal
                    lfItalic = 0,
                    lfUnderline = 0,
                    lfStrikeOut = 0,
                    lfCharSet = 1, // DEFAULT_CHARSET
                    lfOutPrecision = 0,
                    lfClipPrecision = 0,
                    lfQuality = 5, // CLEARTYPE_QUALITY
                    lfPitchAndFamily = 0,
                    lfFaceName = faceName
                };
                return CreateFontIndirect(ref lf);
            }
            catch
            {
                return IntPtr.Zero;
            }
        }

        private static void TrySetButton(IntPtr hDialog, int id, string text)
        {
            var hBtn = GetDlgItem(hDialog, id);
            if (hBtn != IntPtr.Zero)
            {
                SetWindowText(hBtn, text);
            }
        }

        private static System.Windows.MessageBoxOptions AddRtl(System.Windows.MessageBoxOptions options)
        {
            return options | System.Windows.MessageBoxOptions.RtlReading | System.Windows.MessageBoxOptions.RightAlign;
        }

        private static string DefaultCaption()
        {
            var appTitle = System.Windows.Application.Current?.MainWindow?.Title;
            if (!string.IsNullOrWhiteSpace(appTitle))
                return appTitle!;

            var entry = Assembly.GetEntryAssembly() ?? Assembly.GetExecutingAssembly();
            var titleAttr = entry.GetCustomAttribute<AssemblyTitleAttribute>();
            if (!string.IsNullOrWhiteSpace(titleAttr?.Title))
                return titleAttr!.Title!;

            return entry.GetName().Name ?? string.Empty;
        }

        private static MessageBoxResult PreferredDefaultFor(MessageBoxButton button)
        {
            return button switch
            {
                MessageBoxButton.OK => MessageBoxResult.OK,
                MessageBoxButton.OKCancel => MessageBoxResult.OK,
                MessageBoxButton.YesNo => MessageBoxResult.Yes,
                MessageBoxButton.YesNoCancel => MessageBoxResult.Yes,
                _ => MessageBoxResult.None
            };
        }

        // Insert zero-width space (U+200B) periodically in long uninterrupted tokens to allow wrapping
        private static string PrepareTextForWrapping(string text)
        {
            if (!InsertSoftWrapsForLongTokens || string.IsNullOrEmpty(text) || LongTokenWrapThreshold <= 0)
                return text;

            const char ZWSP = '\u200B';
            var builder = new System.Text.StringBuilder(text.Length + 16);
            int run = 0;

            foreach (var ch in text)
            {
                builder.Append(ch);

                // Reset run on whitespace or explicit newline
                if (char.IsWhiteSpace(ch))
                {
                    run = 0;
                    continue;
                }

                // Encourage breaks after common punctuation and separators frequently seen in long tokens
                if (ch is '-' or '_' or '/' or '\\' or ':' or '.' or '@' or '#' or '?' or '&' or '=' or '+' or '~' or '|' or ',')
                {
                    builder.Append(ZWSP);
                    run = 0;
                    continue;
                }

                run++;
                if (run >= LongTokenWrapThreshold)
                {
                    builder.Append(ZWSP);
                    run = 0;
                }
            }

            return builder.ToString();
        }

        // Ownerless overloads
        public static MessageBoxResult Show(string messageBoxText)
        {
            return Show(messageBoxText, DefaultCaption());
        }

        public static MessageBoxResult Show(string messageBoxText, string caption)
        {
            return Show(messageBoxText, caption, MessageBoxButton.OK);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button)
        {
            return Show(messageBoxText, caption, button, MessageBoxImage.None);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return Show(messageBoxText, caption, button, icon, MessageBoxResult.None);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return Show(messageBoxText, caption, button, icon, defaultResult, System.Windows.MessageBoxOptions.None);
        }

        public static MessageBoxResult Show(string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, System.Windows.MessageBoxOptions options)
        {
            using (InstallPersianButtonsHook())
            {
                var preferred = PreferredDefaultFor(button);
                var prepared = PrepareTextForWrapping(messageBoxText);
                return System.Windows.MessageBox.Show(prepared, caption, button, icon, preferred, AddRtl(options));
            }
        }

        // Owner-aware overloads
        public static MessageBoxResult Show(Window owner, string messageBoxText)
        {
            var caption = owner?.Title;
            if (string.IsNullOrWhiteSpace(caption))
                caption = DefaultCaption();
            return Show(owner, messageBoxText, caption!);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption)
        {
            return Show(owner, messageBoxText, caption, MessageBoxButton.OK);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button)
        {
            return Show(owner, messageBoxText, caption, button, MessageBoxImage.None);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon)
        {
            return Show(owner, messageBoxText, caption, button, icon, MessageBoxResult.None);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult)
        {
            return Show(owner, messageBoxText, caption, button, icon, defaultResult, System.Windows.MessageBoxOptions.None);
        }

        public static MessageBoxResult Show(Window owner, string messageBoxText, string caption, MessageBoxButton button, MessageBoxImage icon, MessageBoxResult defaultResult, System.Windows.MessageBoxOptions options)
        {
            using (InstallPersianButtonsHook())
            {
                var preferred = PreferredDefaultFor(button);
                var prepared = PrepareTextForWrapping(messageBoxText);
                return System.Windows.MessageBox.Show(owner, prepared, caption, button, icon, preferred, AddRtl(options));
            }
        }
    }
}