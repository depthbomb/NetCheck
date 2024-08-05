namespace NetCheck.Extensions;

public static class FormExtensions
{
    public static void ShowHelpMessageBox(this Form form, string title, string message)
        => MessageBox.Show(form, message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);

    public static void RespectDarkMode(this Form form)
    {
        form.HandleCreated += (_, _) =>
        {
            if (Utils.IsSystemUsingDarkMode())
            {
                NativeMethods.SetPreferredAppMode(2);
                NativeMethods.UseImmersiveDarkMode(form.Handle, true);
                NativeMethods.FlushMenuThemes();
            }
        };
    }
    
    public static void ToggleVisibility(this Form form)
    {
        if (form.Visible)
        {
            form.Hide();
        }
        else
        {
            form.Show();
            form.BringToFront();
        }
    }
}
