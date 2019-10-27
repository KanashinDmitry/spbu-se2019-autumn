using System;
using System.Threading;

namespace Task03
{
    class Program
    {
        private static int amountConsumers = 8;
        private static int amountProducers = 8;

        static void CreateProducers()
        {
            Random random = new Random();
            
            for (int currProd = 0; currProd < amountProducers; ++currProd)
            {
                Producer<int> producer = new Producer<int>();
                Thread thr = new Thread(() => producer.PutData(random.Next(1, 101)));
                thr.Start();
            }
        }

        static void CreateConsumers()
        {
            for (int currCons = 0; currCons < amountConsumers; ++currCons)
            {
                Consumer<int> consumer = new Consumer<int>();
                Thread thr = new Thread(() => consumer.GetData());
                thr.Start();
            }
        }

        static void Main(string[] args)
        {
            CreateProducers();
            CreateConsumers();
            
            Console.ReadKey();

            Consumer<int>.StopGetting();
            Producer<int>.StopInserting();
        }
    }
}