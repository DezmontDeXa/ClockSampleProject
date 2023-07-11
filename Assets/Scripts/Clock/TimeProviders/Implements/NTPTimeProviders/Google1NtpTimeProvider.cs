namespace DDX.Clock.TimeProviders.Implements
{
    public class Google1NtpTimeProvider : NtpTimeProviderBase
    {
        protected override string NtpServer => "time1.google.com";
    }
}
