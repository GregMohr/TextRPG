using System;

namespace TextRPG
{
    public class MenuEntry
    {
        public string optionText;
        public Action optionMethod;
        public MenuEntry(string optionText, Action optionMethod){
            this.optionText = optionText;
            this.optionMethod = optionMethod;
        }
    }
}