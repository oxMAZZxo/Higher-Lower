using System;
using System.Diagnostics;

namespace Higher_Lower{
public class Leaderboard
{
    public List<Player> players {get; private set;}
    private FileInfo leaderboard;
    public event EventHandler<EventArgs> readFinished;
    public bool available {get; private set;}
    private FileSystemWatcher? fileSystemWatcher;

    /// <summary>
    /// Instantiates a leaderboard from a file
    /// </summary>
    /// <param name="leaderboardFile"></param>
    public Leaderboard(FileInfo leaderboardFile)
    {
        leaderboard = leaderboardFile;
        if(leaderboard.Exists && leaderboard.DirectoryName != null)
        {
            fileSystemWatcher = new FileSystemWatcher(leaderboard.DirectoryName, "*.csv");
            fileSystemWatcher.Changed += OnFileChange;
            fileSystemWatcher.EnableRaisingEvents = true;
        }
        available = false;
        players = new List<Player>();
        readFinished += SortPlayers;
        ReadData();
    }

    /// <summary>
    /// Reads Leaderboard Data Asyncronously
    /// </summary>
    private async void ReadData()
    {
        string? currentLine;
        StreamReader streamReader = new StreamReader(leaderboard.FullName);
        while(!streamReader.EndOfStream)
        {
            currentLine =  await streamReader.ReadLineAsync();
            if(currentLine != null)
            {
                string[] data = currentLine.Split(",");
                Player newPlayer = new Player(data[0], Convert.ToInt32(data[1]),Convert.ToInt32(data[2]),data[3],data[4]);
                players.Add(newPlayer);
            }
        }
        streamReader.Close();
        streamReader.Dispose();
        readFinished?.Invoke(this, new EventArgs());
        Debug.WriteLine("Leaderboard finished reading");
    }

    /// <summary>
    /// Sorts the list of players descending
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void SortPlayers(object? sender, EventArgs e)
    {
        players = players.OrderByDescending(player => player.highScore).ToList();
        available = true;
    }

    private void OnFileChange(object? sender, FileSystemEventArgs e)
    {
        Debug.WriteLine($"File System Watcher detected a change. File Name: {e.Name}");
        ReadData();
    }
}
}