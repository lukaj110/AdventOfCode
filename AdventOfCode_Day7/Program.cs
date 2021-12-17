List<int> submarines = new List<int>();

using (var reader = new StreamReader("Input.txt"))
{
    foreach (var submarine in reader.ReadLine().Split(","))
    {
        submarines.Add(int.Parse(submarine));
}
}
submarines.Sort();

int closest = submarines.Aggregate((x, y) => Math.Abs(x - submarines.Average()) < Math.Abs(y - submarines.Average()) ? x : y);

List<int> positions = new List<int>();

for(int x = 0; x < 2000; x++)
{
    int totalFuel = 0;

    for (int i = 0; i < submarines.Count; i++)
    {
        totalFuel += x > submarines[i] ? x - submarines[i] : submarines[i] - x;
    }

    positions.Add(totalFuel);

    Console.WriteLine($"Position {x}: {totalFuel}");
}

Console.WriteLine(positions.Min());