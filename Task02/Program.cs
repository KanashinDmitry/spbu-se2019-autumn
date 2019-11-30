using System;
using System.IO;

namespace Graphs
{
    class Program
    {
        static void Main()
        {
            var graphInit = new Graph();

            int[,] graph = graphInit.GenerateGraphWithEdges();
            
            using (StreamWriter writer = new StreamWriter(Paths.PathInitGraph, false
                                                            , System.Text.Encoding.Default))
            {
                graphInit.PrintGraph(graph, graphInit.Size, writer);
            }

            var resolver = new Algorithms();
            var resolverParallel = new ParallelAlgorithms();

            Console.WriteLine($"{graphInit.Size} {graphInit.AmountEdges}");
            
            int kruskal = resolver.SolveKruskal(graph, graphInit.Size).Item1;
            Console.WriteLine("kruskal done");
            int prim = resolver.SolvePrim(graph, graphInit.Size).Item1;
            Console.WriteLine("prim done");
            int[,] floyd = resolver.SolveFloyd(graph, graphInit.Size);
            Console.WriteLine("floyd done");
            
            int kruskalPar = resolverParallel.SolveKruskal(graph, graphInit.Size).Item1;
            Console.WriteLine("kruskalPar done");
            int primPar = resolverParallel.SolvePrim(graph, graphInit.Size).Item1;
            Console.WriteLine("primPar done");
            int[,] floydPar = resolverParallel.SolveFloyd(graph, graphInit.Size);
            Console.WriteLine("floydPar done");

            using (var writer = new StreamWriter(Paths.PathResFl, false
                                                                               , System.Text.Encoding.Default))
            {
                if (graphInit.CompareMatrixGraphs(floyd, floydPar, graphInit.Size))
                {
                    graphInit.PrintGraph(floydPar, graphInit.Size, writer);
                }
                else
                {
                    writer.WriteLine(Constants.NotTheSameResultLog);
                }
            }
            
            using (var writer = new StreamWriter(Paths.PathResPr, false
                                                            , System.Text.Encoding.Default))
            {
                if (prim == primPar)
                {
                    writer.WriteLine(primPar);
                }
                else
                {
                    writer.WriteLine(Constants.NotTheSameResultLog);
                }
            }
            
            using (var writer = new StreamWriter(Paths.PathResKr, false
                                                            , System.Text.Encoding.Default))
            {
                if (kruskal == kruskalPar)
                {
                    writer.WriteLine(kruskalPar);
                }
                else
                {
                    writer.WriteLine(Constants.NotTheSameResultLog);
                }
            }
        }
    }
}