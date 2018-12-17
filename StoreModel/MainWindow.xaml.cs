using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;
using StoreModel.Controller;
using StoreModel.MathModel;

namespace StoreModel
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Point> rez = new List<Point>();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            Model m = new Model(10, 15, 30);

            LineSeries1.ItemsSource = m.Dlist;
            LineSeries2.ItemsSource = m.Ilist;

            rez = m.SolveSystem(0, 29, 10, 20, 3000);

            double[] t = new double[rez.Count()];
            int index = 0;

            Col_dostav_label.Content = "Количество доставок: " + (t.Count() - 1).ToString();

            foreach (Point r in rez)
            {
                t[index] = r.X;
                index++;
            }

            LineSeries3.ItemsSource = m.Otrisovka(t);

            K_input.Text = 10.ToString();
            N_input.Text = 30.ToString();
            Ch_input.Text = 20.ToString();
            C0_input.Text = 3000.ToString();
            Date_input.Text = DateTime.Now.ToString();

            // Работает пример
            // ModelExample.Solve(new Point(0.4, -0.75));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            double k = 10;
            double n = 30;
            double ch = 20;
            double c0 = 3000;

            if (
                double.TryParse(K_input.Text, out k) &
                double.TryParse(N_input.Text, out n) & 
                n > 0 &
                double.TryParse(Ch_input.Text, out ch) &
                ch > 0 &
                double.TryParse(C0_input.Text, out c0)
            )
            {
                Model m = new Model(k, 15, n);

                LineSeries1.ItemsSource = m.Dlist;
                LineSeries2.ItemsSource = m.Ilist;

                rez = m.SolveSystem(0, n - 1, 10, ch, c0);

                double[] t = new double[rez.Count()];
                int index = 0;

                Col_dostav_label.Content = "Количество доставок: " + (t.Count() - 1).ToString();

                foreach (Point r in rez)
                {
                    t[index] = r.X;
                    index++;
                }

                LineSeries3.ItemsSource = m.Otrisovka(t);
            }
            else
            {
                string str = "";
                if (!double.TryParse(K_input.Text, out k))
                    str += " - коэффициент k";
                if (!double.TryParse(N_input.Text, out n))
                    str += " - числа поставок";
                if (!double.TryParse(Ch_input.Text, out ch))
                    str += " - стоимости хранения";
                if (!double.TryParse(C0_input.Text, out c0))
                    str += " - стоимости перевозок";

                MessageBox.Show("Возникла ошибка при парсинге: " + str);

                Col_dostav_label.Content = "Количество доставок: ";
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            if (!DateTime.TryParse(Date_input.Text, out date))
            {
                MessageBox.Show("Возникла ошибка при парсинге: даты");
                return;
            }

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            if (saveFileDialog.ShowDialog() == true)
            {
                RecordWriter.Write(rez, date, saveFileDialog.FileName);
            }
        }
    }
}
