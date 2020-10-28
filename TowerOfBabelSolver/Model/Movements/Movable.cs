using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model.Movements
{
    abstract class Movable
    {

        public abstract string[,] Move(string[,] matrix);
        public abstract bool IsValid(string[,] matrix);
        public abstract string GetString();

        protected static int[] GetFreeSpaceIndex(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == "X")
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return null;
        }

        public static string CalcOposite(Movable move)
        {
            switch(move.GetString()[0])
            {
                case 'N':
                    return "S-" +move.GetString()[2];
                case 'S':
                    return "N-" + move.GetString()[2];
                case 'E':
                    return "O-" + move.GetString()[2];
                case 'O':
                    return "E-" + move.GetString()[2];
                default:
                    return "";
            }
        }

    }
}
