using System.Collections.Generic;
using System.Threading.Tasks;

namespace Msi.FluentEmail
{
    public interface IFluentEmailer
    {

        IFluentEmailer From(string name, string email);

        IFluentEmailer To(string email);

        IFluentEmailer To(IEnumerable<string> emails);

        IFluentEmailer To(params string[] emails);

        IFluentEmailer Subject(string subject);

        IFluentEmailer Attach(string name, string base64);

        IFluentEmailer Body(string content);

        IFluentEmailer Sender(string name, string email, string password);

        IFluentEmailer UseSmtpServer(string smtpServer, int smtpPort);

        Task Send();

    }
}
