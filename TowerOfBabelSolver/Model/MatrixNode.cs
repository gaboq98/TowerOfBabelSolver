using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model
{
    class MatrixNode
    {
        private string[,] matrix;
        private List<int> sucesors = new List<int>();
        private int id=0;
        private string[,] finishMatrix = FileManager.LoadFinishMatrix();
        public string[,] Matrix { get=> matrix; set=>matrix=value ; }
        public List<int> Sucesors { get => sucesors; set => sucesors = value; }
        public int Id { get => id; set => id = value; }

        public MatrixNode(string[,] matrix) {
            this.matrix = matrix;
            this.id += 1;
        }

        public double calculateEvaluationFunction() {
            double h = calculateEvaluationFunction();
            int g = calculateCost();
            double f = g + (1/19)*h;
            return f;
        }

        public int calculateCost() {
            return this.sucesors.Count;
        }

        public int calculateHeuristFunction() {
            int count = 0;
            for (int i = 0; i < 5; i++) {
                for (int j = 0; j < 4; j++) {
                    string objectiveValue = finishMatrix.GetValue(i, j).ToString();
                    string initialValue = this.matrix.GetValue(i, j).ToString();
                    if (initialValue != objectiveValue) {
                        count++;
                    }
                }
            }
            return count;
        }

    }
}
