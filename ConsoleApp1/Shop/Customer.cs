using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SleepingBarber
{
    class Customer
    {
        private static int _Id_s = 1;
        private int _Id;
        private Semaphore _Gatekeeper;

        public Customer()
        {
            _Id = _Id_s++;
            _Gatekeeper = new Semaphore(1, 1);
            Acquire();
        }

        public void Acquire()
        {
            _Gatekeeper.WaitOne();
        }

        public void Release()
        {
            _Gatekeeper.Release();
        }

        public void Shave()
        {
            Thread.Sleep(new Random().Next(0, 10000));
            Console.WriteLine($"Customer {_Id} has been shaved.");
            CustomerCounter.GetInstance().Increment();
        }

        public int GetID()
        {
            return _Id;
        }
    }
}
