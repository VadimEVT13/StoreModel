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

namespace StoreModel.View
{
    /// <summary>
    /// Логика взаимодействия для PlotExample.xaml
    /// </summary>
    public partial class PlotExample : UserControl
    {
        List<Point> points = new List<Point>();
        public IEnumerable<Point> Points
        {
            get { return points; }
        }
        List<Point> points2 = new List<Point>();
        public IEnumerable<Point> Points2
        {
            get { return points2; }
        }

        public PlotExample()
        {
            InitializeComponent();
            DataContext = this;

            points.Add(new Point { X = 0, Y = 4 });
            points.Add(new Point { X = 1, Y = 6 });
            points.Add(new Point { X = 1.5, Y = 8 });

            points2.Add(new Point { X = 0, Y = 3 });
            points2.Add(new Point { X = 0.5, Y = 1 });
            points2.Add(new Point { X = 1, Y = 5 });
        }
    }
}
