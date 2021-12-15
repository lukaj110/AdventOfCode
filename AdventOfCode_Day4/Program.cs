using AdventOfCode_Day4;

List<short> chosenNumbers = new List<short>();
List<BingoBoard> bingoBoards = new List<BingoBoard>();

using (var reader = new StreamReader("Input.txt"))
{
    foreach (string chosenNumber in reader.ReadLine().Split(",")) chosenNumbers.Add(short.Parse(chosenNumber));

    string[] inputs = reader.ReadToEnd().Split(Environment.NewLine).Where(e => e.Length > 0).ToArray();

    for (int i = 0; i < inputs.Length; i += 5) bingoBoards.Add(new BingoBoard(inputs[i..(i + 5)]));
}

//Part 1

List<BingoBoard> wonBoards = new List<BingoBoard>();

foreach (short chosenNumber in chosenNumbers)
{
    foreach (BingoBoard bingoBoard in bingoBoards)
    {
        if (!bingoBoard.Won) bingoBoard.ChooseNumber(chosenNumber);
        else if (bingoBoard.Won && !wonBoards.Contains(bingoBoard)) wonBoards.Add(bingoBoard);
    }
}

Console.WriteLine(wonBoards.First().FinalScore);

//Part 2

Console.WriteLine(wonBoards.Last().FinalScore);