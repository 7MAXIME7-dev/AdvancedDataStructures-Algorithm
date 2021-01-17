using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace ADSA_Project_AmongUs
{
    public class Node
    {
        public int score;
        public Node left;
        public Node right;
        public int height;
        public List<Player> players;

        public Node(Player player)
        {
            this.score = player.score;
            this.left = null;
            this.right = null;
            this.height = 1;                            
            players = new List<Player>().ToList();
            players.Add(player);
        }

        public override string ToString()
        {
            return String.Format("{0}(n:{1})", this.score, this.players.Count);
        }

        public string ToStringDetails()
        {     
            string ids = "";
            foreach(Player p in players) { ids = ids + " " + p.id; }

            return String.Format("Score : {0} | (id players:{1})\n", this.score, ids);
        }

    }
}
