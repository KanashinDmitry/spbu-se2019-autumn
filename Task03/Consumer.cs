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
                SharedRes<T>.Empty.WaitOne();

                if (!IsGetting)
                {
                    break;
                }
                
                SharedRes<T>.MConsumer.WaitOne();

                T removed = SharedRes<T>.Data[^1];
                SharedRes<T>.Data.RemoveAt(SharedRes<T>.Data.Count - 1);
                Console.WriteLine($"Consumer withdrew {removed} by thread {Thread.CurrentThread.ManagedThreadId}");
                
                Thread.Sleep(500);

                SharedRes<T>.MConsumer.ReleaseMutex();
                SharedRes<T>.Empty.Release();
            }
        }
    }
}