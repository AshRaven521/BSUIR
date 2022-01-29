using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwp_test.PointCluster
{
    public class Point
    {
        public Point(double x, double y, int id)
        {
            X = x;
            Y = y;
            ID = id;
        }

        public double X { get; set; }
        public double Y { get; set; }
        public double ID { get; set; }
    }
}
