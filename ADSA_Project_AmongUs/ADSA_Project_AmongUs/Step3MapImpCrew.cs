using System;
using System.Collections.Generic;
using System.Text;

namespace ADSA_Project_AmongUs
{
    class Step3MapImpCrew
    {
        /// <summary>
        /// declaration of the rooms
        /// </summary>
        public static Dictionary<int, string> rooms = new Dictionary<int, string>()
        {
                { 0, "Reactor"},
                { 1, "Upper E."},
                { 2, "Lower E."},
                { 3, "Security"},
                { 4, "Medbay"},
                { 5, "Electrical"},
                { 6, "Cafeteria"},
                { 7, "03"},
                { 8, "Storage"},
                { 9, "Weapons"},
                { 10, "02"},
                { 11, "04"},
                { 12, "Shield"},
                { 13, "Navigation"},
                {14, "Corridor" }
        };


        public const int INF = 10000;

        /// <summary>
        /// Allows to display a matrix
        /// </summary>
        /// <param name="M"></param>
        public static void displayMatrix(int[,] M)
        {
            for (int i = 0; i < M.GetLength(0); i++)
            {
                for (int j = 0; j < M.GetLength(1); j++)
                {
                    if (M[i, j] == INF)
                        Console.Write("INF".PadLeft(4));
                    else
                        Console.Write(M[i, j].ToString().PadLeft(4));
                }
                Console.WriteLine("\n");
            }
        }

        /// <summary>
        /// allows to read a txt file, with the matrix inside and return into a 2D tab
        /// </summary>
        /// <param name="matrixLength">Length of the matrix</param>
        /// <param name="file">name of the file with the extension .txt</param>
        /// <returns></returns>
        public static int[,] getMatrixUsingTxt(int matrixLength, string file)
        {
            int[,] G = new int[matrixLength, matrixLength];

            string[] lines = System.IO.File.ReadAllLines(file);

            for (int i = 0; i < lines.Length; i++)
            {
                string[] values = lines[i].Split(", ");

                for (int j = 0; j < lines.Length; j++)
                {
                    G[i, j] = Convert.ToInt32(values[j]);
                }
            }

            return G;
        }

        /// <summary>
        /// alows to display the matrix
        /// </summary>
        /// <param name="distance"></param>
        public static void printDistMatrix(int[,] distance)
        {
            Console.WriteLine("Distance Matrix for Shortest Distance between the nodes");
            Console.Write("\n");

            int verticesCount = distance.GetLength(0);

            for (int i = 0; i < verticesCount; ++i)
            {
                for (int j = 0; j < verticesCount; ++j)
                {
                    // IF THE DISTANCE TO THE NODE IS NOT DIRECTED THAN THE COST IN iNIFINITY  

                    if (distance[i, j] == INF)
                        Console.Write("INF".PadLeft(7));
                    else
                        Console.Write(distance[i, j].ToString().PadLeft(7));
                }

                Console.WriteLine();
            }

            Console.WriteLine("\n\n");
        }

        /// <summary>
        /// allows to display some sentences in order to explain distances between rooms
        /// </summary>
        /// <param name="distance"></param>
        /// <param name="rooms"></param>
        public static void printDistText(int[,] distance, Dictionary<int, string> rooms)
        {
            int verticesCount = distance.GetLength(0);
            int cpt = 1;

            for (int i = 0; i < verticesCount; ++i)
            {
                for (int j = i + 1; j < verticesCount; ++j)
                {
                    if (distance[i, j] == INF) { Console.WriteLine("{0}. {1} <-----> {2} \n >>>> {3} sec \n\n", cpt, rooms[i], rooms[j], "INF"); }
                    else { Console.WriteLine("{0}. {1} <-----> {2} \n >>>> {3} sec \n\n", cpt, rooms[i], rooms[j], distance[i, j]); }
                    cpt++;
                }
            }
        }

        /// <summary>
        /// Implementation of FloydWarshall algorithm in order to compute all pairs of distances between rooms
        /// </summary>
        /// <param name="graph">graph matrix</param>
        /// <returns>distance matrix</returns>
        public static int[,] FloydWarshall(int[,] graph)
        {
            int verticesCount = graph.GetLength(0);

            int[,] distance = new int[verticesCount, verticesCount];

            for (int i = 0; i < verticesCount; ++i)
                for (int j = 0; j < verticesCount; ++j)
                    distance[i, j] = graph[i, j];

            for (int k = 0; k < verticesCount; k++)
            {
                for (int i = 0; i < verticesCount; i++)
                {
                    for (int j = 0; j < verticesCount; j++)
                    {
                        if (distance[i, k] + distance[k, j] < distance[i, j])
                            distance[i, j] = distance[i, k] + distance[k, j];
                    }
                }
            }

            return distance;
        }

    }
}
