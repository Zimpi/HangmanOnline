namespace HangmanOnline.Models;

public class Session : IDisposable
{
    public Session(string wordToGuess, string hostName)
    {
        WordToGuess = wordToGuess;
        WordToGuessDisplay = new string('_', wordToGuess.Length);
        Host = new Player(hostName);
        SessionCode = new Random().Next(1000, 9999);
    }

    public Guid SessionGuid { get; set; } = Guid.NewGuid();
    public string? WordToGuess { get; set; }
    public string? WordToGuessDisplay { get; set; }
    public int GuessesRemaining { get; set; } = 6;
    public List<char> LettersGuessed { get; set; } = new();
    public bool IsGameOver { get; set; } = false;
    public Player Host { get; private set; }
    private List<Player> Players { get; } = new();

    public int PlayersCount => Players.Count;
    public int SessionCode { get; }

    public void Dispose()
    {
        PlayerAdded = null;
        PlayerRemoved = null;
        GC.SuppressFinalize(this);
    }

    public void AddPlayer(string playerName)
    {
        var newPlayer = new Player(playerName);
        Players.Add(newPlayer);
        PlayerAdded?.Invoke(newPlayer);
    }

    public List<Player> GetPlayers()
    {
        return Players;
    }

    public void RemovePlayer(Guid playerId)
    {
        var player = Players.FirstOrDefault(p => p.PlayerId == playerId);
        if (player == null) return;
        Players.Remove(player);
        PlayerRemoved?.Invoke(player.PlayerName);
    }

    public event Action<Player>? PlayerAdded;
    public event Action<string>? PlayerRemoved;
}