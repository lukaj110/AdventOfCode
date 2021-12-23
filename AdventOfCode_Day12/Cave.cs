using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode_Day12
{
    public class Cave
    {
        public string Name { get; set; }
        public List<Cave> ConnectedTo { get; set; } = new List<Cave>();
        public bool BigCave { get; set; }

        public IEnumerable<IEnumerable<Cave>> TraverseToEndPart1(IEnumerable<Cave> traversedPath)
        {
            traversedPath = traversedPath.Append(this);

            if (Name == "end")
            {
                yield return traversedPath;
                yield break;
            }

            foreach (Cave cave in ConnectedTo)
            {
                if ((!cave.BigCave && traversedPath.Any(e => e.Name == cave.Name)) || cave.Name == "start") continue;

                foreach (var traversedToEnd in cave.TraverseToEndPart1(traversedPath.ToList())) yield return traversedToEnd;
            }
        }

        public IEnumerable<IEnumerable<Cave>> TraverseToEndPart2(IEnumerable<Cave> traversedPath)
        {
            traversedPath = traversedPath.Append(this);

            if (Name == "end")
            {
                yield return traversedPath;
                yield break;
            }

            foreach (Cave cave in ConnectedTo)
            {
                if (cave.Name == "start") continue;

                if (!cave.BigCave)
                {
                    var pathGrouping = traversedPath.Where(e => !e.BigCave).GroupBy(e => e.Name).Select(e => new { Name = e.Key, Count = e.Count() });

                    if (pathGrouping.Max(e => e.Count) == 2)
                    {
                        if (traversedPath.Where(e => e.Name == cave.Name).Count() == 0)
                        {
                            foreach (var traversedToEnd in cave.TraverseToEndPart2(traversedPath.ToList())) yield return traversedToEnd;
                        }
                    }
                    else
                    {
                        foreach (var traversedToEnd in cave.TraverseToEndPart2(traversedPath.ToList())) yield return traversedToEnd;
                    }
                }
                else
                {
                    foreach (var traversedToEnd in cave.TraverseToEndPart2(traversedPath.ToList())) yield return traversedToEnd;
                }
            }
        }
    }
}