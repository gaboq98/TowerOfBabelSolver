using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model.Movements
{
    class North4Move : Movable
    {
        public override string GetString()
        {
            return "N-4";
        }

        public override bool IsValid(string[,] matrix)
        {
            int[] index = GetFreeSpaceIndex(matrix);
            if (index[0]-4 <= -1)
            {
                return false;
            }
            return true;
        }

        public override string[,] Move(string[,] matrix)
        {
            throw new NotImplementedException();
        }
    }
}
