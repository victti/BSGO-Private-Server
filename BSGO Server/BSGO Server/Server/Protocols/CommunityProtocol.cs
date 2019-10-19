namespace BSGO_Server
{
    internal class CommunityProtocol : Protocol
    {
        public enum Request : byte
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

        public enum Reply : byte
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
            : base(ProtocolID.Community) {}

        public static CommunityProtocol GetProtocol() =>
            ProtocolManager.GetProtocol(ProtocolID.Community) as CommunityProtocol;
        
        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.RecruitLevel:
                    SendRecruitLevel(index);
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, ProtocolID));
                    break;
            }
        }

        private void SendRecruitLevel(int index)
        {
            using BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.RecruitLevel);
            buffer.Write((uint)1); // idk

            SendMessageToUser(index, buffer);
        }
    }
}
