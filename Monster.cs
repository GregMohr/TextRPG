using System;
namespace TextRPG
{
    public class Monster
    {
        public string type;
        public int strength;
        public int health;
        public Monster(int strength=3, int health=50){
            this.strength = strength;
            this.health = health;
        }
        public int Attack(Human foe){
            int dmg = this.strength * 4;
            foe.health -= dmg;
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(this.type + " attacked " + foe.name + " for " + dmg + " damage!");
            Console.ForegroundColor = ConsoleColor.White;
            foe.healthCheck();            
            return dmg;
        }
        public void healthCheck(){
            if(this.health <= 0){
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(this.type + " DIED!");        
                Console.ForegroundColor = ConsoleColor.White;            
                Program.monsters.Remove(this);
            }
        }
    }
}