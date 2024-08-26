using CrmApiV2.Dtos.Email;

namespace CrmApiV2.Interface
{
    public interface IEmailService
    {
        public void SendEmail(EmailDto request);
    }
}
