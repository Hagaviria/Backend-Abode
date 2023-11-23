// ImageModel.cs
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

public class Image
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; }

    public string FileName { get; set; }

    public byte[] ImageData { get; set; }
}
