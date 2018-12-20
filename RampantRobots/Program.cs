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
            Factory brieque = new Factory(4, 4);
            Console.WriteLine(brieque.ToString());
            Console.ReadLine();
        }
    }
}
