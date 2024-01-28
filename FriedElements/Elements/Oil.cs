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
        }
        public override bool ReciveHeat(CellularMatrix matrix, int heat) 
        {
            DieAndReplace(matrix,new Steam());
            return true;
        }
    }
}
