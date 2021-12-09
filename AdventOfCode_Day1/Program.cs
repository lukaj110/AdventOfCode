List<int> values = new List<int>();

using (var reader = new StreamReader("Input.txt"))
{
    while (!reader.EndOfStream) values.Add(int.Parse(reader.ReadLine()));
}

//Part 1

int count = 0;

for (int i = 0; i < values.Count - 1; i++)
{
    if (values[i] < values[i + 1]) count++;
}

Console.WriteLine(count);

//Part 2

List<int> values2 = new List<int>();

count = 0;

for (int i = 0; i < values.Count - 2; i++) values2.Add(values[i] + values[i + 1] + values[i + 2]);

for (int i = 0; i < values2.Count - 1; i++) if (values2[i] < values2[i + 1]) count++;

Console.WriteLine(count);