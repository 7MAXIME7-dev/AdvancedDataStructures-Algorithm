using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ADSA_Project_AmongUs
{
    class Tournament
    {
        static Random rand = new Random();

        public Node root { get; set; }

        /// <summary>
        /// This function allow us to get the height of a player
        /// </summary>
        /// <param name="player"></param>
        /// <returns> height of this Player (int)</returns>  
        static int height(Node N)
        {
            if (N == null)
                return 0;
            return N.height;
        }


        /// <summary>
        /// Utile function allow us to have the maximum between 2 integer
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns> a or b </returns>
        static int max(int a, int b)
        {
            return (a > b) ? a : b;
        }


        /// <summary>
        /// Allow us to do a rigth rotate subtree rooted with x (AVL)
        /// </summary>
        /// <param name="x"></param>
        /// <returns> Player new root </returns>
        static Node rightRotate(Node y)
        {
            Node x = y.left;
            Node T2 = x.right;

            // Perform rotation 
            x.right = y;
            y.left = T2;

            // Update heights 
            y.height = max(height(y.left),
                           height(y.right))
                       + 1;
            x.height = max(height(x.left),
                           height(x.right))
                       + 1;

            // Return new root 
            return x;
        }

        /// <summary>
        /// Allow us to do a left rotate subtree rooted with x (AVL)
        /// </summary>
        /// <param name="x"></param>
        /// <returns> Player new root </returns>
        static Node leftRotate(Node x)
        {
            Node y = x.right;
            Node T2 = y.left;

            // Perform rotation 
            y.left = x;
            x.right = T2;

            // Update heights 
            x.height = max(height(x.left),
                           height(x.right))
                       + 1;
            y.height = max(height(y.left),
                           height(y.right))
                       + 1;

            // Return new root 
            return y;
        }

        /// <summary>
        /// Get the balance factor of a Player node in order to do the rotations
        /// </summary>
        /// <param name="p"></param>
        /// <returns> The balance factor (int) </returns>
        static int getBalance(Node N)
        {
            if (N == null)
                return 0;
            return height(N.left) - height(N.right);
        }

        /// <summary>
        /// Allows to insert a player into the tournament/tree
        /// Find the place => insert elt => get balance => perform rotations
        /// </summary>
        /// <param name="node">the root of the tree</param>
        /// <param name="player">a new player to add</param>
        /// <returns>the new root of the tree</returns>
        public Node insert(Node node, Player player)
        {
            
            if (node == null)
            {
                Node newNode = new Node(player);              
                return newNode;
            }


            if (player.score == node.score)
            {
                node.players.Add(player);
                return node;
            }

            if (player.score < node.score)
                node.left = insert(node.left, player);
            else
                node.right = insert(node.right, player);

            /* 2. Update height of this  
              ancestor node */
            node.height = max(height(node.left),
                              height(node.right))
                          + 1;


            int balance = getBalance(node);

            // Left Left Case 
            if (balance > 1 && player.score < node.left.score)
                return rightRotate(node);

            // Right Right Case 
            if (balance < -1 && player.score > node.right.score)
                return leftRotate(node);

            // Left Right Case 
            if (balance > 1 && player.score > node.left.score)
            {
                node.left = leftRotate(node.left);
                return rightRotate(node);
            }

            // Right Left Case 
            if (balance < -1 && player.score < node.right.score)
            {
                node.right = rightRotate(node.right);
                return leftRotate(node);
            }

            return node;
        }


        /// <summary>
        ///  Find the node with minimum score value found in the tournament
        /// </summary>
        /// <param name="node">a subtree node or the root of the tree</param>
        /// <returns>the min score node</returns>
        static Node minValueNode(Node node)
        {
            Node current = node;

            /* loop down to find the 
           leftmost leaf */
            while (current.left != null)
                current = current.left;

            return current;
        }

        /// <summary>
        /// Allows to delete a player thanks to its score and id
        /// If there is just one player in the node => deletion of the node
        /// </summary>
        /// <param name="root">root of the tree</param>
        /// <param name="score">score of the player to delete</param>
        /// <param name="id">id of the player to delete</param>
        /// <returns></returns>
        public Node deleteNode(Node root, int score, int id)
        {
           
            if (root == null)
                return root;


            if (score < root.score)
                root.left = deleteNode(root.left, score, id);


            else if (score > root.score)
                root.right = deleteNode(root.right, score, id);


            else
            {

                if (root.players != null && root.players.Count > 1)
                {
                    foreach(Player p in root.players.ToList()) 
                    { 
                        if (p.id == id) 
                        { 
                            root.players.Remove(p); 
                        } 
                    }                   
                    return root;
                }


                if ((root.left == null) || (root.right == null))
                {
                    Node temp = root.left != null ? root.left : root.right;
   
                   
                    if (temp == null)
                    {
                        temp = root;
                        root = null;                   
                    }
                    else 
                    {
                        root = temp;  
                                     
                    }
                }
                else
                {

                    Node temp = minValueNode(root.right);


                    root.score = temp.score;
                    root.players = temp.players;
                    temp.players = null;
                    
       
                    root.right = deleteNode(root.right, temp.score, id);
                }
            }
   
            if (root == null)
                return root;


            root.height = max(height(root.left),
                            height(root.right))
                        + 1;

            int balance = getBalance(root);


            // Left Left Case 
            if (balance > 1 && getBalance(root.left) >= 0)
                return rightRotate(root);

            // Left Right Case 
            if (balance > 1 && getBalance(root.left) < 0)
            {
                root.left = leftRotate(root.left);
                return rightRotate(root);
            }

            // Right Right Case 
            if (balance < -1 && getBalance(root.right) <= 0)
                return leftRotate(root);

            // Right Left Case 
            if (balance < -1 && getBalance(root.right) > 0)
            {
                root.right = rightRotate(root.right);
                return leftRotate(root);
            }

            return root;
        }

    
        /// <summary>
        /// Allows to print the tree thanks to BTreePrinter Class find on internet
        /// </summary>
        public void Print()
        {
            this.root.Print();
        }

        /// <summary>
        /// Allows to initialize the tournament by filling  n  players inside.
        /// </summary>
        /// <param name="playerNumber"> n players (all the time 100) but can be less for unit testing</param>
        /// <returns>the tournament set</returns>
        public static Tournament initializeTournament(int playerNumber)
        {
            Player.playerCount = 0;
            Tournament tree = new Tournament();

            for (int i = 1; i <= playerNumber; i++)
            {

                tree.root = tree.insert(tree.root, new Player());
                //tree.Print();
            }

            return tree;
        }

        /// <summary>
        /// Allows to create the games tab based on number of Games in a round. 
        /// </summary>
        /// <param name="nbGame">number of Games in a round</param>
        /// <returns>Game table set</returns>
        public static Game[] initializeGames(int nbGame)
        {
            Game[] games = new Game[nbGame];

            for (int i = 0; i < nbGame; i++) 
            {
                Game temp = new Game();
                games[i] = temp;
                //Console.WriteLine(games[i]);
            }
            return games;
        }


        /// <summary>
        /// This function is used in the Main Program in order to play a round
        /// This is an adaptable function, it can be used for all steps of the games thinks to boolean inputs
        /// </summary>
        /// <param name="roundNumber">integer. allow to display the round</param>
        /// <param name="nbGame"> integer of Game by round</param>
        /// <param name="isRandomGame">true if we want random games</param>
        /// <param name="drop10Last">true if we want to drop the 10 last Players</param>
        /// <param name="updateTournament">true if we want to update scores of DB</param>
        /// <param name="printPodium">true if we want to display the podium</param>
        public void playGame(int roundNumber, int nbGame, bool isRandomGame, bool drop10Last, bool updateTournament, bool printPodium, bool reinitiatedRanking)
        {
            Game[] games = Tournament.initializeGames(nbGame);

            if (updateTournament)
            {
                Console.WriteLine("\n\n\n ----------------------------------------------------------------- ROUND " + roundNumber + " -----------------------------------------------------------------");              
            }

            if (isRandomGame)
            {
                games = randomSplitDatabase(this.root, games);
            }
            else
            {
                games = rankingSplitDatabase(this.root, games);
            }

            if (reinitiatedRanking)
            {
                this.resetScores(games);
            }

            if (updateTournament)
            {
                for (int i = 0; i < games.Length; i++) { Console.WriteLine("Game n°{0}{1}", i + 1, games[i]); }
                this.updateTournament(games);
                Console.WriteLine("\nAfter PLaying Game (Not Droped) : ");
                this.inOrder(this.root);
            }
        
            if (drop10Last)
            {
                this.dropLast10Players();

                Console.WriteLine("\nAfter Dropping : ");
                this.inOrder(this.root);
            }

            if (printPodium)
            {
                this.displayPodium(this.root, games);
            }
        }

        
        /// <summary>
        ///  This function browse each node 
        ///  foreach node, browse players List, add them in t (new tournament) 
        ///  Finally replace this.root by t.root in order to update the DB
        /// </summary>
        /// <param name="root"></param>
        /// <param name="t"></param>
        /// <param name="games">a Game table wich contains all players</param>
        public void updateScore(Tournament t, Game[] games)
        {
            if (root != null)
            {         
                for(int i=0; i < games.Length; i++)
                {
                    foreach (Player p in games[i].players)
                    {
                        p.setNewScore();
                        t.root = this.insert(t.root, p);
                    }
                }  
            }
            this.root = t.root;
        }

        public void setScoresTo0(Tournament t, Game[] games)
        {
            if (root != null)
            {
                for (int i = 0; i < games.Length; i++)
                {
                    foreach (Player p in games[i].players)
                    {
                        p.score = 0;
                        t.root = this.insert(t.root, p);
                    }
                }
            }
            this.root = t.root;
        }


        /// <summary>
        /// Implement the updateScore function
        /// </summary>
        public void updateTournament(Game[] games)
        {
            Tournament newGame = new Tournament();            
            this.updateScore(newGame, games);
        }

        public void resetScores(Game[] games)
        {
            Tournament newGame = new Tournament();
            this.setScoresTo0(newGame, games);
        }

        /// <summary>
        /// This is a util function. 
        /// It allows to check if games is full (all game has 10 players) 
        /// It allows to get index in order to fill games in Random / Ranking SplitDatabase functions
        /// </summary>
        /// <param name="games">a table wich contain n games, but games Tab is not completely full</param>
        /// <returns>(games is Full ? , index to start if we want to fill more) </returns>
        public Tuple<bool,int> isFullGames(Game[] games)
        {
            int cpt = 0;

            foreach(Game g in games){ if(g.players.Count == 10) { cpt++; } }

            if(cpt == games.Length) { return new Tuple<bool, int>(true,cpt); } else { return new Tuple<bool, int>(false, cpt); }
        }

        /// <summary>
        /// Allows us to get the game table based on random selecting player.
        /// Game 1 : 10 random player in the DB
        /// Game 2 : 10 random player in the DB
        /// etc...
        /// </summary>
        /// <param name="root">root of the node</param>
        /// <param name="games">a table wich contain n games, but all Game.List<Player> are empty</param>
        /// <returns></returns>
        public Game[] randomSplitDatabase(Node root, Game[] games)
        {
            if (root != null)
            {
                foreach(Player p in root.players)
                {
                    int tempRand = rand.Next(0, games.Length);

                    bool isFull = isFullGames(games).Item1;

                    while (games[tempRand].players.Count == 10 && !isFull)
                    {
                        tempRand = rand.Next(0, games.Length);
                    }
                    
                    if(games[tempRand].players.Count != 10)
                    {
                        games[tempRand].players.Add(p);
                    }
                                      
                }
                randomSplitDatabase(root.left, games);
                randomSplitDatabase(root.right, games);
            }
            return games;
        }


        /// <summary>
        /// Allows us to get the game table based on ranking.
        /// Game 1 : the 10 first players
        /// Game 2 : the 10 folowing
        /// etc...
        /// </summary>
        /// <param name="root">root of the table</param>
        /// <param name="games">a table wich contain n games, but all Game.List<Player> are empty</param>
        /// <returns></returns>
        public Game[] rankingSplitDatabase(Node root, Game[] games)
        {

            if (root != null)
            {
                rankingSplitDatabase(root.right, games);

                foreach (Player p in root.players)
                {
                    games[isFullGames(games).Item2].players.Add(p);
                }

                rankingSplitDatabase(root.left, games);
            }

            return games;
        }


        /// <summary>
        /// Allows us to drop the 10 last players folowing scores
        /// </summary>
        public void dropLast10Players()
        {
            int cpt = 0;

            while(cpt != 10)
            {
                Node node = this.root;

                if(node != null)
                {
                    while (node.left != null)
                    {
                        node = node.left;
                    }


                    this.root = this.deleteNode(this.root, node.score, node.players[0].id);
                    cpt++;                      
                    
                }
                else
                {
                    Console.WriteLine("Node NULL");
                    break;
                }
             
            }

        }


        /// <summary>
        /// Allows us to display nodes in an decreasing order 
        /// </summary>
        /// <param name="node">root of the tree</param>
        public void inOrder(Node node)
        {
            if (node != null)
            {
                inOrder(node.right);

                Console.WriteLine("  " + node.ToStringDetails());

                inOrder(node.left);
            }


        }



        /// <summary>
        /// This function allows us to Display the final Podium
        /// </summary>
        /// <param name="root">root of the tree</param>
        /// <param name="games">game tabb, contain all game of a round</param>
        public void displayPodium(Node root, Game[] games)
        {
            List<Player> podium = games[0].players;

            Console.WriteLine(

                "\n\n                             " + podium[0] + "                                    " +
                "\n                                _________________                                   " +
                "\n                                |       1       |                                   " +
                "\n                                |               |                                   " +
                "\n                                                                                    " +
                "\n     " + podium[1] + "                         " + podium[2] + "                    " +
                "\n  _______________________________               _______________________________     " +
                "\n  |              2              |               |              3              |     " +
                "\n  |                             |               |                             |     " +
                "\n                                                                                    " +
                "\n                                                                                    "

                );

        }


    }
}
