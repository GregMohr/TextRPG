using System;
using System.Collections.Generic;

namespace TextRPG
{
    public class Human
    {
        public string name;
        public int strength;
        public int intelligence;
        public int dexterity;
        public int health;
        public List<PlayerAction> playerActions = new List<PlayerAction>();
        public Human(string name, int strength=3, int intelligence=3, int dexterity=3, int health=100)
        {
            this.name = name;
            this.strength = strength;
            this.intelligence = intelligence;
            this.dexterity = dexterity;
            this.health = health;
        }
        public Monster FoePrompt(){
            int cnt2 = 1;
            System.Console.WriteLine("Please select opponent:");
            foreach(Monster monster in Program.monsters){
                System.Console.Write(cnt2 + " - " + monster.type + "(" + monster.health + ")  ");
                cnt2++;
            }
            System.Console.WriteLine();
            System.Console.Write("Selection(1-" + (cnt2 - 1) + "): ");
            
            int foe;           
            while(true){
                ConsoleKeyInfo result = Console.ReadKey(true);
                if ((result.KeyChar == '1') || (result.KeyChar == '2') || (result.KeyChar == '3'))//needs to adjust for fewer or more options
                {
                    var numVal = Char.GetNumericValue(result.KeyChar);
                    foe = (int)numVal;
                    System.Console.WriteLine();            
                    break;
                }
                System.Console.WriteLine();            
            }                                  
            return Program.monsters[foe - 1];
        }
        public int Attack()
        {
            var foe = FoePrompt();
            int dmg = this.strength * 5;
            foe.health = foe.health - dmg;
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(this.name + " attacked " + foe.type + " for " + dmg + " damage!");
            Console.ForegroundColor = ConsoleColor.White;
            foe.healthCheck();
            return dmg;
        }
        public void healthCheck(){
            if(this.health <= 0){
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine(this.name + " DIED!");
                Console.ForegroundColor = ConsoleColor.White;
                Program.players.Remove(this);
            }
        }
    }
}