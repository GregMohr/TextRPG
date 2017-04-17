using System;
using System.Collections.Generic;

namespace TextRPG
{
    public class Program
    {// Potential additions: ; Adventure/Exploration/Puzzles;
     // Random Encounter generator; Loot; Inventory; Experience/Levelling
        public static List<Human> players = new List<Human>();      
        public static List<Monster> monsters = new List<Monster>();
        public static Random randomNum = new Random();
        public static void Main(string[] args)
        {
            // banner
            Console.WriteLine("##############################");
            Console.WriteLine("#                            #");
            Console.WriteLine("#     A Journey Begins...    #");
            Console.WriteLine("#                            #");
            Console.WriteLine("##############################");
            Console.WriteLine();
            // As is often the case, it starts in a cave...
            // With a dagger...
            // And a torch...
            // Character generation (auto-generate option)
            // Player input for character name and class selection
            // Random gneerate base stats
            // Player division of free points
            // Generate inventory
            bool running = true;
            while(running){
                Town newPlace = new Town();
            }
        }// Main end
    }
}
