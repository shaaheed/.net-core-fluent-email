namespace Msi.FluentEmail
{
    public class FluentEmail : IFluentEmail
    {
        public IComposedMessage NewMessage()
        {
            return new ComposedMessage();
        }
    }
}
