using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Npo2
{
    internal class Graph
    {
        public List<Vertex> Vertexes { get; private set; }
        public List<Edge> Edges { get; private set; }
        public int[,] Matrix { get; private set; }

        public Graph(List<Vertex> vs, List<Edge> es)
        {
            Vertexes = vs;
            Edges = es;
            Matrix = CreateMatrix(vs, es);
        }

        private static int[,] CreateMatrix(List<Vertex> vertexes, List<Edge> edges)
        {
            var count = vertexes.Count;
            var m = new int[count, count];
            for (var i = 0; i < count; i++)
            {
                m[edges[i].Startpoint.Value - 1, edges[i].Endpoint.Value - 1] = 1;
            }
            return m;
        }

        public static List<List<int>> ShortestPath(List<List<int>> plist)
        {
            plist.Sort((l1, l2) => l1.Count.CompareTo(l2.Count));
            return plist;
        }

        public static void ShortestPathOut(List<List<int>> plist, int start)
        {
            var groupped = plist.GroupBy(list => list.Count).ToList();
            var outstr = string.Empty;
            if (groupped.Count == 0) return;
            foreach (var shortest in groupped[0])
            {
                outstr += start + " ";
                outstr = shortest.Aggregate(outstr, (current, path) => current + (path.ToString() + " "));
                outstr += "\n";
            }
            File.WriteAllText(@"D:\output.txt", outstr);
        }

        public static List<List<int>> FindPath(int[,] m, int start, int end)
        {
            Console.WriteLine("Path");
            var next = start - 1;
            List<List<int>> plist = new List<List<int>>();

            if (m[start - 1, end - 1] == 1)
            {   
                Console.WriteLine("\nFrom {0} => {1}", start, end);
                Console.WriteLine("Sortest path finded");
                plist.Add(new List<int>() {end});
                ShortestPathOut(ShortestPath(plist), start);
                return plist;
            }
            for (var i = 0; i < m.GetLength(0); i++)
            {
                var k = 0;
                var tlist = new List<int>();
                if (m[next, i] == 1)
                {
                    var t = i;
                    Console.WriteLine("\nFrom {0} => {1}", start, i + 1);
                    tlist.Add(i + 1);
                    while (k < m.GetLength(0))
                    {
                        if (k<m.GetLength(0)&& m[t, k] == 1)
                        {
                            tlist.Add(k + 1);
                            Console.WriteLine("Cur {0} Next => {1}", t + 1, k + 1);
                            while (k<i && m[start-1,k] == m[t,end-1])
                            {
                                Console.WriteLine("Loop");
                                k++;
                                t--;

                            }
                            if(m[t, end - 1] == 1)
                            {
                                plist.Add(tlist);
                                tlist = new List<int>();
                                Console.WriteLine("End point finded");
                            }
                            else
                            {
                                t = k;
                                k = 0;
                            }
                        }
                        k++;
                    }
                }
            }
            foreach (var path in plist)
            {
                foreach (var p in path)
                {
                    Console.Write(p);
                }
                Console.WriteLine();               
            }
            ShortestPathOut(ShortestPath(plist), start);
            return plist;
        }

        public static void PrintMatrix(int[,] m)
        {
            Console.WriteLine("\nMatrix");
            for (var i = 0; i < m.GetLength(0); i++)
            {
                for (var j = 0; j < m.GetLength(1); j++)
                {
                    Console.Write(m[i, j]);
                }
                Console.WriteLine();
            }
        }

        public static List<Vertex> CreateVertexes(int vcount)
        {
            var temp = new List<Vertex>();
            for (var i = 1; i <= vcount; i++)
            {
                temp.Add(new Vertex(i));
            }
            return temp;
        }

        public static List<Edge> CreateEdges(int[][] parr)
        {
            var temp = new List<Edge>();
            for (var i = 0; i < parr.GetLength(0); i++)
            {
                temp.Add(new Edge(new Vertex(parr[i][0]), new Vertex(parr[i][1])));
            }
            return temp;
        }
    }
}
