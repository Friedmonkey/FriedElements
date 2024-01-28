using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedElements.Elements
{
    internal class Steel : ImmovableSolid
    {
        //public override Color Color { get; set; }
        public Steel() 
        {
            Color = new Color(36, 40, 41);
        }
    }
}
