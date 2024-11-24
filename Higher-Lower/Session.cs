using System;
using System.Threading.Tasks;

namespace Higher_Lower
{
public class Session
{
    const int cardWidth = 13;
    const int cardHeight = 11;
    const int cardXPadding = 2;
    const int cardYPadding = 2;
    const int scorePosX = 0;
    const int scorePosY = 0;
    const int resetPosX = 30;
    const int resetPosY = 0;
    const int highScorePosX = 60;
    const int highScorePosY = 0;
    const int cardDrawPosX = 30;
    const int cardDrawPosY = 2;
    const int questionPosX = 0;
    const int questionPosY = 15;
    const int delayUntilNextCard = 2500;
    private Deck myDeck;
    private string? userInput;
    private bool exitGame;
    private int currentScore;
    private int highScore;
    private int noOfResets;
    private Player currentPlayer;

    /// <summary>
    /// Instantiates a session
    /// </summary>
    public Session(Player newPlayerName)
    {
        currentPlayer = newPlayerName;
        Console.Write("Create Jokers? Enter Y/N or yes/no: ");
        userInput = Console.ReadLine();
        userInput = userInput?.ToUpper();
        bool createJokers = false;
        if(userInput == "Y" || userInput == "YES")
        {
            createJokers = true;
        }

        Console.Write("Shuffle Deck? Enter Y/N or yes/no: ");
        userInput = Console.ReadLine();
        userInput = userInput?.ToUpper();
        
        bool shuffleDeck = false;
        if(userInput == "Y" || userInput == "YES")
        {
            shuffleDeck = true;
        }

        myDeck = new Deck(createJokers, shuffleDeck);
        noOfResets = 0;
        currentScore = 0;
        highScore = 0;
        exitGame = false;
    }

    /// <summary>
    /// Starts a game session
    /// </summary>
    public void PlayGame()
    {
        bool firstRun = true;
        Card hiddenCard = myDeck.GetRandomCard(); // this is done to avoid the compiler error (Use of unassigned local variable) on line 78
        Card visibleCard;
        while(!exitGame)
        {
            userInput = null;
            if(firstRun)
            {
                firstRun = false;
                visibleCard = myDeck.GetRandomCard();
            }else
            {
                visibleCard = hiddenCard;
            }
            hiddenCard = myDeck.GetRandomCard();

            bool valid;
            do
            {
                DisplayHUD();
                DrawCard(visibleCard,cardDrawPosX,cardDrawPosY);
                DrawCardFaceDown(cardDrawPosX + cardWidth + 2, cardDrawPosY);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.BackgroundColor = ConsoleColor.Black;
                Console.SetCursorPosition(questionPosX,questionPosY);
                Console.Write("Is the hidden card higher, lower, or equal? Otherwise enter Q to quit the game. H/L/E/Q: ");
                userInput = Console.ReadLine()?.ToUpper();
                
                valid = Program.ValidateUserInput(userInput,"H","L","E","Q");
                if(!valid)
                {
                    Console.WriteLine("You entered an invalid input, you will be prompted again in 2 seconds");
                    Thread.Sleep(2000);
                }
            }while(!valid);
            
            string message;
            bool correct = CalculateResponse(visibleCard,hiddenCard,out message);
            if(exitGame) {break;}
            if(correct) 
            {
                currentScore ++; 
                if(currentScore > highScore) {highScore++;}
            } else
            {
                noOfResets ++; currentScore = 0;
            }
            DisplayHUD();
            DrawCard(visibleCard,cardDrawPosX, cardDrawPosY);
            DrawCard(hiddenCard,cardDrawPosX + cardWidth + 2, cardDrawPosY);
            myDeck.ReturnCardToDeck(visibleCard);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(questionPosX,questionPosY);
            if(correct) {Console.ForegroundColor = ConsoleColor.Green;} else{Console.ForegroundColor = ConsoleColor.Red;}
            Console.Write(message);
            currentPlayer.SetHighScore(highScore);
            currentPlayer.SetNumberOfLoses(noOfResets);
            Thread.Sleep(delayUntilNextCard);
        }
        Console.WriteLine("Saving Game....");
        SaveGame();
        Thread.Sleep(100);
    }

