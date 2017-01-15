using System;
using System.Collections.Generic;

namespace TextRPG
{

    public class Program
    {// Potential additions: Player character generation; Randomize encounter sizes; Adventure/Exploration/Puzzles;
     // Random Encounter generator; Loot; Inventory; Experience/Levelling
        public static List<Human> players = new List<Human>();      
        public static List<Monster> monsters = new List<Monster>();
        // public static List<Object> entities = new List<Object>();
        public static void Main(string[] args)
        {
            //init game objects
            Human jed = new Wizard("Jed");
            Human hank = new Ninja("Hank");
            Human frank = new Samurai("Frank");
            players.Add(jed);
            players.Add(hank);
            players.Add(frank);

            Monster zomzom = new Zombie();
            Monster skelly = new Skeleton();
            Monster spiddy = new Spider();
            monsters.Add(zomzom);
            monsters.Add(skelly);
            monsters.Add(spiddy);
            
            //game loop          
            while(players.Count > 0 && monsters.Count > 0){
                // init turns (randomize order of monsters and players arrays? or maybe use dexterities? Give monster Dexterities?) 
                List<Object> entities = new List<Object>();
                Random rnd = new Random();
                int toss = rnd.Next(0,20);
                int mpIdx = 0;
                if(toss % 2 == 0){
                    //players go first
                    while(mpIdx < players.Count && mpIdx < monsters.Count){
                        entities.Add(players[mpIdx]);
                        entities.Add(monsters[mpIdx]);
                        mpIdx++;
                    }
                    while(mpIdx < players.Count){
                        entities.Add(players[mpIdx]);
                        mpIdx++;                    
                    }
                    while(mpIdx < monsters.Count){
                        entities.Add(monsters[mpIdx]);                    
                        mpIdx++;
                    }
                } else {
                    //monsters go first
                    while(mpIdx < players.Count && mpIdx < monsters.Count){
                        entities.Add(monsters[mpIdx]);                    
                        entities.Add(players[mpIdx]);
                        mpIdx++;
                    }
                    while(mpIdx < players.Count){
                        entities.Add(players[mpIdx]);
                        mpIdx++;                    
                    }
                    while(mpIdx < monsters.Count){
                        entities.Add(monsters[mpIdx]);                    
                        mpIdx++;
                    }
                }
                //execute round
                foreach(object entity in entities){
                    if(players.Count > 0 && monsters.Count > 0){
                        if(entity is Human){
                            var player = (Human)entity;
                            
                            if(player is Wizard){
                                player = (Wizard)entity;
                            } else if(player is Ninja) {
                                player = (Ninja)entity;
                            } else {
                                player = (Samurai)entity;
                            }
                            if(!players.Contains(player)){
                                continue;
                            }
                            //prompt user for action
                            System.Console.WriteLine();
                            System.Console.WriteLine(player.name + "(" + player.health + "), Please select action:");
                            //player's actions menu
                            foreach(PlayerAction pAction in player.playerActions){
                                Console.Write(pAction.menuText);
                            }
                            System.Console.WriteLine();
                            System.Console.Write("Selection(1-" + player.playerActions.Count + "): ");
                            int actionIdx = int.Parse(Console.ReadLine());//refactor to Console.Read
                            System.Console.WriteLine();
                            
                            var selectedAction = player.playerActions[actionIdx - 1].actionMethod;
                            int healthMod = selectedAction();
                        } else {
                            if(monsters.Count > 0){
                                var monster = (Monster)entity;
                                if(!monsters.Contains(monster)){
                                    continue;
                                }
                                Random rng = new Random();
                                int idx = rng.Next(0,players.Count);
                                int dmg = monster.Attack(players[idx]);
                            }
                        }
                    }
                }
            } //game loop end
            if(players.Count > 0){
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine("Players WIN!!!!");
                Console.ForegroundColor = ConsoleColor.White;
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Monsters WIN!!!!");     
                Console.ForegroundColor = ConsoleColor.White;           
            }
            Console.Read();
        }
    }
}
