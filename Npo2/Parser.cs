using System;
using System.IO;
using System.Linq;

namespace Npo2
{
    public class Parser
    {   
        public int StartPtr { get; private set; }
        public int DestPtr { get; private set; }
        public int EdgesCount { get; private set; }

        public int[][] ParseFile(string path)
        {
            var header =
                File.ReadAllLines(path)
                    .Skip(0)
                    .First()
                    .Split(' ')
                    .Where(s => !string.IsNullOrWhiteSpace(s))
                    .Select(int.Parse)
                    .ToArray();

            EdgesCount = header[0];
            StartPtr = header[1];
            DestPtr = header[2];

            Console.Write(@"Edges count {0}", EdgesCount);
            Console.Write(@" Start point {0}", StartPtr);
            Console.Write(@" Dest point {0}", DestPtr);
            Console.WriteLine();
            var sarray = File.ReadAllLines(path).Skip(1).ToArray();
            var g = new int[EdgesCount][];

            for (var i = 0; i < EdgesCount; i++)
            {
                g[i] = sarray[i].Split(' ').
                    Where(x => !string.IsNullOrWhiteSpace(x)).
                    Select(int.Parse).ToArray();
            }
            return g;
        }
    }
}