using AdventOfCode_Day5;
using System.Drawing;
using System.Numerics;

List<Line> lines = new List<Line>();

using (var reader = new StreamReader("Input.txt"))
{
    while (!reader.EndOfStream)
    {
        string[] points = reader.ReadLine().Split(" -> ");
        string[] point1 = points[0].Split(",");
        string[] point2 = points[1].Split(",");

        lines.Add(new Line(new Point(int.Parse(point1[0]), int.Parse(point1[1])), new Point(int.Parse(point2[0]), int.Parse(point2[1]))));
    }
}

//Part 1

int[,] Board = new int[1000, 1000];

foreach (Line line in lines)
{
    foreach (Point point in line.GetPointsBetweenHorizontalVertical())
    {
        Board[point.X, point.Y] = Board[point.X, point.Y] + 1;
    }
}

int result = Board.Cast<int>().Where(e => e >= 2).Count();

Console.WriteLine(result);

//Part 2

foreach (Line line in lines)
{
    foreach (Point point in line.GetPointsBetweenDiagonal())
    {
        Board[point.X, point.Y] = Board[point.X, point.Y] + 1;
    }
}

result = Board.Cast<int>().Where(e => e >= 2).Count();

Console.WriteLine(result);