namespace FitnessAI.Maui.Pages;

public partial class MainPage
{
    public MainPage()
    {
        InitializeComponent();
        LblUserName.Text
            = "Cześć, "
              + Preferences.Get("first_name", string.Empty) + " "
              + Preferences.Get("last_name", string.Empty) + "!";
        
    }
}