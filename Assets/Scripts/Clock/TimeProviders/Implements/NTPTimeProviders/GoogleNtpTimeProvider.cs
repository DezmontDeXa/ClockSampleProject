namespace DDX.Clock.TimeProviders.Implements
{
    public class GoogleNtpTimeProvider : NtpTimeProviderBase
    {
        protected override string NtpServer => "time.google.com";
    }
}
