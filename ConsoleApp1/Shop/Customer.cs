﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SleepingBarber
{
    class Customer
    {
        private static int _Id_s;
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
            Console.WriteLine($"Customer {_Id} has been shaved.");
            CustomerCounter.GetInstance().Increment();
        }

        public int GetID()
        {
            return _Id;
        }
    }
}
