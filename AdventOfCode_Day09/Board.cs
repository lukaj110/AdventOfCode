using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_Day9
{
    internal class Board
    {
        private int[,] Table = new int[100, 100];

        public void AddPoint(int Row, int Col, int Value)
        {
            Table[Row, Col] = Value;
        }

        public Board() { }

        public IEnumerable<Tuple<int, int>> GetAdjacentPositions(int Row, int Col)
        {
            if (Row > 0 && Row < 99)
            {
                yield return Tuple.Create(Row - 1, Col);
                yield return Tuple.Create(Row + 1, Col);
            }
            else if (Row == 0) yield return Tuple.Create(Row + 1, Col);
            else yield return Tuple.Create(Row - 1, Col);

            if (Col > 0 && Col < 99)
            {
                yield return Tuple.Create(Row, Col - 1);
                yield return Tuple.Create(Row, Col + 1);
            }
            else if (Col == 0) yield return Tuple.Create(Row, Col + 1);
            else yield return Tuple.Create(Row, Col - 1);
        }

        public IEnumerable<Tuple<int, int>> GetAdjacentPositionsWithoutBasin(int Row, int Col, List<Tuple<int, int>> Basin)
        {
            return GetAdjacentPositions(Row, Col).Where(el => !Basin.Any(x => x.Item1 == el.Item1 && x.Item2 == el.Item2));
        }

        public IEnumerable<int> GetAdjacentValues(int Row, int Col)
        {
            foreach (var adjacentPosition in GetAdjacentPositions(Row, Col))
            {
                yield return Table[adjacentPosition.Item1, adjacentPosition.Item2];
            }
        }

        public bool IsLowestAdjacent(int Row, int Col) => GetAdjacentValues(Row, Col).All(e => GetValue(Row, Col) < e);

        public bool IsLowestAdjacentInBasin(int Row, int Col, List<Tuple<int, int>> Basin)
        {
            return GetAdjacentPositionsWithoutBasin(Row, Col, Basin).All(e => GetValue(Row, Col) < GetValue(e.Item1, e.Item2));
        }

        public bool HasAdjacentBasins(int Row, int Col, List<List<Tuple<int, int>>> BasinList)
        {
            return GetAdjacentPositions(Row, Col)
                   .Any(e => BasinList.Any(x => x.Any(el => el.Item1 == e.Item1 && el.Item2 == e.Item2)));
        }

        public int GetValue(int Row, int Col) => Table[Row, Col];

        public void TraverseBasin(int Row, int Col, List<List<Tuple<int, int>>> BasinList)
        {
            int value = GetValue(Row, Col);

            if (value != 9)
            {
                if (!BasinList.Any(e => e.Any(x => x.Item1 == Row && x.Item2 == Col)))
                {
                    if (HasAdjacentBasins(Row, Col, BasinList))
                    {
                        var basins = BasinList.FindIndex(e => GetAdjacentPositions(Row, Col).Any(el =>
                                         e.Any(el2 => el.Item1 == el2.Item1 && el.Item2 == el2.Item2)));

                        BasinList[basins].Add(Tuple.Create(Row, Col));

                        foreach (var adjacentPos in GetAdjacentPositionsWithoutBasin(Row, Col, BasinList[basins]))
                        {
                            TraverseBasin(adjacentPos.Item1, adjacentPos.Item2, BasinList);
                        }
                    }
                    else
                    {
                        if (IsLowestAdjacent(Row, Col))
                        {
                            var basin = new List<Tuple<int, int>>();
                            basin.Add(Tuple.Create(Row, Col));
                            BasinList.Add(basin);

                            int index = BasinList.IndexOf(basin);

                            foreach (var adjacentPos in GetAdjacentPositionsWithoutBasin(Row, Col, basin))
                            {
                                TraverseBasin(adjacentPos.Item1, adjacentPos.Item2, BasinList);
                            }
                        }
                    }
                }
            }
        }
    }
}
