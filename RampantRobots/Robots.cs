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
        public Robots(int height, int width, bool randomize, int index)
        {   
            if (randomize)
            {
                // Robbie bedenkt willekeurig waar het naar toe wil
                Random rnd = new Random();
                this.xPos = rnd.Next(1, width);
                this.yPos = rnd.Next(1, height);
            }
            else
            {
                this.xPos = index;
                this.yPos = index;
            }
        }

        // Robbie vraagt of iemand anders al naar zijn bestemming gaat
        public bool Equals(object obj)
        {
            // Kijk of robbie met robots aan het praten is
            if (obj.GetType() == typeof(Robots))
            {
                Robots r = (Robots) obj;
                return r.xPos == this.xPos & r.yPos == this.yPos;
            }
            return false;
        }

        // De robots zetten een stapje
        public void Walk()
        {
            Random rnd = new Random();
            int direction = 0;
            while (direction == 0)
            {
                // Kies welke richting robbie op moet stappen
                direction = rnd.Next(-2, 2);
            }
            
            if (direction == 1) xPos++;
            if (direction == 2) xPos--;
            if (direction == -1) yPos++;
            if (direction == -2) yPos--;
                        
        }

        public object Clone()
        {
            return this.MemberwiseClone();
        }
        
    }
}
