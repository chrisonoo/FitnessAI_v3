using System.Drawing.Imaging;
using FitnessAI.Application.Common.Interfaces;
using QRCoder;

namespace FitnessAI.Infrastructure.Services;

public class QrCodeGenerator : IQrCodeGenerator
{
    public string Get(string message)
    {
        var qrGenerator = new QRCodeGenerator();

        var qRCodeData = qrGenerator.CreateQrCode(message, QRCodeGenerator.ECCLevel.Q);

        var qRCode = new QRCode(qRCodeData);

        var bitmap = qRCode.GetGraphic(20);

        using (var ms = new MemoryStream())
        {
            bitmap.Save(ms, ImageFormat.Png);
            var byteImage = ms.ToArray();
            return "data:image/png;base64," + Convert.ToBase64String(byteImage);
        }
    }
}