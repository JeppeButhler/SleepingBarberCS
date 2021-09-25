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
            new Thread(new ThreadStart(Run)).Start();

            for (int i = 0; i < _NumberOfBarbers; i++)
            {
                _Workers[i] = new Barber(_WaitingRoom);
                _Workers[i].Run();
            }
        }

        public void Run()
        {
            //int index = 1;
            bool areAllBarbersDone = false;
            int barbersFinished = 0;
            int minNumOfCustomers = 50;
            while(CustomerCounter.GetInstance().CustomerCount() < minNumOfCustomers || (areAllBarbersDone == false || _WaitingRoom.IsQueueEmpty() == false))
            {
                try
                {
                    if(CustomerCounter.GetInstance().CustomerCount() <= minNumOfCustomers)
                    {
                        _WaitingRoom.SeatCustomer(new Customer());
                        Thread.Sleep(new Random().Next(0, 5000));
                    }
                    
                    foreach(Barber barber in _Workers)
                    {
                        if(barber.IsOccupied() == false)
                        {
                            barbersFinished++;
                        }
                    }

                    if(barbersFinished == _Workers.Length)
                    {
                        areAllBarbersDone = true;
                    }
                }
                catch (ThreadInterruptedException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
        Console.WriteLine("Workday over, shop is now closing.");
        }
    }
}