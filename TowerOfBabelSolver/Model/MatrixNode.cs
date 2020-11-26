using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerOfBabelSolver.Model.Movements;

namespace TowerOfBabelSolver.Model
{
    class MatrixNode
    {
        private string[,] matrix;
        private List<MatrixNode> sucesors = new List<MatrixNode>();
        private int id;
        private string[,] finishMatrix { get; set; }
        public string[,] Matrix { get=> matrix; set=>matrix=value ; }
        public List<MatrixNode> Sucesors { get => sucesors; set => sucesors = value; }
        public int Id { get => id; set => id = value; }
        Random rnd = new Random();
        public List<Movable> Moves { get; set; }
        public double Value { get; set; }
        public double HeuristValue { get; set; }

        public MatrixNode(string[,] matrix, string[,] finish) {
            finishMatrix = finish;
            this.matrix = matrix;
            this.id = generateId();
            Moves = new List<Movable>();
        }

        public string GetString()
        {
            string text = "";
            for(int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    text += (matrix[i,j] + ", ") ; 
                }
                text += "\n";
            }
            return text;
        }

        /**/
        public double calculateEvaluationFunction() {
            double h = CalculateHeuristFunction();
            int g = calculateCost();
            double f = g + h;
            Value = f;
            return f;
        }

        public int generateId() {
            return rnd.Next(1,985655652);
        }

        public int calculateCost() {
            return this.sucesors.Count - 1;
        }


        public double CalculateHeuristFunction()
        {
            double count = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    string initialValue = matrix.GetValue(i, j).ToString();
                    List<int[]> points = GetIndexBySimbol(finishMatrix, initialValue);
                    switch(points.Count)
                    {
                        case 1:
                            count += Pitagoras(i, j, points[0][0], points[0][1]);
                            break;
                        case 3:
                            count += Math.Min(Pitagoras(i, j, points[0][0], points[0][1]),
                                Math.Min(Pitagoras(i, j, points[1][0], points[1][1]), Pitagoras(i, j, points[2][0], points[2][1])));
                            break;
                        case 4:
                            count += Math.Min(Pitagoras(i, j, points[0][0], points[0][1]),
                                Math.Min(Pitagoras(i, j, points[1][0], points[1][1]),
                                Math.Min(Pitagoras(i, j, points[2][0], points[2][1]), Pitagoras(i, j, points[3][0], points[3][1]))));
                            break;
                    }
                }
            }
            HeuristValue = count;
            return count;
        }

        private List<int[]> GetIndexBySimbol(string[,] matrix, string simbol)
        {
            List<int[]> list = new List<int[]>();
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrix.GetValue(i, j).ToString() == simbol)
                    {
                        list.Add(new int[2] { i, j });
                    }
                }
            }
            return list;
        }

        private double Pitagoras(int a1, int a2, int b1, int b2)
        {
            return Math.Sqrt( Math.Pow((a1-b1), 2) + Math.Pow((a2 - b2), 2) );
        }

        /*
        public int calculateHeuristFunction() {
            int count = 0;
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    string objectiveValue = finishMatrix.GetValue(i, j).ToString();
                    string initialValue = this.matrix.GetValue(i, j).ToString();
                    if (initialValue != objectiveValue) {
                        count++;
                    }
                }
            }
            HeuristValue = count;
            return count;
        }
        */
    }
}
