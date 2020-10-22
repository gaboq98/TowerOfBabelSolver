using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model.Movements
{
    class West1Move : Movable
    {
        public override string GetString()
        {
            return "O-1";
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
            replace = result[index[0], ((index[1]-1) % 4 + 4) % 4];
            result[index[0], index[1]] = replace;
            result[index[0], ((index[1] - 1) % 4 + 4) % 4] = "X";
            return result;
        }
    }
}
