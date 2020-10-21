using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model.Movements
{
    class West2Move : Movable
    {
        public override string GetString()
        {
            return "O-2";
        }

        public override bool IsValid(string[,] matrix)
        {
            return true;
        }

        public override string[,] Move(string[,] matrix)
        {
            string[,] result = (string[,])matrix.Clone();
            int[] index = GetFreeSpaceIndex(result);
            string replace;
            if (index[1] - 1 == -1)
            {
                replace = result[index[0], result.GetLength(1) - 1];
                result[index[0], index[1]] = replace;
                result[index[0], result.GetLength(1) - 1] = "X";
            }
            else
            {
                replace = result[index[0], index[1] - 1];
                result[index[0], index[1]] = replace;
                result[index[0], index[1] - 1] = "X";
            }
            return result;
        }
    }
}
