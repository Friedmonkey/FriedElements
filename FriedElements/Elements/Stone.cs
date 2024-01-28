using SFML.Graphics;
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
            Color = new Color(128,128,128);
        }
        public override bool ReciveHeat(CellularMatrix matrix, int heat)
        {
            return false;
        }
    }
}
