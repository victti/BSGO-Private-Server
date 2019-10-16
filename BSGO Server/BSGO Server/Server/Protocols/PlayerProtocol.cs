using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    class PlayerProtocol : Protocol
    {
        public enum Request : ushort
        {
            MoveItem = 1,
            BuySkill = 2,
            SelectTitle = 3,
            BindSticker = 4,
            SelectConsumable = 5,
            UnbindSticker = 6,
            AddShip = 7,
            RemoveShip = 8,
            SelectShip = 9,
            UpgradeShip = 10,
            RepairSystem = 11,
            RepairShip = 12,
            ScrapShip = 13,
            CreateAvatar = 14,
            SelectFaction = 0xF,
            UpgradeSystem = 0x10,
            SetShipName = 17,
            UseAugment = 18,
            [Obsolete]
            OldChangeFaction = 19,
            ChooseDailyBonus = 20,
            UpgradeSystemByPack = 21,
            MoveAll = 22,
            ReadMail = 23,
            RemoveMail = 24,
            MailAction = 25,
            RepairAll = 26,
            [Obsolete]
            CheckUserBonus = 27,
            CheckNameAvailability = 28,
            PopupSeen = 30,
            ChooseName = 0x20,
            CreateAvatarFactionChange = 33,
            [Obsolete]
            OldChangeName = 34,
            InstantSkillBuy = 35,
            ReduceSkillLearnTime = 36,
            SubmitMission = 37,
            AugmentMassActivation = 38,
            SendDradisData = 39,
            RequestCharacterServices = 40,
            ChangeFaction = 41,
            ChangeName = 42,
            ChangeAvatar = 43,
            ResourceHardcap = 44,
            DeselectTitle = 45
        }

        public enum Reply : ushort
        {
            Reset = 1,
            PlayerInfo = 2,
            Skills = 3,
            Missions = 4,
            RemoveMissions = 5,
            Duties = 6,
            HoldItems = 7,
            RemoveHoldItems = 8,
            LockerItems = 9,
            RemoveLockerItems = 10,
            ShipInfo = 11,
            Slots = 12,
            Stickers = 13,
            RemoveStickers = 14,
            AddShip = 0xF,
            RemoveShip = 0x10,
            ActiveShip = 17,
            ShipName = 19,
            NameAvailable = 20,
            NameNotAvailable = 21,
            ID = 22,
            Name = 23,
            Faction = 24,
            Experience = 25,
            SpentExperience = 26,
            Level = 27,
            NormalExperience = 28,
            Avatar = 29,
            Loot = 30,
            RemoveLootItems = 0x1F,
            Stats = 0x20,
            [Obsolete]
            AbilityValidation = 33,
            PaymentInfo = 34,
            Counters = 35,
            Title = 36,
            ResetDuties = 37,
            AllowFactionSwitch = 38,
            Factors = 39,
            RemoveFactors = 40,
            Mail = 41,
            RemoveMail = 42,
            Capability = 43,
            AnswerUserBonus = 44,
            FactorModify = 45,
            UpdatePopupSeenList = 50,
            CannotStackBoosters = 51,
            Anchor = 52,
            Unanchor = 53,
            CarrierDradis = 54,
            HoldOverflow = 55,
            SettingsInfo = 56,
            [Obsolete]
            DotArea = 57,
            [Obsolete]
            SlotStats = 58,
            ActivateOnByDefaultSlots = 59,
            WaterExchangeValues = 60,
            BonusMapParts = 61,
            Statistics = 62,
            CharacterServices = 0x3F,
            FactionChangeSuccess = 0x40,
            NameChangeSuccess = 65,
            AvatarChangeSuccess = 66,
            CharacterServiceError = 67,
            ResourceHardcap = 68
        }

        public enum DamageOverTimeEffect : ushort
        {
            SectorBorder,
            MineField,
            SpeedDebuff,
            PowerDrain,
            DradisRange
        }

        public PlayerProtocol()
            : base(ProtocolID.Player)
        {
        }

        public static PlayerProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Player) as PlayerProtocol;
        }

        public void SendPlayerID()
        {

        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = (ushort)br.ReadUInt16();

            switch ((Request)msgType)
            {
                // This case is ran when the player selects his faction. It will send to the client an
                // empty Avatar Description, so the customization will be set to default, and sends back
                // the faction, so the game will be sure that the selected faction is real. Also fater that
                // we change the scene to Avatar.
                case Request.SelectFaction:
                    SendNewAvatarDescription(index);
                    SendFaction(index, (Faction)br.ReadByte());
                    Server.GetClientByIndex(index).Character.GameLocation = GameLocation.Avatar;
                    break;
                // Since we are not using a real database yet, we can just use the fake database to check if
                // the name is available or not.
                case Request.CheckNameAvailability:
                    SendNameAvailability(index, br.ReadString());
                    break;
                case Request.ChooseName:
                    Server.GetClientByIndex(index).Character.name = br.ReadString();
                    SendName(index);
                    break;
                case Request.CreateAvatar:
                    Dictionary<AvatarItem, string> items = new Dictionary<AvatarItem, string>();

                    int num = br.ReadLength();
                    for (int i = 0; i < num; i++)
                    {
                        items[(AvatarItem)br.ReadByte()] = br.ReadString();
                    }

                    Client client = Server.GetClientByIndex(index);

                    Database.Database.CreateCharacter(client.Character.name, client.playerId.ToString(), (byte)client.Character.Faction, items);

                    Server.GetClientByIndex(index).Character.items = items;
                    SendAvatar(index);

                    SendPlayerId(index);
                    SendPlayerShips(index, 100, (uint)22131177);

                    Server.GetClientByIndex(index).Character.GameLocation = GameLocation.Space;
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        private void SendNewAvatarDescription(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)29);
            buffer.Write(0);
            buffer.Write(0);
            buffer.Write((byte)0);

            SendMessageToUser(index, buffer);
        }

        public void SendFaction(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)24);
            buffer.Write((byte)Server.GetClientByIndex(index).Character.Faction);

            SendMessageToUser(index, buffer);
        }

        public void SendFaction(int index, Faction faction)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)24);
            buffer.Write((byte)faction);

            SendMessageToUser(index, buffer);
        }

        private void SendNameAvailability(int index, string name)
        {
            BgoProtocolWriter buffer = NewMessage();
            if (Database.Database.CheckCharacterNameAvailability(name))
            {
                buffer.Write((ushort)20);
            } else
            {
                buffer.Write((ushort)21);
            }

            SendMessageToUser(index, buffer);
        }

        public void SendName(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)23);
            buffer.Write(Server.GetClientByIndex(index).Character.name);

            SendMessageToUser(index, buffer);
        }

        public void SendAvatar(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)29);

            Dictionary<AvatarItem, string> items = Server.GetClientByIndex(index).Character.items;

            buffer.Write((ushort)items.Count);
            foreach (KeyValuePair<AvatarItem, string> item in items)
            {
                buffer.Write((byte)item.Key);
                buffer.Write(item.Value);
            }
            buffer.Write((ushort)0);
            buffer.Write((byte)0);

            SendMessageToUser(index, buffer);
        }

        public void SendPlayerShips(int index, ushort shipId, uint shipGuid)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.AddShip);
            buffer.Write(shipId);
            buffer.Write(shipGuid);

            SendMessageToUser(index, buffer);
        }

        public void SetActivePlayerShip(int index, uint shipId)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.ActiveShip);
            buffer.Write(shipId);

            SendMessageToUser(index, buffer);
        }

        public void SendUnanchor(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Unanchor);
            buffer.Write((uint)SpaceEntityType.Player);
            buffer.Write((byte)0);

            SendMessageToUser(index, buffer);
        }

        public void SendPlayerId(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.ID);
            buffer.Write(Server.GetClientByIndex(index).playerId);

            SendMessageToUser(index, buffer);
        }
    }
}
