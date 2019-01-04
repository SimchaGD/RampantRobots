using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantRobots
{
    class Robots
    {
        public int xPos { get; set; }
        public int yPos { get; set; }

        // Robbie wordt geïnstalleerd
        public Robots(int height, int width, bool randomize)
        {   
            if (randomize)
            {
                // Robbie bedenkt willekeurig waar het naar toe wil
                Random rnd = new Random();
                this.xPos = rnd.Next(1, width);
                this.yPos = rnd.Next(1, height);
            }
        }

        // Robbie vraagt of iemand anders al naar zijn bestemming gaat
        public override bool Equals(object obj)
        {
            // Kijk of robbie met robots aan het praten is
            if (obj.GetType() == typeof(Robots))
            {
                Robots r = (Robots) obj;
                return r.xPos == this.xPos & r.yPos == this.yPos;
            }
            return false;
        }
    }
}