    /// <summary>
    /// Displays the basic Heads Up Display of a HIGHER/LOWER Session
    /// </summary>
    void DisplayHUD()
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.SetCursorPosition(scorePosX,scorePosY);
        Console.Write($"Score: {currentScore}");
        Console.SetCursorPosition(resetPosX,resetPosY);
        Console.Write($"Resets: {noOfResets}");
        Console.SetCursorPosition(highScorePosX,highScorePosY);
        Console.Write($"High Score: {highScore}");
    }

    /// <summary>
    /// Calculates the response that the console will give the player based on their input
    /// </summary>
    /// <param name="visibleCard">The visible card</param>
    /// <param name="hiddenCard">The hidden card</param>
    /// <param name="message">The output message</param>
    /// <returns></returns>
    bool CalculateResponse(Card visibleCard, Card hiddenCard, out string message)
    {
        bool isHiddenCardHigher = false;
        bool equal = false;

        if(visibleCard.GetCardValue() == hiddenCard.GetCardValue())
        {
            equal = true;
        }

        if(!equal)
        {
            if(visibleCard.GetCardValue() < hiddenCard.GetCardValue()) {isHiddenCardHigher = true;}
        }

        message = "You are wrong!";
        bool correct = false;
        switch(userInput)
        {
            case "E":
                if(equal) {
                    message = "You are corect! They are equal in value.";
                    correct = true;
                }
            break;
            case "H":
                if(isHiddenCardHigher && !equal)
                {
                    message = "You are correct! The hidden card has a higher value!";
                    correct = true;
                }
            break;
            case "L":
                if(!isHiddenCardHigher && !equal)
                {
                    message = "You are correct! The hidden card has a lower value";
                    correct = true;
                }
            break;
            case "Q":
                exitGame = true;
            return false;
        }

        return correct;
    }

    /// <summary>
    /// Draws a card on the screen with a specifed location
    /// </summary>
    /// <param name="card">The card to draw</param>
    /// <param name="startX">The x position on the console</param>
    /// <param name="startY">The y position on the console</param>
    private void DrawCard(Card card, int startX, int startY)
    {
        int charCodeToPrint;
        Console.BackgroundColor = ConsoleColor.White;
        Console.ForegroundColor = ConsoleColor.Black;
        for (int y = 0; y < cardHeight + 1; y++)
        {
            for (int x = 0; x < cardWidth + 1; x++)
            {
                charCodeToPrint = 32;
                Console.SetCursorPosition(x + startX, y + startY);
                if (x == 0 && y == 0)
                {
                    charCodeToPrint = 9555; // Top left corner
                }
                else if (y == 0 && x == cardWidth)
                {
                    charCodeToPrint = 9558; // Top right corner
                }
                else if (y == cardHeight && x == 0)
                {
                    charCodeToPrint = 9561; // Bottom left corner
                }
                else if (y == cardHeight && x == cardWidth)
                {
                    charCodeToPrint = 9564; // Bottom right corner
                }
                else if (y == 0 || y == cardHeight)
                {
                    charCodeToPrint = 9552; // Top or bottom row
                }
                else if (x == 0 || x == cardWidth)
                {
                    charCodeToPrint = 9553; // Left or right column
                }

                Console.Write((char)charCodeToPrint);
            }
        }

        if(card.value == "J" || card.value == "Q" || card.value == "K" || card.value == "Joker")
        {
            DrawPictureCard(startX,startY);
        }

        Console.SetCursorPosition(startX + 2, startY + 1);
        Console.ForegroundColor = card.suit.color;
        Console.BackgroundColor = ConsoleColor.White;
        Console.Write(card.value);
        Console.SetCursorPosition(startX + 2, startY + 2);
        Console.Write(card.suit.code);
        Console.SetCursorPosition(startX + cardWidth - 2, startY + cardHeight - 2);
        Console.Write(card.suit.code);
        if(card.value == "Joker")
        {
            Console.SetCursorPosition(startX + cardWidth - 5, startY + cardHeight - 1);
        }else
        {
            Console.SetCursorPosition(startX + cardWidth - 2, startY + cardHeight - 1);
        }
            Console.Write(card.value);
    }

