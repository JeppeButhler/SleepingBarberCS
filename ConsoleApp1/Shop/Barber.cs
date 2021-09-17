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
        private static int Id_s = 1;
        private int Id = -1;
        private ThreadState state = ThreadState.Stopped;
        private WaitingRoom WaitingRoom;

        public Barber(WaitingRoom waitingRoom)
        {
            Id = Id_s++;
            this.WaitingRoom = waitingRoom;
        }

        public void GiveHaircut(Customer customer)
        {
            Random randomizer = new Random();
            customer.Acquire();
            Console.WriteLine("Barber {Id} started shaving customer {Customer.GetID}.");
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
            state = ThreadState.Running;
            Console.WriteLine("The barber {Id} woke up.");
        }

        public void Sleep()
        {
            state = ThreadState.Stopped;
        }

        public int GetID()
        {
            return Id;
        }


    }
}
