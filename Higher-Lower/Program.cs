using System;

namespace Higher_Lower
{
    public class Program
    {
        private static Session? session;

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1200);
            Console.InputEncoding  = System.Text.Encoding.GetEncoding(1200);
            Console.SetBufferSize(1000,800);
            session = new Session();
            session.StartGame();
        }
    }
}