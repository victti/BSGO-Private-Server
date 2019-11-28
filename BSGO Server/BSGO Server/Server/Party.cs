using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    internal class Party
    {
        public const uint NO_PARTY = 0u;

        public const float INVITE_RANGE_LIMIT = 1000f;

        private readonly List<Client> members = new List<Client>();

        public Client Leader;

        public uint PartyId
        {
            get
            {
                return (uint)Leader.index;
            }
        }

        public Party(Client Leader)
        {
            this.Leader = Leader;
            members.Add(Leader);
        }

        public void InviteMember(uint PlayerId, int inviteOwnerIndex)
        {
            CommunityProtocol.GetProtocol().SendPartyInvite(Server.GetClientByPlayerId(PlayerId.ToString()).index, PartyId, Leader.playerId, Server.GetClientByIndex(inviteOwnerIndex).Character.name);
        }

        public void AddMember(Client Player)
        {
            members.Add(Player);
            Player.Character.partyId = PartyId;

            SendPartyUpdate();
        }

        public void RemoveMember(Client Player)
        {
            members.Remove(Player);
            Player.Character.partyId = 0;

            CommunityProtocol.GetProtocol().SendParty(Player.index, 0, 0, new uint[0]);
            if (members.Count > 1)
                SendPartyUpdate();
            else
            {
                CommunityProtocol.GetProtocol().SendParty(Leader.index, 0, 0, new uint[0]);
                Server.Parties.Remove(PartyId);
            }
        }

        private void SendPartyUpdate()
        {
            foreach (Client member in members)
            {
                CommunityProtocol.GetProtocol().SendParty(member.index, PartyId, Leader.playerId, MemberIds());
            }
        }

        private uint[] MemberIds()
        {
            int length = members.Count;
            uint[] ids = new uint[length];
            for(int i = 0; i < length; i++)
            {
                ids[i] = members[i].playerId;
            }
            return ids;
        }
    }
}
