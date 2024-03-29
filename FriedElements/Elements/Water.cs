﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedElements.Elements
{
    public class Water : Liquid
    {
        public Water() 
        {
            Color = new Color(28, 163, 236, 128);
            Density = 1000;
            DispersionRate = 5;
            flamabilityResistance = 1000;
        }
        public override bool ReciveHeat(CellularMatrix matrix, int heat) 
        {
            flamabilityResistance -= heat;
            if (flamabilityResistance < 0)
            {
                DieAndReplace(matrix, new Steam());
            }
            return true;
        }
    }
}
