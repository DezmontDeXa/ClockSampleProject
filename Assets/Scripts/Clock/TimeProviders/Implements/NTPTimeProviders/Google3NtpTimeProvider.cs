namespace DDX.Clock.TimeProviders.Implements
{
    public class Google3NtpTimeProvider : NtpTimeProviderBase
    {
        protected override string NtpServer => "time3.google.com";
    }
}
