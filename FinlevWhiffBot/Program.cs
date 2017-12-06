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
                msg = Console.ReadLine();
                Client.Say(msg);
            }
            Client.EndThread();
        }
    }
}
