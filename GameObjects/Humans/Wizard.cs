using System;
// using System.Collections.Generic;

namespace TextRPG
{
    public class Wizard : Human
    {        
        public Wizard(string name) : base(name,3,25,3,50){
            playerActions.Add(new PlayerAction("1 - Basic Attack | ", this.Attack));
            playerActions.Add(new PlayerAction("2 - Fireball | ", this.Fireball));
            playerActions.Add(new PlayerAction("3 - Heal", this.Heal));
        }
        public int Heal(){
            this.intelligence += 10;
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(this.name + " healed themselves for 10 health!");
            Console.ForegroundColor = ConsoleColor.White;
            return 10;
        }
        public int Fireball(){
            var foe = FoePrompt();            
            Random rng = new Random();
            int dmg = rng.Next(20,51);
            foe.health -= dmg;
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(this.name + " hit " + foe.type + " with a fireball, for " + dmg + " damage"); 
            Console.ForegroundColor = ConsoleColor.White; 
            foe.healthCheck();                                  
            return dmg;
        }
    }
}