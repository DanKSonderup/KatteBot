using MongoDB.Bson.Serialization.Attributes;

namespace KatteBotDiscordBot.Features.Lobbies;

public class Lobby
{
    [BsonId]
    public Guid Id { get; set; }
    [BsonElement("lobby_name")]
    public string LobbyName { get; set; } = null!;
    [BsonElement("owner_id")]
    public ulong OwnerId { get; set; }
    [BsonElement("events_start_time")]
    public DateTime EventStartTime { get; set; }
    [BsonElement("player_ids")]
    public List<ulong> PlayerIds { get; set; } = [];
    [BsonElement("max_players")]
    public int MaxPlayers { get; set; } = 5;
    [BsonElement("is_active")]
    public bool IsActive { get; set; } = true;
}