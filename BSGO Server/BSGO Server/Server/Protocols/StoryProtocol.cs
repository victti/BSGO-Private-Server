namespace BSGO_Server
{
    internal class StoryProtocol : Protocol
    {
        public enum Reply : ushort
        {
            BannerBox = 1,
            MessageBox,
            HelpBox,
            Mark,
            MissionLog,
            SelectTarget,
            HighlightControl,
            Progress,
            HighlightObject,
            CloseMessageBoxes,
            CloseMissionLog,
            PlayCutscene,
            SimplifyTutorialUi,
            AddSkipButton,
            EnableTargetting,
            StartLookAtTriggerClientCheck,
            EnableMissileTutorial,
            ShowOnScreenNotification,
            EnableGear,
            AskContinue
        }

        public enum Request : ushort
        {
            TriggerControl = 1,
            MessageBoxOk,
            Skip,
            Abandon,
            Continue,
            Decline,
            CutsceneFinished,
            LookingAtTrigger
        }

        public enum ControlType : ushort
        {
            TargetEnemy = 19,
            TargetAlly = 20,
            ThrottleBar = 21,
            Boosters = 22,
            Camera = 23,
            SystemMap = 24,
            MatchSpeed = 25,
            Follow = 26,
            Turn = 27,
            Strafe = 28,
            Roll = 29,
            Tournament = 51,
            Weapon1 = 101,
            Weapon2 = 102,
            Weapon3 = 103,
            Weapon4 = 104,
            Weapon5 = 105,
            Weapon6 = 106,
            Weapon7 = 107,
            Weapon8 = 108,
            Weapon9 = 109,
            Weapon10 = 110,
            Weapon11 = 111,
            Weapon12 = 112,
            WEAPON_MAX = 113,
            Ability1 = 114,
            Ability2 = 115,
            Ability3 = 116,
            Ability4 = 117,
            Ability5 = 118,
            Ability6 = 119,
            Ability7 = 120,
            Ability8 = 121,
            Ability9 = 122,
            Ability10 = 123,
            ABILITY_MAX = 126,
            HpGuiSlot = 130
        }

        public StoryProtocol()
            : base(ProtocolIDType.Story) { }

        public static StoryProtocol GetProtocol() =>
            ProtocolManager.GetProtocol(ProtocolIDType.Story) as StoryProtocol;
        

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch (msgType)
            {
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, ProtocolID));
                    break;
            }
        }

        public void EnableGear(int index, bool enable)
        {
            using BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.EnableGear);

            buffer.Write(enable);

            SendMessageToUser(index, buffer);
        }
    }
}
