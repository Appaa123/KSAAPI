using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class User
{

    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = string.Empty;
    [Required]
    public string username { get; set;} = string.Empty;

    [Required]
    [DataType(DataType.Password)]
    public string password { get; set;} = string.Empty;
}