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
            // Maak een fabriek en geef op:
            // - [INTEGER] De breedte
            // - [INTEGER] De hoogte
            // - [INTEGER] Het aantal robots
            // - [INTEGER] Het aantal beurten
            // - [TRUE/FALSE] Of de robots mogen lopen
            Factory fabriek = new Factory(10, 10, 5, 10, true);
            fabriek.Run();
        }
    }
}
