int lastVal = 0;
int incrementCount = 0;

using (var reader = new StreamReader("Input.txt"))
{
    lastVal = int.Parse(reader.ReadLine());
    while (!reader.EndOfStream)
    {
        int newVal = int.Parse(reader.ReadLine());

        if (newVal > lastVal) incrementCount++;
        lastVal = newVal;
    }    
}

Console.WriteLine(incrementCount);