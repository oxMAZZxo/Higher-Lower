﻿using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

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
    const int cardDrawPosX = 10;
    const int cardDrawPosY = 2;
    const int questionPosX = 0;
    const int questionPosY = 15;
    const int delayUntilNextCard = 5000;
    private Deck myDeck;
    private string? userInput;
    private bool exitGame;
    private int score;
    private int noOfResets;

    /// <summary>
    /// Instantiates a session
    /// </summary>
    public Session()
    {
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
        score = 0;
        exitGame = false;
    }

    /// <summary>
    /// Starts a game session
    /// </summary>
    public void StartGame()
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
            DisplayHUD();

            DrawCard(visibleCard,cardDrawPosX,cardDrawPosY);
            DrawCardFaceDown(cardDrawPosX + cardWidth + 2, cardDrawPosY);

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.BackgroundColor = ConsoleColor.Black;
            Console.SetCursorPosition(questionPosX,questionPosY);
            Console.Write("Is the hidden card higher, lower, or equal? H/L/E: ");
            userInput = Console.ReadLine()?.ToUpper();
            
            string message;
            bool correct = CalculateResponse(visibleCard,hiddenCard,out message);

            if(correct) {score ++;} else{noOfResets ++;}
            DisplayHUD();
            DrawCard(visibleCard,cardDrawPosX, cardDrawPosY);
            DrawCard(hiddenCard,cardDrawPosX + cardWidth + 2, cardDrawPosY);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.SetCursorPosition(questionPosX,questionPosY);
            if(correct) {Console.ForegroundColor = ConsoleColor.Green;} else{Console.ForegroundColor = ConsoleColor.Red;}
            Console.Write(message);
            Thread.Sleep(delayUntilNextCard);
        }
    }

    void DisplayHUD()
    {
        Console.Clear();
        Console.BackgroundColor = ConsoleColor.Black;
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.SetCursorPosition(scorePosX,scorePosY);
        Console.Write($"Score: {score}");
        Console.SetCursorPosition(resetPosX,resetPosY);
        Console.Write($"Resets: {noOfResets}");
    }

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

                Console.Write(Strings.ChrW(charCodeToPrint));
            }
            Console.WriteLine();
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

                Console.Write(Strings.ChrW(charCodeToPrint));
            }
            Console.WriteLine();
        }
    }

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
    
}
}
