using System;
using System.Xml;
using Microsoft.VisualBasic;

namespace Higher_Lower
{
    public class Program
    {
        private static Session? session;

        static void Main(string[] args)
        {
            Console.SetBufferSize(1000,800);
            session = new Session();
            Console.ReadLine();
        }
    }
}