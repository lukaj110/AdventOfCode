List<string> input = new List<string>();

using (var reader = new StreamReader("Input.txt"))
{
    while (!reader.EndOfStream) input.Add(reader.ReadLine());
}

//Part 1

int horizontalSum = input.Where(e => e.Split(' ')[0] == "forward").Sum(e => int.Parse(e.Split(' ')[1]));
int depthSum = input.Where(e => e.Split(' ')[0] == "down").Sum(e => int.Parse(e.Split(' ')[1]));
int upSum = input.Where(e => e.Split(' ')[0] == "up").Sum(e => int.Parse(e.Split(' ')[1]));

int result = horizontalSum * (depthSum - upSum);

Console.WriteLine(result);

//Part 2

horizontalSum = 0;
depthSum = 0;
int aim = 0;

foreach(string inputItem in input)
{
    string[] inputSplit = inputItem.Split(' ');
    int inputNum = int.Parse(inputSplit[1]);
    if (inputSplit[0] == "forward")
    {
        horizontalSum += inputNum;
        depthSum += aim * inputNum;
    }
    else if (inputSplit[0] == "down") aim += inputNum;
    else if (inputSplit[0] == "up") aim -= inputNum;
}

result = horizontalSum * depthSum;

Console.WriteLine(result);