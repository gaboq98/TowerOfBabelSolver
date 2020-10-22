using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerOfBabelSolver.Model;
using TowerOfBabelSolver.Model.Movements;

namespace TowerOfBabelSolver.Controller
{
    class GameController
    {

        public MainWindow StartWindow { get; set; }
        public FileManager FileManager { get; set; }
        public Logic GameLogic { get; set; }

        public GameController(MainWindow startWindow)
        {
            StartWindow = startWindow;
            FileManager = new FileManager();
            GameLogic = new Logic();
            var r = FileManager.LoadStartMatrix();
            MatrixNode n = new MatrixNode(r);
            GameLogic.aStartSearch();
            Console.WriteLine("valor " + n.calculateHeuristFunction());

        }
    }
}
