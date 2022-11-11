using System;
using System.Collections.Generic;
using System.Linq;

namespace Lr2
{
    class Program
    {
        private static Stack<GraphNode> stack = new Stack<GraphNode>();
        private static List<GraphNode> graph = new List<GraphNode>();
        static void Main(string[] args)
        {
            int start = 0;
            int finish = 5;

            graph.Add(new GraphNode(0, "gray", 1, 1));
            graph.Add(new GraphNode(0, "white", 2, 2));
            graph.Add(new GraphNode(1, "white", 2, 2));
            graph.Add(new GraphNode(1, "white", 4, 1));
            graph.Add(new GraphNode(2, "white", 3, 1));
            graph.Add(new GraphNode(3, "white", 4, 2));
            graph.Add(new GraphNode(3, "white", 5, 1));
            graph.Add(new GraphNode(4, "white", 5, 2));
            graph.Add(new GraphNode(5, "white"));

            var (opt, x) = STPath(start, finish, graph);
            foreach (var item in opt)
            {
                Console.WriteLine($"Элемент массива opt: {item}");
            }
            foreach (var item in x)
            {
                Console.WriteLine($"Элемент массива x: {item}");
            }
        }

        private static void DFS(GraphNode node)
        {
            node.Color = "gray";
            foreach (var item in graph)
            {
                if (item.Color == "white")
                {
                    DFS(item);
                }
            }

            stack.Push(node);
        }

        private static Tuple<double[], double?[]> STPath(int source, int target, List<GraphNode> graphNodes)
        {
            try
            {

                DFS(graphNodes.ElementAt(source));

                stack = new Stack<GraphNode>(stack.Reverse().ToList());

                int startIndex = stack.ToList().FindIndex(x => x.Source == source);
                int finishIndex = stack.ToList().FindIndex(x => x.Destination == target);

                if (startIndex > finishIndex)
                {
                    throw new Exception("Нет пути!");
                }

                var sortedSources = new SortedSet<int>(stack.Select(x => x.Source));

                double[] opt = new double[sortedSources.Count];
                Array.Fill(opt, Double.NegativeInfinity);

                double?[] x = new double?[sortedSources.Count];
                Array.Fill(x, 0);

                opt[source] = 0;
                x[source] = null;

                sortedSources.ToList().ForEach((sor) =>
                {
                    
                    stack.ToList()
                    .ForEach((s) =>
                    {
                        if (s.Destination == sor)
                        {
                            var cur = sortedSources.ElementAt(s.Source);
                            if (s.Weight + opt[cur] > opt[sor])
                            {
                                opt[sor] = s.Weight + opt[cur];
                                x[sor] = s.Source;
                            }
                        }
                    });
                });

                return Tuple.Create(opt, x);
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Произошла ошибка: {ex.Message}");
                return Tuple.Create(new double[1], new double?[1]);
            }
        }
    }
}
