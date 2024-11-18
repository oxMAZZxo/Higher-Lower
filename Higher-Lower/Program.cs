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
            session = new Session();
            Console.ReadLine();
        }
    }
}