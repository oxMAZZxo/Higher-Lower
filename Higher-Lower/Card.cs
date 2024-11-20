using System;

namespace Higher_Lower{

public struct Suit
{
    public char code;
    public ConsoleColor color;
}

public class Card{
    public string value {get; private set;}
    public Suit suit {get; private set;}
    public bool isJoker {get; private set;}
    public bool isUsed {get; private set;}

    public Card(string cardValue, Suit newSuit, bool joker)
    {
        value = cardValue;
        suit = newSuit;
        isJoker = joker;
        isUsed = false;
    }

    public int GetCardValue()
    {
        int result = 0;
        switch (value)
        {
            case "A": result = 1; break;
            case "J": result = 11; break;
            case "Q": result = 12; break;
            case "K": result = 13; break;
            case "Joker":
                if(suit.color == ConsoleColor.Black)
                {
                    result = 14;
                }else
                {
                    result = 15;
                }
                break;
            default:
            result = Convert.ToInt32(value);
            break;
        }

        return result;
    }

    public void SetUsed(bool newValue)
    {
        isUsed = newValue;
    }
}
}