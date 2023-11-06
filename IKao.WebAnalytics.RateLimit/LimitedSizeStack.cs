﻿using System.Collections.Generic;

namespace IKao.WebAnalytics.RateLimit
{
    /// <summary>
    /// LinkedList with a limited size
    /// If the size exceeds the limit older entry are removed
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class LimitedSizeStack<T>: LinkedList<T>
    {
        private readonly int _maxSize;

        /// <summary>
        /// Construct the LimitedSizeStack with the given limit
        /// </summary>
        /// <param name="maxSize"></param>
        public LimitedSizeStack(int maxSize)
        {
            _maxSize = maxSize;
        }

        /// <summary>
        /// Push new entry. If he size exceeds the limit, the oldest entry is removed
        /// </summary>
        /// <param name="item"></param>
        public void Push(T item)
        {
            AddFirst(item);

            if (Count > _maxSize)
                RemoveLast();
        }
    }
}
