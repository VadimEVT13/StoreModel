using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MathNet.Numerics.Integration;
using System.Windows;
using MathNet.Numerics.LinearAlgebra;

namespace StoreModel.MathModel
{
    class Output_rez
    {
        public double[] T { get; set; }
        public double[] C { get; set; }
        public double SUM { get; set; }
    }

    class Model
    {
        public Model(double A = 10, double B = 15, double Period = 30)
        {
            this.A = A;
            this.B = B;
            this.Period = Period;
        }

        public double A { get; set; }
        public double B { get; set; }
        public double Period { get; set; }
        
        // Для отрисовки D
        List<Point> _dlist = new List<Point>();
        public List<Point> Dlist {
            get
            {
                _dlist = new List<Point>();

                for (int i = 0; i < Period; i++)
                {
                    _dlist.Add(new Point() { X = i + 1, Y = D(i) });
                }

                return _dlist;
            }
        }

        // Для отрисовки I
        List<Point> _ilist = new List<Point>();
        public List<Point> Ilist
        {
            get
            {
                _ilist = new List<Point>();

                for (int i = 0; i < Period; i++)
                {
                    _ilist.Add(new Point() { X = i + 1, Y = I(0, Period - 1, i) });
                }

                return _ilist;
            }
        }

        // Для отрисовки интервалов I
        List<Point> _list_postavok = new List<Point>();
        public List<Point> List_postavok
        {
            get
            {
                _list_postavok = new List<Point>();

                for (int i = 0; i < Period; i++)
                {
                    _list_postavok.Add(new Point() { X = i + 1, Y = I(0, Period - 1, i) });
                }

                return _list_postavok;
            }
        }

        public List<Point> Otrisovka(double[] t)
        {
            List<Point> output = new List<Point>();
            int q = int.Parse(t[0].ToString());

            for (int k = 1; k < t.Count(); k++)
            {
                double t0 = t[k];
                // не факт что t[k] - 1
                for (; q < t[k] - 1; q++)
                {
                    double x = q + 1;
                    double y = I(t0, t[k], q);
                    
                    output.Add(new Point { X = x, Y = y });
                }
                // в точке t[k] отрисовать тоже
                output.Add(new Point { X = t[k] + 1, Y = 0 });
                if (k + 1 != t.Count())
                {
                    output.Add(new Point { X = t[k] + 1, Y = I(t[k], t[k + 1], t[k]) });
                }
                q++;
            }

            return output;
        }

        /// <summary>
        /// Спрос
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        double D(double t) { return A * t + B; }

        /// <summary>
        /// Остаток на складе
        /// </summary>
        /// <param name="lb"></param>
        /// <param name="ub"></param>
        /// <param name="t"></param>
        /// <returns></returns>
        double I(double lb, double ub, double t)
        {
            double rez = SimpsonRule.IntegrateThreePoint(x => D(x), lb, ub) - SimpsonRule.IntegrateThreePoint(x => D(x), lb, t);
            return rez;
        }
                
        // ИСПОЛЬЗУЮ ЭТУ ФУНКЦИЮ
        double Function(double t_beg, double t_end, double t1, double t2)
        {
            // всего осталось к времени t1
            double Ct1 = I(t_beg, t_end, t1);
            // всего осталось к времени t2
            double Ct2 = I(t_beg, t_end, t2);

            // разница между ними или сколько надо от t до t+1
            double razn = Ct1 - Ct2;

            return razn;
        }

        public List<Point> SolveSystem(double t_beg, double t_end, double n, double ch = 100, double co = 20)
        {
            List<Point> output = new List<Point>();
            List<Output_rez> list_rezults = new List<Output_rez>();

            double eps = 1E-6;

            for (int i = 0; i <= n; i++)
            {
                double S = I(t_beg, t_end, t_beg);
                double deltaS = S / (double)(i + 1);

                double[] t = new double[i + 2];
                t[0] = t_beg;
                t[i + 1] = t_end;

                for (int j = 1; j < i + 1; j++)
                {
                    double delta = 1.0 / (n * 1.5) * (t_end - t_beg);

                    t[j] = t[j - 1] + delta;
                    double Scur = Function(t_beg, t_end, t[j - 1], t[j]);

                    double old_t = t[j];
                    double switch_old = 1;
                    while (Math.Abs(deltaS - Scur) > eps)
                    {
                        double switch_new = (deltaS - Scur) / Math.Abs(deltaS - Scur);
                        if (switch_old != switch_new) { delta = delta / 3; }

                        switch_old = switch_new;
                        old_t = old_t + (deltaS - Scur) / Math.Abs(deltaS - Scur) * delta;

                        Scur = Function(t_beg, t_end, t[j - 1], old_t);
                    }

                    t[j] = old_t;
                }

                double sum = 0;
                double[] c_arr = new double[t.Count()];

                for (int q = 0; q < t.Count() - 1; q++)
                {
                    double x = Function(t_beg, t_end, t[q], t[q + 1]);
                    double Ci = ch * x * (t[q+1] - t[q]) / (t[t.Count() - 1] - t[0]) + co;
                    c_arr[q] = Math.Round(x, 0);
                    sum += Ci;
                }

                list_rezults.Add(new Output_rez { T = t, C = c_arr, SUM = sum });
            }
            Output_rez min_rez = list_rezults[0]; 
            foreach (Output_rez rez in list_rezults)
            {
                if (rez.SUM < min_rez.SUM)
                    min_rez = rez;
            }

            for (int q = 0; q < min_rez.T.Count(); q++)
            {
                output.Add(new Point { X = min_rez.T[q], Y = min_rez.C[q] });
            }

            return output;
        }
    }
}
