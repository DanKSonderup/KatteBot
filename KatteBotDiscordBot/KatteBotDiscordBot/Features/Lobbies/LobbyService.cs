using MongoDB.Driver;

namespace KatteBotDiscordBot.Features.Lobbies;

public class LobbyService
{
    private readonly IMongoCollection<Lobby> _lobbies;
    
    public LobbyService(IMongoClient client)
    {
        var database = client.GetDatabase("KatteBot");
        _lobbies = database.GetCollection<Lobby>("Lobbies");
    }
    
    // Constructor der kalder vores DB og derefter sætter _lobbies til at være lig med den
    
    
    public async Task<Lobby> CreateLobbyAsync(string ownerName, ulong ownerId, TimeSpan time)
    {
        var copenhagenZone = TimeZoneInfo.FindSystemTimeZoneById("Central European Standard Time");
        var now = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, copenhagenZone);
        var eventStartTime = now.Date + time;

        string candidateName = ownerName;
        int count = 1;
        
        while (await _lobbies.Find(l => l.LobbyName == candidateName && l.IsActive).AnyAsync())
        {
            candidateName = $"{ownerName}-{count}";
            count++;
        }

        var lobby = new Lobby
        {
            Id = Guid.NewGuid(),
            LobbyName = candidateName,
            OwnerId = ownerId,
            EventStartTime = eventStartTime,
            PlayerIds = new List<ulong> { ownerId },
            IsActive = true
        };

        // await _lobbies.InsertOneAsync(lobby);
        return lobby;
    }

    public async Task<Lobby?> GetLobbyAsync(Guid id) 
        => await _lobbies.Find(l => l.Id == id).FirstOrDefaultAsync();

    public async Task UpdateLobbyAsync(Lobby lobby) 
        => await _lobbies.ReplaceOneAsync(l => l.Id == lobby.Id, lobby);
    
}