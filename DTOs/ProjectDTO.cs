using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AgileTaskMaster.DTOs
{
    public class ProjectDTO
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }  = ObjectId.GenerateNewId().ToString();

        public string Name { get; set; }

        public List<string>? TaskIds { get; set; }

        public List<string>? TeamIds { get; set; }
    }
}
