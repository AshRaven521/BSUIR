using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwp_test.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Population { get; set; }
    }

    public class BookManager
    {
        public static List<Book> GetBooks(DataTable dt)
        {
            var books = new List<Book>();
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                books.Add(new Book { Id = Convert.ToInt32(dt.Rows[i].ItemArray[0]),
                    Name = dt.Rows[i].ItemArray[1].ToString(), Latitude = dt.Rows[i].ItemArray[2].ToString(),
                    Longitude = dt.Rows[i].ItemArray[3].ToString(), Population = dt.Rows[i].ItemArray[4].ToString()
                });
            }

            return books;
        }
    }

    public class SubCluster
    {
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public string Distance { get; set; }
        public string Time { get; set; }
    }

    public class ClusterManager
    {
        public static List<SubCluster> GetClusters(DataTable dt)
        {
            var books = new List<SubCluster>();
            int count = dt.Rows.Count;
            for (int i = 0; i < count; i++)
            {
                string d = string.Format("{0:F2}", dt.Rows[i].ItemArray[2]);
                string t = string.Format("{0:F2}", dt.Rows[i].ItemArray[3]);
                books.Add(new SubCluster
                {
                    Name1 = dt.Rows[i].ItemArray[0].ToString(),
                    Name2 = dt.Rows[i].ItemArray[1].ToString(),
                    Distance = d,
                    Time = t
                });
            }

            return books;
        }
    }
}
