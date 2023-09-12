namespace HangmanOnline.Models;

public class Player
{
    public Player(string hostName)
    {
        PlayerName = hostName;
    }

    public Guid PlayerId { get; set; } = Guid.NewGuid();
    public string PlayerName { get; set; }
}