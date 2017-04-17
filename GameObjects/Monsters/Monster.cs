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
        public static void GenerateRandom(int qty){
            Program.monsters.Clear();
            string[] monsterTypes = {"Zombie", "Skeleton", "Spider"};
            for(var i = 1; i <= qty; i++){
                int pick = Program.randomNum.Next(0,3);
                Monster newAdd = MakeMonster(monsterTypes[pick]);
                Program.monsters.Add(newAdd);
            }
        }
        public static Monster MakeMonster(string type){
            if(type == "Zombie"){
                return new Zombie();
            } else if (type == "Skeleton"){
                return new Skeleton();
            } else if (type == "Spider"){
                return new Spider();
            } else {
                return new Monster();
            }
        }
    }
}