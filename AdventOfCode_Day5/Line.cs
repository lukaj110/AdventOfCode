using System;
using System.Collections;
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
        public float Slope => (To.Y - From.Y) / (To.X - From.X);

        public Line(Point Point1, Point Point2)
        {
            From = new Vector2(Point1.X, Point1.Y);
            To = new Vector2(Point2.X, Point2.Y);
        }

        public IEnumerable<Point> GetPointsBetweenHorizontalVertical()
        {
            if (From.X == To.X)
            {
                if (From.Y > To.Y) for (int i = (int)To.Y; i <= From.Y; i++) yield return new Point((int)From.X, i);
                else for (int i = (int)From.Y; i <= To.Y; i++) yield return new Point((int)From.X, i);
            }
            else if (From.Y == To.Y)
            {
                if (From.X > To.X) for (int i = (int)To.X; i <= From.X; i++) yield return new Point(i, (int)From.Y);
                else for (int i = (int)From.X; i <= To.X; i++) yield return new Point(i, (int)From.Y);
            }
        }

        public IEnumerable<Point> GetPointsBetweenDiagonal()
        {
            if (Math.Abs(Slope) == 1)
            {
                if (From.X < To.X && From.Y < To.Y)
                {
                    for (int i = 0; i <= To.X - From.X; i++)
                    {
                        yield return new Point((int)From.X + i, (int)From.Y + i);
                    }
                }
                else if (From.X > To.X && From.Y > To.Y)
                {
                    for (int i = 0; i <= From.X - To.X; i++)
                    {
                        yield return new Point((int)From.X - i, (int)From.Y - i);
                    }
                }
                if (From.X > To.X && From.Y < To.Y)
                {
                    for (int i = 0; i <= From.X - To.X; i++)
                    {
                        yield return new Point((int)From.X - i, (int)From.Y + i);
                    }
                }
                else if (From.X < To.X && From.Y > To.Y)
                {
                    for (int i = 0; i <= To.X - From.X; i++)
                    {
                        yield return new Point((int)From.X + i, (int)From.Y - i);
                    }
                }
            }
        }
    }
}