using MailKit.Security;
using ModelsLibrary.DtoModels;

namespace ServiceLibrary.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(Email emailModel, SecureSocketOptions secureSocketOptions);
    }
}
