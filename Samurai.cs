using System;

namespace TextRPG
{

    public class Samurai : Human{
        static int cnt = 0;        
        public Samurai(string name) : base(name,3,3,3,200){
            playerActions.Add(new PlayerAction("1 - Basic Attack | ", this.Attack));
            playerActions.Add(new PlayerAction("2 - Death Blow | ", this.DeathBlow));
            playerActions.Add(new PlayerAction("3 - Meditate", this.Meditate));            
            cnt++;
        }
        public int DeathBlow(){
            var foe = FoePrompt();
            if(foe.health < 50){
                foe.health = 0;
                Console.ForegroundColor = ConsoleColor.Yellow;
                System.Console.WriteLine(this.name + " struck a MIGHTY blow and finished off a " + foe.type + "!");
                Console.ForegroundColor = ConsoleColor.White;
                foe.healthCheck();                
                return 50;
            } else {
                foe.health -= 20;
                Console.ForegroundColor = ConsoleColor.Green;
                System.Console.WriteLine(this.name + " devastated a " + foe.type + " for 20 health!");  
                Console.ForegroundColor = ConsoleColor.White;  
                foe.healthCheck();               
                return 20;            
            }
        }
        public int Meditate(){
            this.health = 200;
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(this.name + " meditated thmselves back to full health!");
            Console.ForegroundColor = ConsoleColor.White;
            return 200;
        }
        public static int CountSamurai(){
            return cnt;
        }
    }
}