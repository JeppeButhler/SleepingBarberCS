using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SleepingBarber
{
    class Barber
    {
        private static int _Id_s = 1;
        private int _Id = -1;
        private States _State = States.SLEEPING;
        private CustomerCounter _CustomerCounter;
        private WaitingRoom _WaitingRoom;

        public Barber(WaitingRoom waitingRoom)
        {
            _Id = _Id_s++;
            this._CustomerCounter = CustomerCounter.GetInstance();
            this._WaitingRoom = waitingRoom;
            Sleep();
        }

        public void GiveHaircut(Customer customer)
        {
            Random randomizer = new Random();
            customer.Acquire();
            Console.WriteLine($"Barber {_Id} started shaving customer {customer.GetID()}.");
            Thread.Sleep(new Random().Next(0, 10000));
            customer.Shave();
            customer.Release();
        }

        public Boolean IsOccupied()
        {
            return _State == States.WORKING;
        }

        public void WakeBarber()
        {
            _State = States.WORKING;
            Console.WriteLine($"Barber {_Id} woke up.");
        }

        public void Sleep()
        {
            _State = States.SLEEPING;
            Console.WriteLine($"Barber {_Id} went to sleep.");
        }

        public int GetID()
        {
            return _Id;
        }

        public void Run()
        {
            int numOfCustomersToShavePerDay = 50;
            while (_CustomerCounter.CustomerCount() < numOfCustomersToShavePerDay)
            {
                Customer customer = _WaitingRoom.UnseatCustomer();
                if (customer != null)
                {
                    if (_State == States.SLEEPING)
                    {
                        WakeBarber();
                        GiveHaircut(customer);
                    }
                    else if (_State == States.WORKING)
                    {
                        Sleep();
                    }
                }
                else if(customer == null && numOfCustomersToShavePerDay < _CustomerCounter.CustomerCount()) { Console.WriteLine("All work is done!"); }
            }
        }

        public enum States
        {
            SLEEPING, WORKING
        }
    }
}