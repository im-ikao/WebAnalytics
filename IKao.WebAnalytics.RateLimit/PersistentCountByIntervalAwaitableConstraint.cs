namespace IKao.WebAnalytics.RateLimit
{
    /// <summary>
    /// <see cref="CountByIntervalAwaitableConstraint"/> that is able to save own state.
    /// </summary>
    public sealed class PersistentCountByIntervalAwaitableConstraint : CountByIntervalAwaitableConstraint
    {
        private readonly Action<DateTime> _safeStateAction;

        /// <summary>
        /// Create an instance of <see cref="PersistentCountByIntervalAwaitableConstraint"/>.
        /// </summary>
        /// <param name="count">Maximum actions allowed per time interval.</param>
        /// <param name="timeSpan">Time interval limits are applied for.</param>
        /// <param name="safeStateAction">Action is used to save state.</param>
        /// <param name="initialTimeStamps">Initial timestamps.</param>
        public PersistentCountByIntervalAwaitableConstraint(int count, TimeSpan timeSpan,
            Action<DateTime> safeStateAction, IEnumerable<DateTime> initialTimeStamps) : base(count, timeSpan)
        {
            _safeStateAction = safeStateAction;

            if (initialTimeStamps == null)
                return;

            foreach (var timeStamp in initialTimeStamps)
            {
                _timeStamps.Push(timeStamp);
            }
        }

        /// <summary>
        /// Save state
        /// </summary>
        protected override void OnEnded(DateTime now)
        {
            _safeStateAction(now);
        }
    }
}