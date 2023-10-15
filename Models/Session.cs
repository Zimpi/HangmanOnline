using MudBlazor;

namespace HangmanOnline.Models;

public class Session
{
    public Session(string wordToGuess, string hostName)
    {
        WordToGuess = wordToGuess;
        WordToGuessDisplay = new string('_', wordToGuess.Length);
        Host = new Player(hostName);
        SessionCode = new Random().Next(1000, 9999);
    }

    public Guid SessionGuid { get; set; } = Guid.NewGuid();
    public string WordToGuess { get; set; }
    public string WordToGuessDisplay { get; set; }
    public int GuessesRemaining { get; set; } = 6;
    public List<char> LettersGuessed { get; set; } = new();
    public bool IsGameOver { get; set; }
    public Player Host { get; private set; }
    private List<Player> Players { get; } = new();

    public int PlayersCount => Players.Count;
    public int SessionCode { get; }

    public void GuessCharacter(char character)
    {
        if (IsGameOver)
        {
            OnMessageReceived("Das Spiel ist bereits beendet!", Severity.Error);
            return;
        }

        if (LettersGuessed.Contains(character))
        {
            OnMessageReceived("Der Buchstabe wurde bereits geraten!", Severity.Error);
            return;
        }

        LettersGuessed.Add(character);

        if (WordToGuess.Contains(character))
        {
            var wordToGuessDisplay = WordToGuessDisplay.ToCharArray();
            for (var i = 0; i < WordToGuess.Length; i++)
                if (WordToGuess[i] == character)
                    wordToGuessDisplay[i] = character;

            WordToGuessDisplay = new string(wordToGuessDisplay);
            if (WordToGuessDisplay != WordToGuess) return;

            OnMessageReceived("Das Wort wurde erraten!", Severity.Success);
            IsGameOver = true;
        }
        else
        {
            GuessesRemaining--;
            if (GuessesRemaining == 0) IsGameOver = true;
        }
    }

    public Player AddPlayer(string playerName)
    {
        var newPlayer = new Player(playerName);
        Players.Add(newPlayer);
        PlayerAdded?.Invoke(newPlayer);
        return newPlayer;
    }

    public List<Player> GetPlayers()
    {
        return Players;
    }

    public Player? GetPlayer(Guid playerId)
    {
        return Players.SingleOrDefault(p => p.PlayerId == playerId);
    }

    public void RemovePlayer(Guid playerId)
    {
        var player = GetPlayer(playerId);
        if (player != null)
        {
            Players.Remove(player);
            PlayerRemoved?.Invoke(player.PlayerName);
        }

        if (Host.PlayerId == playerId)
        {
            if (Players.Any())
            {
                Host = Players.First();
                HostChanged?.Invoke(Host);
            }
        }
    }

    public event Action<Player>? PlayerAdded;
    public event Action<string>? PlayerRemoved;
    public event Action<string, Severity>? MessageReceived;
    public event Action<Player>? HostChanged;

    protected virtual void OnMessageReceived(string message, Severity type)
    {
        MessageReceived?.Invoke(message, type);
    }
}