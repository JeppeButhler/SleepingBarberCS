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
        private static CustomerCounter instance;
        private int CustomersFinished = 0;
        private Semaphore gatekeeper;

        private CustomerCounter()
        {
            gatekeeper = new Semaphore(0, 1);
        }

        public static CustomerCounter GetInstance()
        {
            if(instance == null)
            {
                instance = new CustomerCounter();
            }
            return instance;
        }

        public void Increment()
        {
            try
            {
                gatekeeper.WaitOne();
                ++CustomersFinished;
                Console.WriteLine($"+1: {CustomersFinished} persons have left the barbershop.");
                gatekeeper.Release();
            } catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public int CustomerCount()
        {
            int ValueToReturn = -1;
            try
            {
                gatekeeper.WaitOne();
                ValueToReturn = CustomersFinished;
                gatekeeper.Release();
            } catch (ThreadInterruptedException e)
            {
                Console.WriteLine(e.ToString());
            }
            return ValueToReturn;
        }
    }
}
