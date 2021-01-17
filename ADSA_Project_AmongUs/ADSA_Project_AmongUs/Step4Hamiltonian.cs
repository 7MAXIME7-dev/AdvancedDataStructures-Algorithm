using System;
using System.Collections.Generic;
using System.Text;

namespace ADSA_Project_AmongUs
{
    class Step4Hamiltonian
    {
        /// <summary>
        /// number of vertex
        /// </summary>
        public readonly int V = 14;

        /// <summary>
        /// path to go between A from B
        /// </summary>
        public int[] path;

        bool isSafe(int v, int[,] graph, int[] path, int pos)
        {
  
            if (graph[path[pos - 1], v] == 0)
                return false;

            for (int i = 0; i < pos; i++)
                if (path[i] == v)
                    return false;

            return true;
        }


        bool hamCycleUtil(int[,] graph, int[] path, int pos)
        {
            if (pos == V)
            {
                if (graph[path[pos - 1], path[0]] == 1)
                    return true;
                else
                    return false;
            }

            for (int v = 0; v < V; v++)
            {
                if (isSafe(v, graph, path, pos))
                {
                    path[pos] = v;

                    if (hamCycleUtil(graph, path, pos + 1) == true)
                        return true;

                    path[pos] = -1;
                }
            }

            return false;
        }

        /// <summary>
        /// This function allows you to find hamilton cycle from a source if exist
        /// </summary>
        /// <param name="graph">matrix of the graph</param>
        /// <param name="s">source vertex</param>
        /// <returns> 1 if solution exists, else 0 </returns>
        public int hamCycle(int[,] graph, int s)
        {
            path = new int[V];
            for (int i = 0; i < V; i++)
                path[i] = -1;

            path[0] = s;
            if (hamCycleUtil(graph, path, 1) == false)
            {
                Console.WriteLine("\nSource: {0}({1}) => Solution does not exist", Step3MapImpCrew.rooms[s], s);
                return 0;
            }

            printSolution(path, true);
            return 1;
        }



        bool hamPathUtil(int[,] graph, int[] path, int pos)
        {
            if (pos == V)
            {
                return true;
            }

            for (int v = 0; v < V; v++)
            {
                if (isSafe(v, graph, path, pos))
                {
                    path[pos] = v;

                    if (hamPathUtil(graph, path, pos + 1) == true)
                        return true;

                    path[pos] = -1;
                }
            }

            return false;
        }

        /// <summary>
        /// This function allows you to find hamilton PATH from a source if exist
        /// </summary>
        /// <param name="graph">matrix of the graph</param>
        /// <param name="s">source vertex</param>
        /// <returns> 1 if solution exists, else 0 </returns>
        public int hamPath(int[,] graph, int s)
        {
            path = new int[V];
            for (int i = 0; i < V; i++)
                path[i] = -1;

            path[0] = s;
            if (hamPathUtil(graph, path, 1) == false)
            {
                Console.WriteLine("\nSource: {0}({1}) => Solution does not exist", Step3MapImpCrew.rooms[s], s);
                return 0;
            }                                     

            printSolution(path, false);
            return 1;
        }

        /// <summary>
        /// this function display the results
        /// </summary>
        /// <param name="path"></param>
        /// <param name="hCycle"></param>
        public void printSolution(int[] path, bool hCycle)
        {
            if (hCycle) { Console.WriteLine("\nSource: {0}({1}) => Following is one Hamiltonian Cycle", Step3MapImpCrew.rooms[path[0]], path[0]); }
            else { Console.WriteLine("\nSource: {0}({1}) => Following is one Hamiltonian Path", Step3MapImpCrew.rooms[path[0]], path[0]); }

            for (int i = 0; i < V; i++)
                Console.Write(" {0}({1}) ", Step3MapImpCrew.rooms[path[i]], path[i]);

            if (hCycle) { Console.WriteLine(" {0}({1}) ", Step3MapImpCrew.rooms[path[0]], path[0]); }
            else { Console.WriteLine(); }
            
        }


    }
}
