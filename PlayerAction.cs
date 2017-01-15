using System;
// using System.Collections.Generic;

namespace TextRPG
{
    public class PlayerAction
    {
        static int actionCount = 0;
        public int actionID;
        public string menuText;
        public Func<int> actionMethod;
        // if Action delegate can allow no params & no return values, I should switch this Func<int> delegate
        // as the health mod no longer needs to be returned and all actio nmethods can change to void return.
        public PlayerAction(string menuText, Func<int> actionMethod)
        {
            this.actionID = actionCount;
            this.menuText = menuText;
            this.actionMethod = actionMethod;
            actionCount++;
        }
    }
}