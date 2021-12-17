long[] lanternFishPool = new long[9];

using (var reader = new StreamReader("Input.txt"))
{
    foreach (var lanternfish in reader.ReadLine().Split(","))
    {
        lanternFishPool[int.Parse(lanternfish)]++;
    }
}

//Part 1

void MoveForDays(int Days)
{
    for (int count = 0; count < Days; count++)
    {
        long newFish = lanternFishPool[0];
        lanternFishPool[0] = lanternFishPool[1];
        lanternFishPool[1] = lanternFishPool[2];
        lanternFishPool[2] = lanternFishPool[3];
        lanternFishPool[3] = lanternFishPool[4];
        lanternFishPool[4] = lanternFishPool[5];
        lanternFishPool[5] = lanternFishPool[6];
        lanternFishPool[6] = lanternFishPool[7];
        lanternFishPool[7] = lanternFishPool[8];
        lanternFishPool[8] = newFish;
        lanternFishPool[6] += newFish;
    }
}

MoveForDays(80);

long result = 0;

foreach(long lanternFishCount in lanternFishPool)
{
    result += lanternFishCount;
}

Console.WriteLine(result);

//Part 2

MoveForDays(256 - 80);

result = 0;

foreach (long lanternFishCount in lanternFishPool)
{
    result += lanternFishCount;
}

Console.WriteLine(result);