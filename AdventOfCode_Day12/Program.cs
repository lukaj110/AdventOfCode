using AdventOfCode_Day12;

List<string> input = new List<string>();

using (var reader = new StreamReader("Input.txt"))
{
    while (!reader.EndOfStream)
    {
        input.Add(reader.ReadLine());
    }
}

//Part 1

List<Cave> caves = new List<Cave>();

foreach (string line in input.SelectMany(e => e.Split("-")).Distinct())
{
    caves.Add(new Cave()
    {
        BigCave = Char.IsUpper(line[0]),
        Name = line
    }); ;
}

foreach (string[] line in input.Select(e => e.Split("-")))
{
    caves.Where(e => e.Name == line[0]).Single().ConnectedTo.Add(caves.Where(e => e.Name == line[1]).Single());

    caves.Where(e => e.Name == line[1]).Single().ConnectedTo.Add(caves.Where(e => e.Name == line[0]).Single());
}

Cave startNode = caves.Where(e => e.Name == "start").Single();

var paths = startNode.TraverseToEndPart1(new List<Cave>());

Console.WriteLine(paths.Count());

//Part 2