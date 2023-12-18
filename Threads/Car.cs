using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Threads
{
    internal class Car
    {
        public string Name { get; set; }
        public int SpeedPerHour { get; set; }
        public int DistanceTraveled { get; set; }


        public Car(string name)
        {
            Name = name;
            SpeedPerHour = 120;
            DistanceTraveled = 0;
        }


    }
}
