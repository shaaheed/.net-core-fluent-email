namespace Msi.FluentEmail.Extensions
{
    public static class FluentEmailExtensions
    {

        public static IComposedMessage UseGoogleServer(this IComposedMessage message, ServerType serverType)
        {
            message.UseServer(serverType, "smtp.gmail.com", 587);
            return message;
        }

    }
}
