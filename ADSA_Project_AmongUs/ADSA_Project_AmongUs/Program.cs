using System;
using System.Collections.Generic;

namespace ADSA_Project_AmongUs
{
    class Program
    {
        static void step1()
        {
            Console.WriteLine("Welcome To Among Us World !\n");

            Tournament tree = Tournament.initializeTournament(100);

            Console.WriteLine("Initialization : ");
            tree.Print();
        
                
            // Total number of Game
            int nbGame = 10;

            // First part :  3 random games
            Console.WriteLine("\n\n\n\n\n ------------------------------------------------------ First Part : 3 Random Games ----------------------------------------------------------------- ");

            // ------------- SET PARAMS --------------
            bool isRandomGame = true, drop10Last = true, updateTournament = true, printPodium = false, reinitiatedRanking = false;


            for (int i = 1; i <= 3; i++)
            {
                tree.playGame(i, nbGame, isRandomGame, drop10Last, updateTournament, printPodium, reinitiatedRanking);
                nbGame--;

                tree.Print();
            }

            

            // Second part : 6 ranking games
            Console.WriteLine("\n\n\n\n\n ------------------------------------------------------------ 6 Ranking Games ----------------------------------------------------------------- ");

            // ------------- SET PARAMS --------------
            isRandomGame = false; drop10Last = true; updateTournament = true; printPodium = false; reinitiatedRanking = false;


            for (int i = 1; i <= 6; i++)
            {
                tree.playGame(i, nbGame, isRandomGame, drop10Last, updateTournament, printPodium, reinitiatedRanking);
                nbGame--;

                tree.Print();
            }

            


            // Final part : play 5 games with the last 10 Players
            Console.WriteLine("\n\n\n\n\n ---------------------------------------------------- 5 Games With The 10 Last Players ----------------------------------------------------------------- ");

            // ------------- SET PARAMS --------------
            isRandomGame = false; drop10Last = false; updateTournament = false; printPodium = false; reinitiatedRanking = true;

            tree.playGame(1, nbGame, isRandomGame, drop10Last, updateTournament, printPodium, reinitiatedRanking);
            reinitiatedRanking = false;
            updateTournament = true;

            for (int i = 1; i <= 5; i++)
            {
                tree.playGame(i, nbGame, isRandomGame, drop10Last, updateTournament, printPodium, reinitiatedRanking);

                tree.Print();
            }


            Console.WriteLine("\n\n\n\n\n ---------------------------------------------------------------- RESULTS ----------------------------------------------------------------- ");
            Console.WriteLine("\n\n\n ------------ TOP 10 -------------\n");

            tree.inOrder(tree.root);


            Console.WriteLine("\n\n\n ------------ Podium -------------\n");

            // ------------- SET PARAMS --------------
            isRandomGame = false; drop10Last = false; updateTournament = false; printPodium = true; reinitiatedRanking = false;

            tree.playGame(0, nbGame, isRandomGame, drop10Last, updateTournament, printPodium, reinitiatedRanking);

        }

        static void step2()
        {
            Step2Graph graph = new Step2Graph(10);

            graph.AddEdge(0, 1);
            graph.AddEdge(0, 4);
            graph.AddEdge(0, 5);

            graph.AddEdge(1, 0);
            graph.AddEdge(1, 2);
            graph.AddEdge(1, 6);

            graph.AddEdge(2, 1);
            graph.AddEdge(2, 3);
            graph.AddEdge(2, 7);

            graph.AddEdge(3, 2);
            graph.AddEdge(3, 4);
            graph.AddEdge(3, 8);

            graph.AddEdge(4, 0);
            graph.AddEdge(4, 3);
            graph.AddEdge(4, 9);

            graph.AddEdge(5, 0);
            graph.AddEdge(5, 7);
            graph.AddEdge(5, 8);

            graph.AddEdge(6, 1);
            graph.AddEdge(6, 8);
            graph.AddEdge(6, 9);

            graph.AddEdge(7, 2);
            graph.AddEdge(7, 5);
            graph.AddEdge(7, 9);

            graph.AddEdge(8, 3);
            graph.AddEdge(8, 5);
            graph.AddEdge(8, 6);

            graph.AddEdge(9, 4);
            graph.AddEdge(9, 6);
            graph.AddEdge(9, 7);

            Console.WriteLine("Graph representation:");
            graph.PrintAdjanceyList();

            Console.WriteLine();
            Console.WriteLine("Impostors may be:");
            graph.set_of_impostors();

        }

