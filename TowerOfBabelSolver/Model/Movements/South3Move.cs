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
            throw new NotImplementedException();
        }

        public override string[,] Move(string[,] matrix)
        {
            throw new NotImplementedException();
        }
    }
}
