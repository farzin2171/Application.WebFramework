namespace Application.Services.QRCode
{
    public interface IQRCodeService
    {
        Byte[] GenerateQRCodeBytes(string provider, string key, string userEmail);
    }
}
