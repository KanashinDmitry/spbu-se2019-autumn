using System;
using System.Threading;

namespace Task03
{
    public class Producer<T>
    {
        private static bool isInserting = true;
        public static void StopInserting() => isInserting = false;

        public void PutData(T value)
        {
            while (isInserting)
            {
                SharedRes<T>.mtx.WaitOne();

                if (isInserting)
                {
                    SharedRes<T>.Data.Add(value);
                }
                else
                {
                    SharedRes<T>.empty.Release();
                    SharedRes<T>.mtx.ReleaseMutex();
                    return;
                }
                Console.WriteLine($"Producer added {value} by thread {Thread.CurrentThread.ManagedThreadId}");
                
                if (++SharedRes<T>.AmountInsertions % 2 == 0)
                {
                    Thread.Sleep(500);
                }
                
                SharedRes<T>.empty.Release();
                SharedRes<T>.mtx.ReleaseMutex();
            }
        }
    }
}