using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model
{
    class Logic
    {
        public List<MatrixNode> closedList = new List<MatrixNode>();
        public List<MatrixNode> openList = new List<MatrixNode>();
        public string[,] StartMatrix{ get; set; }
        public string[,] FinishMatrix { get; set; }

        int indexToRemove = -1;

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
            for (int i = 0; i < 5; i++) {
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
                closedList.Add(newNode);
                int[] index = getIndex(StartMatrix);
                string[,] m = newNode.Matrix;
                AddChildren(newNode.Id,m , index, newNode);
            }
            bool found = false;
            while (!found) {
                MatrixNode minNode = CalculateMin();
                if (minNode.calculateHeuristFunction() == 0)
                {
                    found = true;
                }
                else {
                    int indexR = IndexToRemove(minNode.Id);
                    if (indexR == -1)
                    {
                        Console.WriteLine("Hay un error");
                        Console.WriteLine(string.Join(" ", minNode.Matrix));
                        break;
                    }
                    else
                    {
                        openList.RemoveAt(indexR);
                        int[] index = getIndex(minNode.Matrix);
                        AddChildren(minNode.Id, minNode.Matrix, index, minNode);
                        closedList.Add(minNode);
                    }
                }
            }
        }

        public MatrixNode CalculateMin() {
            double minValue = this.openList[0].calculateEvaluationFunction();
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

        public void AddChildren(int id,string[,] matrix, int[] index, MatrixNode node) {
            string[,] updatedMatrix;
            //UP => i-1 , j 
            string[,] oldMatrix = (string[,])matrix.Clone();
            if (isUniquePosition(node, index[0] - 1, index[1])){
                updatedMatrix = MoveTokens(oldMatrix, "up");
                MatrixNode newNode = new MatrixNode(updatedMatrix);
                List<MatrixNode> list = node.Sucesors;
                foreach (MatrixNode m in list) { 
                    newNode.Sucesors.Add(m);    
                }
                newNode.Sucesors.Add(newNode);
                this.openList.Add(newNode);
            }
            //DOWN => i+1 , j
            string[,] oldMatrix1 = (string[,])matrix.Clone();
            if (isUniquePosition(node, index[0] + 1, index[1]))
            {
                updatedMatrix = MoveTokens(oldMatrix1, "down");
                MatrixNode newNode = new MatrixNode(updatedMatrix);
                List<MatrixNode> list = node.Sucesors;
                foreach (MatrixNode m in list)
                {
                    newNode.Sucesors.Add(m);
                }
                newNode.Sucesors.Add(newNode);
                this.openList.Add(newNode);
            }
            //RIGHT => i, j+1
            string[,] oldMatrix2 = (string[,])matrix.Clone();
            if (isUniquePosition(node, index[0], index[1]+1))
            {
                updatedMatrix = MoveTokens(oldMatrix2, "right");
                MatrixNode newNode = new MatrixNode(updatedMatrix);
                List<MatrixNode> list = node.Sucesors;
                foreach (MatrixNode m in list)
                {
                    newNode.Sucesors.Add(m);
                }
                newNode.Sucesors.Add(newNode);
                this.openList.Add(newNode);
            }
            //LEFT => i, j-1
            string[,] oldMatrix3 = (string[,])matrix.Clone();
            if (isUniquePosition(node, index[0], index[1] - 1))
            {
                updatedMatrix = MoveTokens(oldMatrix3, "left");
                MatrixNode newNode = new MatrixNode(updatedMatrix);
                List<MatrixNode> list = node.Sucesors;
                foreach (MatrixNode m in list)
                {
                    newNode.Sucesors.Add(m);
                }
                newNode.Sucesors.Add(newNode);
                this.openList.Add(newNode);
            }
        }

        public bool isUniquePosition(MatrixNode node, int i, int j) {
            if (i >= 5) {
                i = 4;
                return false;
            }
            if (i < 0) {
                i = 0;
                return false;
            }
            if (j >= 4) {
                j = 3;
                return false;
            }
            if (j < 0) {
                j = 0;
                return false;
            }
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
