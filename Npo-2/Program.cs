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
            m.CreateGraf(@"C:\input.txt");
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
                    while (e[0] == t)
                    {
                        Console.Write("\nStep " + st + " Start => " + startp + " Next " + e[1]);
                        m.Add(e[1]);
                        st++;
                        if (e[1] == destp)
                        {
                            Console.WriteLine(" End point finded");
                            m = new List<int>();
                            st = 1;
                        }
                        else if (!mlist.Contains(m))
                        {
                            mlist.Add(m);
                        }
                        t = e[1];
                    }
                }
                mlist.Sort((e1,e2)=>e1.Count.CompareTo(e2.Count));
                var shortst = mlist[0];
                Console.Write("Corrected shortest path : " + " C " + mlist.Count);       
                Out(shortst,startp); 
                        
            }
        }
    }
}
