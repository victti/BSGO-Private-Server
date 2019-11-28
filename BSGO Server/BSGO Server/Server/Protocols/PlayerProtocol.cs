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
                    if (Server.GetClientByIndex(index).Character.Faction == Faction.Colonial)
                    {
                        SendPlayerShips(index, 11, 22131180u);
                        SetActivePlayerShip(index, 11);
                    } else if (Server.GetClientByIndex(index).Character.Faction == Faction.Cylon)
                    {
                        SendPlayerShips(index, 11, 22131208u);
                        SetActivePlayerShip(index, 11);
                    }
                    CommunityProtocol.GetProtocol().SendChatSessionId(index);

                    client.Character.PlayerShip.sectorId = client.Character.Faction == Faction.Colonial ? 0u : 6u;

                    Server.GetClientByIndex(index).Character.GameLocation = GameLocation.Space;
                    break;
                case Request.SelectShip:
                    SetActivePlayerShip(index, br.ReadUInt16());
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
            Server.GetClientByIndex(index).Character.Faction = faction;

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

        public void SetActivePlayerShip(int index, ushort shipId)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.ActiveShip);
            buffer.Write(shipId);

            SendMessageToUser(index, buffer);

            Server.GetClientByIndex(index).Character.PlayerShip.HangarId = shipId;
            SendStats(index); // These are the stats of your ship, not the base ones.
            SendShipInfo(index);
            SendSlots(index);

            SendStatsPowerPoints(index);
            SendStatsHullPoints(index);
        }

        public void SendUnanchor(int index, uint objId)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Unanchor);
            buffer.Write(objId);
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

        public void SendStats(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Stats);

            ShipCard currentShip = ((ShipCard)Catalogue.FetchCard(Server.GetClientByIndex(index).Character.PlayerShip.WorldGuid, CardView.Ship));

            buffer.Write((ushort)currentShip.Stats.ObjStats.Count);

            foreach (KeyValuePair<ObjectStat, float> stat in currentShip.Stats.ObjStats)
            {
                buffer.Write((byte)1);
                buffer.Write((ushort)stat.Key);
                buffer.Write(stat.Value);
            }

            SendMessageToUser(index, buffer);
        }

        public void SendStatsPowerPoints(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Stats);

            ShipCard currentShip = ((ShipCard)Catalogue.FetchCard(Server.GetClientByIndex(index).Character.PlayerShip.WorldGuid, CardView.Ship));

            buffer.Write((ushort)1);

            buffer.Write((byte)6);
            buffer.Write(currentShip.Stats.MaxPowerPoints);

            SendMessageToUser(index, buffer);
        }

        public void SendStatsHullPoints(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Stats);

            ShipCard currentShip = ((ShipCard)Catalogue.FetchCard(Server.GetClientByIndex(index).Character.PlayerShip.WorldGuid, CardView.Ship));

            buffer.Write((ushort)1);

            buffer.Write((byte)7);
            buffer.Write(currentShip.Stats.MaxHullPoints);

            SendMessageToUser(index, buffer);
        }

        public void SendShipInfo(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.ShipInfo);

            uint worldGuid = Server.GetClientByIndex(index).Character.PlayerShip.WorldGuid;
            ShipCard currentShip = (ShipCard)Catalogue.FetchCard(worldGuid, CardView.Ship);

            buffer.Write((ushort)currentShip.HangarID);
            buffer.Write(currentShip.Durability);

            SendMessageToUser(index, buffer);
        }

        //Unfinished
        public void SendSlots(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Slots);

            uint worldGuid = Server.GetClientByIndex(index).Character.PlayerShip.WorldGuid;
            ShipCard currentShip = (ShipCard)Catalogue.FetchCard(worldGuid, CardView.Ship);

            buffer.Write((ushort)currentShip.HangarID);
            int slotsCount = currentShip.Slots.Count;

            buffer.Write((ushort)slotsCount);

            for(int i = 0; i < slotsCount; i++)
            {
                buffer.Write(currentShip.Slots[i].SlotId);
                buffer.Write((byte)0);
                buffer.Write((uint)1);
                buffer.Write(false);
            }

            SendMessageToUser(index, buffer);
        }

        public void SendItems(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.HoldItems);

            //size
            buffer.Write((ushort)1);
            buffer.Write((ushort)1); //serverId
            buffer.Write((byte)2); //itemType
            buffer.Write(215278030u);
            buffer.Write(1000000);

            SendMessageToUser(index, buffer);
        }

        public void SendCylonDuties(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Duties);

            List<Duty> dutyList = new List<Duty>() { new Duty(40, 2884276912), new Duty(25, 3631064602), new Duty(24, 411842165), new Duty(23, 877373150), new Duty(21, 323350849), new Duty(20, 642749237), new Duty(19, 1662453525), new Duty(18, 1873016968), new Duty(17, 2281196597), new Duty(16, 1408844491), new Duty(15, 2377323303), new Duty(14, 2258791199), new Duty(13, 1268650759), new Duty(12, 59322080), new Duty(10, 4282113518), new Duty(9, 1440120643), new Duty(8, 1291811644), new Duty(7, 2896850115), new Duty(5, 2200949658), new Duty(4, 3607566419), new Duty(3, 1287173964), new Duty(2, 3999754698), new Duty(1, 3065590220), };
            buffer.Write((ushort)dutyList.Count);
            foreach(Duty duty in dutyList)
            {
                buffer.Write(duty.serverID);
                buffer.Write(duty.cardGUID);
            }

            SendMessageToUser(index, buffer);
        }
    }
}
