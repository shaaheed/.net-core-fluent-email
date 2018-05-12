using System.Collections.Generic;
using System.Threading.Tasks;

namespace Msi.FluentEmail
{
    public interface IComposedMessage
    {

        IComposedMessage From(string name, string email);

        IComposedMessage To(string email);

        IComposedMessage To(IEnumerable<string> emails);

        IComposedMessage To(params string[] emails);

        IComposedMessage Subject(string subject);

        IComposedMessage Attach(string name, string base64);

        IComposedMessage Body(string content);

        IComposedMessage Sender(string name, string email, string password);

        IComposedMessage UseServer(ServerType serverType, string server, int port);

        Task Send();

    }
}
