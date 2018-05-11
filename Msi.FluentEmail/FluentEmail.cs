namespace Msi.FluentEmail
{
    public class FluentEmail : IFluentEmail
    {
        public IFluentEmailer Emailer {
            get { return new FluentEmailer(); }
        }
    }
}
