using System;

namespace Higher_Lower{

public class Player{
    public string name {get; private set;}
    public int highScore {get; private set;}
    public int noOfLoses {get; private set;}
    public string? date {get; private set;}
    public string? time {get; private set;}

    public Player(string newName)
    {
        name = newName;
        highScore = 0;
        noOfLoses = 0;
    }

    public Player(string newName,int newScore, int loses, string newDate, string newTime)
    {
        name = newName;
        highScore = newScore;
        noOfLoses = loses;
        date = newDate;
        time = newTime;
    }

    public void SetHighScore(int newValue) {highScore = newValue;}
    public void SetNumberOfLoses(int newValue) {noOfLoses = newValue;}
    public void SetDate(string newValue) {date = newValue;}
    public void SetTime(string newValue) {time = newValue;}
}
}