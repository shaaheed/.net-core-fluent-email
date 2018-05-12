using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Msi.FluentEmail
{
    public class ComposedMessage : IComposedMessage
    {

        private MimeMessage _message = new MimeMessage();
        private Multipart _multipart = null;
        private TextPart _body = null;
        private string _senderEmail = null;
        private string _senderName = null;
        private string _senderPassword = null;
        private string _server = null;
        private int _port = 0;
        private ServerType _serverType = ServerType.Smtp;

        public IComposedMessage From(string name, string email)
        {
            _message.From.Add(new MailboxAddress(name, email));
            return this;
        }

        public IComposedMessage To(string email)
        {
            _message.To.Add(new MailboxAddress(string.Empty, email));
            return this;
        }

        public IComposedMessage To(IEnumerable<string> emails)
        {
            AddMultipleTo(emails);
            return this;
        }

        public IComposedMessage To(params string[] emails)
        {
            AddMultipleTo(emails);
            return this;
        }

        public IComposedMessage Subject(string subject)
        {
            _message.Subject = subject;
            return this;
        }

        public IComposedMessage Attach(string name, string base64)
        {
            if (_multipart == null)
            {
                _multipart = new Multipart("mixed");
            }
            using (var stream = new MemoryStream(Convert.FromBase64String(base64)))
            {
                var attachment = new MimePart("image", "jpg")
                {
                    Content = new MimeContent(stream, ContentEncoding.Default),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = $"{name}.jpg"
                };
                _multipart.Add(attachment);
            };
            return this;
        }

        public IComposedMessage Body(string content)
        {
            _body = new TextPart(TextFormat.Html) { Text = content };
            return this;
        }

        public IComposedMessage Sender(string name, string email, string password)
        {
            _senderName = name;
            _senderEmail = email;
            _senderPassword = password;
            return From(name, email);
        }

        public IComposedMessage UseServer(ServerType serverType, string smtpServer, int smtpPort)
        {
            _serverType = serverType;
            _server = smtpServer;
            _port = smtpPort;
            return this;
        }

        public async Task Send()
        {
            switch (_serverType)
            {
                case ServerType.Smtp:
                    using (var client = new SmtpClient())
                    {
                        var credentials = new NetworkCredential { UserName = _senderEmail, Password = _senderPassword };
                        await client.ConnectAsync(_server, _port, SecureSocketOptions.Auto).ConfigureAwait(false);
                        await client.AuthenticateAsync(credentials).ConfigureAwait(false);
                        PrepareBody();
                        await client.SendAsync(_message).ConfigureAwait(false);
                        await client.DisconnectAsync(true).ConfigureAwait(false);
                    }
                    break;
            }
        }

        /// <summary>
        /// Preapare email body. Should be called only before send.
        /// </summary>
        private void PrepareBody()
        {
            if (_multipart == null)
            {
                _message.Body = _body;
            }
            else
            {
                _multipart.Add(_body);
                _message.Body = _multipart;
            }
        }

        private void AddMultipleTo(IEnumerable<string> emails)
        {
            var mailboxAddresses = emails.Select(x => new MailboxAddress(string.Empty, x));
            _message.To.AddRange(mailboxAddresses);
        }
    }
}
