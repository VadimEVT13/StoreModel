using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using MathNet.Numerics;
using MathNet.Numerics.LinearAlgebra;

namespace StoreModel.MathModel
{
    class ModelExample
    {
        static double fun1(double x, double y)
        {
            return Math.Sin(2 * x - y) - 1.2 * x - 0.4;
        }

        static double fun2(double x, double y)
        {
            return 0.8 * x * x + 1.5 * y * y - 1;
        }

        static double fun1dx(double x, double y)
        {
            double eps = 0.000000000001;
            return (fun1(x + eps, y) - fun1(x, y)) / eps;
        }

        static double fun1dy(double x, double y)
        {
            double eps = 0.000000000001;
            return (fun1(x, y + eps) - fun1(x, y)) / eps;
        }

        static double fun2dx(double x, double y)
        {
            double eps = 0.000000000001;
            return (fun2(x + eps, y) - fun2(x, y)) / eps;
        }

        static double fun2dy(double x, double y)
        {
            double eps = 0.000000000001;
            return (fun2(x, y + eps) - fun2(x, y)) / eps;
        }

        static public Point Solve(Point p_nachalo)
        {
            double x = p_nachalo.X;
            double y = p_nachalo.Y;

            Vector<double> p = Vector<double>.Build.Dense(new double[] { x, y });

            for (int i = 0; i < 20; i++)
            {
                Vector<double> b = Vector<double>.Build.Dense(new double[] { fun1(p[0],p[1]), fun2(p[0],p[1]) });                
                Matrix<double> A = Matrix<double>.Build.DenseOfArray(new double[,] { 
                    { fun1dx(p[0],p[1]), fun1dy(p[0],p[1]) }, 
                    { fun2dx(p[0],p[1]), fun2dy(p[0],p[1]) } });

                A.Inverse();

                p = p - A.Inverse() * b;
            }


            MessageBox.Show(p[0].ToString());
            MessageBox.Show(p[1].ToString());
            return new Point(p[0], p[1]);
        }
    }
}
