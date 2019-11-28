namespace BSGO_Server
{
    internal class CommunityProtocol : Protocol
    {
        public enum Request : ushort
        {
            PartyInvitePlayer = 1,
            PartyDismissPlayer = 2,
            PartyLeave = 3,
            PartyAppointLeader = 4,
            PartyAccept = 5,
            FriendInvite = 6,
            FriendAccept = 7,
            FriendRemove = 8,
            IgnoreAdd = 9,
            IgnoreRemove = 10,
            ChatConnected = 11,
            ChatFleetAllowed = 12,
            ChatAuthFailed = 13,
            GuildStart = 14,
            GuildLeave = 0xF,
            GuildChangeRankName = 0x10,
            GuildChangeRankPermissions = 17,
            GuildPromote = 18,
            GuildKick = 19,
            GuildInvite = 21,
            GuildAccept = 22,
            RecruitInvited = 24,
            PartyChatInvite = 0x20,
            RecruitLevel = 33,
            PartyMemberFtlState = 34,
            IgnoreClear = 35
        }

        public enum Reply : ushort
        {
            Party = 1,
            PartyIgnore = 2,
            PartyInvite = 3,
            FriendInvite = 4,
            FriendAccept = 5,
            FriendRemove = 6,
            FriendAdd = 7,
            IgnoreAdd = 8,
            IgnoreRemove = 9,
            ChatSessionId = 10,
            ChatFleetAllowed = 11,
            GuildQuit = 12,
            GuildRemove = 13,
            GuildInvite = 14,
            GuildInfo = 0xF,
            GuildSetPromotion = 0x10,
            GuildMemberUpdate = 17,
            GuildSetChangeRankName = 18,
            GuildSetChangePermissions = 19,
            GuildStartError = 21,
            GuildJoinError = 22,
            GuildInviteResult = 23,
            GuildOperationResult = 24,
            Recruits = 26,
            ActivateJumpTargetTransponder = 37,
            CancelJumpTargetTransponder = 38,
            PartyAnchor = 39,
            PartyChatInviteFailed = 40,
            RecruitLevel = 41,
            PartyMemberFtlState = 42
        }

        public CommunityProtocol()
            : base(ProtocolID.Community)
        {
        }

        public static CommunityProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Community) as CommunityProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.RecruitLevel:
                    SendRecruitLevel(index);
                    break;
                case Request.PartyInvitePlayer:
                    uint partyInvitedPlayerId = br.ReadUInt32();

                    if (Server.GetClientByPlayerId(partyInvitedPlayerId.ToString()).Character.partyId != 0)
                    {
                        SendPartyIgnore(Server.GetClientByPlayerId(partyInvitedPlayerId.ToString()).index, Server.GetClientByIndex(index).Character.name, 1);
                        break;
                    }

                    Party p = Server.GetPartyById(Server.GetClientByIndex(index).Character.partyId);
                    if(p == null)
                    {
                        SendPartyInvite(Server.GetClientByPlayerId(partyInvitedPlayerId.ToString()).index, (uint)index, Server.GetClientByIndex(index).playerId, Server.GetClientByIndex(index).Character.name);
                        break;
                    }
                    p.InviteMember(partyInvitedPlayerId, index);
                    break;
                case Request.PartyAccept:
                    uint partyAcceptPartyId = br.ReadUInt32();
                    uint partyAcceptInviterId = br.ReadUInt32();
                    bool partyAcceptBool = br.ReadBoolean();

                    if (!partyAcceptBool) {
                        SendPartyIgnore(Server.GetClientByPlayerId(partyAcceptInviterId.ToString()).index, Server.GetClientByIndex(index).Character.name, 2);
                        break;
                    }
                    Party p2 = Server.GetPartyById(partyAcceptPartyId);
                    if(p2 == null)
                    {
                        Server.Parties.Add(partyAcceptPartyId, new Party(Server.GetClientByPlayerId(partyAcceptInviterId.ToString())));
                        Party p3 = Server.GetPartyById(partyAcceptPartyId);
                        p3.AddMember(Server.GetClientByIndex(index));
                        break;
                    }
                    p2.AddMember(Server.GetClientByIndex(index));
                    break;
                case Request.PartyLeave:
                    Party p4 = Server.GetPartyById(Server.GetClientByIndex(index).Character.partyId);
                    if(p4 != null)
                        p4.RemoveMember(Server.GetClientByIndex(index));

                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        private void SendRecruitLevel(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.RecruitLevel);
            buffer.Write((uint)1); // idk

            SendMessageToUser(index, buffer);
        }

        public void SendChatSessionId(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.ChatSessionId);
            buffer.Write(index.ToString());
            buffer.Write(Server.ChatProjectID);
            buffer.Write("en");
            buffer.Write("127.0.0.1");

            SendMessageToUser(index, buffer);
        }

        public void SendPartyInvite(int index, uint partyId, uint leaderId, string characterName)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.PartyInvite);

            buffer.Write(partyId);
            buffer.Write(leaderId);
            buffer.Write(characterName);

            SendMessageToUser(index, buffer);
        }

        public void SendPartyIgnore(int index, string characterName, byte reason)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.PartyIgnore);

            buffer.Write(characterName);
            buffer.Write(reason);

            SendMessageToUser(index, buffer);
        }

        public void SendParty(int index, uint partyId, uint leaderId, uint[] memberIds)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Party);

            buffer.Write(partyId);
            buffer.Write(leaderId);

            buffer.Write((ushort)memberIds.Length);
            foreach (uint memberId in memberIds)
            {
                buffer.Write(memberId);
            }

            buffer.Write((ushort)0); //temp

            SendMessageToUser(index, buffer);
        }
    }
}
