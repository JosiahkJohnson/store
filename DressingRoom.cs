using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ClothingStore
{
    class DressingRoom
    {
        //initilize variables that will be needed
        int rooms;
        long waitTimer;
        long runTimer;

        //object to control access to the rooms
        Semaphore semaphore;

        //default constructor
        public DressingRoom()
        {
            //three rooms by default
            rooms = 3;

            //give value to the semaphore object to control access to the rooms
            semaphore = new Semaphore(rooms, rooms);
        }

        //dressing room class with parameter
        public DressingRoom(int room)
        {
            rooms = room;

            //properly set the semaphore object
            semaphore = new Semaphore(rooms, rooms);
        }

        //use threading to assign and request rooms
        public async Task RequestRoom(Customer customer)
        {
            //create a stopwatch to start counting
            Stopwatch stopWatch = new Stopwatch();

            //write to the console that a new customer is waiting
            Console.WriteLine("Customer is Waiting.");

            //start the timer and wait for the next room
            stopWatch.Start();
            semaphore.WaitOne();

            //this next bit will happen once one is availible
            //stop the stopwatch and get the time that elapsed
            stopWatch.Stop();
            waitTimer += stopWatch.ElapsedTicks;

            //determine how long the customer is in the room
            Random rnd = new Random();
            //how long each item takes is random
            int roomWaitTime = rnd.Next(1, 3);
            stopWatch.Start();
            //wait for the simulated amount of time it takes to do all items in customer inventory
            Thread.Sleep(roomWaitTime * customer.getNumberOfItems());
            //afterwards stop the timer to stop counting
            stopWatch.Stop();
            //calculate the amount to time elapsed
            runTimer += stopWatch.ElapsedTicks;

            //write to the screen that the customer is finished
            Console.WriteLine("Customer is finished in the room.");
            semaphore.Release();
        }

        //methods to get variables
        public long getWaitTime()
        {
            return waitTimer;
        }
        public long getRunTime()
        {
            return runTimer;
        }
    }
}
