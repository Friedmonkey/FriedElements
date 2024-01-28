using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedElements.Elements
{
    internal class Lava : Liquid
    {
        public Lava() 
        {
            Color = new Color(234, 92, 15, 150);
            Density = 2400;
            DispersionRate = 2;
        }
    }
}
