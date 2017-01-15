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
        public Dictionary<string,Func<int>> actions = new Dictionary<string,Func<int>>();
        // if Action delegate can allow no parameters or return values, I should switch this Func<int> delegate as the health mod messages are handled within the action methods now
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
            System.Console.WriteLine("Selection(1-" + (cnt2 - 1) + "): ");
            int foe;           
            while(true){
                ConsoleKeyInfo result = Console.ReadKey(true);
                if ((result.KeyChar == '1') || (result.KeyChar == '2') || (result.KeyChar == '3'))//needs to adjust for fewer or more options
                {
                    var numVal = Char.GetNumericValue(result.KeyChar);
                    foe = (int)numVal;
                    break;
                }
            }                                  
            return Program.monsters[foe - 1];
        }
        public int Attack()
        {
            var foe = FoePrompt();
            int dmg = this.strength * 5;
            foe.health = foe.health - dmg;
            System.Console.WriteLine(this.name + " attacked " + foe.type + " for " + dmg + " damage!");
            foe.healthCheck();
            return dmg;
        }
        public void healthCheck(){
            if(this.health <= 0){
                System.Console.WriteLine(this.name + " DIED!");
                Program.players.Remove(this);
            }
        }
    }
    public class Wizard : Human
    {        
        public Wizard(string name) : base(name,3,25,3,50){
            actions.Add("Basic Attack", this.Attack);
            actions.Add("Fireball", this.Fireball);      
            actions.Add("Heal", this.Heal);                   
        }
        public int Heal(){
            this.intelligence += 10;
            System.Console.WriteLine(this.name + " healed themselves for 10 health!");
            return 10;
        }
        public int Fireball(){
            var foe = FoePrompt();            
            Random rng = new Random();
            int dmg = rng.Next(20,51);
            foe.health -= dmg;
            System.Console.WriteLine(this.name + " hit " + foe.type + " with a fireball, for " + dmg + " damage");  
            foe.healthCheck();                                  
            return dmg;
        }
    }
    public class Ninja : Human
    {
        public Ninja(string name) : base(name,3,3,175,100){
            actions.Add("Basic Attack", this.Attack);            
            actions.Add("Steal", this.Steal);
            actions.Add("GetAway", this.GetAway);            
        }
        public int Steal(){
            var foe = FoePrompt();     
            this.health += 10;
            foe.health -= 10;
            System.Console.WriteLine(this.name + " stole 10 health " + foe.type + " and increased their own!");   
            foe.healthCheck();                                             
            return 10;
        }
        public int GetAway(){
            Program.players.Remove(this);
            this.health -= 15;
            System.Console.WriteLine(this.name + " ran off and lost 15 health tripping over a branch.");   
            this.healthCheck();       
            return 15;
        }
    }
    public class Samurai : Human{
        static int cnt = 0;        
        public Samurai(string name) : base(name,3,3,3,200){
            actions.Add("Basic Attack", this.Attack);            
            actions.Add("DeathBlow", this.DeathBlow);
            actions.Add("Meditate", this.Meditate);     
            cnt++;
        }
        public int DeathBlow(){
            var foe = FoePrompt();
            if(foe.health < 50){
                foe.health = 0;
                System.Console.WriteLine(this.name + " struck a MIGHTY blow and finished off a " + foe.type + "!");
                foe.healthCheck();                
                return 50;
            } else {
                foe.health -= 20;
                System.Console.WriteLine(this.name + " devastated a " + foe.type + " for 20 health!");    
                foe.healthCheck();               
                return 20;            
            }
        }
        public int Meditate(){
            this.health = 200;
            System.Console.WriteLine(this.name + " meditated thmselves back to full health!");
            return 200;
        }
        public static int CountSamurai(){
            return cnt;
        }
    }
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
            System.Console.WriteLine(this.type + " attacked " + foe.name + " for " + dmg + " damage!");
            foe.healthCheck();            
            return dmg;
        }
        public void healthCheck(){
            if(this.health <= 0){
                System.Console.WriteLine(this.type + " DIED!");                
                Program.monsters.Remove(this);
            }
        }
    }
    public class Zombie : Monster
    {
        public Zombie() : base(7){
            type = "Zombie";
        }
    }
    public class Spider : Monster
    {
        public Spider() : base(3,30){
            type = "Spider";
        }
    }
    public class Skeleton : Monster
    {
        public Skeleton() : base(6,40){
            type = "Skeleton";
        }
    }
    public class Program
    {// Potential additions: Player character generation; Randomize encounter sizes; Adventure/Exploration/Puzzles;
     // Random Encounter generator; Loot; Inventory; Experience/Levelling
        public static List<Human> players = new List<Human>();      
        public static List<Monster> monsters = new List<Monster>();
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
                //init turns (randomize order of monsters and players arrays? or maybe use dexterities? Givemonster Dexterities?) 
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
                    if(entity is Human){
                        var player = (Human)entity;
                        
                        if(player is Wizard){
                            player = (Wizard)entity;
                        } else if(player is Ninja) {
                            player = (Ninja)entity;
                        } else {
                            player = (Samurai)entity;
                        }
                        //prompt user for action
                        int cnt1 = 1;
                        Dictionary<int,string> options = new Dictionary<int,string>();
                        System.Console.WriteLine();
                        System.Console.WriteLine(player.name + "(" + player.health + "), Please select action:");
                        foreach(KeyValuePair<string, Func<int>> entry in player.actions)
                        {
                            options.Add(cnt1,entry.Key);
                            System.Console.Write(cnt1 + " - " + entry.Key + "  ");
                            cnt1++;
                        }
                        System.Console.WriteLine();
                        System.Console.WriteLine("Selection(1-" + (cnt1 - 1) + "): ");
                        int actionIdx = int.Parse(Console.ReadLine());//refactor to Console.Read
                        string selection = options[actionIdx];
                        var selectedAction = player.actions[selection];
                        int healthMod = selectedAction();
                    } else {
                        var monster = (Monster)entity;
                        Random rng = new Random();
                        int idx = rng.Next(0,players.Count);
                        int dmg = monster.Attack(players[idx]);
                    }
                }
            } //game loop end
            if(players.Count > 0){
                System.Console.WriteLine("Players WIN!!!!");
            } else {
                System.Console.WriteLine("Monsters WIN!!!!");                
            }
        }
    }
}
