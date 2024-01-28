using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedElements.Elements
{
    public class Oil : Liquid
    {
        public Oil() 
        {
            Color = new Color(219, 207, 92, 128);
            Density = 800;
            DispersionRate = 3;
            flamabilityResistance = 300;
        }
        public override bool ReciveHeat(CellularMatrix matrix, int heat) 
        {
            flamabilityResistance -= heat;
            if (flamabilityResistance < 0)
            { 
                DieAndReplace(matrix,new Smoke());
            }
            return true;
        }
    }
}
