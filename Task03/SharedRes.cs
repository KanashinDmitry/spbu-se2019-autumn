using System;
using System.Collections.Generic;
using System.Threading;

namespace Task03
{
    public class SharedRes<T>
    {
        public static int AmountWithdraws = 0;
        public static int AmountInsertions = 0;
        public static readonly List<T> Data = new List<T>();
        public static readonly Semaphore empty = new Semaphore(0, Int32.MaxValue);
        public static readonly Mutex mConsumer = new Mutex();
        public static readonly Mutex mProducer = new Mutex();
    }
}