using Msi.FluentEmail;
using System;

namespace FluentEmailExample
{
    class Program
    {
        static void Main(string[] args)
        {
            IFluentEmail fluentEmail = new FluentEmail();
            fluentEmail.Emailer
                    .Subject("Hello Subject")
                    .To("shahidcse6@gmail.com")
                    .Body("Hello World!")
                    .Sender("SenderName", "sender@gmail.com", "123456")
                    .UseSmtpServer("smtp.gmail.com", 587)
                    .Send();
            Console.ReadLine();
        }
    }
}
