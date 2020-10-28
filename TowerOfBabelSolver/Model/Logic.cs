using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TowerOfBabelSolver.Model.Movements;

namespace TowerOfBabelSolver.Model
{
    class Logic
    {
        public List<MatrixNode> openList = new List<MatrixNode>();
        public string[,] StartMatrix{ get; set; }
        public string[,] FinishMatrix { get; set; }

        public Logic(string[,] start, string[,] finish)
        {
            StartMatrix = start;
            FinishMatrix = finish;
        }

        /* A* algorithm */
        public MatrixNode aStartSearch() {
            MatrixNode newNode = new MatrixNode(StartMatrix, FinishMatrix);
            newNode.Sucesors.Add(newNode);
            AddChildren(newNode);
            bool found = false;
            MatrixNode minNode = null;
            while (!found) {
                minNode = openList[0];  // El minimo siempre es el primero: openList[0]
                if (minNode.HeuristValue == 0)
                    found = true;
                else
                {
                    openList.RemoveAt(0);
                    AddChildren(minNode);
                }
            }
            Console.WriteLine("Fin");
            return minNode;
        }

        public void AddChildren(MatrixNode node) {
            string opposite;
            if (node.Moves.Count > 0)
                opposite = Movable.CalcOposite(node.Moves.Last());
            else
                opposite = "";
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Movable aux = MovesFactory.GetInstance(i, j);
                    if (aux.IsValid(node.Matrix))
                    {
                        if ( !opposite.Equals(aux.GetString()) )
                        {
                            string[,] movedMatrix = aux.Move(node.Matrix);
                            MatrixNode child = new MatrixNode(movedMatrix, FinishMatrix);
                            child.Sucesors.AddRange(node.Sucesors);
                            child.Sucesors.Add(child);
                            child.Moves.AddRange(node.Moves);
                            child.Moves.Add(aux);
                            child.calculateEvaluationFunction();
                            AddInOrder(child);
                        }
                    }
                }
            }
        }

        private void AddInOrder(MatrixNode child)
        {
            if (openList.Count == 0)
            {
                openList.Add(child);
                return;
            }
            double value = child.Value;
            if (value > openList[openList.Count-1].Value)
            {
                openList.Add(child);
            }
            for (int i = 0; i < openList.Count; i++)
            {
                
                if ( value < openList[i].Value)
                {
                    openList.Insert(i, child);
                    return;
                }
            }
            openList.Add(child);
        }

        public int[] getIndex(string[,] matrix)
        {
            int[] index = new int[2];
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (matrix.GetValue(i, j).ToString() == "X")
                    {
                        index[0] = i;
                        index[1] = j;
                        break;
                    }
                }
            }
            return index;
        }

        public MatrixNode CalculateMin()
        {
            double minValue = 999;
            MatrixNode minNode = null;
            foreach (MatrixNode m in this.openList)
            {
                double value = 0;//m.calculateEvaluationFunction();
                if (value <= minValue)
                {
                    minValue = value;
                    minNode = m;
                }
            }
            return minNode;
        }

        public int IndexToRemove(int id)
        {
            for (int i = 0; i < this.openList.Count; i++)
            {
                if (this.openList[i].Id == id)
                {
                    return i;
                }
            }
            return -1;
        }

        public bool IsValidMove(Movable move)
        {
            return false;
        }

        public bool isUniquePosition(MatrixNode node, int i, int j) {
            List<MatrixNode> listOfSucesors = node.Sucesors;
            foreach (MatrixNode m in listOfSucesors) {
                if (m.Matrix.GetValue(i, j).ToString() == "X") {
                    return false;
                }
            }
            return true;
        }

    }
}
