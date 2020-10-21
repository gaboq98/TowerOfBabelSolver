using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model.Movements
{
    class South3Move : Movable
    {
        public override string GetString()
        {
            return "S-3";
        }

        public override bool IsValid(string[,] matrix)
        {
            int[] index = GetFreeSpaceIndex(matrix);
            if (index[0] + 3 >= 4)
            {
                return false;
            }
            return true;
        }

        public override string[,] Move(string[,] matrix)
        {
            string[,] result = (string[,])matrix.Clone();
            int[] index = GetFreeSpaceIndex(result);
            string replace1, replace2, replace3;
            replace1 = result[index[0] + 1, index[1]];
            replace2 = result[index[0] + 2, index[1]];
            replace3 = result[index[0] + 3, index[1]];
            result[index[0], index[1]] = replace1;
            result[index[0] + 1, index[1]] = replace2;
            result[index[0] + 2, index[1]] = replace3;
            result[index[0] + 3, index[1]] = "X";
            return result;
        }
    }
}
