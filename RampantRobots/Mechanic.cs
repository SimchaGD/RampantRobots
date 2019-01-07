using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantRobots
{
    class Mechanic
    {
        public int xPos { get; set; }
        public int yPos { get; set; }

        // Robbie wordt geïnstalleerd
        public Mechanic()
        {
            xPos = 1;
            yPos = 1;
        }

        public override bool Equals(object obj)
        {
            // Kijk of robbie met robots aan het praten is
            if (obj.GetType() == typeof(Robots))
            {
                Robots r = (Robots)obj;
                return r.xPos == this.xPos & r.yPos == this.yPos;
            }
            return false;
        }

        public void Walk(int direction)
        {
            if (direction == 1) xPos++;
            if (direction == 2) xPos--;
            if (direction == -1) yPos++;
            if (direction == -2) yPos--;
        }
    }
}
