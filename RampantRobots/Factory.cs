using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantRobots
{
    class Factory
    {
        public int width { get; set; }
        public int height { get; set; }
        public int nRobots { get; set; }
        public List<Robots> train; // Maak een leeg treintje aan
        
        public Factory(int width, int height, int nRobots)
        {
            this.width = width;
            this.height = height;
            this.nRobots = nRobots;
            deployRobots(nRobots, true);
        }
        
        // Het treintje rijdt rond in de fabriek en zet de robots op een plek neer.
        // Deze plek kan random zijn. Het ligt er aan hoe de programmeur zich voelt.
        public void deployRobots(int nRobots, bool randomize)
        {
            train = new List<Robots>();
            if (randomize)
            {
                // Het treintje is leeg en moet gevuld worden
                for (int i = 0; i < nRobots; i++)
                {
                    // Robbie zegt waar het naartoe gebracht wil worden
                    Robots robbie = new Robots(this.height, this.width, randomize);
                    if (!train.Contains(robbie)) // Robbie vraagt of iemand anders al naar die plek wil
                    {
                        // Er zit niemand in de trein die daar ook naar toe wil en Robbie stapt in
                        train.Add(robbie);
                    }
                    else i--; // Er zat al een andere robot in de trein die naar dezelfde plek wil. 
                    // Omdat robots hun eigen terretorium willen hebben, bedenkt Robbie opnieuw waar het naar toe wil
                }
            }
            //else
            //{

            //}
            
        } // Het treintje zit vol en vertrekt naar de bestemmingen van de robots

        // Maak een kaart van de fabriek
        public override string ToString()
        {
            string factoryString = ""; // pak een tafel waar je iets op kan leggen
            
            // Begin met tekenen op een nieuw papiertje
            for (int row = 0; row < this.height; row++)
            {
                // Bepaal hoe breedt de fabriek is
                StringBuilder sb = new StringBuilder(this.width);
                sb.Insert(0, "-", this.width); // Teken een lege rij
                sb.Append(Environment.NewLine); // Zorg dat er niks achter geplegd kan worden
                factoryString += sb.ToString(); // Leg de rij op tafel (onder aan de vorige rij)
            }
            // HOERA een lege fabriek is gemaakt

            // Maak een foto foto van de lege fabriek
            StringBuilder factorySB = new StringBuilder(factoryString);
            // Loop naar het treintje toe en vraag aan alle robots waar ze staan
            for (int robot = 0; robot < train.Count; robot++) 
            {
                // Loop naar Robbie toe
                Robots robbie = train[robot];

                // Vraag robbier waar hij staat
                int position = robbie.xPos - 1 + (this.width+2) * (robbie.yPos-1);
                // Zet op die plek een R
                factorySB[position] = 'R';
            }
            return factorySB.ToString(); // De kaart is af.
        }
    }
}