        static void step3()
        {


            Console.WriteLine("\n\nCrewmate Graph Adjency Matrix --------------------------------------------------------------------------------------------------- \n");
            int[,] G_Crewmate = Step3MapImpCrew.getMatrixUsingTxt(14, "GraphCrewMate.txt");
            Step3MapImpCrew.displayMatrix(G_Crewmate);


            int[,] distanceMatrixCrew = Step3MapImpCrew.FloydWarshall(G_Crewmate);

            Console.WriteLine("\n\nCrewmate Distance Matrix : Time to travel for any pair of rooms\n");
            Step3MapImpCrew.displayMatrix(distanceMatrixCrew);

            Step3MapImpCrew.printDistText(distanceMatrixCrew, Step3MapImpCrew.rooms);



            Console.WriteLine("\n\nImpostor Graph Adjency Matrix  --------------------------------------------------------------------------------------------------- \n");
            int[,] G_Impostor = Step3MapImpCrew.getMatrixUsingTxt(15, "GraphImpostor.txt");
            Step3MapImpCrew.displayMatrix(G_Impostor);

            int[,] distanceMatrixImpostor = Step3MapImpCrew.FloydWarshall(G_Impostor);

            Console.WriteLine("\n\nImpostor Distance Matrix : Time to travel for any pair of rooms\n");
            Step3MapImpCrew.displayMatrix(distanceMatrixImpostor);

            Step3MapImpCrew.printDistText(distanceMatrixImpostor, Step3MapImpCrew.rooms);
        }

        static void step4()
        {
            Step4Hamiltonian hamiltonian = new Step4Hamiltonian();

            int[,] graph1 = Step3MapImpCrew.getMatrixUsingTxt(14, "hamiltonianGraph.txt");

            Step3MapImpCrew.displayMatrix(graph1);

            Console.WriteLine("\n\nHamiltonian Cycles --------------------------------------------------- ");
            for (int source = 0; source < 14; source++) { hamiltonian.hamCycle(graph1, source); }


            Console.WriteLine("\n\nHamiltonian Path --------------------------------------------------- ");
            for (int source = 0; source < 14; source++) { hamiltonian.hamPath(graph1, source); }
        }

        

        static void Main(string[] args)
        {
            int res = 5;


            while (true)
            {
                Console.WriteLine("Info Steps : ");
                Console.WriteLine("Step 1: To organize the tournament");
                Console.WriteLine("Step 2: Professor Layton < Guybrush Threepwood < You");
                Console.WriteLine("Step 3: I don't see him, but I can give proofs he vents!");
                Console.WriteLine("Step 4: Secure the last tasks");

                Console.Write("\n\nSelect a Step (1, 2, 3 or 4) | (exit 0):   ");
                res = Convert.ToInt32(Console.ReadLine());
                if(res == 1 || res==2 || res==3 || res == 4 || res==0) { break; }
            }

            switch (res)
            {
                case 0:
                    break;
                case 1:
                    Console.WriteLine("Welcome to STEP 1");
                    step1();
                    Main(args);
                    break;
                case 2:
                    Console.WriteLine("Welcome to STEP 2");
                    step2();
                    Main(args);
                    break;
                case 3:
                    Console.WriteLine("Welcome to STEP 3");
                    step3();
                    Main(args);
                    break;
                case 4:
                    Console.WriteLine("Welcome to STEP 4");
                    step4();
                    Main(args);
                    break;
            }

        }
    }
}
