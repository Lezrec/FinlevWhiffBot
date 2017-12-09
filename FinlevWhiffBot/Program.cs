using System;
using System.Threading;
namespace FinlevWhiffBot
{
    class Program
    {
        static void Main(string[] args)
        {
            Client.Init();
            Client.TryConnect();
            //Client.JoinChannel();
            string msg = null;
            while(msg != ">exit")
            {
                if (msg != null) Client.Say(msg);
                msg = Console.ReadLine();
            }
            Client.EndThread();
        }
    }
}
