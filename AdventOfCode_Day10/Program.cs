List<string> input = new List<string>();

using (var reader = new StreamReader("Input.txt"))
{
    while (!reader.EndOfStream)
    {
        input.Add(reader.ReadLine());
    }
}

//Part 1

List<char> openList = new List<char>();
List<char> closeList = new List<char>();

Dictionary<char, int> penalties = new Dictionary<char, int>()
{
    { ')', 3 },
    { ']', 57 },
    { '}', 1197 },
    { '>', 25137 },
};

Dictionary<char, char> closingPair = new Dictionary<char, char>()
{
    { '(', ')' },
    { '[', ']' },
    { '{', '}' },
    { '<', '>' },
};

int totalErrorScore = 0;

foreach (string line in input)
{
    openList.Clear();
    closeList.Clear();
    foreach (char c in line)
    {
        if (c == '(' || c == '[' || c == '{' || c == '<')
        {
            openList.Add(c);
            closeList.Insert(0, closingPair[c]);
        }
        else
        {
            if (closeList[0] == c)
            {
                closeList.RemoveAt(0);
            }
            else
            {
                totalErrorScore += penalties[c];
                break;
            }
        }
    }
}

Console.WriteLine(totalErrorScore);

//Part 2

Dictionary<char, int> autoCompleteBonus = new Dictionary<char, int>()
{
    { ')', 1 },
    { ']', 2 },
    { '}', 3 },
    { '>', 4 },
};

List<long> totalPointsList = new List<long>();

foreach (string line in input)
{
    openList.Clear();
    closeList.Clear();

    bool hadError = false;

    foreach (char c in line)
    {
        if (c == '(' || c == '[' || c == '{' || c == '<')
        {
            openList.Add(c);
            closeList.Insert(0, closingPair[c]);
        }
        else
        {
            if (closeList[0] == c)
            {
                closeList.RemoveAt(0);
            }
            else
            {
                hadError = true;
                break;
            }
        }
    }

    if (!hadError)
    {
        long totalPoints = 0;

        foreach (char c in closeList)
        {
            totalPoints *= 5;
            totalPoints += autoCompleteBonus[c];
        }

        totalPointsList.Add(totalPoints);
    }
}

totalPointsList.Sort();

Console.WriteLine(totalPointsList[totalPointsList.Count / 2]);