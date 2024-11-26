using System;
using System.Reflection.Metadata;

namespace Higher_Lower
{
    public class Program
    {
        private static Session? session;
        private static Leaderboard? leaderboard;
        public static readonly string leaderboardFilePath = AppDomain.CurrentDomain.BaseDirectory + "\\leaderboard.csv";
        private const string invalidCharacters = " \\`¬/{}%$=£&*";

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
                FileStream fileStream = File.Create(leaderboardFilePath);
                fileStream.Close();
                fileStream.Dispose();
            }
            leaderboardFile.Refresh();
            
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
                    string playerName = GetPlayerName();                    
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
            for(int i = 0; i < leaderboard.players.Count; i++)
            {
                Console.BackgroundColor = (ConsoleColor)currentBackgroundColour;
                Console.WriteLine($"{i + 1} - {leaderboard.players[i].name}, High Score: {leaderboard.players[i].highScore}, No Of Loses: {leaderboard.players[i].noOfLoses}, Date & Time: {leaderboard.players[i].date} | {leaderboard.players[i].time}");
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

        static string GetPlayerName()
        {
            string? input = "value";
            bool invalid;
            do{
                Console.Write("Enter your name: ");
                input = Console.ReadLine();
                invalid = CheckForInvalidCharacter(input);
                if(invalid)
                {
                    Console.WriteLine("You have entered an invalid character. You will be prompted again in 2 seconds");
                    Thread.Sleep(2000);
                }
            }while(invalid);
            
            return input;
        }

        /// <summary>
        /// Checks if a given input contains invalid characters
        /// </summary>
        /// <param name="input">The input being checked for invalid characters</param>
        /// <returns>Returns false if no invalidCharacter is found</returns>
        private static bool CheckForInvalidCharacter(string? input)
        {
            if(string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)) {return true;}   
            for(int i = 0; i < input.Length; i++)
            {
                if(invalidCharacters.IndexOf(input[i]) > -1)
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