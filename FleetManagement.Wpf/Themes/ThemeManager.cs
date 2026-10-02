using System.IO;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Windows;
using System.Windows.Interop;
using System.Windows.Threading;
using Microsoft.Win32;

namespace FleetManagement.Wpf.Themes;

public enum ThemePreference
{
    Automatic,
    Light,
    Dark
}

public sealed class ThemeManager : IDisposable
{
    private const string WindowsThemeKey = @"Software\Microsoft\Windows\CurrentVersion\Themes\Personalize";
    private readonly Application _application;
    private readonly Func<bool> _systemDark;
    private readonly string _settingsPath;
    private readonly bool _monitorSystem;
    private readonly DispatcherTimer? _systemCheck;
    private bool _disposed;

    public ThemePreference Preference { get; private set; }
    public bool IsDark { get; private set; }

    public ThemeManager(Application application, Func<bool>? systemDark = null,
        string? settingsPath = null, bool monitorSystem = true)
    {
        _application = application;
        _systemDark = systemDark ?? ReadWindowsDarkMode;
        _settingsPath = settingsPath ?? Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "FleetManagement", "settings.json");
        _monitorSystem = monitorSystem;
        Preference = ReadPreference();
        ApplyTheme(force: true);
        if (_monitorSystem)
        {
            SystemEvents.UserPreferenceChanged += OnUserPreferenceChanged;
            _systemCheck = new DispatcherTimer(TimeSpan.FromSeconds(5),
                DispatcherPriority.Background, (_, _) => RefreshSystemTheme(),
                _application.Dispatcher);
            _systemCheck.Start();
        }
    }

    public void SetPreference(ThemePreference preference)
    {
        ObjectDisposedException.ThrowIf(_disposed, this);
        if (!Enum.IsDefined(preference))
            throw new ArgumentOutOfRangeException(nameof(preference));
        if (Preference == preference) return;

        SavePreference(preference);
        Preference = preference;
        ApplyTheme(force: true);
    }

    public void RefreshSystemTheme()
    {
        if (!_disposed && Preference == ThemePreference.Automatic)
            ApplyTheme(force: false);
    }

    public void AttachWindow(Window window)
    {
        window.SourceInitialized += (_, _) => ApplyWindowChrome(window);
        if (new WindowInteropHelper(window).Handle != IntPtr.Zero)
            ApplyWindowChrome(window);
    }

    private void OnUserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e)
    {
        if (!_disposed && !_application.Dispatcher.HasShutdownStarted)
            _application.Dispatcher.BeginInvoke(RefreshSystemTheme);
    }

    private void ApplyTheme(bool force)
    {
        var isDark = Preference switch
        {
            ThemePreference.Dark => true,
            ThemePreference.Light => false,
            _ => _systemDark()
        };
        if (!force && IsDark == isDark) return;

        IsDark = isDark;
        var palette = isDark ? "Dark" : "Light";
        _application.Resources.MergedDictionaries[0] = new ResourceDictionary
        {
            Source = new Uri($"/FleetManagement.Wpf;component/Themes/{palette}.xaml", UriKind.Relative)
        };
        foreach (Window window in _application.Windows)
            ApplyWindowChrome(window);
    }

    private ThemePreference ReadPreference()
    {
        try
        {
            if (!File.Exists(_settingsPath)) return ThemePreference.Automatic;
            using var document = JsonDocument.Parse(File.ReadAllText(_settingsPath));
            var value = document.RootElement.GetProperty("theme").GetString();
            return Enum.TryParse<ThemePreference>(value, ignoreCase: true, out var preference)
                && Enum.IsDefined(preference) ? preference : ThemePreference.Automatic;
        }
        catch (Exception ex) when (ex is IOException or UnauthorizedAccessException
            or JsonException or KeyNotFoundException or InvalidOperationException)
        {
            return ThemePreference.Automatic;
        }
    }

    private void SavePreference(ThemePreference preference)
    {
        var directory = Path.GetDirectoryName(_settingsPath);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);
        var temporaryPath = _settingsPath + ".tmp";
        try
        {
            File.WriteAllText(temporaryPath,
                JsonSerializer.Serialize(new { theme = preference.ToString() }));
            File.Move(temporaryPath, _settingsPath, overwrite: true);
        }
        finally
        {
            if (File.Exists(temporaryPath)) File.Delete(temporaryPath);
        }
    }

    private static bool ReadWindowsDarkMode()
    {
        try
        {
            using var key = Registry.CurrentUser.OpenSubKey(WindowsThemeKey);
            return key?.GetValue("AppsUseLightTheme") is int value && value == 0;
        }
        catch (Exception ex) when (ex is System.Security.SecurityException or UnauthorizedAccessException)
        {
            return false;
        }
    }

    private void ApplyWindowChrome(Window window)
    {
        var handle = new WindowInteropHelper(window).Handle;
        if (handle == IntPtr.Zero) return;

        var dark = IsDark ? 1 : 0;
        _ = DwmSetWindowAttribute(handle, 20, ref dark, sizeof(int));
        var caption = IsDark ? 0x0023150D : 0x00FBF7F4;
        var text = IsDark ? 0x00FAF4F0 : 0x003D2618;
        _ = DwmSetWindowAttribute(handle, 35, ref caption, sizeof(int));
        _ = DwmSetWindowAttribute(handle, 36, ref text, sizeof(int));
    }

    [DllImport("dwmapi.dll")]
    private static extern int DwmSetWindowAttribute(IntPtr window, int attribute,
        ref int value, int valueSize);

    public void Dispose()
    {
        if (_disposed) return;
        _disposed = true;
        if (_monitorSystem)
            SystemEvents.UserPreferenceChanged -= OnUserPreferenceChanged;
        _systemCheck?.Stop();
    }
}
