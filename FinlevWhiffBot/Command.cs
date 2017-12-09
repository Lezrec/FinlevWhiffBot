using System;
using System.Collections.Generic;
using System.Text;

namespace FinlevWhiffBot
{
    public abstract class Command : ITriggerable //ATTENTION GITHUB PEOPLE: THIS IS AN A B S T R A C T CLASS WHICH MEANS THIS IS A FRAME FOR COMMMANDS
    {
        public string Line { get; set; }

        public delegate void ToDo();

        public ToDo Executable { get; set; }


        public Command(string line, ToDo exec)
        {
            Line = line;
            Executable = exec;
        }

        public virtual bool CheckForTrigger(object obj)
        {
            return CheckForTrigger((string)obj);
        }

        public virtual bool CheckForTrigger(string check)
        {
            return true;
        }

        public virtual void OnTrigger()
        {
            Executable();
        }
    }
}
