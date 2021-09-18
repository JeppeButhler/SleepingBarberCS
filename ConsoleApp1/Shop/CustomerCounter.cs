using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SleepingBarber
{
    class CustomerCounter
    {
        private static CustomerCounter _Instance;
        private int _CustomersFinished = 0;
        private Semaphore _Gatekeeper;

        private CustomerCounter()
        {
            _Gatekeeper = new Semaphore(1, 1);
        }

        public static CustomerCounter GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new CustomerCounter();
            }
            return _Instance;
        }

        public void Increment()
        {
            try
            {
                _Gatekeeper.WaitOne();
                ++_CustomersFinished;
                Console.WriteLine($"+1: {_CustomersFinished} persons have left the barbershop.");
                _Gatekeeper.Release();
            }
            catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public int CustomerCount()
        {
            int ValueToReturn = -1;
            _Gatekeeper.WaitOne();
            ValueToReturn = _CustomersFinished;
            _Gatekeeper.Release();
            return ValueToReturn;
        }
    }
}
