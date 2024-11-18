using System;
using System.Drawing;

namespace Higher_Lower{
    public class Deck{
        
        #region suitCode
        const char spades = '\u2660';   // ♠
        const char hearts = '\u2665';   // ♥
        const char diamonds = '\u2666'; // ♦
        const char clubs = '\u2663';    // ♣
        #endregion
        
        private Card[] cards;
        
        public Deck()
        {
            cards = new Card[54];
            GenerateDeck();
        }

        private void GenerateDeck()
        {
            string currentCardValue = "1";
            char currentSuit = spades;
            ConsoleColor currentSuitColor = ConsoleColor.Black;
            
            int deckCounter = 0;
            for(int i = 0; i < 4; i++)
            {
                switch (i)
                {
                    case 1: currentSuit = hearts; currentSuitColor = ConsoleColor.Red; break;
                    case 2: currentSuit = diamonds; currentSuitColor = ConsoleColor.Red; break;
                    case 3: currentSuit = clubs; currentSuitColor = ConsoleColor.Black; break;
                }
                
                for(int j = 1; j < 14; j++)
                {
                    Suit newSuit; newSuit.code = currentSuit; newSuit.color = currentSuitColor;
                    currentCardValue = j.ToString();
                    switch (j)
                    {
                        case 11: currentCardValue = "J"; break;
                        case 12: currentCardValue = "Q"; break;
                        case 13: currentCardValue = "K"; break;
                    }
                    Card newCard; newCard.value = currentCardValue; newCard.suit = newSuit;
                    cards[deckCounter] = newCard;
                    deckCounter ++;
                }
            }
        }
    
        public Card GetCard(int index)
        {
            return cards[index];
        }
    }
    public struct Card()
    {
        public string value;
        public Suit suit;
    }

    public struct Suit{
        public char code;
        public ConsoleColor color;
    }
}