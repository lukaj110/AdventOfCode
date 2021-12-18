using System.Linq;

namespace AdventOfCode_Day4
{
    public class BingoBoard
    {
        public short[,] Board = new short[5, 5];
        List<short> ChosenValues = new List<short>();

        public BingoBoard(string[] Input)
        {
            for (int i = 0; i < 5; i++)
            {
                short[] row = Input[i].Split(' ').Where(e => e.Length > 0).Select(e => short.Parse(e)).ToArray();
                for (int j = 0; j < 5; j++) Board[i, j] = row[j];
            }
        }

        public void ChooseNumber(short Value) => ChosenValues.Add(Value);

        public bool Won
        {
            get
            {
                List<short> horizontal = new List<short>();
                List<short> vertical = new List<short>();

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        horizontal.Add(Board[i, j]);
                        vertical.Add(Board[j, i]);
                    }
                    if (horizontal.All(e => ChosenValues.Contains(e)) || vertical.All(e => ChosenValues.Contains(e))) return true;

                    horizontal.Clear();
                    vertical.Clear();
                }

                return false;
            }

        }

        public short SumUnmarked
        {
            get
            {
                List<short> values = new List<short>();

                for (int i = 0; i < 5; i++)
                {
                    for (int j = 0; j < 5; j++)
                    {
                        short val = Board[i, j];

                        if (!ChosenValues.Contains(val)) values.Add(val);
                    }

                }

                return (short)values.Sum(e=>e);
            }
        }

        public int FinalScore => SumUnmarked * ChosenValues.Last();
    }
}
