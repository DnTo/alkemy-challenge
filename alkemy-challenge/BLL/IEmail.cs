namespace alkemy_challenge.BLL
{
    public interface IEmail
    {
       void SendEmail(string body, string subject , string to, string displayName, bool isHtml );
    }
}