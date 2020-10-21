using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model.Movements
{
    class East1Move : Movable
    {
        public override string GetString()
        {
            return "E-1";
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
            if (index[1] + 1 < result.GetLength(1))
            {
                replace = result[index[0], index[1] + 1];
                result[index[0], index[1]] = replace;
                result[index[0], index[1] + 1] = "X";
            }
            else
            {
                replace = result[index[0], 0];
                result[index[0], index[1]] = replace;
                result[index[0], 0] = "X";
            }
            return result;
        }
    }
}
