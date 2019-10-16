using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BSGO_Server.Database.Entities
{
    class Users
    {
        [BsonId()]
        public ObjectId Id { get; set; }

        [BsonElement("playerid")]
        [BsonRequired()]
        public string PlayerId { get; set; }

        [BsonElement("sessioncode")]
        [BsonRequired()]
        public string SessionCode { get; set; }

        // Should have more later
    }
}
