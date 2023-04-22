using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore
{
    class Scenario
    {
        //call the customer class from the other file and initilize needed variables
        static Customer customer;
        static int items = 0;
        static int numberOfItems;
        static int controlItemNumber;

        //the scenario class
        public Scenario(int rooms, int customers)
        {
            //describe the current scenario to the screen
            Console.WriteLine(rooms + " dressing rooms available for " + customers + " customers.");

            //set default parameters
            controlItemNumber = 0;
            numberOfItems = 0;
        }
        static void Main(string[] args)
        {
            //The main part of the program
            //gets user input to determine number of items, number of customers, and number of rooms

            //get the number of items from user
            Console.Write("How many clothing items does each customer have? (value of 0 is random): ");
            controlItemNumber = Int32.Parse(Console.ReadLine());

            //get the number of customers
            Console.Write("How many customers are there? ");
            int numberOfCustomers = Int32.Parse(Console.ReadLine());

            //get the total number of rooms from the user
            Console.Write("How many rooms are there? ");
            int totalRooms = Int32.Parse(Console.ReadLine());

            //Scenario Starts
            Scenario scenario = new Scenario(totalRooms, numberOfCustomers);

            //create the dressingRoom instance from user input
            DressingRoom dressingRoom = new DressingRoom(totalRooms);

            //Create a list of tasks so they are handled in order
            List<Task> tasks = new List<Task>();

            //main loop to handle the customers, each iteration is a new customer in the list
            for(int i = 0; i < numberOfCustomers; i++)
            {
                //assign the customer
                customer = new Customer(controlItemNumber);

                //get their number of items
                numberOfItems = customer.getNumberOfItems();

                //Total number of items tallied
                items += numberOfItems;

                //start the room request method
                tasks.Add(Task.Factory.StartNew(async () =>
                {
                    await dressingRoom.RequestRoom(customer);
                }));
            }

            //convert to an array
            Task.WaitAll(tasks.ToArray());

            //display results output to the user
            Console.WriteLine("Average run time in milliseconds: " + dressingRoom.getRunTime()/numberOfCustomers);
            Console.WriteLine("Average wait time in milliseconds: " + dressingRoom.getWaitTime()/numberOfCustomers);
            Console.WriteLine("Total customers: " + numberOfCustomers);
            Console.WriteLine("Average number of items per customer: " + items/numberOfCustomers);

            //keeps the console open for user to read
            Console.Read();
        }
    }
}
