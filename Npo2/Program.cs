using System;

namespace Npo2
{
    class Program
    {
        static void Main(string[] args)
        {
            
            var p = new Parser();
            var parsedarray = p.ParseFile(@"D:\input.txt");
            var edges = Graph.CreateEdges(parsedarray);
            var vertexes = Graph.CreateVertexes(p.EdgesCount);
            var graph = new Graph(vertexes,edges);
            foreach (var v in graph.Edges)
            {
                Console.WriteLine(v.Startpoint.Value + " " + v.Endpoint.Value);
            }
            Graph.PrintMatrix(graph.Matrix);
            Graph.FindPath(graph.Matrix,p.StartPtr,p.DestPtr);

            Console.ReadLine();
        }
    }
}
