﻿using System;
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
        private string[,] finishMatrix = FileManager.LoadFinishMatrix();
        public string[,] Matrix { get=> matrix; set=>matrix=value ; }
        public List<MatrixNode> Sucesors { get => sucesors; set => sucesors = value; }
        public int Id { get => id; set => id = value; }
        Random rnd = new Random();

        public List<Movable> Moves { get; set; }


        public MatrixNode(string[,] matrix) {
            this.matrix = matrix;
            this.id = generateId();
            Moves = new List<Movable>();
        }

        /**/
        public double calculateEvaluationFunction() {
            int h = calculateHeuristFunction();
            int g = calculateCost();
            double f = g+(0.06667)*h;
            return f;
        }

        public int generateId() {
            return rnd.Next(1,985655652);
        }

        public int calculateCost() {
            return this.sucesors.Count - 1;
        }

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
            return count;
        }

    }
}
