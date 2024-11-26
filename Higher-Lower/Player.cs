using System;

namespace Higher_Lower{

public class Player{
    public string name {get; private set;}
    public int highScore {get; private set;}
    public int noOfLoses {get; private set;}
    public string? date {get; private set;}
    public string? time {get; private set;}

    /// <summary>
    /// Instantiates a new player
    /// </summary>
    /// <param name="newName">The players name</param>
    public Player(string newName)
    {
        name = newName;
        highScore = 0;
        noOfLoses = 0;
    }

    /// <summary>
    /// Instantiates a player with specified values
    /// </summary>
    /// <param name="newName">Player's name</param>
    /// <param name="newScore">Player's score</param>
    /// <param name="loses">The number of loses of the player</param>
    /// <param name="newDate">The date entry of the player</param>
    /// <param name="newTime">The time entry of the player</param>
    public Player(string newName,int newScore, int loses, string newDate, string newTime)
    {
        name = newName;
        highScore = newScore;
        noOfLoses = loses;
        date = newDate;
        time = newTime;
    }
    /// <summary>
    /// Sets the player highscore
    /// </summary>
    /// <param name="newValue">The highscore value to set</param>
    public void SetHighScore(int newValue) {highScore = newValue;}
    /// <summary>
    /// Sets the players number of loses
    /// </summary>
    /// <param name="newValue">The number of loses value to set</param>
    public void SetNumberOfLoses(int newValue) {noOfLoses = newValue;}
}
}