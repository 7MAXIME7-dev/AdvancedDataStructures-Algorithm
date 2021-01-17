using System;
using System.Collections.Generic;
using System.Text;

namespace ADSA_Project_AmongUs
{
    class Game
    {
        public List<Player> players;

        public Game()
        {
            this.players = new List<Player>();
        }


        public override string ToString()
        {
            string playersStr = " Composition ID Players:\n";

            foreach(Player p in this.players)
            {
                playersStr = playersStr + p.id + "(" + p.score + " pts)" + " | ";
            }

            return playersStr;
        }
    }
}
