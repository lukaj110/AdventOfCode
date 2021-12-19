using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_Day11
{
    public class Octopus
    {
        public bool Flashed { get; set; } = false;
        public int Value { get; set; }

        public Octopus(int Value)
        {
            this.Value = Value;
        }
    }
    public class OctopusBoard
    {
        public Octopus[,] Board = new Octopus[10, 10];

        public int FlashCounter = 0;

        public void AddValue(int Row, int Col, int Value) => Board[Row, Col] = new Octopus(Value);

        public void IncreaseAll(int Value)
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++) Board[i, j].Value += Value;
            }
        }

        public IEnumerable<Tuple<int, int>> GetAdjacentOctopuses(int Row, int Col)
        {
            if (Row > 0 && Row < Board.GetLength(0) - 1)
            {
                if(Col > 0 && Col < Board.GetLength(1) - 1)
                {
                    yield return Tuple.Create(Row - 1, Col - 1);
                    yield return Tuple.Create(Row - 1, Col + 1);
                    yield return Tuple.Create(Row + 1, Col + 1);
                    yield return Tuple.Create(Row + 1, Col - 1);
                }else if (Col == 0)
                {
                    yield return Tuple.Create(Row - 1, Col + 1);
                    yield return Tuple.Create(Row + 1, Col + 1);
                }
                else
                {
                    yield return Tuple.Create(Row - 1, Col - 1);
                    yield return Tuple.Create(Row + 1, Col - 1);
                }

                yield return Tuple.Create(Row - 1, Col);
                yield return Tuple.Create(Row + 1, Col);
            }
            else if (Row == 0)
            {
                if (Col > 0 && Col < Board.GetLength(1) - 1)
                {
                    yield return Tuple.Create(Row + 1, Col - 1);
                    yield return Tuple.Create(Row + 1, Col + 1);
                }
                else if (Col == 0) yield return Tuple.Create(Row + 1, Col + 1);
                else yield return Tuple.Create(Row + 1, Col - 1);
                yield return Tuple.Create(Row + 1, Col);
            }
            else
            {
                if (Col > 0 && Col < Board.GetLength(1) - 1)
                {
                    yield return Tuple.Create(Row - 1, Col - 1);
                    yield return Tuple.Create(Row - 1, Col + 1);
                }
                else if (Col == 0) yield return Tuple.Create(Row - 1, Col + 1);
                else yield return Tuple.Create(Row - 1, Col - 1);
                yield return Tuple.Create(Row - 1, Col);
            }

            if (Col > 0 && Col < Board.GetLength(1) - 1)
            {
                yield return Tuple.Create(Row, Col - 1);
                yield return Tuple.Create(Row, Col + 1);
            }
            else if (Col == 0) yield return Tuple.Create(Row, Col + 1);
            else yield return Tuple.Create(Row, Col - 1);
        }

        public IEnumerable<Tuple<int, int>> GetAdjacentOctopusesCanFlash(int Row, int Col)
        {
            return GetAdjacentOctopuses(Row, Col).Where(e => !Board[e.Item1, e.Item2].Flashed);
        }

        public void Flash(int Row, int Col)
        {
            FlashCounter += 1;
            Board[Row, Col].Flashed = true;
            Board[Row, Col].Value = 0;

            foreach(var octopus in GetAdjacentOctopuses(Row, Col))
            {
                if(!Board[octopus.Item1, octopus.Item2].Flashed)
                {
                    Board[octopus.Item1, octopus.Item2].Value += 1;
                    if(Board[octopus.Item1, octopus.Item2].Value > 9)
                    {
                        Flash(octopus.Item1, octopus.Item2);
                    }
                }
            }
        }

        public void ClearFlashes()
        {
            for (int i = 0; i < Board.GetLength(0); i++)
            {
                for (int j = 0; j < Board.GetLength(1); j++) Board[i, j].Flashed = false;
            }
        }
    }
}
