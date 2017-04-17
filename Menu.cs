using System;
using System.Collections.Generic;

namespace TextRPG
{
    public class Menu : List<MenuEntry>
    {
        public string headerText;
        public Menu(string headerText){
            this.headerText = headerText;
        }
        public void Generate(){
            Console.WriteLine();
            Console.WriteLine(headerText);
            foreach(MenuEntry entry in this){// for reliability I should make the menu nums auto gen to porperly correlate to the idx call
                Console.Write(entry.optionText + " | ");
            }
            Console.WriteLine();
            Console.Write("Selection(1-" + this.Count + "): ");
            List<int> optionKeys = new List<int>();//could I do some sort of weird recursive lambda or spread notation here to zero out count, or less 1, then count back up to init the list values?
            for(int optionNum = 1; optionNum <= this.Count; optionNum++){
               optionKeys.Add(optionNum + 48);
            }
            int actionIdx;           
            while(true){
                ConsoleKeyInfo result = Console.ReadKey(true);

                if (optionKeys.Contains(result.KeyChar))
                {
                    Console.WriteLine("We made it");
                    var numVal = Char.GetNumericValue(result.KeyChar);
                    actionIdx = (int)numVal;
                    Console.WriteLine();            
                    break;
                }
                Console.WriteLine();            
            }
            Console.WriteLine();
            
            var selectedAction = this[actionIdx - 1].optionMethod;
            selectedAction();
        }

    }
}