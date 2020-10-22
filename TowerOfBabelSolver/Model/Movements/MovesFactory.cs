using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model.Movements
{
    class MovesFactory
    {

        public enum TYPE { N, S, E, O };

        public static Movable GetInstance(int type, int range)
        {
            switch (type)
            {
                case 0:     // North
                    switch (range)
                    {
                        case 0:
                            return new North1Move();
                        case 1:
                            return new North2Move();
                        case 2:
                            return new North3Move();
                        default:
                            return null;
                    }
                case 1:     // South
                    switch (range)
                    {
                        case 0:
                            return new South1Move();
                        case 1:
                            return new South2Move();
                        case 2:
                            return new South3Move();
                        default:
                            return null;
                    }
                case 2:     // East
                    switch (range)
                    {
                        case 0:
                            return new East1Move();
                        case 1:
                            return new East2Move();
                        case 2:
                            return new East3Move();
                        default:
                            return null;
                    }
                case 3:     // West
                    switch (range)
                    {
                        case 0:
                            return new West1Move();
                        case 1:
                            return new West2Move();
                        case 2:
                            return new West3Move();
                        default:
                            return null;
                    }
                default:
                    return null;
            }
        }
    }
}
