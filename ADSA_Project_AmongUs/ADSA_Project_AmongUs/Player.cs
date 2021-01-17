using System;
using System.Collections.Generic;
using System.Text;

namespace ADSA_Project_AmongUs
{
    public class Player
    {
        public static int playerCount = 1;
        public int id;
        public int score;


        public Player()
        {
            this.score = 0;
            this.id = playerCount;
            playerCount++;
        }

        public Player(int s)
        {
            this.score = s;
            this.id = playerCount;
            playerCount++;
        }


        /// <summary>
        /// Generate the new score
        /// No parameter
        /// </summary>
        /// <returns> the new score (integer) </returns>
        public void setNewScore()
        {
            Random rand = new Random();

            this.score = (rand.Next(0, 13) + this.score) / 2;
        }

        /// <summary>
        /// allow to describe a player 
        /// </summary>
        /// <returns>a string description</returns>
        public override string ToString()
        {
            return "Player n° " + this.id + " | Score: " + this.score;
        }

    }
}
