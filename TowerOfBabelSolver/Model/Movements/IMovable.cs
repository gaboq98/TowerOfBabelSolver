using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model.Movements
{
    interface IMovable
    {

        void Move();
        void IsValid();
        string GetString();
    }
}
