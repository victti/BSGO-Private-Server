using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;
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

        // This is temporary. Just making sure that my second user does exist
        private static readonly Expression<Func<Users, bool>> filter2 =
            x => x.PlayerId.Equals("5085936");
        private static readonly Users user2 = colUsers.Find(filter2).FirstOrDefault();

        // This is temporary. Just making sure that my third user does exist
        private static readonly Expression<Func<Users, bool>> filter3 =
            x => x.PlayerId.Equals("5085937");
        private static readonly Users user3 = colUsers.Find(filter3).FirstOrDefault();

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

            if(user2 == null)
            {
                Users docUser2 = new Users
                {
                    PlayerId = "5085936",
                    SessionCode = "b1b23d2fa2769bd59d4c1b67554599b88381afd653b156aa54cb689969ab4fb8"
                };

                colUsers.InsertOne(docUser2);
            }

            if (user3 == null)
            {
                Users docUser3 = new Users
                {
                    PlayerId = "5085937",
                    SessionCode = "b1b23d2fa2769bd59d4c1b67554599b88381afd653b156aa54cb689969ab4fb9"
                };

                colUsers.InsertOne(docUser3);
            }
        }

        /// <summary>
        /// Checks if the Session exists on the database by its Session Code.
        /// </summary>
        /// <param name="sessionCode"></param>
        /// <returns></returns>
        public static bool CheckSessionCodeExistance(string sessionCode)
        {
            Expression<Func<Users, bool>> filter =
                x => x.SessionCode.Equals(sessionCode);

            Users user = colUsers.Find(filter).FirstOrDefault();

            return user != null;
        }

        /// <summary>
        /// Checks if the Session exists on the database by its playerId.
        /// </summary>
        /// <param name="sessionCode"></param>
        /// <returns></returns>
        public static bool CheckPlayerIdExistance(uint playerId)
        {
            Expression<Func<Users, bool>> filter =
                x => x.PlayerId.Equals(playerId);

            Users user = colUsers.Find(filter).FirstOrDefault();

            return user != null;
        }

        /// <summary>
        /// Checks if the User exists on the database by his Player Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckPlayerExistanceById(string id)
        {
            Expression<Func<Users, bool>> filter =
                x => x.PlayerId.Equals(id);

            Users user = colUsers.Find(filter).FirstOrDefault();

            return user != null;
        }

        /// <summary>
        /// Checks if the Character exists on the database by its Player Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool CheckCharacterExistanceById(string id)
        {
            Expression<Func<Characters, bool>> filter =
                x => x.PlayerId.Equals(id);

            Characters character = colCharacters.Find(filter).FirstOrDefault();

            return character != null;
        }

        /// <summary>
        /// Checks if the given name is available.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool CheckCharacterNameAvailability(string name)
        {
            Expression<Func<Characters, bool>> filter =
                x => x.Name.Equals(name);

            Characters character = colCharacters.Find(filter).FirstOrDefault();

            return character == null;
        }

        /// <summary>
        /// Creates a new character on the database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerId"></param>
        /// <param name="faction"></param>
        /// <param name="items"></param>
        public static void CreateCharacter(string name, string playerId, byte faction, Dictionary<AvatarItem, string> items)
        {
            Expression<Func<Characters, bool>> filter =
                x => x.Name.Equals(name);
            Characters character = colCharacters.Find(filter).FirstOrDefault();

            if (character != null)
                return;

            Dictionary<string, string> avatarItems = new Dictionary<string, string>();
            foreach(KeyValuePair< AvatarItem, string> item in items)
            {
                avatarItems.Add(item.Key.ToString(), item.Value);
            }

            Faction charFaction = Faction.Colonial;
            if ((Faction)faction != charFaction)
                charFaction = Faction.Cylon;

            character = new Characters {
                Name = name,
                GameLocation = 1,
                Level = 1,
                PlayerId = playerId,
                Faction = (byte)charFaction,
                AvatarItems = avatarItems,
                SectorId = charFaction == Faction.Colonial ? 0 : 6,
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
            Expression<Func<Users, bool>> filter =
                x => x.SessionCode.Equals(session);

            Users user = colUsers.Find(filter).FirstOrDefault();

            return user;
        }

        /// <summary>
        /// Returns a User(Database Entity) by searching his player Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Users GetUserById(string id)
        {
            Expression<Func<Users, bool>> filter =
                x => x.PlayerId.Equals(id);

            Users user = colUsers.Find(filter).FirstOrDefault();

            return user;
        }

        /// <summary>
        /// Returns a Character(Database Entity) by searching his player Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Characters GetCharacterById(string id)
        {
            Expression<Func<Characters, bool>> filter =
                x => x.PlayerId.Equals(id);

            Characters character = colCharacters.Find(filter).FirstOrDefault();

            return character;
        }

        public static void SaveSector(string id, uint sectorId)
        {
            Expression<Func<Characters, bool>> filter =
    x => x.PlayerId.Equals(id);
            Characters character = colCharacters.Find(filter).FirstOrDefault();
            character.SectorId = (int)sectorId;
            colCharacters.ReplaceOne(filter, character);
        }

        public static void SaveSettings(string id, IDictionary<UserSetting, object> settings)
        {
            if (GetUserById(id) == null)
                return;

            IDictionary<string, string> newSettings = new Dictionary<string, string>();

            foreach (KeyValuePair<UserSetting, object> setting in settings)
            {
                UserSettingValueType valueType = SettingProtocol.GetValueType(setting.Key);
                switch (valueType)
                {
                    case UserSettingValueType.Byte:
                        newSettings.Add(setting.Key.ToString(), "byte|" + setting.Value);
                        break;
                    case UserSettingValueType.Float:
                        newSettings.Add(setting.Key.ToString(), "float|" + setting.Value);
                        break;
                    case UserSettingValueType.Boolean:
                        newSettings.Add(setting.Key.ToString(), "bool|" + setting.Value);
                        break;
                    case UserSettingValueType.Integer:
                        newSettings.Add(setting.Key.ToString(), "int|" + setting.Value);
                        break;
                    case UserSettingValueType.Float2:
                        {
                            float2 @float = (float2)setting.Value;
                            newSettings.Add(setting.Key.ToString(), "float2|" + ((float2)setting.Value).x + "|" + ((float2)setting.Value).y);
                            break;
                        }
                    case UserSettingValueType.HelpScreenType:
                        {
                            List<HelpScreenType> list = (List<HelpScreenType>)setting.Value;
                            string listString = "";
                            foreach (HelpScreenType item in list)
                            {
                                listString += item.ToString() + "|";
                            }
                            newSettings.Add(setting.Key.ToString(), "hstList|" + list.Count + "|" + listString);

                            break;
                        }
                    default:

                        break;
                }
            }

            Expression<Func<Users, bool>> filter =
                x => x.PlayerId.Equals(id);
            Users user = colUsers.Find(filter).FirstOrDefault();
            user.settings = newSettings;
            colUsers.ReplaceOne(filter, user);
        }

        public static void SaveKeys(string id, List<string> controlKeys)
        {
            if (GetUserById(id) == null)
                return;

            Expression<Func<Users, bool>> filter =
                x => x.PlayerId.Equals(id);
            Users user = colUsers.Find(filter).FirstOrDefault();
            user.controlKeys = controlKeys;
            colUsers.ReplaceOne(filter, user);
        }
    }
}
