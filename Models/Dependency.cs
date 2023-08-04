using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AgileTaskMaster.Models
{
    public class Dependency
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string TaskId { get; set; }

        [BsonRequired]
        public string DependentTaskId { get; set; }
    }
}
