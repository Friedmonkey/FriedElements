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
            TryMove(matrix, MatrixX, MatrixY - 1);

            if (matrix.Get(MatrixX, MatrixY - 1) is Solid)
            {
                bool moveLeftFirst = RandomBool();
                int moveDirection = moveLeftFirst ? 1 : -1;

                //try to move
                bool alreadyMoved = TryMove(matrix, MatrixX + moveDirection, MatrixY - 1,true);

                //if you heavent moved try to move the other way
                if (!alreadyMoved)
                    TryMove(matrix, MatrixX + moveDirection, MatrixY - 1,true);
            }
        }
        private bool TryMove(CellularMatrix matrix, int targetX, int targetY, bool diagonally = false)
        {
            var targetCell = matrix.Get(targetX, targetY);

            if (targetCell is Empty or Liquid)
            {
                if (diagonally)
                { 
                    targetCell = matrix.Get(targetX, targetY+1);
                    if (targetCell is Empty or Liquid)
                    { 
                        SwapPositions(matrix, targetX, targetY+1);
                        return true;
                    }
                }
                else
                {
                    SwapPositions(matrix, targetX, targetY);
                    return true;
                }
            }
            return false;
        }
        private bool RandomBool()
        {
            return Random.Shared.Next(2) == 0;
            //return new Random().Next(2) == 0;
        }
    }
}
