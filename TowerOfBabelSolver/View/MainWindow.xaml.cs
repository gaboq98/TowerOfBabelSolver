using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TowerOfBabelSolver.Controller;
using TowerOfBabelSolver.Model;

namespace TowerOfBabelSolver
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public List<Brush> Colors { get; set; }
        public Label[,] StartMatrix{ get; set; }
        public Label[,] FinishMatrix { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MakeColorsList();
            MakeStartMatrixList();
            MakeFinishMatrixList();
            new GameController(this);
        }

        private void MakeColorsList()
        {
            Colors = new List<Brush>();
            Colors.Add(Brushes.Black);
            BrushConverter bc = new BrushConverter();
            Brush brush = (Brush)bc.ConvertFrom("#FFC2C2C2");
            brush.Freeze();
            Colors.Add(brush);
            brush = (Brush)bc.ConvertFrom("#FF56F025");
            brush.Freeze();
            Colors.Add(brush);
            brush = (Brush)bc.ConvertFrom("#FFFF4F4F");
            brush.Freeze();
            Colors.Add(brush);
            brush = (Brush)bc.ConvertFrom("#FF4E90F1");
            brush.Freeze();
            Colors.Add(brush);
            Colors.Add(Brushes.White);
        }

        private void MakeStartMatrixList()
        {
            StartMatrix = new Label[,] { 
                { Label0x0, Label0x1, Label0x2, Label0x3 },
                { Label1x0, Label1x1, Label1x2, Label1x3 },
                { Label2x0, Label2x1, Label2x2, Label2x3 },
                { Label3x0, Label3x1, Label3x2, Label3x3 },
                { Label4x0, Label4x1, Label4x2, Label4x3 }
            };
        }

        private void MakeFinishMatrixList()
        {
            FinishMatrix = new Label[,] {
                { LabelFinal0x0, LabelFinal0x1, LabelFinal0x2, LabelFinal0x3 },
                { LabelFinal1x0, LabelFinal1x1, LabelFinal1x2, LabelFinal1x3 },
                { LabelFinal2x0, LabelFinal2x1, LabelFinal2x2, LabelFinal2x3 },
                { LabelFinal3x0, LabelFinal3x1, LabelFinal3x2, LabelFinal3x3 },
                { LabelFinal4x0, LabelFinal4x1, LabelFinal4x2, LabelFinal4x3 }
            };
        }

    }
}
