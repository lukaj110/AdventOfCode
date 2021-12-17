using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_Day5
{
    public class Line
    {
        public Vector2 From { get; set; }
        public Vector2 To { get; set; }

        public Line(Point Point1, Point Point2)
        {
            From = new Vector2(Point1.X, Point1.Y);
            To = new Vector2(Point2.X, Point2.Y);
        }

        public IEnumerable<Point> GetPointsBetween()
        {
            
        }
    }
}
