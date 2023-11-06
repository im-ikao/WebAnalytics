using System.Diagnostics;

namespace IKao.WebAnalytics.RateLimit
{
    /// <summary>
    /// Provide an awaitable constraint based on number of times per duration
    /// </summary>
    public class CountByIntervalAwaitableConstraint : IAwaitableConstraint
    {
        /// <summary>
        /// List of the last time stamps
        /// </summary>
        public IReadOnlyList<DateTime> TimeStamps => _timeStamps.ToList();

        /// <summary>
        /// Stack of the last time stamps
        /// </summary>
        protected LimitedSizeStack<DateTime> _timeStamps { get; }

        private int _count { get; }
        private TimeSpan _timeSpan { get; }
        private SemaphoreSlim _semaphore { get; } = new(1, 1);
        private ITime _time { get; }

        /// <summary>
        /// Constructs a new AwaitableConstraint based on number of times per duration
        /// </summary>
        /// <param name="count"></param>
        /// <param name="timeSpan"></param>
        public CountByIntervalAwaitableConstraint(int count, TimeSpan timeSpan) : this(count, timeSpan, TimeSystem.StandardTime)
        {
        }

        internal CountByIntervalAwaitableConstraint(int count, TimeSpan timeSpan, ITime time)
        {
            if (count <= 0)
                throw new ArgumentException("count should be strictly positive", nameof(count));

            if (timeSpan.TotalMilliseconds <= 0)
                throw new ArgumentException("timeSpan should be strictly positive", nameof(timeSpan));

            _count = count;
            _timeSpan = timeSpan;
            _timeStamps = new LimitedSizeStack<DateTime>(_count);
            _time = time;
        }

        /// <summary>
        /// returns a task that will complete once the constraint is fulfilled
        /// </summary>
        /// <param name="cancellationToken">
        /// Cancel the wait
        /// </param>
        /// <returns>
        /// A disposable that should be disposed upon task completion
        /// </returns>
        public async Task<IDisposable> WaitForReadiness(CancellationToken cancellationToken)
        {
            await _semaphore.WaitAsync(cancellationToken);
            
            var count = 0;
            var now = _time.GetNow();
            var target = now - _timeSpan;
            
            LinkedListNode<DateTime> element = _timeStamps.First, last = null;
            
            while ((element != null) && (element.Value > target))
            {
                last = element;
                element = element.Next;
                count++;
            }

            if (count < _count)
                return new DisposeAction(OnEnded);

            Debug.Assert(element == null);
            Debug.Assert(last != null);
            
            var timeToWait = last.Value.Add(_timeSpan) - now;
            try
            {
                await _time.GetDelay(timeToWait, cancellationToken);
            }
            catch (Exception)
            {
                _semaphore.Release();
                throw;
            }

            return new DisposeAction(OnEnded);
        }

        /// <summary>
        /// Clone CountByIntervalAwaitableConstraint
        /// </summary>
        /// <returns></returns>
        public IAwaitableConstraint Clone()
        {
            return new CountByIntervalAwaitableConstraint(_count, _timeSpan, _time);
        }

        private void OnEnded()
        {
            var now = _time.GetNow();
            _timeStamps.Push(now);
            OnEnded(now);
            _semaphore.Release();
        }

        /// <summary>
        /// Called when action has been executed
        /// </summary>
        /// <param name="now"></param>
        protected virtual void OnEnded(DateTime now)
        {
        }
    }
}
