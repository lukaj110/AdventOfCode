List<string> input = new List<string>();

using (var reader = new StreamReader("Input.txt"))
{
    while (!reader.EndOfStream)
    {
        input.Add(reader.ReadLine());
    }
}

//Part 1

var outputs = input.SelectMany(e => e.Split(" | ")[1].Split(' '));

int result = outputs.Count(e => (e.Length >= 2 && e.Length <= 4) || e.Length == 7);

Console.WriteLine(result);