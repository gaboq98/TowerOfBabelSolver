using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using TowerOfBabelSolver.Model;
using TowerOfBabelSolver.View;

namespace TowerOfBabelSolver.Controller
{
    class GameController
    {
        private enum COLORS { Black, Grey, Green, Red, Blue, White };

        public MainWindow StartWindow { get; set; }
        public FileManager FileManager { get; set; }
        public Logic GameLogic { get; set; }
        public string StartPath { get; set; }
        public string FinishPath { get; set; }
        public int MoveCounter { get; set; }
        public MatrixNode MatrixSolution { get; set; }
        public MatrixNode MatrixSolutionMin { get; set; }
        public MatrixNode MatrixSolutionMax { get; set; }
        public MatrixNode MatrixSolutionMid { get; set; }
        public bool Found { get; set; }

        public GameController(MainWindow startWindow)
        {
            StartWindow = startWindow;
            MoveCounter = 0;
            Found = true;
            FileManager = new FileManager();
            initWindow();
        }

        private void initWindow()
        {
            StartWindow.NextButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(NextButton));
            StartWindow.StartButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(StartButton));
            StartWindow.PreviousButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(PreviousButton));
            StartWindow.HelpButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(HelpButton));
            StartWindow.RestartButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(RestartButton));
            StartWindow.SaveButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(SaveButton));
            StartWindow.LoadButton.AddHandler(Button.ClickEvent, new RoutedEventHandler(LoadButton));
            StartWindow.MoveLabel.Content = "=>     =>";
            StartWindow.NextButton.Visibility = Visibility.Hidden;
            StartWindow.PreviousButton.Visibility = Visibility.Hidden;
            StartWindow.StartButton.Visibility = Visibility.Visible;
            StartMatrixToGraphic(new string[,] { { "x", "R", "A", "B" }, { "V", "R", "A", "B" }, { "V", "R", "A", "B" }, { "V", "R", "A", "B" } });
            FinishMatrixToGraphic(new string[,] { { "x", "R", "A", "B" }, { "V", "R", "A", "B" }, { "V", "R", "A", "B" }, { "V", "R", "A", "B" } });
            UpdateMoveLabel("   ");
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

        private void LoadButton(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                StartWindow.NextButton.Visibility = Visibility.Visible;
                StartWindow.PreviousButton.Visibility = Visibility.Visible;
                StartWindow.StartButton.Visibility = Visibility.Hidden;
                ASerch();
            }
        }

        private bool Load()
        {
            if (MessageBox.Show("Desea cargar una configuracion nueva?", "Cargar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                return false;
            }
            else
            {
                MoveCounter = 0;
                int counter = 0;
                MessageBox.Show("Elija configuracion inicial");
                OpenFileDialog openFileDialog = new OpenFileDialog();
                openFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == true)
                {
                    StartPath = openFileDialog.FileName;
                    StartMatrixToGraphic(FileManager.LoadStartMatrix(openFileDialog.FileName));
                }
                MessageBox.Show("Elija configuracion final");
                if (openFileDialog.ShowDialog() == true)
                {
                    FinishPath = openFileDialog.FileName;
                    FinishMatrixToGraphic(FileManager.LoadFinishMatrix(openFileDialog.FileName));
                }
                if (counter > 0)
                {
                    MessageBox.Show("Algo salió mal");
                }
                else
                {
                    UpdateMoveLabel("   ");
                }
                return true;
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea guardar la solución a la configuración actual?", "Guardar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                List<string> result = new List<string>();
                for (int i = 0; i < MatrixSolution.Moves.Count; i++)
                {
                    result.Add(MatrixSolution.Sucesors[i].GetString());
                    result.Add(MatrixSolution.Moves[i].GetString());
                    result.Add("");
                }
                result.Add(MatrixSolution.Sucesors[MatrixSolution.Sucesors.Count-1].GetString());
                string text = string.Join("\n", result);
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.DefaultExt = "txt";
                saveFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                if (saveFileDialog.ShowDialog() == true)
                    File.WriteAllText(saveFileDialog.FileName, text);
            }
        }

        private void StartButton(object sender, RoutedEventArgs e)
        {
            if (Load())
            {
                StartWindow.NextButton.Visibility = Visibility.Visible;
                StartWindow.PreviousButton.Visibility = Visibility.Visible;
                StartWindow.StartButton.Visibility = Visibility.Hidden;
                ASerch();
            }
        }

        private void NextButton(object sender, RoutedEventArgs e)
        {
            if (MoveCounter + 1 <= MatrixSolution.Sucesors.Count-2)
            {
                MoveCounter++;
                StartMatrixToGraphic(MatrixSolution.Sucesors[MoveCounter].Matrix);
                FinishMatrixToGraphic(MatrixSolution.Sucesors[MoveCounter + 1].Matrix);
                UpdateMoveLabel(MatrixSolution.Moves[MoveCounter].GetString());
            }
            else
            {
                MessageBox.Show("No más movimientos");
            }
        }

        private void PreviousButton(object sender, RoutedEventArgs e)
        {
            if (MoveCounter - 1 >= 0)
            {
                MoveCounter--;
                StartMatrixToGraphic(MatrixSolution.Sucesors[MoveCounter].Matrix);
                FinishMatrixToGraphic(MatrixSolution.Sucesors[MoveCounter + 1].Matrix);
                UpdateMoveLabel(MatrixSolution.Moves[MoveCounter].GetString());
            }
            else
            {
                MessageBox.Show("No más movimientos");
            }
        }

        private void RestartButton(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea reiniciar", "Reiniciar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                StartWindow.NextButton.Visibility = Visibility.Hidden;
                StartWindow.PreviousButton.Visibility = Visibility.Hidden;
                StartWindow.StartButton.Visibility = Visibility.Visible;
                MoveCounter = 0;
                MatrixSolution = null;
                StartMatrixToGraphic(new string[,] { {"X","R","A","B" },{ "V", "R", "A", "B" },{ "V", "R", "A", "B" }, { "V", "R", "A", "B" } });
                FinishMatrixToGraphic(new string[,] { { "X", "R", "A", "B" }, { "V", "R", "A", "B" }, { "V", "R", "A", "B" }, { "V", "R", "A", "B" } });
                UpdateMoveLabel("   ");
            }
            
        }

        private void HelpButton(object sender, RoutedEventArgs e)
        {
            HelpWindow helpWindow = new HelpWindow();
            helpWindow.Show();
        }

        private void UpdateMoveLabel(string text)
        {
            StartWindow.MoveLabel.Content = "=> " + text + " =>";
        }

        private void ASerch()
        {
            GameLogic = new Logic(FileManager.LoadStartMatrix(StartPath), FileManager.LoadFinishMatrix(FinishPath));
            ASerchMin();
            /*
            ThreadStart delegado1 = new ThreadStart(ASerchMin);
            Thread hilo1 = new Thread(delegado1);
            hilo1.Start();
            ThreadStart delegado2 = new ThreadStart(ASerchMax);
            Thread hilo2 = new Thread(delegado2);
            hilo2.Start();
            ThreadStart delegado3 = new ThreadStart(ASerchMid);
            Thread hilo3 = new Thread(delegado3);
            hilo3.Start();
            while (Found)
            {
                // Busy waiting
            } */
            Console.WriteLine("Hilos terminados");
            if (MatrixSolutionMin != null)
            {
                MatrixSolution = MatrixSolutionMin;
            } 
            else if (MatrixSolutionMax != null)
            {
                MatrixSolution = MatrixSolutionMax;
            } 
            else if (MatrixSolutionMid != null)
            {
                MatrixSolution = MatrixSolutionMid;
            }
            if (MatrixSolution == null)
                Console.WriteLine("Error");
            if (MoveCounter + 1 < MatrixSolution.Sucesors.Count)
            {
                StartMatrixToGraphic(MatrixSolution.Sucesors[MoveCounter].Matrix);
                FinishMatrixToGraphic(MatrixSolution.Sucesors[MoveCounter + 1].Matrix);
                UpdateMoveLabel(MatrixSolution.Moves[MoveCounter].GetString());
            }
        }

        private void ASerchMin()
        {
            MatrixSolutionMin = GameLogic.aStartSearch();
            Console.WriteLine("Min Fin");
            if (MatrixSolutionMin != null)
            {
                Found = false;
            }

        }

        private void ASerchMax()
        {
            MatrixSolutionMax = GameLogic.aStartSearchMax();
            Console.WriteLine("Max Fin");
            if (MatrixSolutionMax != null)
            {
                Found = false;
            }
        }

        private void ASerchMid()
        {
            MatrixSolutionMid = GameLogic.aStartSearchMid();
            Console.WriteLine("Mid Fin");
            if (MatrixSolutionMid != null)
            {
                Found = false;
            }
        }

    }
}
