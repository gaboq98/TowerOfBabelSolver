using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using TowerOfBabelSolver.Model;
using TowerOfBabelSolver.Model.Movements;

namespace TowerOfBabelSolver.Controller
{
    class GameController
    {
        private enum COLORS { Black, Grey, Green, Red, Blue, White };

        public MainWindow StartWindow { get; set; }
        public FileManager FileManager { get; set; }
        public Logic GameLogic { get; set; }

        public GameController(MainWindow startWindow)
        {
            StartWindow = startWindow;
            FileManager = new FileManager();
            initWindow();
            /*
            GameLogic = new Logic();
            var r = FileManager.LoadStartMatrix();
            MatrixNode n = new MatrixNode(r);
            GameLogic.aStartSearch();
            Console.WriteLine("valor " + n.calculateHeuristFunction());
            */
        }

        private void initWindow()
        {
            StartWindow.MoveLabel.Content = "=>     =>";
            StartWindow.NextButton.Visibility = Visibility.Hidden;
            StartWindow.PreviousButton.Visibility = Visibility.Hidden;
            StartMatrixToGraphic(FileManager.LoadStartMatrix());
            FinishMatrixToGraphic(FileManager.LoadFinishMatrix());
        }

        private void StartMatrixToGraphic(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i,j] == "X")
                    {
                        StartWindow.StartMatrix[i, j].Background = StartWindow.Colors.ElementAt((int)COLORS.Black);
                    } else if (matrix[i, j] == "O")
                    {
                        StartWindow.StartMatrix[i, j].Background = StartWindow.Colors.ElementAt((int)COLORS.Grey);
                    }
                    else if (matrix[i, j] == "V")
                    {
                        StartWindow.StartMatrix[i, j].Background = StartWindow.Colors.ElementAt((int)COLORS.Green);
                    }
                    else if (matrix[i, j] == "R")
                    {
                        StartWindow.StartMatrix[i, j].Background = StartWindow.Colors.ElementAt((int)COLORS.Red);
                    }
                    else if (matrix[i, j] == "A")
                    {
                        StartWindow.StartMatrix[i, j].Background = StartWindow.Colors.ElementAt((int)COLORS.Blue);
                    }
                    else if (matrix[i, j] == "B")
                    {
                        StartWindow.StartMatrix[i, j].Background = StartWindow.Colors.ElementAt((int)COLORS.White);
                    }
                }
            }
        }

        private void FinishMatrixToGraphic(string[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == "X")
                    {
                        StartWindow.FinishMatrix[i, j].Background = StartWindow.Colors.ElementAt((int)COLORS.Black);
                    }
                    else if (matrix[i, j] == "O")
                    {
                        StartWindow.FinishMatrix[i, j].Background = StartWindow.Colors.ElementAt((int)COLORS.Grey);
                    }
                    else if (matrix[i, j] == "V")
                    {
                        StartWindow.FinishMatrix[i, j].Background = StartWindow.Colors.ElementAt((int)COLORS.Green);
                    }
                    else if (matrix[i, j] == "R")
                    {
                        StartWindow.FinishMatrix[i, j].Background = StartWindow.Colors.ElementAt((int)COLORS.Red);
                    }
                    else if (matrix[i, j] == "A")
                    {
                        StartWindow.FinishMatrix[i, j].Background = StartWindow.Colors.ElementAt((int)COLORS.Blue);
                    }
                    else if (matrix[i, j] == "B")
                    {
                        StartWindow.FinishMatrix[i, j].Background = StartWindow.Colors.ElementAt((int)COLORS.White);
                    }
                }
            }
        }

    }
}
