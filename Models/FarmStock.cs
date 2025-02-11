using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class FarmStock
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public string? Type { get; set; }
    public string? Summary { get; set; }
    public int Quantity { get; set; }
    public int Days{ get; set; }
}