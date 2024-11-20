using System;

namespace Higher_Lower
{
public class Deck
{

    #region suitCode
    const char spades = '\u2660';   // ♠
    const char hearts = '\u2665';   // ♥
    const char diamonds = '\u2666'; // ♦
    const char clubs = '\u2663';    // ♣
    #endregion

    private Card[] cards;
    public int length
    {
        get => cards.Length;
    }
    public int redJokerIndex;
    public int blackJokerIndex;

    /// <summary>
    /// Instantiates a deck, with optional jokers and shuffling
    /// </summary>
    /// <param name="jokers">If true, the deck will contain Jokers</param>
    /// <param name="shuffle">If true, the deck will be shuffled on instantiation</param>
    public Deck(bool jokers, bool shuffle)
    {
        cards = new Card[52];
        GenerateDeck();
        if (jokers) { AddJokers(); }
        if (shuffle) { ShuffleDeck(); }

    }

    /// <summary>
    /// Generates a standard 52 size deck.
    /// </summary>
    private void GenerateDeck()
    {
        string currentCardValue = "1";
        char currentSuit = spades;
        ConsoleColor currentSuitColor = ConsoleColor.Black;

        int deckCounter = 0;
        for (int i = 0; i < 4; i++)
        {
            switch (i)
            {
                case 1: currentSuit = hearts; currentSuitColor = ConsoleColor.Red; break;
                case 2: currentSuit = diamonds; currentSuitColor = ConsoleColor.Red; break;
                case 3: currentSuit = clubs; currentSuitColor = ConsoleColor.Black; break;
            }

            for (int j = 1; j < 14; j++)
            {
                Suit newSuit; newSuit.code = currentSuit; newSuit.color = currentSuitColor;
                currentCardValue = j.ToString();
                switch (j)
                {
                    case 1: currentCardValue = "A"; break;
                    case 11: currentCardValue = "J"; break;
                    case 12: currentCardValue = "Q"; break;
                    case 13: currentCardValue = "K"; break;
                }
                Card newCard = new Card(currentCardValue,newSuit,false);
                cards[deckCounter] = newCard;
                deckCounter++;
            }
        }
    }

    /// <summary>
    /// Adds Jokers to the deck
    /// </summary>
    private void AddJokers()
    {
        List<Card> tempCards = new List<Card>(cards);

        Suit blackSuit; blackSuit.code = '\u2300'; blackSuit.color = ConsoleColor.Black;
        Card blackJoker = new Card("Joker", blackSuit,true); 
        tempCards.Add(blackJoker);
        Suit redSuit; redSuit.code = '\u2300'; redSuit.color = ConsoleColor.Red;
        Card redJoker = new Card("Joker", redSuit, true);
        tempCards.Add(redJoker);
        blackJokerIndex = tempCards.Count - 2;
        redJokerIndex = tempCards.Count - 1;

        cards = tempCards.ToArray();
    }

    /// <summary>
    /// Shuffles the deck
    /// </summary>
    public void ShuffleDeck()
    {
        Random random = new Random();
        for (int i = 0; i < cards.Length; i++)
        {
            int rnd = i + random.Next(cards.Length - i);
            Card temp = cards[rnd];
            cards[rnd] = cards[i];
            cards[i] = temp;
        }
    }

    /// <summary>
    /// Gets a card
    /// </summary>
    /// <param name="index">The index of the card to return</param>
    /// <returns></returns>
    public Card GetCard(int index)
    {
        return cards[index];
    }

    /// <summary>
    /// Returns a random that isn't being currently used in the game
    /// </summary>
    /// <returns></returns>
    public Card GetRandomCard()
    {
        Random rnd = new Random();
        int index = 0;
        bool valid = false;
        Card result = cards[0]; //default value to resolve 'Use of unnasigned variable' compiler error
        while(!valid)
        {
            valid = true;
            index = rnd.Next(cards.Length -1);
            result = cards[index];
            if(result.isUsed)
            {
                valid = false;
            }
        }
        result.SetUsed(true);
        return result;
    }

    public void ReturnCardToDeck(Card card)
    {
        card.SetUsed(false);
    }
}

}