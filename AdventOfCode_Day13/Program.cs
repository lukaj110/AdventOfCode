List<string> input = File.ReadAllLines("Input.txt").ToList();

List<Tuple<int, int>> dots = input.Where(e => e.Length > 0 && !e.StartsWith("fold"))
                                  .Select(e =>
                                  {
                                      var split = e.Split(',');
                                      return Tuple.Create(int.Parse(split[0]), int.Parse(split[1]));
                                  }).ToList();

List<Tuple<bool, int>> foldInstructions = new List<Tuple<bool, int>>();

foreach (var line in input.Where(e => e.Length > 0 && e.StartsWith("fold")).ToList())
{
    var split = line[11..].Split("=");
    foldInstructions.Add(Tuple.Create(split[0][0] == 'x' ? false : true, int.Parse(split[1])));
}

//Part 1

bool[,] paper = new bool[dots.Max(e => e.Item2) + 1, dots.Max(e => e.Item1) + 1];

foreach (var dot in dots)
{
    paper[dot.Item2, dot.Item1] = true;
}

//foreach (var foldInstruction in foldInstructions) Fold(foldInstruction.Item1, foldInstruction.Item2);

var firstInstruction = foldInstructions.First();

Fold(firstInstruction.Item1, firstInstruction.Item2);

Console.WriteLine(paper.Cast<bool>().Count(e => e));

//Part 2

for (int i = 1; i < foldInstructions.Count; i++)
{
    Fold(foldInstructions[i].Item1, foldInstructions[i].Item2);
}

PrintPaper(paper);

void Fold(bool foldUp, int pos)
{
    int ySize = paper.GetLength(0);
    int xSize = paper.GetLength(1);

    if (foldUp)
    {
        //Fold vertical

        for (int i = 0; i < ySize - pos - 1; i++)
        {
            for (int j = 0; j < xSize; j++)
            {
                if(!paper[pos - 1 - i, j])
                {
                    paper[pos - 1 - i, j] = paper[pos + 1 + i, j];
                }
            }
        }

        //Strip rest

        bool[,] tempPaper = new bool[pos, xSize];

        for(int i = 0; i < pos; i++)
        {
            for(int j = 0; j < xSize; j++)
            {
                tempPaper[i, j] = paper[i, j];
            }
        }

        paper = tempPaper;
    }
    else
    {
        //Fold horizontal

        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < xSize - pos - 1; j++)
            {
                if (!paper[i, pos - 1 - j])
                {
                    paper[i, pos - 1 - j] = paper[i, pos + 1 + j];
                }
            }
        }

        //Strip rest

        bool[,] tempPaper = new bool[ySize, pos];

        for (int i = 0; i < ySize; i++)
        {
            for (int j = 0; j < pos; j++)
            {
                tempPaper[i, j] = paper[i, j];
            }
        }

        paper = tempPaper;
    }
}

void PrintPaper(bool[,] Paper)
{
    for (int i = 0; i < Paper.GetLength(0); i++)
    {
        for (int j = 0; j < Paper.GetLength(1); j++)
        {
            Console.Write(Paper[i, j] ? '#' : '.');
        }
        Console.Write(Environment.NewLine);
    }
}