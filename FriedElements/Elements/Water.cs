using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedElements.Elements
{
    public class Water : Liquid
    {
        public override bool ReciveHeat(CellularMatrix matrix, int heat) 
        {
            DieAndReplace(matrix,new Steam());
            return true;
        }
    }
}
