using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RampantRobots
{
    class Factory
    {
        int width { get; set; }
        int height { get; set; }

        public Factory(int width, int height)
        {
            this.width = width;
            this.height = height;
        }
        public override string ToString()
        {
            string factoryString= "";
            
            for (int i = 0; i < this.height; i++)
            {
                StringBuilder sb = new StringBuilder(this.width);
                sb.Insert(0, "-", this.width);
                sb.Append(Environment.NewLine);
                factoryString += sb.ToString();
            }
            return factoryString;
        }
    }
}
