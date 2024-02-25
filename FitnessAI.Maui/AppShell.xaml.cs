using FitnessAI.Maui.Pages;

namespace FitnessAI.Maui;

public partial class AppShell
{
    public AppShell()
    {
        InitializeComponent();
        
        Routing.RegisterRoute(nameof(QrCodePage), typeof(QrCodePage));
        
        BindingContext = this;
    }
}
