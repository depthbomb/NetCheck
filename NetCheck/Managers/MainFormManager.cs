using NetCheck.Forms;

namespace NetCheck.Managers;

public static class MainFormManager
{
    public static MainForm? Form;

    public static void Initialize()
    {
        Form         = new MainForm();
        Form.Text    = "NetCheck";
        Form.Visible = false;
    }

    public static void ToggleVisibility()
    {
        if (Form == null)
        {
            return;
        }
        
        if (Form.Visible)
        {
            Form.Hide();
        }
        else
        {
            Form.Show();
            Form.BringToFront();
        }
    }
}
