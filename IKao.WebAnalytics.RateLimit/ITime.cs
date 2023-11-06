﻿namespace IKao.WebAnalytics.RateLimit
{
    /// <summary>
    /// Time abstraction
    /// </summary>
    internal interface ITime
    {
        /// <summary>
        /// Return Now DateTime
        /// </summary>
        /// <returns></returns>
        DateTime GetNow();

        /// <summary>
        /// Returns a task delay
        /// </summary>
        /// <param name="timespan"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task GetDelay(TimeSpan timespan, CancellationToken cancellationToken);
    }
}
