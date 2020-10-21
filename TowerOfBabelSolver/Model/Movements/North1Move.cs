using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model.Movements
{
    class North1Move : Movable
    {

        public override string GetString()
        {
            return "N-1";
        }

        public override bool IsValid(string[,] matrix)
        {
            int[] index = GetFreeSpaceIndex(matrix);
            if (index[0]-1 <= -1)
            {
                return false;
            }
            return true;
        }

        public override string[,] Move(string[,] matrix)
        {
            string[,] result = (string[,])matrix.Clone();
            int[] index = GetFreeSpaceIndex(result);
            string replace;
            replace = result[index[0] - 1, index[1]];
            result[index[0], index[1]] = replace;
            result[index[0] - 1, index[1]] = "X";
            return result;
        }
    }
}
