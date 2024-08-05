using Microsoft.Win32;

namespace NetCheck;

public static class Utils
{
    public static bool IsSystemUsingDarkMode()
    {
        try
        {
            var res = (int)(Registry.GetValue(@"HKEY_CURRENT_USER\SOFTWARE\Microsoft\Windows\CurrentVersion\Themes\Personalize", "AppsUseLightTheme", -1) ?? 0);

            return res == 0;
        }
        catch
        {
            return false;
        }
    }
}
