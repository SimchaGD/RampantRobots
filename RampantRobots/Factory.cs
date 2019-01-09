using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace RampantRobots
{
    class Factory
    {
        public int width { get; set; }
        public int height { get; set; }
        public int nRobots { get; set; }
        public bool robotsMove { get; set; }
        public int numberOfSteps { get; set; }
        public List<Robots> train; // Maak een leeg treintje aan
        public Mechanic Bob;

        public void Run()
        {
            string userInput;
            Console.WriteLine("Write 'stop' to quit...");
            do
            {
                Console.WriteLine(this.ToString());
                Console.Write("User input:");
                userInput = Console.ReadLine(); // Vraag input van de gebruiker

                if (("stop".Equals(userInput.ToLower()))) break;

                numberOfSteps--;
                Console.WriteLine("Number of steps left: " + numberOfSteps.ToString());

                this.MoveMechanic(userInput);
            } while (numberOfSteps > 0 & train.Count > 0);
            Console.WriteLine("Final Situation");
            Console.WriteLine(this.ToString());
            Console.Write("Press enter to quit...");
            Console.ReadLine();
        }

        public Factory(int width, int height, int nRobots, int numberOfSteps, bool robotsMove)
        {
            this.width = width;
            this.height = height;
            this.nRobots = nRobots;
            this.robotsMove = robotsMove;
            this.numberOfSteps = numberOfSteps;

            Bob = new Mechanic();
            DeployRobots(nRobots, true);
        }
        
        // Het treintje rijdt rond in de fabriek en zet de robots op een plek neer.
        // Deze plek kan random zijn. Het ligt er aan hoe de programmeur zich voelt.
        public void DeployRobots(int nRobots, bool randomize)
        {
            train = new List<Robots>();
            // Het treintje is leeg en moet gevuld worden
            for (int i = 0; i < nRobots; i++)
            {
                int robotIndex = (randomize) ? 0 : i;

                // Robbie zegt waar het naartoe gebracht wil worden
                Robots robbie = new Robots(this.height, this.width, randomize, robotIndex+1);
                if (!RobotExistsInTrain(train, robbie) & !Bob.Equals(robbie))// Robbie vraagt of iemand anders al naar die plek wil
                {
                    // Er zit niemand in de trein die daar ook naar toe wil en Robbie stapt in
                    train.Add(robbie);
                }
                else i--; // Er zat al een andere robot in de trein die naar dezelfde plek wil. 
                // Omdat robots hun eigen terretorium willen hebben, bedenkt Robbie opnieuw waar het naar toe wil
            }
            
            
        } // Het treintje zit vol en vertrekt naar de bestemmingen van de robots

        // Maak een kaart van de fabriek
        public override string ToString()
        {
            string factoryString = ""; // pak een tafel waar je iets op kan leggen
            
            
            for (int row = 0; row < this.height; row++)
            {   // Begin met tekenen op een nieuw papiertje
                // Bepaal hoe breedt de fabriek is
                StringBuilder sb = new StringBuilder(this.width);
                sb.Insert(0, "-", this.width); // Teken een lege rij
                sb.Append(Environment.NewLine); // Zorg dat er niks achter geplegd kan worden
                factoryString += sb.ToString(); // Leg de rij op tafel (onder aan de vorige rij)
            }
            // HOERA een lege fabriek is gemaakt

            // Maak een foto van de lege fabriek
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
            int MechanicPosition = Bob.xPos - 1 + (this.width + 2) * (Bob.yPos - 1);
            factorySB[MechanicPosition] = 'M';
            return factorySB.ToString(); // De kaart is af.
        }

        // Laat alle robot in de fabriek een stapje zetten
        public void MoveRobots()
        {
            for (int robot = 0; robot < train.Count; robot++)
            {
                // Hier wordt een kopie gemaakt, anders verandert het object mee en zal het object altijd in de trein van zitten
                Robots robbie = train[robot];
                Robots robbie2 = (Robots)robbie.Clone();
                int xRobbie = robbie.xPos;
                int yRobbie = robbie.yPos;
                robbie2.Walk();

                // Definieer wat robbie moet doen als het tegen een muur aan loopt
                if (robbie2.xPos <= 0 | robbie2.xPos >= width) robbie2.xPos = xRobbie;
                if (robbie2.yPos <= 0 | robbie2.yPos >= height)robbie2.yPos = yRobbie;

                // Kijk of er op de plek van robot al een robot stond
                if (RobotExistsInTrain(train, robbie2))
                {// zo ja, dan behoudt de robot zijn positie
                    robbie2.xPos = xRobbie;
                    robbie2.yPos = yRobbie;
                }
                
                train[robot] = robbie2;
            }
        }
        
        public void MoveMechanic(string directionsString)
        {
            // Bob heeft nieuwe speurtocht aanwijzingen gekregen
            List<char> directionList = new List<char>();
            directionList.AddRange(directionsString); // Maak de richtingscode aan
            for (int step = 0; step < directionList.Count(); step++)
            {
                // Geef Bob de 1 richtingscode
                char stepDirection = directionList[step];
                int direction = 0;
                int xBob = Bob.xPos;
                int yBob = Bob.yPos;

                // Ontcijfer de code
                if (stepDirection == 'd') direction = 1;
                if (stepDirection == 'a') direction = 2;
                if (stepDirection == 's') direction = -1;
                if (stepDirection == 'w') direction = -2;
                
                
                Bob.Walk(direction); // Bob zet een stapje vooruit

                // Definieer wat Bob moet doen als hij tegen een muur aan loopt
                if (Bob.xPos <= 0 | Bob.xPos > width) Bob.xPos = xBob;
                if (Bob.yPos <= 0 | Bob.yPos > height) Bob.yPos = yBob;
                
                // Bob heeft honger een kijkt of er een robot in de buurt is om op te eten
                EatRobots(Bob, train);
                if (robotsMove)
                {
                    // Laat de robots bewegen
                    this.MoveRobots();
                    EatRobots(Bob, train); // misschien is er nu wel een robot in de buurt?
                }                
            }
        }
        public void EatRobots(Mechanic Bob, List<Robots> Train)
        {
            for (int ii = 0; ii < Train.Count; ii++)
            {
                if (Bob.Equals(Train[ii]))
                {
                    Train.RemoveAt(ii); // De robot is opgegeten en bevindt zich niet meer in de trein
                }
            }
        }
        public bool RobotExistsInTrain(List<Robots> train, Robots robot)
        {
            for (int ii = 0; ii < train.Count; ii++)
            {
                if (train[ii].Equals(robot))
                {
                    return true;
                }
            }
            return false;
        }
    }
}
