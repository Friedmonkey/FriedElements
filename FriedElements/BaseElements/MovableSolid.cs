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
            int posX = MatrixX;
            int posY = MatrixY - 1;
            for (int i = 0; i < 3; i++)
            {
                var targetCell = matrix.Get(posX, posY);

                if (targetCell is Empty or Liquid)
                {
                    SwapPositions(matrix, posX, posY);
                }

                if (targetCell is Solid)
                {
                    //SwapPositions(matrix, MatrixX, MatrixY - 1);\
                    if (i == 0) posX = MatrixX - 1;
                    if (i == 1) posX = MatrixX + 1;
                    continue;
                }
                break;
            }
        }
    }
}
