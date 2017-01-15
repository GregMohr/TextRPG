using System;

namespace TextRPG
{
    public class Ninja : Human
    {
        public Ninja(string name) : base(name,3,3,175,100){
            playerActions.Add(new PlayerAction("1 - Basic Attack | ", this.Attack));            
            playerActions.Add(new PlayerAction("2 - Steal | ", this.Steal));
            playerActions.Add(new PlayerAction("2 - Get Away", this.GetAway));
        }
        public int Steal(){
            var foe = FoePrompt();     
            this.health += 10;
            foe.health -= 10;
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine(this.name + " stole 10 health " + foe.type + " and increased their own!");   
            Console.ForegroundColor = ConsoleColor.White;
            foe.healthCheck();                                             
            return 10;
        }
        public int GetAway(){
            Program.players.Remove(this);
            this.health -= 15;
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine(this.name + " ran off and lost 15 health tripping over a branch.");   
            Console.ForegroundColor = ConsoleColor.White;
            this.healthCheck();       
            return 15;
        }
    }
}