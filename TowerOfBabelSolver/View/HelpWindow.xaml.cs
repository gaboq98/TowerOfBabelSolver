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
using System.Windows.Shapes;

namespace TowerOfBabelSolver.View
{
    /// <summary>
    /// Lógica de interacción para HelpWindow.xaml
    /// </summary>
    public partial class HelpWindow : Window
    {
        public HelpWindow()
        {
            InitializeComponent();
            ConfigTextBox.Text = "Configuracion del tablero:\n[\nX, R, A, B,\nV, R, A, B,\nV, R, A, B,\nV, R, A, B\n]\n\nV: Bolitas verdes\nR: Bolitas rojas\nA: Bolitas azules\nB: Bolitas blancas\nX: Espacio libre" ;
            MovesTextBox.Text = "Movimientos:\nPara definir un movimiento se toma en cuenta desde la posicion inicial hasta la final del espacio libre, de modo que hay que llevar control de la direccion y la cantidad de espacios a mover. Se escribe de la siguiente forma: { Direccion - Espacios }\nDonde\nDireccion:\nN\nS\nE\nO\ny Espacios:\n1\n2\n3\n4";
        }
    }
}
