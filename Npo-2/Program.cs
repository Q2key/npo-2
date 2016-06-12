using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Npo_2
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Matrix m = new Matrix();
            m.CreateGraf(@"С:\input.txt");
            Console.ReadKey();
        }
        private class Matrix
        {
            private static void Out(List<int> output, int startpoint )
            {
                var outputstr = output.Aggregate(startpoint.ToString() + " ", (current, t) => current + (t.ToString()+ " "));
                File.WriteAllText(@"C:\output.txt",outputstr);             
            }

            public void CreateGraf(string path)
            {
                if (!File.Exists(path))
                {
                    Console.WriteLine("File is not exists");
                    return;
                }
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

                var m = new List<int>();
                var mlist = new List<List<int>>();
                var t = startp;
                var st = 1;

                foreach (var e in g)
                {
                    if (e[0] == startp)
                    {
                        foreach (var se in g)
                        {
                            while (se[0] == t)
                            {   
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.Write("\nStep " + st + " Start " + startp + " Next => " + se[1]);
                                m.Add(se[1]);
                                st++;
                                if (se[1] == destp)
                                {
                                    mlist.Add(m);
                                    Console.WriteLine(" End point finded");
                                    m = new List<int>();
                                    st = 1;
                                }
                                t = se[1];
                            }
                        }                                                
                    }
                }
                mlist.Sort((e1,e2)=>e1.Count.CompareTo(e2.Count));
                if (mlist.Count == 0)
                {
                    Console.WriteLine("\nNo possibly way");
                    return;;
                }
                var shortst = mlist[0];
                Console.ForegroundColor = ConsoleColor.Cyan;;
                Console.Write("\nCorrected shortest path : " + " C " + mlist.Count);       
                Out(shortst,startp);                        
            }
        }
    }
}
