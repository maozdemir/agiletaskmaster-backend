using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AgileTaskMaster.Models
{
    public class Role
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string Name { get; set; }
    }
}
