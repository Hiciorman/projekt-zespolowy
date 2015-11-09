namespace ProjectManager.Services.Interfaces
{
    public interface IEmailService
    {
        void SendEmail(string sendTo, string subject, string body);
    }
}