    /// <summary>
    /// Draws a template card with the face down
    /// </summary>
    /// <param name="startX">The x position on the console</param>
    /// <param name="startY">The y position on the console</param>
    private void DrawCardFaceDown(int startX, int startY)
    {
        int charCodeToPrint;
        Console.ForegroundColor = ConsoleColor.Black;
        for (int y = 0; y < cardHeight + 1; y++)
        {
            for (int x = 0; x < cardWidth + 1; x++)
            {
                charCodeToPrint = 32;
                Console.SetCursorPosition(x + startX, y + startY);
                Console.BackgroundColor = ConsoleColor.DarkRed;
                if(y == 0 || y == cardHeight || x == 0 || x == cardWidth)
                {
                    Console.BackgroundColor = ConsoleColor.White;
                }

                if (x == 1 && y == 1)
                {
                    charCodeToPrint = 9555; // Top left corner
                }
                else if (y == 1 && x == cardWidth - 1)
                {
                    charCodeToPrint = 9558; // Top right corner
                }
                else if (y == cardHeight -1 && x == 1)
                {
                    charCodeToPrint = 9561; // Bottom left corner
                }
                else if (y == cardHeight - 1 && x == cardWidth - 1)
                {
                    charCodeToPrint = 9564; // Bottom right corner
                }

                if(x > 1 && x < cardWidth -1 && y == 1)
                {
                     charCodeToPrint = 9574; // Top row
                }

                if(x > 1 && x < cardWidth -1 && y == cardHeight -1)
                {
                     charCodeToPrint = 9577; // bottom row
                }

                if(y > 1 && y < cardHeight -1 && x == 1 )
                {
                    charCodeToPrint = 9568; // Left column
                }

                if(y > 1 && y < cardHeight -1 &&  x == cardWidth - 1)
                {
                    charCodeToPrint = 9571; // right column
                }

                if(y > 1 && y < cardHeight -1 && x > 1 && x < cardWidth -1)
                {
                    charCodeToPrint = 9580; // all directions
                }
                
                Console.Write((char)charCodeToPrint);
            }
        }
    }

    /// <summary>
    /// Draws a picture card, which is a card like Jack, Queen or King, which contain a "picture"
    /// </summary>
    /// <param name="startX">Start X position on the console</param>
    /// <param name="startY">Start Y position on the console</param>
    void DrawPictureCard(int startX, int startY)
    {
        Random rnd = new Random();
        for(int y = cardYPadding; y < cardHeight + 1 - cardYPadding; y++)
        {
            for(int x = cardXPadding; x < cardWidth + 1 - cardXPadding; x++)
            {
                Console.BackgroundColor = (ConsoleColor)rnd.Next(15);
                Console.SetCursorPosition(x + startX, y + startY);
                Console.Write(" ");
            }
        }
    }

    /// <summary>
    /// Draws the whole deck on the console.
    /// This function may cause bugs on your console, dependning on its settings
    /// </summary>
    /// <param name="jokers">If true, it will draw the jokers as well</param>
    private void DrawDeck(bool jokers)
    {
        int nextCard = 0;
        int lastY = 0;
        for (int y = 0; y < (cardHeight + 2) * 4; y += cardHeight + 2)
        {
            for (int x = 0; x < (cardWidth + 2) * 13; x += cardWidth + 2)
            {
                Thread.Sleep(50);
                DrawCard(myDeck.GetCard(nextCard), x, y);
                nextCard++;
            }
            lastY = y; 
        }

        for(int i = 0; i < (cardWidth + 2) * 2; i += cardWidth + 2)
        {
            DrawCard(myDeck.GetCard(myDeck.blackJokerIndex),i,lastY);
        }

    }
    
    /// <summary>
    /// Saves the current player session syncronously
    /// </summary>
    private void SaveGame()
    {
        string currentTime = Program.GetTimeOfDay();
        string currentDate = Program.GetCurrentDate();
        StreamWriter streamWriter = new StreamWriter(Program.leaderboardFilePath,true);
        streamWriter.WriteLine($"{currentPlayer.name},{currentPlayer.highScore},{currentPlayer.noOfLoses},{currentDate},{currentTime}");
        streamWriter.Close();
        streamWriter.Dispose();
    }
}
}
