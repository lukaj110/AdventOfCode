List<string> input = new List<string>();

using (var reader = new StreamReader("Input.txt"))
{
    while (!reader.EndOfStream) input.Add(reader.ReadLine());
}

//Part 1

void CountBits(List<string> Input, ref int[] OneBits, ref int[] ZeroBits)
{
    for (int i = 0; i < OneBits.Length; i++)
    {
        OneBits[i] = 0;
        ZeroBits[i] = 0;
    }

    for (int i = 0; i < Input.Count; i++)
    {
        for (int j = 0; j < Input[i].Length; j++)
        {
            if (Input[i][j] == '1') OneBits[j]++;
            else ZeroBits[j]++;
        }
    }
}

int[] oneBitCount = new int[input[0].Length];
int[] zeroBitCount = new int[input[0].Length];

CountBits(input, ref oneBitCount, ref zeroBitCount);

string gammaRateBinary = "";
string epsilonRateBinary = "";

for (int i = 0; i < oneBitCount.Length; i++)
{
    if (oneBitCount[i] > zeroBitCount[i])
    {
        gammaRateBinary += 1;
        epsilonRateBinary += 0;
    }
    else
    {
        gammaRateBinary += 0;
        epsilonRateBinary += 1;
    }
}

int gammaRate = Convert.ToInt32(gammaRateBinary, 2);
int epsilonRate = Convert.ToInt32(epsilonRateBinary, 2);
int result = gammaRate * epsilonRate;

Console.WriteLine(result);

//Part 2

List<string> ratings = input.ToList();

for (int i = 0; i < input[0].Length; i++)
{
    if (ratings.Count == 1) break;
    if (oneBitCount[i] >= zeroBitCount[i]) ratings = ratings.Where(e => e[i] == '1').ToList();
    else ratings = ratings.Where(e => e[i] == '0').ToList();

    CountBits(ratings, ref oneBitCount, ref zeroBitCount);
}

int oxygenGeneratorRating = Convert.ToInt32(ratings[0], 2);

ratings = input.ToList();

CountBits(ratings, ref oneBitCount, ref zeroBitCount);

for (int i = 0; i < input[0].Length; i++)
{
    if (ratings.Count == 1) break;
    if (oneBitCount[i] >= zeroBitCount[i]) ratings = ratings.Where(e => e[i] == '0').ToList();
    else ratings = ratings.Where(e => e[i] == '1').ToList();

    CountBits(ratings, ref oneBitCount, ref zeroBitCount);
}

int co2ScrubberRating = Convert.ToInt32(ratings[0], 2);

int lifeSupportRating = oxygenGeneratorRating * co2ScrubberRating;

Console.WriteLine(lifeSupportRating);