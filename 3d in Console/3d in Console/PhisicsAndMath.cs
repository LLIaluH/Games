using System;
using System.Collections.Generic;
using System.Text;

namespace _3d_in_Console
{
    class PhisicsAndMath
    {
        public static double GetHypotenuseLength(Point p1, Point p2)
        {
            //return Math.Sqrt(Math.Abs(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2) + Math.Pow(p2.Z - p1.Z, 2)));
            return Math.Sqrt(Math.Pow(p2.X - p1.X, 2) + Math.Pow(p2.Y - p1.Y, 2) + Math.Pow(p2.Z - p1.Z, 2));
        }

        public static Vector3 MultyplyMatrixOnVector(Vector3 v, double[,] matrix)
        {
            //Vector3 resV = new Vector3();

            int rows = matrix.GetUpperBound(0) + 1;
            int columns = matrix.Length / rows;
            if (rows != 3 || columns != 3)
            {
                throw new Exception("Malformed matrix");
            }
            double x = 0;
            double y = 0;
            double z = 0;
            double[] arr = new double[3] { v.B.X, v.B.Y, v.B.Z };
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    for (int q = 0; q < 3; q++)
                    {
                        switch (i)
                        {
                            case 0:
                                x += matrix[i, j] * arr[j];
                                break;
                            case 1:
                                y += matrix[i, j] * arr[j];
                                break;
                            case 2:
                                z += matrix[i, j] * arr[j];
                                break;
                        }
                    }
                }
            }
            v.B.X = (float)(x * Consts.OneOfTree);//я понять не могу, почему надо делить на три, где я ошибся???
            v.B.Y = (float)(y * Consts.OneOfTree);
            v.B.Z = (float)(z * Consts.OneOfTree);
            return new Vector3(new Point(x, y, z));
        }
    }
}
