using System;
using System.Linq;
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
                SharedRes<T>.mConsumer.WaitOne();

                T removed = SharedRes<T>.Data[0];
                SharedRes<T>.Data.RemoveAt(0);
                Console.WriteLine($"Consumer withdrew {removed} by thread {Thread.CurrentThread.ManagedThreadId}");
                
                Thread.Sleep(500);

                SharedRes<T>.mConsumer.ReleaseMutex();
                SharedRes<T>.empty.Release();
            }
        }
    }
}