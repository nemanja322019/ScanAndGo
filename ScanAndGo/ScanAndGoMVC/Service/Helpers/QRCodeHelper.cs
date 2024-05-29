using ModelsLibrary.Exceptions.QRCode;
using QRCoder;
using System;
using System.Drawing;
using ZXing.Windows.Compatibility;

public static class QRCodeHelper
{
    public static string GenerateQRCodeBase64(string text)
    {
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
        BitmapByteQRCode qrCode = new (qrCodeData);
        return Convert.ToBase64String(qrCode.GetGraphic(20));
    }
}