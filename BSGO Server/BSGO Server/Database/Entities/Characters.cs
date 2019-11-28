using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace BSGO_Server.Database.Entities
{
    internal class Characters
    {
        [BsonId]
        public ObjectId Id { get; set; }  

        [BsonElement("playerid")]
        [BsonRequired]
        public string PlayerId { get; set; }

        [BsonElement("name")]
        [BsonRequired]
        public string Name { get; set; }

        [BsonElement("gamelocation")]
        [BsonRequired]
        public int GameLocation { get; set; }

        [BsonElement("level")]
        [BsonRequired]
        public int Level { get; set; }

        [BsonElement("faction")]
        [BsonRequired]
        public int Faction { get; set; }

        [BsonElement("avataritems")]
        [BsonRequired]
        public Dictionary<string, string> AvatarItems { get; set; }

        [BsonElement("sectorid")]
        [BsonRequired]
        public int SectorId { get; set; }

        [BsonElement("cubits")]
        [BsonRequired]
        public string Cubits { get; set; }

        [BsonElement("water")]
        [BsonRequired]
        public string Water { get; set; }

        [BsonElement("tylium")]
        [BsonRequired]
        public string Tylium { get; set; }

        [BsonElement("titanium")]
        [BsonRequired]
        public string Titanium { get; set; }

        [BsonElement("experience")]
        [BsonRequired]
        public string Experience { get; set; }

        // Should have more later
    }
}
