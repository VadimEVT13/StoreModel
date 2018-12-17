using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StoreModel.Controller
{
    static class RecordWriter
    {
        static public void Write(List<Point> Record_List, DateTime date, string path)
        {
            using (StreamWriter sw = new StreamWriter(new FileStream(path, FileMode.Create, FileAccess.Write)))
            {
                foreach (Point p in Record_List)
                {
                    if (p.Y != 0)
                    {
                        sw.WriteLine("Дата поставки = " + date.AddDays(p.X) +
                            "; Количество товара = " + p.Y);
                    }
                }
                sw.Close();
            }
        }
    }
}
