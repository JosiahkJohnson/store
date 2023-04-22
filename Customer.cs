using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothingStore
{
    //class that represents a customer in the store
    class Customer
    {
        //initilizing the required variable for number of items
        int numberOfItems;

        //create the constructor
        public Customer() 
        {
            //default varible
            numberOfItems = 6;
        }

        //customizable constructor
        public Customer(int items)
        {
            //generate a random number between 1 and 20 if set to 0, but can be manually set for testing
            Random rnd = new Random();
            if(items == 0)
            {
                numberOfItems = rnd.Next(1, 20);
            } else
            {
                numberOfItems = items;
            }
        }

        //method to return number of items when called for
        public int getNumberOfItems()
        {
            return numberOfItems;
        }
    }
}
