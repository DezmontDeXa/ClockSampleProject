namespace DDX.Clock.TimeProviders.Implements
{
    public class Google2NtpTimeProvider : NtpTimeProviderBase
    {
        protected override string NtpServer => "time2.google.com";
    }
}
