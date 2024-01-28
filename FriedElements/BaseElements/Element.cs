global using SFML.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedElements.Elements
{
    public abstract class Element
    {
        public Element() { }
        public Element(int x, int y)
        {
            this.MatrixX = x;
            this.MatrixY = y;
        }
        private bool isIgnited;
        private int flamabilityResistance;
        //public virtual Color Color { get; set; } = Color.White;
        public Color Color = Color.White;
        private bool discolored;

        public int MatrixX = 0;
        public int MatrixY = 0;
        public virtual void DarkenColor() 
        {
            this.Color = new Color((byte) (this.Color.R * .85f), this.Color.G, this.Color.B);
            this.discolored = true;
        }

        public virtual void Step(CellularMatrix matrix) { }

        public virtual void DieAndReplace(CellularMatrix matrix, Element element)
        {
            matrix.Set(MatrixX, MatrixY, element);
        }
        public virtual void CheckIfIgnited() 
        {
            isIgnited = (flamabilityResistance < 0);
        }
        public virtual void SwapPositions(CellularMatrix matrix, int swapX, int swapY) 
        {
            if (MatrixX == swapX && MatrixY == swapY)
            {
                return; 
            }
            Element toSwap = matrix.Get(swapX, swapY);
            matrix.Set(MatrixX, MatrixY, toSwap);
            matrix.Set(swapX, swapY, this);
        }
        public virtual bool ReciveHeat(CellularMatrix matrix, int heat) 
        {
            if (isIgnited) 
            {
                return false;
            }

            this.flamabilityResistance -= (int) (Random.Shared.NextSingle() * heat);
            return true;
        }
    }
}
