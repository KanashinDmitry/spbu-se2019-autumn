using System;
using System.Threading;

namespace Task03
{
    public class Consumer<T>
    {
        private static bool IsGetting = true;
        public static void StopGetting() => IsGetting = false;
        
        public void GetData()
        {
            while (IsGetting)
            {
                SharedRes<T>.empty.WaitOne();
                SharedRes<T>.mtx.WaitOne();

                if (IsGetting)
                {
                    T removed = SharedRes<T>.Data[0];
                    SharedRes<T>.Data.RemoveAt(0);
                    Console.WriteLine($"Consumer withdrew {removed} by thread {Thread.CurrentThread.ManagedThreadId}");
                }
                else
                {
                    SharedRes<T>.mtx.ReleaseMutex();
                    SharedRes<T>.empty.Release();
                    return;
                }

                if (++SharedRes<T>.AmountWithdraws % 2 == 0)
                {
                    Thread.Sleep(500);
                }
                
                SharedRes<T>.mtx.ReleaseMutex();
                SharedRes<T>.empty.Release();
            }
        }
    }
}