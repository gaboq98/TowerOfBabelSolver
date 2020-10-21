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
        MatrixNode node;
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
                this.node = new MatrixNode(StartMatrix);
                closedList.Add(node);
                int[] index = getIndex(StartMatrix);
                AddChildren(node.Id, node.Matrix, index,this.node);
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
                if (m.calculateEvaluationFunction() <= minValue) {
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
            if (isUniquePosition(node, index[0] - 1, index[1])){
                updatedMatrix = MoveTokens(matrix, "up");
                this.node = new MatrixNode(updatedMatrix);
                List<MatrixNode> list = node.Sucesors;
                foreach (MatrixNode m in list) { 
                    this.node.Sucesors.Add(m);    
                }
                this.node.Sucesors.Add(this.node);
                this.openList.Add(this.node);
            }
            //DOWN => i+1 , j
            if (isUniquePosition(node, index[0] + 1, index[1]))
            {
                updatedMatrix = MoveTokens(matrix, "down");
                this.node = new MatrixNode(updatedMatrix);
                List<MatrixNode> list = node.Sucesors;
                foreach (MatrixNode m in list)
                {
                    this.node.Sucesors.Add(m);
                }
                this.node.Sucesors.Add(this.node);
                this.openList.Add(this.node);
            }
            //RIGHT => i, j+1
            if (isUniquePosition(node, index[0], index[1]+1))
            {
                updatedMatrix = MoveTokens(matrix, "right");
                this.node = new MatrixNode(updatedMatrix);
                List<MatrixNode> list = node.Sucesors;
                foreach (MatrixNode m in list)
                {
                    this.node.Sucesors.Add(m);
                }
                this.node.Sucesors.Add(this.node);
                this.openList.Add(this.node);
            }
            //LEFT => i, j-1
            if (isUniquePosition(node, index[0], index[1] - 1))
            {
                updatedMatrix = MoveTokens(matrix, "left");
                this.node = new MatrixNode(updatedMatrix);
                List<MatrixNode> list = node.Sucesors;
                foreach (MatrixNode m in list)
                {
                    this.node.Sucesors.Add(m);
                }
                this.node.Sucesors.Add(this.node);
                this.openList.Add(this.node);
            }
        }

        public bool isUniquePosition(MatrixNode node, int i, int j) {
            if (i >= 5) {
                i = 4;
            }
            if (i < 0) {
                i = 0;
            }
            if (j >= 4) {
                j = 3;
            }
            if (j < 0) {
                j = 0;
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
                if (index[0] - 1 < 0)
                {
                    string temp1 = matrix.GetValue(1, index[1]).ToString(); string temp2 = matrix.GetValue(2, index[1]).ToString();
                    string temp3 = matrix.GetValue(3, index[1]).ToString(); string temp4 = matrix.GetValue(4, index[1]).ToString();
                    matrix.SetValue(temp1, 0, index[1]); matrix.SetValue(temp2, 1, index[1]);
                    matrix.SetValue(temp3, 2, index[1]); matrix.SetValue(temp4, 3, index[1]);
                    matrix.SetValue("X", 4, index[1]);
                    return matrix;
                }
                else
                {
                    string temp1 = matrix.GetValue(index[0] - 1, index[1]).ToString();
                    matrix.SetValue("X", index[0] - 1, index[1]); matrix.SetValue(temp1, index[0], index[1]);
                    return matrix;
                }
            }
            else if (direction == "down")
            {
                if (index[0] + 1 >= 5)
                {
                    string temp1 = matrix.GetValue(0, index[1]).ToString(); string temp2 = matrix.GetValue(1, index[1]).ToString();
                    string temp3 = matrix.GetValue(2, index[1]).ToString(); string temp4 = matrix.GetValue(3, index[1]).ToString();
                    matrix.SetValue(temp1, 0, index[1]); matrix.SetValue(temp2, 1, index[1]);
                    matrix.SetValue(temp3, 2, index[1]); matrix.SetValue(temp4, 3, index[1]);
                    matrix.SetValue("X", 4, index[1]);
                    return matrix;
                }
                else
                {
                    string temp1 = matrix.GetValue(index[0] + 1, index[1]).ToString();
                    matrix.SetValue("X", index[0] + 1, index[1]); matrix.SetValue(temp1, index[0], index[1]);
                    return matrix;
                }
            }
            else if (direction == "right")
            {
                if (index[1] + 1 >= 4)
                {
                    string temp1 = matrix.GetValue(index[0], 0).ToString(); string temp2 = matrix.GetValue(index[0], 1).ToString();
                    string temp3 = matrix.GetValue(index[0], 2).ToString();
                    matrix.SetValue("X", index[0], 0); matrix.SetValue(temp1, index[0], 1);
                    matrix.SetValue(temp2, index[0], 2); matrix.SetValue(temp3, index[0], 3);
                    return matrix;
                }
                else
                {
                    string temp1 = matrix.GetValue(index[0], index[1] + 1).ToString();
                    matrix.SetValue("X", index[0], index[1] + 1); matrix.SetValue(temp1, index[0], index[1]);
                    return matrix;
                }
            }
            else {
                if (index[1] - 1 < 0)
                {
                    string temp1 = matrix.GetValue(index[0], 1).ToString(); string temp2 = matrix.GetValue(index[0], 2).ToString();
                    string temp3 = matrix.GetValue(index[0], 3).ToString();
                    matrix.SetValue(temp1, index[0], 0); matrix.SetValue(temp2, index[0], 1);
                    matrix.SetValue(temp3, index[0], 2); matrix.SetValue("X", index[0], 3);
                    return matrix;
                }
                else
                {
                    string temp1 = matrix.GetValue(index[0], index[1] - 1).ToString();
                    matrix.SetValue("X", index[0], index[1] - 1); matrix.SetValue(temp1, index[0], index[1]);
                    return matrix;
                }
            }
        }

    }
}
