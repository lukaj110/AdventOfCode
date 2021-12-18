List<int> submarines = new List<int>();

using (var reader = new StreamReader("Input.txt"))
{
    foreach (var submarine in reader.ReadLine().Split(","))
    {
        submarines.Add(int.Parse(submarine));
    }
}

//Part 1

submarines.Sort();

int average = submarines[submarines.Count / 2];

int totalFuel = 0;

for (int i = 0; i < submarines.Count; i++)
{
    totalFuel += Math.Abs(average - submarines[i]);
}

Console.WriteLine(totalFuel);

//Part 2

List<int> fuelList = new List<int>();
List<Task> taskList = new List<Task>();

for (int count = 0; count < 2000; count++)
{
    taskList.Add(Task.Factory.StartNew((state) =>
    {
        int totalFuel = 0;
        for (int i = 0; i < submarines.Count; i++)
        {
            for (int j = 1; j <= Math.Abs((int)state - submarines[i]); j++)
            {
                totalFuel += j;
            }
        }
        fuelList.Add(totalFuel);
    }, count));
}

taskList.ForEach(e => e.Wait());

Console.WriteLine(fuelList.Min());