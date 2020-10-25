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
        public List<MatrixNode> closedList = new List<MatrixNode>();
        public List<MatrixNode> openList = new List<MatrixNode>();
        public string[,] StartMatrix{ get; set; }
        public string[,] FinishMatrix { get; set; }


        public Logic()
        {
            StartMatrix = FileManager.LoadStartMatrix();
            FinishMatrix = FileManager.LoadFinishMatrix();
        }

        /*This is for validate the move*/
        public bool isValidMove(int i,int j) {
            if (i >= 5 || i < 0 || j >= 4 || j < 0)
            {
                return false;
            }
            else {
                return true;
            }
        }

        public int[] getIndex(string[,] matrix) {
            int[] index = new int[2];
            for (int i = 0; i < 4; i++) {
                for (int j = 0; j < 4; j++) {
                    if (matrix.GetValue(i, j).ToString() == "X") {
                        index[0]=i;
                        index[1]=j;
                        break;
                    }
                }
            }
            return index;
        }

        /* A* algorithm */
        public void aStartSearch() {
            if (this.openList.Count == 0) {
                MatrixNode newNode = new MatrixNode(StartMatrix);
                newNode.Sucesors.Add(newNode);
                AddChildren(newNode);
            }
            bool found = false;
            MatrixNode minNode = null;
            while (!found) {
                minNode = CalculateMin();
                if (minNode.calculateHeuristFunction() == 0)
                {
                    found = true;
                }
                else
                {
                    int indexR = IndexToRemove(minNode.Id);
                    if (indexR == -1)
                    {
                        Console.WriteLine("Hay un error");
                        Console.WriteLine(string.Join(" ", minNode.Matrix));
                    }
                    else
                    {
                        openList.RemoveAt(indexR);
                        AddChildren(minNode);
                    }
                }
            }
            Console.WriteLine("Hay un error");
        }

        public MatrixNode CalculateMin() {
            double minValue = 999;
            MatrixNode minNode=null;
            foreach (MatrixNode m in this.openList) {
                double value = m.calculateEvaluationFunction();
                if ( value <= minValue) {
                    minValue = m.calculateEvaluationFunction();
                    minNode = m; 
                }
            }
            return minNode;
        }

        public int IndexToRemove(int id) {
            for (int i = 0; i < this.openList.Count; i++) {
                if (this.openList[i].Id == id) {
                    return i;
                }
            }
            return -1;
        }

        public void AddChildren(MatrixNode node) {

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    Movable aux = MovesFactory.GetInstance(i, j);
                    if (aux.IsValid(node.Matrix))
                    {
                        int[] index = getIndex(aux.Move(node.Matrix));
                        if (isUniquePosition(node, index[0], index[1]))
                        {
                            MatrixNode child = new MatrixNode(aux.Move(node.Matrix));
                            child.Sucesors.AddRange(node.Sucesors);
                            child.Sucesors.Add(child);
                            child.Moves.AddRange(node.Moves);
                            child.Moves.Add(aux);
                            openList.Add(child);
                        }
                    }
                }
            }
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

        public string[,] MoveTokens(string[,] matrix, string direction) {
            int[] index = getIndex(matrix);
            if (direction == "up")
            {
                string temp1 = matrix.GetValue(index[0] - 1, index[1]).ToString();
                matrix.SetValue("X", index[0] - 1, index[1]); matrix.SetValue(temp1, index[0], index[1]);
                return matrix;
            }
            else if (direction == "down")
            {
                string temp1 = matrix.GetValue(index[0] + 1, index[1]).ToString();
                matrix.SetValue("X", index[0] + 1, index[1]); matrix.SetValue(temp1, index[0], index[1]);
                return matrix;
            }
            else if (direction == "right")
            {
                string temp1 = matrix.GetValue(index[0], index[1] + 1).ToString();
                matrix.SetValue("X", index[0], index[1] + 1); matrix.SetValue(temp1, index[0], index[1]);
                return matrix;
            }
            else {
                string temp1 = matrix.GetValue(index[0], index[1] - 1).ToString();
                matrix.SetValue("X", index[0], index[1] - 1); matrix.SetValue(temp1, index[0], index[1]);
                return matrix;
            }
        }

    }
}
