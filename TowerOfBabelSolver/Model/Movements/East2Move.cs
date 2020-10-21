using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model.Movements
{
    class East2Move : Movable
    {
        public override string GetString()
        {
            return "E-2";
        }

        public override bool IsValid(string[,] matrix)
        {
            return true;
        }

        public override string[,] Move(string[,] matrix)
        {
            string[,] result = (string[,])matrix.Clone();
            int[] index = GetFreeSpaceIndex(result);
            string replace1, replace2;
            replace1 = result[index[0], (index[1] + 1) % 4];
            replace2 = result[index[0], (index[1] + 2) % 4];
            result[index[0], index[1]] = replace1;
            result[index[0], (index[1] + 2) % 4] = replace2;
            result[index[0], (index[1] + 2) % 4] = "X";
            return result;
        }
    }
}
