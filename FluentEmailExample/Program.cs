using Msi.FluentEmail;
using Msi.FluentEmail.Extensions;
using System;

namespace FluentEmailExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IFluentEmail fluentEmail = new FluentEmail();
            fluentEmail.NewMessage()
                    .Subject("Hello Subject")
                    .To("shahidcse6@gmail.com")
                    .Body("Hello World!")
                    .Sender("SenderName", "sender@gmail.com", "123456")
                    .UseGoogleServer(ServerType.Smtp)
                    .Send();
            Console.ReadLine();
        }
    }
}
