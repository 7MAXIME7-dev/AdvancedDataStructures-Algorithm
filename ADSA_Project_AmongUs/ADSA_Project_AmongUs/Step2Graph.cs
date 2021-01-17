using System;
using System.Collections.Generic;
using System.Text;

namespace ADSA_Project_AmongUs
{
    class Step2Graph
    {
        public LinkedList<int>[] linkedListArray;

        public Step2Graph(int v)
        {
            linkedListArray = new LinkedList<int>[v];
        }

        //Prints the probable impostors
        public void set_of_impostors()
        {
            for (int i = 0; i < linkedListArray.Length; i++)
            {
                //It can be 1, 4, 5
                if (i == 0 || i == 1 || i == 4 || i == 5)
                {
                    continue;
                }
                foreach (var item in linkedListArray[i])
                {
                    if (item == 1)
                    {
                        Console.WriteLine("Probable impostors : " + (i, 4));
                        Console.WriteLine("Probable impostors : " + (i, 5));
                    }
                    if (item == 4)
                    {
                        Console.WriteLine("Probable impostors : " + (i, 1));
                        Console.WriteLine("Probable impostors : " + (i, 5));
                    }
                    if (item == 5)
                    {
                        Console.WriteLine("Probable impostors : " + (i, 1));
                        Console.WriteLine("Probable impostors : " + (i, 4));
                    }
                }
            }
        }

        public void AddEdge(int u, int v, bool blnBiDir = true)
        {
            if (linkedListArray[u] == null)
            {
                linkedListArray[u] = new LinkedList<int>();
                linkedListArray[u].AddFirst(v);
            }
            else
            {
                var last = linkedListArray[u].Last;
                linkedListArray[u].AddAfter(last, v);
            }
        }

        public void PrintAdjanceyList()
        {
            //Taversing over each of the vertices
            for (int i = 0; i < linkedListArray.Length; i++)
            {
                //Printing the vertices
                Console.Write("Player n°{0} has seen player ", i);

                //Printing the linked list of vertex
                foreach (var item in linkedListArray[i])
                {
                    Console.Write(item + ",");
                }

                Console.WriteLine();
            }
        }
    }
}

