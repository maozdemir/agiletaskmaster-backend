using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace AgileTaskMaster.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonRequired]
        public string FirstName { get; set; }

        [BsonRequired]
        public string LastName { get; set; }

        [BsonRequired]
        public string Email { get; set; }

        [BsonRequired]
        public UserRole Role { get; set; }

        [BsonRequired]
        public string PasswordHash { get; set; } 

        [BsonDateTimeOptions(Kind = DateTimeKind.Utc)]
        public DateTime CreatedAt { get; set; }
    }
}
