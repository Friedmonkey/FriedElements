using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriedElements.Elements
{
    public abstract class MovableSolid : Solid
    {
        public override void Step(CellularMatrix matrix) 
        {
            var targetCell = matrix.Get(MatrixX, MatrixY - 1);

            if (targetCell is Empty or Liquid)
            {
                SwapPositions(matrix, MatrixX, MatrixY - 1);
            }

            if (targetCell is Solid)
            {
                //SwapPositions(matrix, MatrixX, MatrixY - 1);
            }
        }
    }
}
