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
        private static int _id_s;
        private int _id;
        private Semaphore _gatekeeper;

        public Customer()
        {
            _id = _id_s++;
            _gatekeeper = new Semaphore(1, 1);
            Acquire();
        }

        public void Acquire()
        {
            _gatekeeper.WaitOne();
        }

        public void Release()
        {
            _gatekeeper.Release();
        }

        public void Shave()
        {
            Console.WriteLine("{_id} has been shaved.");
        }

        public int GetID()
        {
            return _id;
        }
    }
}
