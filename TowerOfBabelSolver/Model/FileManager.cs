﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TowerOfBabelSolver.Model
{
    class FileManager
    {
        public FileManager()
        {
        }

        public static string[,] LoadStartMatrix()
        {
            // Read the file as one string.
            //string text = System.IO.File.ReadAllText(@"C:\Users\Jean Paul\Desktop\EstadoInicial.txt");
            string text = System.IO.File.ReadAllText(@"D:\Usuarios\gaboq\Escritorio\Gabo\TEC\IA\TowerOfBabelSolver\EstadoInicial2.txt");

            // Display the file contents to the console. Variable text is a string.
            string[] sep = { "\n", "\t", ",", "\r", " " };
            string[] stringArray = text.Split(sep , 20, StringSplitOptions.RemoveEmptyEntries);

            return new string[,] {  { stringArray[0], stringArray[1], stringArray[2], stringArray[3], },
                                    { stringArray[4], stringArray[5], stringArray[6], stringArray[7], },
                                    { stringArray[8], stringArray[9], stringArray[10], stringArray[11], },
                                    { stringArray[12], stringArray[13], stringArray[14], stringArray[15], }};
        }

        public static string[,] LoadStartMatrix(string path)
        {
            // Read the file as one string.
            string text = System.IO.File.ReadAllText(path);

            // Display the file contents to the console. Variable text is a string.
            string[] sep = { "\n", "\t", ",", "\r", " " };
            string[] stringArray = text.Split(sep, 20, StringSplitOptions.RemoveEmptyEntries);

            return new string[,] {  { stringArray[0], stringArray[1], stringArray[2], stringArray[3], },
                                    { stringArray[4], stringArray[5], stringArray[6], stringArray[7], },
                                    { stringArray[8], stringArray[9], stringArray[10], stringArray[11], },
                                    { stringArray[12], stringArray[13], stringArray[14], stringArray[15], }};
        }

        public static string[,] LoadFinishMatrix()
        {
            // Read the file as one string.
            //string text = System.IO.File.ReadAllText(@"C:\Users\Jean Paul\Desktop\EstadoFinal.txt");
            string text = System.IO.File.ReadAllText(@"D:\Usuarios\gaboq\Escritorio\Gabo\TEC\IA\TowerOfBabelSolver\EstadoFinal.txt");

            // Display the file contents to the console. Variable text is a string.
            string[] sep = { "\n", "\t", ",", "\r" };
            string[] stringArray = text.Split(sep, 20, StringSplitOptions.RemoveEmptyEntries);

            return new string[,] {  { stringArray[0], stringArray[1], stringArray[2], stringArray[3], },
                                    { stringArray[4], stringArray[5], stringArray[6], stringArray[7], },
                                    { stringArray[8], stringArray[9], stringArray[10], stringArray[11], },
                                    { stringArray[12], stringArray[13], stringArray[14], stringArray[15], } };
        }

        public static string[,] LoadFinishMatrix(string path)
        {
            // Read the file as one string.
            string text = System.IO.File.ReadAllText(path);

            // Display the file contents to the console. Variable text is a string.
            string[] sep = { "\n", "\t", ",", "\r" };
            string[] stringArray = text.Split(sep, 20, StringSplitOptions.RemoveEmptyEntries);

            return new string[,] {  { stringArray[0], stringArray[1], stringArray[2], stringArray[3], },
                                    { stringArray[4], stringArray[5], stringArray[6], stringArray[7], },
                                    { stringArray[8], stringArray[9], stringArray[10], stringArray[11], },
                                    { stringArray[12], stringArray[13], stringArray[14], stringArray[15], } };
        }

    }
}
