using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantRobots
{
    class Program
    {
        static void Main(string[] args)
        {
            // initialisatie van de fabriek
            Factory brieque = new Factory(4, 4, 5);
            Console.WriteLine(brieque.ToString());
            Console.ReadLine(); // voorkomt dat het programma zich direct afsluit
        }
    }
}
