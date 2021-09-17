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
        private ThreadState _State = ThreadState.Stopped;
        private WaitingRoom _WaitingRoom;

        public Barber(WaitingRoom waitingRoom)
        {
            _Id = _Id_s++;
            this._WaitingRoom = waitingRoom;
        }

        public void GiveHaircut(Customer customer)
        {
            Random randomizer = new Random();
            customer.Acquire();
            Console.WriteLine($"Barber {_Id} started shaving customer {customer.GetID()}.");
            Thread.Sleep(2000 * randomizer.Next(0, 10000));
            customer.Shave();
            customer.Release();
        }

        public static Boolean IsOccupied()
        {
            return Thread.CurrentThread.ThreadState == ThreadState.Running;
        }

        public void WakeBarber()
        {
            _State = ThreadState.Running;
            Console.WriteLine($"The barber {Id} woke up.");
        }

        public void Sleep()
        {
            _State = ThreadState.Stopped;
        }

        public int GetID()
        {
            return _Id;
        }

        public void Run()
        {

        }
    }
}
