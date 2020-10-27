using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using TowerOfBabelSolver.Model;
using TowerOfBabelSolver.Model.Movements;
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
            //StartMatrixToGraphic(FileManager.LoadStartMatrix());
            //FinishMatrixToGraphic(FileManager.LoadFinishMatrix());
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
            if (MessageBox.Show("Desea cargar una configuracion nueva?", "Cargar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
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
                } else
                {
                    UpdateMoveLabel("   ");
                }
            }
        }

        private void SaveButton(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea guardar la configuracion actual?", "Guardar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                //do yes stuff
            }
        }

        private void StartButton(object sender, RoutedEventArgs e)
        {
            LoadButton(null, null);
            StartWindow.NextButton.Visibility = Visibility.Visible;
            StartWindow.PreviousButton.Visibility = Visibility.Visible;
            StartWindow.StartButton.Visibility = Visibility.Hidden;
        }

        private void NextButton(object sender, RoutedEventArgs e)
        {

        }

        private void PreviousButton(object sender, RoutedEventArgs e)
        {
            
        }

        private void RestartButton(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Desea reiniciar", "Reiniciar", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
            }
            else
            {
                initWindow();
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

    }
}
