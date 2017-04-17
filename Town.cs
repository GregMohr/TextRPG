using System;
using System.Collections.Generic;

namespace TextRPG
{
    public class Town
    {
        Menu townMenu = new Menu("You are in a town. You can...");

        public Town(){
                townMenu.Add(new MenuEntry("1 - Adventure", Town.Encounter));
                townMenu.Add(new MenuEntry("2 - Heal", Town.Heal));
                townMenu.Add(new MenuEntry("3 - Gamble", Town.Gamble));
                townMenu.Add(new MenuEntry("4 - Shop", Town.Shop));            
                townMenu.Generate();
        }
        public static void Encounter()
        {
            // Encounter Start
            // Adventure alone or hire party members?
            // additional party members increase encounter party sizes
            // generate a pool of random npc to choose from
            // Generate Foes
            // Determine enemyCount
            // Monster.GenerateRandom(enemyCount)
            Human jed = new Wizard("Jed");
            Human hank = new Ninja("Hank");
            Human frank = new Samurai("Frank");
            Program.players.Add(jed);
            Program.players.Add(hank);
            Program.players.Add(frank);

            Monster zomzom = new Zombie();
            Monster skelly = new Skeleton();
            Monster spiddy = new Spider();
            Program.monsters.Add(zomzom);
            Program.monsters.Add(skelly);
            Program.monsters.Add(spiddy);
        
            //encounter loop          
            while(Program.players.Count > 0 && Program.monsters.Count > 0){
                // init turns (randomize order of monsters and players arrays? or maybe use dexterities? Give monster Dexterities?) 
                List<Object> entities = new List<Object>();
                Random rnd = new Random();
                int toss = rnd.Next(0,20);
                int mpIdx = 0;
                if(toss % 2 == 0){
                    // players go first
                    while(mpIdx < Program.players.Count && mpIdx < Program.monsters.Count){
                        entities.Add(Program.players[mpIdx]);
                        entities.Add(Program.monsters[mpIdx]);
                        mpIdx++;
                    }
                    while(mpIdx < Program.players.Count){
                        entities.Add(Program.players[mpIdx]);
                        mpIdx++;                    
                    }
                    while(mpIdx < Program.monsters.Count){
                        entities.Add(Program.monsters[mpIdx]);                    
                        mpIdx++;
                    }
                } else {
                    //monsters go first
                    while(mpIdx < Program.players.Count && mpIdx < Program.monsters.Count){
                        entities.Add(Program.monsters[mpIdx]);                    
                        entities.Add(Program.players[mpIdx]);
                        mpIdx++;
                    }
                    while(mpIdx < Program.players.Count){
                        entities.Add(Program.players[mpIdx]);
                        mpIdx++;                    
                    }
                    while(mpIdx < Program.monsters.Count){
                        entities.Add(Program.monsters[mpIdx]);                    
                        mpIdx++;
                    }
                }
                //execute round
                foreach(object entity in entities){
                    if(Program.players.Count > 0 && Program.monsters.Count > 0){
                        if(entity is Human){
                            var player = (Human)entity;
                            
                            if(player is Wizard){
                                player = (Wizard)entity;
                            } else if(player is Ninja) {
                                player = (Ninja)entity;
                            } else {
                                player = (Samurai)entity;
                            }
                            if(!Program.players.Contains(player)){
                                continue;
                            }
                            //prompt user for action
                            Console.WriteLine();
                            Console.WriteLine(player.name + "(" + player.health + "), Please select action:");
                            //player's actions menu
                            foreach(PlayerAction pAction in player.playerActions){
                                Console.Write(pAction.menuText);
                            }
                            Console.WriteLine();
                            Console.Write("Selection(1-" + player.playerActions.Count + "): ");

                            int actionIdx;           
                            while(true){
                                ConsoleKeyInfo result = Console.ReadKey(true);
                                if ((result.KeyChar == '1') || (result.KeyChar == '2') || (result.KeyChar == '3'))//needs to adjust for fewer or more options
                                {
                                    var numVal = Char.GetNumericValue(result.KeyChar);
                                    actionIdx = (int)numVal;
                                    Console.WriteLine();            
                                    break;
                                }
                                Console.WriteLine();            
                            }
                            Console.WriteLine();
                            
                            var selectedAction = player.playerActions[actionIdx - 1].actionMethod;
                            int healthMod = selectedAction();
                        } else {
                            if(Program.monsters.Count > 0){
                                var monster = (Monster)entity;
                                if(!Program.monsters.Contains(monster)){
                                    continue;
                                }
                                Random rng = new Random();
                                int idx = rng.Next(0,Program.players.Count);
                                int dmg = monster.Attack(Program.players[idx]);
                            }
                        }
                    }
                }
            } //game loop end
            if(Program.players.Count > 0){
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine();                  
                Console.WriteLine("##############################");
                Console.WriteLine("#                            #");
                Console.WriteLine("#       Players WIN!!!!      #");
                Console.WriteLine("#                            #");
                Console.WriteLine("##############################");
                Console.WriteLine();  
                Console.ForegroundColor = ConsoleColor.White;
            } else {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine();                  
                Console.WriteLine("##############################");
                Console.WriteLine("#                            #");
                Console.WriteLine("#       Monsters WIN!!!      #");
                Console.WriteLine("#                            #");
                Console.WriteLine("##############################");
                Console.WriteLine();  
                Console.ForegroundColor = ConsoleColor.White;           
            }
            Console.Read();
        }// Encounter method end
        public static void Heal(){
            Console.WriteLine("Heal options - Coming Soon");
        }// Heal method end
        public static void Gamble(){
            Console.WriteLine("Casino - Coming Soon");
        }// Casino method end
        public static void Shop(){
            Console.WriteLine("Shop - Coming Soon");
        }// Shop method end
    }
}