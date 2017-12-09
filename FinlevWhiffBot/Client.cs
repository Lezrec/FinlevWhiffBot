using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace FinlevWhiffBot
{
    public static class Client
    {
        private static TcpClient client;
        private static StreamReader reader;
        private static StreamWriter writer;
        private static string oathKey;
        private static readonly string LINE = Environment.NewLine;
        private const string channel = "lezrecop";
        private const string botName = "finlevwhiffbot";
        private static Thread runThread;
        private static int interval;
        private static int ticks;
        private static string lastMsg;
        private static Tuple<string,string> lastUserMsg;

        public static void Init()
        {
            interval = 100; //ms
            string destination = "C:/Users/Noah-PC/source/repos/oath.txt"; //we do not want laypeople to see the oath token monkaS
            oathKey = File.ReadAllText($"{destination}").Trim();
            client = new TcpClient();
            runThread = new Thread(Tick);
            ticks = 0;
            

        }

        private static void Tick()
        {
            if (ticks == 10)
            {
                JoinChannel();
            }
            
            DateTime prev = DateTime.Now;

            while((DateTime.Now.Millisecond + DateTime.Now.Second*1000 + DateTime.Now.Minute * 60000 + DateTime.Now.Hour* 3600000) - (prev.Millisecond + prev.Second*1000 + prev.Minute * 60000 + prev.Hour * 3600000) < interval)
            {

            }
            UpdateReader();
            ticks++;
            Tick();
        }

        private static void UpdateReader()
        {
            if (client.Available > 0 || reader.Peek() >= 0)
            {
                string msg = reader.ReadLine();
                lastMsg = msg;
                Console.WriteLine(msg);
                if (msg.Contains("!") && msg.Contains("#") && msg.Contains(".tmi.twitch.tv") && msg.Substring(1, "finlevwhiffbot".Length) != "finlevwhiffbot")
                {
                    lastUserMsg = GetUserAndMessage(msg);
                    Console.WriteLine(GetUserName(msg) + " said :" + GetMessage(msg));
                    
                }
            }
        }

        internal static void EndThread()
        {
            runThread.Abort();
        }


        public static bool TryConnect()
        {
            try
            {
                client.Connect("irc.chat.twitch.tv", 6667);
                reader = new StreamReader(client.GetStream());
                writer = new StreamWriter(client.GetStream());
                writer.AutoFlush = true; 

                WriteToIRC($"PASS {oathKey}{LINE}" +
                    $"NICK {botName}{LINE}");
                
                
            }
            catch(Exception e)
            {
                return false;
            }
            runThread.Start();
            return true;
        }

        public static void JoinChannel()
        {
            WriteToIRC($"JOIN #{channel}");
        }

        public static void WriteToIRC(string msg)
        {
            writer.WriteLine(msg);
            writer.Flush();
            Console.WriteLine(msg);
        }

        public static void Say(string toSay)
        {
            WriteToIRC($"PRIVMSG #{channel} :{toSay}");
        }

        internal static void TestJoinMessage()
        {
            Say("Hi finley ;)");
        }

        private static string GetUserName(string raw)
        {
            int start = raw.IndexOf("@") + 1; //after this
            int end = raw.IndexOf(".tmi.twitch.tv"); //before this
            int len = end - start;
            return raw.Substring(start, len);
        }

        private static string GetMessage(string raw)
        {
            string oneColon = raw.Substring(1);
            return oneColon.Substring(oneColon.IndexOf(":") + 1);
        }

        internal static Tuple<string, string> GetUserAndMessage(string raw)
        {
            return new Tuple<string, string>(GetUserName(raw), GetMessage(raw));
        }

        internal static Tuple<string,string> GetUserAndMessagFromLast()
        {
            return lastUserMsg;
        }
    }
}
