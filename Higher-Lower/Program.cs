using System;

namespace Higher_Lower
{
    public class Program
    {
        private static Session? session;
        private static Leaderboard? leaderboard;
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
            
            leaderboard = new Leaderboard(leaderboardFile);

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
                    Player newPlayer = new Player(playerName);
                    session = new Session(newPlayer);
                    session.PlayGame();
                    break;
                    case "2":
                    if(!leaderboard.available)
                    {
                        Console.WriteLine("Data isn't available yet");
                        break;
                    }
                    Console.Clear();
                    ShowLeaderboard();
                    break;
                    case "3":
                    exit = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Displays the Leaderboard of players
        /// </summary>
        static void ShowLeaderboard()
        {
            if(leaderboard == null) {return;}
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("________Leaderboard________");
            int currentBackgroundColour = 0;
            Console.ForegroundColor = ConsoleColor.White;
            foreach(Player player in leaderboard.players)
            {
                Console.BackgroundColor = (ConsoleColor)currentBackgroundColour;
                Console.WriteLine($"Name: {player.name}, High Score: {player.highScore}, No Of Loses: {player.noOfLoses}, Date & Time: {player.date} | {player.time}");
                currentBackgroundColour ++;
                if(currentBackgroundColour > 4) {currentBackgroundColour = 0;}
            }
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

        /// <summary>
        /// Get the current date
        /// </summary>
        /// <returns>Retuns the current date in a string</returns>
        public static string GetCurrentDate()
        {
            string CurrentDate;
            string[] TempDate = DateTime.Now.ToString().Split(' ');
            string[] DDMMYY = TempDate[0].Split('/');
            CurrentDate = DDMMYY[0] + "-" + DDMMYY[1] + "-" + DDMMYY[2];

            return CurrentDate;
        }

        /// <summary>
        /// Get the current time of day
        /// </summary>
        /// <returns>Returns the time in a string</returns>
        public static string GetTimeOfDay()
        {
            return DateTime.Now.TimeOfDay.ToString(@"hh\:mm\:ss");;
        }
    }
}