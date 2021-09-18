using System;
using System.Threading;

namespace SleepingBarber
{
    class Program
    {
        private static readonly int _NumberOfBarbers = 2;
        private Barber[] _Workers = new Barber[_NumberOfBarbers];
        private WaitingRoom _WaitingRoom;

        public Program()
        {
            _WaitingRoom = WaitingRoom.GetInstance();
        }

        static void Main(string[] args)
        {
            Program instance = new Program();
            instance.StartWorkDay();
        }

        public void StartWorkDay()
        {
            for (int i = 0; i < _NumberOfBarbers; i++)
            {
                _Workers[i] = new Barber(_WaitingRoom);
                _Workers[i].Run();
            }

            new Thread(new ThreadStart(Run)).Start();
        }

        public void Run()
        {
            int index = 0;
            while(index++ < 50 && CustomerCounter.GetInstance().CustomerCount() < 50)
            {
                try
                {
                    _WaitingRoom.SeatCustomer(new Customer());
                    Thread.Sleep(new Random().Next(0, 10000));
                } catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        }
    }
}
