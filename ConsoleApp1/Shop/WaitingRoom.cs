using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SleepingBarber
{
    class WaitingRoom
    {
        private static WaitingRoom _Instance;
        private readonly int _NumberOfChairs = 5;
        private Queue<Customer> _CustomerQueue;
        private Semaphore _Gatekeeper;

        private WaitingRoom()
        {
            _CustomerQueue = new Queue<Customer>(_NumberOfChairs);
            _Gatekeeper = new Semaphore(1, 1);
        }

        public static WaitingRoom GetInstance()
        {
            if (_Instance == null)
            {
                _Instance = new WaitingRoom();
            }
            return _Instance;
        }

        public void SeatCustomer(Customer customer)
        {
            _Gatekeeper.WaitOne();
            if (_CustomerQueue.Count() < _NumberOfChairs)
            {
                _CustomerQueue.Enqueue(customer);
                Console.WriteLine($"Customer {customer.GetID()} was seated in waiting room.");
            }
            else
            {
                Console.WriteLine($"There was not seat available for customer {customer.GetID()}");
            }
            _Gatekeeper.Release();
        }

        public Customer UnseatCustomer()
        {
            Customer nextCustomer = null;
            _Gatekeeper.WaitOne();
            if (!IsQueueEmpty())
            {
                nextCustomer = _CustomerQueue.Dequeue();
                if (nextCustomer != null)
                {
                    nextCustomer.Release();
                    Console.WriteLine($"It's customer {nextCustomer.GetID()}'s turn.");
                }
            }
            _Gatekeeper.Release();
            return nextCustomer;
        }

        public Boolean IsQueueEmpty()
        {
            return _CustomerQueue.Count == 0;
        }
    }
}
