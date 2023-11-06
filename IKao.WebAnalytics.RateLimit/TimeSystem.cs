namespace IKao.WebAnalytics.RateLimit
{
    internal class TimeSystem : ITime
    {
        public static ITime StandardTime { get; }

        static TimeSystem()
        {
            StandardTime = new TimeSystem();
        }

        private TimeSystem()
        {
        }

        DateTime ITime.GetNow()
        {
            return DateTime.Now;
        }

        Task ITime.GetDelay(TimeSpan timespan, CancellationToken cancellationToken)
        {
            return Task.Delay(timespan, cancellationToken);
        }
    }
}
