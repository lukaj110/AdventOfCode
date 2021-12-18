using AdventOfCode_Day9;

List<string> input = new List<string>();

using (var reader = new StreamReader("Input.txt"))
{
    while (!reader.EndOfStream)
    {
        input.Add(reader.ReadLine());
    }
}

var board = new Board();

for (int i = 0; i < input.Count; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        board.AddPoint(i, j, int.Parse(input[i][j].ToString()));
    }
}

//Part 1

List<int> lowestValues = new List<int>();

for (int i = 0; i < 100; i++)
{
    for (int j = 0; j < 100; j++)
    {
        if (board.IsLowestAdjacent(i, j))
        {
            lowestValues.Add(board.GetValue(i, j));
        }
    }
}

int result = lowestValues.Sum(e => e + 1);

Console.WriteLine(result);

//Part 2

List<List<Tuple<int, int>>> basinList = new List<List<Tuple<int, int>>>();

for (int i = 0; i < 100; i++)
{
    for (int j = 0; j < 100; j++)
    {
        board.TraverseBasin(i, j, basinList);
    }
}

basinList = basinList.OrderByDescending(e=>e.Count).ToList();

int result2 = basinList[0].Count() * basinList[1].Count() * basinList[2].Count();

Console.WriteLine(result2);