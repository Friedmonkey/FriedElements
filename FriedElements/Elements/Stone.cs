using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedElements.Elements
{
    internal class Stone : ImmovableSolid
    {
        //public override Color Color { get; set; }
        public Stone() 
        {
            Color = new Color(136, 140, 141);
            flamabilityResistance = 10000;
        }
        public override bool ReciveHeat(CellularMatrix matrix, int heat)
        {
            flamabilityResistance -= heat;
            if (flamabilityResistance < 0)
            {
                DarkenColor(matrix);
                DieAndReplace(matrix, new Lava());
            }
            return true;
        }
    }
}
