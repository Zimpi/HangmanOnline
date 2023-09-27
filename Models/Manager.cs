namespace HangmanOnline.Models;

public class Manager
{
    public Manager()
    {
        CurrentSessions = new List<Session>();
    }

    public List<Session> CurrentSessions { get; set; }

    public Session CreateSession(string wordToGuess, string hostName)
    {
        var session = new Session(wordToGuess, hostName);
        CurrentSessions.Add(session);
        SessionCreated?.Invoke(session);
        return session;
    }

    public Session? GetSession(Guid sessionGuid)
    {
        return CurrentSessions.SingleOrDefault(s => s.SessionGuid == sessionGuid);
    }

    public void RemoveSession(Guid sessionGuid)
    {
        var foundSession = CurrentSessions.SingleOrDefault(s => s.SessionGuid == sessionGuid);
        if (foundSession == null) return;
        CurrentSessions.Remove(foundSession);
        SessionRemoved?.Invoke(sessionGuid);
    }

    public event Action<Guid>? SessionRemoved;
    public event Action<Session>? SessionCreated;
}