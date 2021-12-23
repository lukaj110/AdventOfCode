List<string> input = File.ReadAllLines("Input.txt").ToList();

List<Tuple<int, int>> dots = input.Where(e => e.Length > 0 && !e.StartsWith("fold"))
                                  .Select(e =>
                                  {
                                      var split = e.Split(',');
                                      return Tuple.Create(int.Parse(split[0]), int.Parse(split[1]));
                                  }).ToList();

List<string> foldInstructions = input.Where(e => e.Length > 0 && e.StartsWith("fold")).ToList();

//Part 1

bool[,] paper = new bool[dots.Max(e => e.Item2) + 1, dots.Max(e => e.Item1) + 1];

foreach (var dot in dots)
{
    paper[dot.Item2, dot.Item1] = true;
}

Fold(true, 7);
PrintPaper();

void Fold(bool foldUp, int pos)
{
    int ySize = paper.GetLength(0);
    int xSize = paper.GetLength(1);

    if (foldUp)
    {
        bool[,] tempPaper = new bool[xSize, pos - 1];

        for(int i = 0; i < tempPaper.GetLength(1); i++)
        {
            for(int j = 0; j < tempPaper.GetLength(0); j++) 
            {
                tempPaper[tempPaper.GetLength(0) - j - 1, i] = paper[pos + j - 1, i];
            }
        }

    }
    else
    {
        int foldPos = xSize - pos - 1;

        bool[,] tempPaper = new bool[ySize, foldPos];
    }
}

void PrintPaper()
{
    for (int i = 0; i < paper.GetLength(1); i++)
    {
        for (int j = 0; j < paper.GetLength(0); j++)
        {
            Console.Write(paper[j, i] ? '#' : '.');
        }
        Console.Write(Environment.NewLine);
    }
}