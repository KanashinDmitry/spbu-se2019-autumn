using System;
using System.Collections.Generic;
using System.Threading;

namespace Task03
{
    public static class SharedRes<T>
    {
        public static readonly List<T> Data = new List<T>();
        public static readonly Semaphore Empty = new Semaphore(0, Int32.MaxValue);
        public static readonly Mutex MConsumer = new Mutex();
        public static readonly Mutex MProducer = new Mutex();
    }
}