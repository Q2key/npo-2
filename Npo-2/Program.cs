using System;
using System.CodeDom;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Npo_2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Matrix m = new Matrix();
            m.CreateGraf(@"D:\input.txt");
            Console.ReadKey();
        }


        private class Matrix
        {
            private static void Out(List<int> output )
            {
                var outputstr = output.Aggregate(string.Empty, (current, t) => current + (t.ToString()+" "));
                File.WriteAllText(@"D:\output2.txt",outputstr);             
            }

            public void CreateGraf(string path)
            {
                var header =
                    File.ReadAllLines(path)
                        .Skip(0)
                        .First()
                        .Split(' ')
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Select(int.Parse)
                        .ToArray();

                var edges = header[0];
                var startp = header[1];
                var destp = header[2];

                Console.Write(@"Edges count {0}", edges);
                Console.Write(@" Start point {0}",startp);
                Console.Write(@" Dest point {0}", destp);

                Console.WriteLine();

                var sarray = File.ReadAllLines(path).Skip(1).ToArray();

                var g = new int[edges][];

                for (var i = 0; i < edges; i++)
                {
                    g[i] = sarray[i].Split(' ').
                    Where(x => !string.IsNullOrWhiteSpace(x)).
                    Select(int.Parse).ToArray();
                }
                foreach (var list in g)
                {
                    foreach (var e in list)
                    {
                        Console.Write(e);
                    }
                    Console.WriteLine();
                }
                Console.WriteLine();

                var temp = new List<List<int>>();
                var patharr = new List<int>();
                for (var i = 0; i < g.Length; i++)
                {
                    for (var j = 0; j < g[i].Length; j++)
                    {
                        if (g[i][j] == startp )
                        {
                            temp.Add(new List<int>(g[i])); 
                        }
                    }
                }
                patharr.Add(startp);
                foreach (var list in temp)
                {
                    Console.WriteLine(@"Potential way - start {0} dest {1}", list[0], list[1]);
                    var startptr = list[1];
                    foreach (var edgelist in g)
                    {
                        if (edgelist[0] == startptr)
                        {   

                            startptr = edgelist[0];
                            patharr.Add(startptr);
                            foreach (var subedges in g)
                            {
                                if (subedges[0] == startptr)
                                {
                          
                                    patharr.Add(edgelist[1]);
                                }
                            }
                        }
                    }
                }
                Console.Write("Corrected path : ");
                patharr.ToList().ForEach(Console.Write);
                Out(patharr);
            }
        }
    }
}
