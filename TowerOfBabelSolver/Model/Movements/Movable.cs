using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model.Movements
{
    abstract class Movable
    {

        public enum TOKEN { X, V, R, A, B };

        public abstract string[,] Move();
        public abstract bool IsValid();
        public abstract string GetString();

        public int[] GetFreeSpaceIndex(string[,] matrix)
        {
            return new int[] { 0, 1 };
        }

    }
}
