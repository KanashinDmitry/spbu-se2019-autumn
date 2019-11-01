using System;
using System.Threading;

namespace Task03
{
    public class Producer<T>
    {
        private static bool isNeedPause = false;
        private static bool isInserting = true;
        public static void StopInserting() => isInserting = false;

        public void PutData(T value)
        {
            while (isInserting)
            {
                SharedRes<T>.mProducer.WaitOne();

                SharedRes<T>.Data.Add(value);
                
                Console.WriteLine($"Producer added {value} by thread {Thread.CurrentThread.ManagedThreadId}");
                
                Thread.Sleep(500);

                SharedRes<T>.empty.Release();
                SharedRes<T>.mProducer.ReleaseMutex();
            }
        }
    }
}