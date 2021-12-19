using AdventOfCode_Day11;

List<string> input = new List<string>();

using (var reader = new StreamReader("Input.txt"))
{
    while (!reader.EndOfStream)
    {
        input.Add(reader.ReadLine());
    }
}

//Part 1

OctopusBoard octopusBoard = new OctopusBoard();

for (int i = 0; i < input.Count; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        octopusBoard.AddValue(i, j, int.Parse(input[i][j].ToString()));
    }
}

for (int i = 1; i <= 100; i++)
{
    octopusBoard.IncreaseAll(1);

    for (int j = 0; j < octopusBoard.Board.GetLength(0); j++)
    {
        for (int k = 0; k < octopusBoard.Board.GetLength(1); k++)
        {
            if (octopusBoard.Board[j, k].Value > 9 && !octopusBoard.Board[j, k].Flashed)
            {
                octopusBoard.Flash(j, k);
            }
        }
    }

    octopusBoard.ClearFlashes();
}

Console.WriteLine(octopusBoard.FlashCounter);

//Part 1

OctopusBoard octopusBoard2 = new OctopusBoard();

for (int i = 0; i < input.Count; i++)
{
    for (int j = 0; j < input[i].Length; j++)
    {
        octopusBoard2.AddValue(i, j, int.Parse(input[i][j].ToString()));
    }
}

int step = 1;
bool allFlashed = false;

while (!allFlashed)
{
    octopusBoard2.IncreaseAll(1);

    for (int i = 0; i < octopusBoard2.Board.GetLength(0); i++)
    {
        for (int j = 0; j < octopusBoard2.Board.GetLength(1); j++)
        {
            if (octopusBoard2.Board[i, j].Value > 9 && !octopusBoard2.Board[i, j].Flashed)
            {
                octopusBoard2.Flash(i, j);
            }
        }
    }

    if (octopusBoard2.Board.Cast<Octopus>().All(e => e.Flashed)) allFlashed = true;
    else
    {
        octopusBoard2.ClearFlashes();
        step++;
    }
}

Console.WriteLine(step);