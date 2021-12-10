using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace classwork
{
   public class Person
    {

        public string name { get; private set; }
        public string numgroup { get; }
        public int points { get; set; }
        
        public Person(string name, string number, int point)
        {
            this.name = name;
            this.numgroup = number;
            this.points = point;

        }
    }
}
