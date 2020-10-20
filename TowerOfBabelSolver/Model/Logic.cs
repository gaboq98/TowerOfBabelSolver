using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model
{
    class Logic
    {

        public string[,] StartMatrix{ get; set; }
        public string[,] FinishMatrix { get; set; }

        public Logic()
        {
            StartMatrix = FileManager.LoadStartMatrix();
            FinishMatrix = FileManager.LoadFinishMatrix();
        }
    }
}
