namespace FitnessAI.Maui.Pages;

public partial class QrCodePage
{
    public QrCodePage()
    {
        InitializeComponent();
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
        ShowQrCode();
    }

    private void ShowQrCode()
    {
        var stringQrCode = Preferences.Get("qr_code", string.Empty);
        var prefix = "data:image/png;base64,";
        var base64 = stringQrCode.Replace(prefix, string.Empty);

        var convertQrCode = Convert.FromBase64String(base64);
        var memoryStream = new MemoryStream(convertQrCode);
        var imageQrCode = ImageSource.FromStream((Func<Stream>)StreamFunc);
        
        ImgQrCode.Source = imageQrCode;
        return;
        
        Stream StreamFunc() => memoryStream;
    }
}