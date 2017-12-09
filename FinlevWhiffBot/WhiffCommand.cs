using System;
using System.Collections.Generic;
using System.Text;

namespace FinlevWhiffBot
{
    public class WhiffCommand : Command
    {
        public WhiffCommand(string line, ToDo exec) : base(line, exec)
        {
            CheckForTrigger(line);
        }

        public override bool CheckForTrigger(object obj)
        {
            return base.CheckForTrigger(obj);
        }

        public override bool CheckForTrigger(string check)
        {
            Line = check;
            try
            {
                if (check.Substring(0, 6) == "!whiff")
                {
                    //at this moment it's just "!whiff" nothing else
                    if (check == check.Substring(0, 6))
                    {
                        OnTrigger();
                        return true;
                    }
                    else return false;
                }
                return false;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public Tuple<bool, string> CheckForTrigger(char[] check) //includes a reason
        {
            return null;
        }

        public override void OnTrigger()
        {
            base.OnTrigger();
            Client.Say("Whiffed!");
        }
    }
}
