using System;

namespace Higher_Lower
{
    public class Program
    {
        private static Session? session;
        public static readonly string leaderboardFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\leaderboard.csv";

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.GetEncoding(1200);
            Console.InputEncoding  = System.Text.Encoding.GetEncoding(1200);
            string? menuChoice = null;
            bool valid = false;
            bool exit = false;
            FileInfo leaderboardFile = new FileInfo(leaderboardFilePath);
            if(!leaderboardFile.Exists)
            {
                File.Create(leaderboardFilePath);
            }
            
            while(!exit)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine("V===================V");
                    Console.WriteLine("1. Play Higher/Lower.");            
                    Console.WriteLine("2. How To Play.");            
                    Console.WriteLine("3. Leaderboards.");            
                    Console.WriteLine("4. Exit.");
                    Console.Write("Enter one of the options above and press enter: ");
                    menuChoice = Console.ReadLine();
                    valid = ValidateUserInput(menuChoice, "1","2","3","4");
                    if(valid == false)
                    {
                        Console.WriteLine("You entered an invalid answer, you will be prompted again in 2 seconds");
                        Thread.Sleep(2000);
                    }
                }while(!valid);
                switch(menuChoice)
                {
                    case "1":
                    string? playerName;
                    Console.Write("Enter your name: ");
                    playerName = Console.ReadLine();
                    if(playerName == null) {playerName = "DefaultName";}
                    session = new Session(playerName);
                    session.StartGame();
                    break;
                    case "2":
                    DemonstrateGame();
                    break;
                    case "3":
                    Console.Clear();
                    ShowLeaderboard();
                    break;
                    case "4":
                    exit = true;
                    break;
                }
            }
        }

        static void DemonstrateGame()
        {
            Console.WriteLine("Demonstrating Game...");
            Thread.Sleep(1000);
        }

        static async void ShowLeaderboard()
        {
            Console.WriteLine("________Leaderboard________");
            string? currentLine;
            StreamReader streamReader = new StreamReader(leaderboardFilePath);
            while(!streamReader.EndOfStream)
            {
                currentLine = await streamReader.ReadLineAsync();
                if(currentLine != null)
                {
                    string[] data = currentLine.Split(",");
                    Console.WriteLine($"Name: {data[0]}, High Score: {data[1]}, Number of Resets: {data[2]}, Date & Time: {data[3]} & {data[4]}");
                }
            }
            Console.WriteLine("____Press Enter to Exit____");
            Console.ReadLine();
        }

        /// <summary>
        /// Will validate users input based on option parameter
        /// </summary>
        /// <returns>Returns true if input is valid</returns>
        public static bool ValidateUserInput(string? userInput, params string[] options)
        {
            foreach(string option in options)
            {
                if(userInput == option)
                {
                    return true;
                }
            }
            return false;
        }
    
        public static string GetCurrentDate()
        {
            string CurrentDate;
            string[] TempDate = DateTime.Now.ToString().Split(' ');
            string[] DDMMYY = TempDate[0].Split('/');
            CurrentDate = DDMMYY[0] + "-" + DDMMYY[1] + "-" + DDMMYY[2];

            return CurrentDate;
        }

        public static string GetTimeOfDay()
        {
            string Time = $"Time {DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss")}";
            return Time;
        }
    }
}