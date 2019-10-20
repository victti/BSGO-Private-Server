using MongoDB.Driver;
using System;
using System.Collections.Generic;
using BSGO_Server.Database.Entities;
using System.Linq.Expressions;

namespace BSGO_Server.Database
{
    class Database
    {
        private static readonly IMongoClient client = new MongoClient("mongodb://localhost");
        private static readonly IMongoDatabase database = client.GetDatabase("bsgo");
        private static readonly IMongoCollection<Users> colUsers = database.GetCollection<Users>("users");
        private static readonly IMongoCollection<Characters> colCharacters = database.GetCollection<Characters>("characters");

        // This is temporary. Just making sure that my user does exist
        private static readonly Expression<Func<Users, bool>> filter =
            x => x.PlayerId.Equals("5085935");
        private static readonly Users user = colUsers.Find(filter).FirstOrDefault();

        /// <summary>
        /// Initializes the database.
        /// </summary>
        public static void Start()
        {
            // Since we want to use my premade user, we are making sure he is on the database at the start
            if (user == null)
            {
                Users docUser = new Users {
                    PlayerId = "5085935",
                    SessionCode = "b1b23d2fa2769bd59d4c1b67554599b88381afd653b156aa54cb689969ab4fb7"
                };

                colUsers.InsertOne(docUser);
            }
        }

        /// <summary>
        /// Checks if the Session exists on the database by its Session Code.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckSessionCodeExistance(string sessionCode)
        {
            Expression<Func<Users, bool>> _filter =
                x => x.SessionCode.Equals(sessionCode);

            Users _user = colUsers.Find(_filter).FirstOrDefault();

            return _user != null;
        }

        /// <summary>
        /// Checks if the User exists on the database by his Player Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckPlayerExistanceById(string id)
        {
            Expression<Func<Users, bool>> _filter =
                x => x.PlayerId.Equals(id);

            Users _user = colUsers.Find(_filter).FirstOrDefault();

            return _user != null;
        }

        /// <summary>
        /// Checks if the Character exists on the database by its Player Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckCharacterExistanceById(string id)
        {
            Expression<Func<Characters, bool>> _filter =
                x => x.PlayerId.Equals(id);

            Characters character = colCharacters.Find(_filter).FirstOrDefault();

            return character != null;
        }

        /// <summary>
        /// Checks if the given name is available.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CheckCharacterNameAvailability(string name)
        {
            Expression<Func<Characters, bool>> _filter =
                x => x.Name.Equals(name);

            Characters character = colCharacters.Find(_filter).FirstOrDefault();

            return character == null;
        }

        /// <summary>
        /// Creates a new character on the database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerId"></param>
        public static void CreateCharacter(string name, string playerId, byte faction, Dictionary<AvatarItem, string> items)
        {
            Expression<Func<Characters, bool>> _filter =
                x => x.Name.Equals(name);
            Characters character = colCharacters.Find(_filter).FirstOrDefault();

            if (character != null)
                return;

            Dictionary<string, string> avatarItems = new Dictionary<string, string>();
            foreach(KeyValuePair< AvatarItem, string> item in items)
            {
                avatarItems.Add(item.Key.ToString(), item.Value);
            }

            character = new Characters {
                Name = name,
                GameLocation = 2,
                Level = 1,
                PlayerId = playerId,
                Faction = faction,
                AvatarItems = avatarItems,
                Titanium = "0",
                Tylium = "0",
                Water = "0",
                Cubits = "0",
                Experience = "0"
            };

            colCharacters.InsertOne(character);
        }

        /// <summary>
        /// Returns a User(Database Entity) by searching his Session Code.
        /// </summary>
        /// <param name="session"></param>
        /// <returns></returns>
        public static Users GetUserBySession(string session)
        {
            Expression<Func<Users, bool>> _filter =
                x => x.SessionCode.Equals(session);

            Users _user = colUsers.Find(_filter).FirstOrDefault();

            return _user;
        }

        /// <summary>
        /// Returns a Character(Database Entity) by searching his player Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Characters GetCharacterById(string id)
        {
            Expression<Func<Characters, bool>> _filter =
                x => x.PlayerId.Equals(id);

            Characters character = colCharacters.Find(_filter).FirstOrDefault();

            return character;
        }
    }
}
