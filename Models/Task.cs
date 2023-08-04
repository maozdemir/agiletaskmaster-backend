
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AgileTaskMaster.Models
{
    public class Task
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string Title { get; set; }

        public string Description { get; set; }

        [BsonRequired]
        public TaskStatus Status { get; set; }

        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime Deadline { get; set; }

        [BsonRequired]
        public string AssigneeId { get; set; }

        [BsonRequired]
        public string CreatedById { get; set; }

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }
        
        public TaskPriority Priority { get; set; }

    }
}
