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
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Clear();
                    Console.WriteLine("V===================V");
                    Console.WriteLine("1. Play Higher/Lower.");            
                    Console.WriteLine("2. Leaderboards.");            
                    Console.WriteLine("3. Exit.");
                    Console.Write("Enter one of the options above and press enter: ");
                    menuChoice = Console.ReadLine();
                    valid = ValidateUserInput(menuChoice, "1","2","3");
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
                    session.PlayGame();
                    break;
                    case "2":
                    Console.Clear();
                    ShowLeaderboard();
                    break;
                    case "3":
                    exit = true;
                    break;
                }
            }
        }

        static void ShowLeaderboard()
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("________Leaderboard________");
            Console.ForegroundColor = ConsoleColor.Gray;
            string? currentLine;
            int currentBackgroundColor = 0;
            StreamReader streamReader = new StreamReader(leaderboardFilePath);
            while(!streamReader.EndOfStream)
            {
                currentLine = streamReader.ReadLine();
                if(currentLine != null)
                {
                    string[] data = currentLine.Split(",");
                    Console.BackgroundColor = (ConsoleColor)currentBackgroundColor;
                    Console.WriteLine($"Name: {data[0]}, High Score: {data[1]}, Number of Resets: {data[2]}, Date & Time: {data[3]} & {data[4]}");
                    currentBackgroundColor ++;
                    if(currentBackgroundColor > 4){currentBackgroundColor = 0;}
                }
            }
            streamReader.Close();
            streamReader.Dispose();
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
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