List<string> input = new List<string>();

using (var reader = new StreamReader("Input.txt"))
{
    while (!reader.EndOfStream)
    {
        input.Add(reader.ReadLine());
    }
}

//Part 1

var allOutputs = input.SelectMany(e => e.Split(" | ")[1].Split(' '));

int result = allOutputs.Count(e => (e.Length >= 2 && e.Length <= 4) || e.Length == 7);

Console.WriteLine(result);

//Part 2

int sum = 0;

for (int i = 0; i < input.Count; i++)
{
    var inputSplit = input[i].Split(" | ");

    var wireSegmentConnections = inputSplit[0].Split(' ');
    var outputSegments = inputSplit[1].Split(' ');
    Dictionary<string, int> segmentConnections = new Dictionary<string, int>();

    char[] oneSegments = wireSegmentConnections.Where(e => e.Length == 2).Single().ToCharArray();
    char[] fourSegments = wireSegmentConnections.Where(e => e.Length == 4).Single().ToCharArray();
    char[] sevenSegments = wireSegmentConnections.Where(e => e.Length == 3).Single().ToCharArray();
    char[] eightSegments = wireSegmentConnections.Where(e => e.Length == 7).Single().ToCharArray();

    var unknownSegmentConnections = wireSegmentConnections.Where(e => e.Length > 4 && e.Length != 7).ToList();

    char segmentA = sevenSegments.Where(e => !oneSegments.Contains(e)).Single();

    char[] segmentBC = oneSegments;

    char[] numberThree = unknownSegmentConnections
                         .Where(e => e.Length == 5 && oneSegments
                         .All(x => e.Contains(x))).Single()
                         .ToCharArray();

    char[] segmentGD = numberThree
        .Where(e => !sevenSegments.Contains(e))
        .ToArray();

    char segmentF = fourSegments.Where(e => !oneSegments.Contains(e) && !segmentGD.Contains(e)).Single();

    char segmentG = fourSegments.Where(e => !oneSegments.Contains(e) && e != segmentF).Single();

    char segmentD = segmentGD.Where(e => e != segmentG).Single();

    char[] numberTwo = unknownSegmentConnections
                       .Where(e => e.Length == 5 && !e.Contains(segmentF) && e.Where(x => oneSegments.Contains(x)).Count() == 1)
                       .Single().ToCharArray();

    char segmentB = numberTwo
                    .Where(e => e != segmentA && !segmentGD.Contains(e) && oneSegments.Contains(e))
                    .Single();

    char segmentC = segmentBC.Where(e => e != segmentB).Single();


    char[] numberNine = unknownSegmentConnections
                        .Where(e => e.Length == 6 && e.Contains(segmentG) && e.Contains(segmentB)).Single()
                        .ToCharArray();

    char segmentE = eightSegments.Where(el => !numberNine.Contains(el)).Single();

    //One
    segmentConnections.Add(new string(oneSegments), 1);

    //Four
    segmentConnections.Add(new string(fourSegments), 4);

    //Seven
    segmentConnections.Add(new string(sevenSegments), 7);

    //Eight
    segmentConnections.Add(new string(eightSegments), 8);

    foreach (var unknownConnection in unknownSegmentConnections)
    {
        //Zero
        if (unknownConnection.All(e => eightSegments.Where(e => e != segmentG).Contains(e)))
        {
            segmentConnections.Add(unknownConnection, 0);
        }

        //Two
        else if (unknownConnection.All(e => e == segmentA || e == segmentB || e == segmentG || e == segmentE || e == segmentD))
        {
            segmentConnections.Add(unknownConnection, 2);
        }

        //Three
        else if (unknownConnection.All(e => e == segmentA || e == segmentB || e == segmentG || e == segmentC || e == segmentD))
        {
            segmentConnections.Add(unknownConnection, 3);
        }

        //Five
        else if (unknownConnection.All(e => e == segmentA || e == segmentG || e == segmentC || e == segmentD || e == segmentF))
        {
            segmentConnections.Add(unknownConnection, 5);
        }

        //Six
        else if (unknownConnection.All(e => e == segmentA || e == segmentG || e == segmentC || e == segmentD || e == segmentE || e == segmentF))
        {
            segmentConnections.Add(unknownConnection, 6);
        }

        //Nine
        else if (unknownConnection.All(e => e == segmentA || e == segmentB || e == segmentG || e == segmentC || e == segmentD || e == segmentF))
        {
            segmentConnections.Add(unknownConnection, 9);
        }
    }

    string digits = "";

    foreach (var outputs in outputSegments)
    {
        digits += segmentConnections.Where(e => e.Key.All(x => outputs.Contains(x) && outputs.Length == e.Key.Length)).Single().Value;
    }

    sum += int.Parse(digits);
}

Console.WriteLine(sum);