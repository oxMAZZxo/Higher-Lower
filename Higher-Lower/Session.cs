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

        public Session()
        {
            myDeck = new Deck();

            DrawCardFaceDown(5,5);
        }

        void DrawCard(Card card,int startX, int startY)
        {
            int charCodeToPrint;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            for(int y = 0; y < cardYWidth + 1; y++)
            {
                for(int x = 0; x < cardXWidth + 1; x++)
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

        void DrawCardFaceDown(int startX, int startY)
        {
            int charCodeToPrint;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            for(int y = 0; y < cardYWidth + 1; y++)
            {
                for(int x = 0; x < cardXWidth + 1; x++)
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
    }
}
