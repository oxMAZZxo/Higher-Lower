using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;

namespace Higher_Lower
{
public class Session
{
    static int cardXWidth = 11;
    static int cardYWidth = 11;
    private Deck myDeck;
    private string? userInput;
    private bool exitGame;

    /// <summary>
    /// Instantiates a session
    /// </summary>
    public Session()
    {
        Console.WriteLine("Welcome to Higher/Lower");
        Console.WriteLine("Creating deck....");
        Console.Write("Create Jokers? Enter Y/N or yes/no: ");
        userInput = Console.ReadLine();
        userInput = userInput?.ToUpper();
        bool createJokers = false;
        if(userInput == "Y" || userInput == "YES")
        {
            createJokers = true;
        }
        myDeck = new Deck(createJokers, false);
        exitGame = false;
    }

    /// <summary>
    /// Starts a game session
    /// </summary>
    public void StartGame()
    {
        Console.Clear();
        Console.WriteLine("Game starting");
        
        while(!exitGame)
        {
            Console.Clear();
            DrawCard(myDeck.GetCard(myDeck.redJokerIndex),0,0);
            Console.ReadLine();
        }
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
        for (int y = 0; y < cardYWidth + 1; y++)
        {
            for (int x = 0; x < cardXWidth + 1; x++)
            {
                charCodeToPrint = 32;
                Console.SetCursorPosition(x + startX, y + startY);
                if (x == 0 && y == 0)
                {
                    charCodeToPrint = 9555; // Top left corner
                }
                else if (y == 0 && x == cardXWidth)
                {
                    charCodeToPrint = 9558; // Top right corner
                }
                else if (y == cardYWidth && x == 0)
                {
                    charCodeToPrint = 9561; // Bottom left corner
                }
                else if (y == cardYWidth && x == cardXWidth)
                {
                    charCodeToPrint = 9564; // Bottom right corner
                }
                else if (y == 0 || y == cardYWidth)
                {
                    charCodeToPrint = 9552; // Top or bottom row
                }
                else if (x == 0 || x == cardXWidth)
                {
                    charCodeToPrint = 9553; // Left or right column
                }

                Console.Write(Strings.ChrW(charCodeToPrint));
            }
            Console.WriteLine();
        }

        Console.SetCursorPosition(startX + 2, startY + 1);
        Console.ForegroundColor = card.suit.color;
        Console.Write(card.value);
        Console.SetCursorPosition(startX + 2, startY + 2);
        Console.Write(card.suit.code);
        Console.SetCursorPosition(startX + cardXWidth - 2, startY + cardYWidth - 2);
        Console.Write(card.suit.code);
        Console.SetCursorPosition(startX + cardXWidth - 2, startY + cardYWidth - 1);
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
        for (int y = 0; y < cardYWidth + 1; y++)
        {
            for (int x = 0; x < cardXWidth + 1; x++)
            {
                charCodeToPrint = 32;
                Console.SetCursorPosition(x + startX, y + startY);
                if (x == 0 && y == 0)
                {
                    charCodeToPrint = 9555; // Top left corner
                }
                else if (y == 0 && x == cardXWidth)
                {
                    charCodeToPrint = 9558; // Top right corner
                }
                else if (y == cardYWidth && x == 0)
                {
                    charCodeToPrint = 9561; // Bottom left corner
                }
                else if (y == cardYWidth && x == cardXWidth)
                {
                    charCodeToPrint = 9564; // Bottom right corner
                }
                else if (y == 0 || y == cardYWidth)
                {
                    charCodeToPrint = 9552; // Top or bottom row
                }
                else if (x == 0 || x == cardXWidth)
                {
                    charCodeToPrint = 9553; // Left or right column
                }

                Console.Write(Strings.ChrW(charCodeToPrint));
            }
            Console.WriteLine();
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
        for (int y = 0; y < (cardYWidth + 2) * 4; y += cardYWidth + 2)
        {
            for (int x = 0; x < (cardXWidth + 2) * 13; x += cardXWidth + 2)
            {
                Thread.Sleep(50);
                DrawCard(myDeck.GetCard(nextCard), x, y);
                nextCard++;
            }
            lastY = y; 
        }

        for(int i = 0; i < (cardXWidth + 2) * 2; i += cardXWidth + 2)
        {
            DrawCard(myDeck.GetCard(myDeck.blackJokerIndex),i,lastY);
        }

    }
}
}
