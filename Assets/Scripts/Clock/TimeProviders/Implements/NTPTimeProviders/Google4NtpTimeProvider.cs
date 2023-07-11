namespace DDX.Clock.TimeProviders.Implements
{
    public class Google4NtpTimeProvider : NtpTimeProviderBase
    {
        protected override string NtpServer => "time4.google.com";
    }
}
