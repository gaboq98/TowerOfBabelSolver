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

        public override bool IsValid()
        {
            throw new NotImplementedException();
        }

        public override string[,] Move()
        {
            throw new NotImplementedException();
        }
    }
}
