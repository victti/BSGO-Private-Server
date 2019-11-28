using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BSGO_Server
{
    internal class SettingProtocol : Protocol
    {
        public enum Request : ushort
        {
            SaveSettings = 1,
            SaveKeys = 2,
            SetSyfyShip = 5,
            SetFullScreen = 6
        }

        public enum Reply : ushort
        {
            Settings = 3,
            Keys
        }

        public SettingProtocol()
            : base(ProtocolID.Setting)
        {
        }

        public static SettingProtocol GetProtocol()
        {
            return ProtocolManager.GetProtocol(ProtocolID.Setting) as SettingProtocol;
        }

        public override void ParseMessage(int index, BgoProtocolReader br)
        {
            ushort msgType = br.ReadUInt16();

            switch ((Request)msgType)
            {
                case Request.SaveSettings:
                    IDictionary<UserSetting, object> settings = new Dictionary<UserSetting, object>();
                    int num = br.ReadUInt16();

                    for(int i = 0; i < num; i++)
                    {
                        ReadProperty(settings, br);
                    }

                    Server.GetClientByIndex(index).Character.settings = settings;
                    Database.Database.SaveSettings(Server.GetClientByIndex(index).playerId.ToString(), settings);
                    break;
                case Request.SaveKeys:
                    List<string> controlKeys = new List<string>();
                    int num2 = br.ReadUInt16();

                    for(int j = 0; j < num2; j++)
                    {
                        ushort deviceTriggerCode = br.ReadUInt16();
                        ushort action = br.ReadUInt16();
                        byte deviceModifierCode = br.ReadByte();
                        byte device = br.ReadByte();
                        byte flags = br.ReadByte();
                        byte profileNo = br.ReadByte();

                        controlKeys.Add(deviceTriggerCode + "|" + action + "|" + deviceModifierCode + "|" + device + "|" + flags + "|" + profileNo);
                    }

                    Server.GetClientByIndex(index).Character.controlKeys = controlKeys;
                    Database.Database.SaveKeys(Server.GetClientByIndex(index).playerId.ToString(), controlKeys);
                    break;
                default:
                    Log.Add(LogSeverity.ERROR, string.Format("Unknown msgType \"{0}\" on {1}Protocol.", (Request)msgType, protocolID));
                    break;
            }
        }

        public void SendSettings(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Settings);

            IDictionary<UserSetting, object> settings = Server.GetClientByIndex(index).Character.settings;
            buffer.Write((ushort)settings.Count);
            
            foreach(KeyValuePair<UserSetting, object> setting in settings)
            {
                WriteProperty(buffer, settings, setting.Key);
            }

            SendMessageToUser(index, buffer);
        }

        public void SendKeys(int index)
        {
            BgoProtocolWriter buffer = NewMessage();
            buffer.Write((ushort)Reply.Keys);

            List<string> controlKeys = Server.GetClientByIndex(index).Character.controlKeys;

            buffer.Write((ushort)controlKeys.Count);

            foreach (string key in controlKeys)
            {
                string[] parsedKey = key.Split('|');
                buffer.Write(ushort.Parse(parsedKey[0]));
                buffer.Write(ushort.Parse(parsedKey[1]));
                buffer.Write(byte.Parse(parsedKey[2]));
                buffer.Write(byte.Parse(parsedKey[3]));
                buffer.Write(byte.Parse(parsedKey[4]));
                buffer.Write(byte.Parse(parsedKey[5]));
            }

            SendMessageToUser(index, buffer);
        }

        private void WriteProperty(BgoProtocolWriter w, IDictionary<UserSetting, object> settings, UserSetting property)
        {
            object obj = settings[property];

            w.Write((byte)property);
            UserSettingValueType valueType = GetValueType(property);
            w.Write((byte)valueType);
            switch (valueType)
            {
                case UserSettingValueType.Byte:
                    w.Write((byte)obj);
                    break;
                case UserSettingValueType.Float:
                    w.Write((float)obj);
                    break;
                case UserSettingValueType.Boolean:
                    w.Write((bool)obj);
                    break;
                case UserSettingValueType.Integer:
                    w.Write((int)obj);
                    break;
                case UserSettingValueType.Float2:
                    {
                        float2 @float = (float2)obj;
                        w.Write(@float.x);
                        w.Write(@float.y);
                        break;
                    }
                case UserSettingValueType.HelpScreenType:
                    {
                        List<HelpScreenType> list = (List<HelpScreenType>)obj;
                        w.Write((ushort)list.Count);
                        foreach (HelpScreenType item in list)
                        {
                            w.Write((ushort)item);
                        }
                        break;
                    }
                default:
                    w.Write((byte)0);
                    break;
            }
        }

        private void ReadProperty(IDictionary<UserSetting, object> settings, BgoProtocolReader br)
        {
            UserSetting property = (UserSetting)br.ReadByte();
            UserSettingValueType valueType = (UserSettingValueType)br.ReadByte();

            switch (valueType)
            {
                case UserSettingValueType.Byte:
                    settings.Add(property, br.ReadByte());
                    break;
                case UserSettingValueType.Float:
                    settings.Add(property, br.ReadSingle());
                    break;
                case UserSettingValueType.Boolean:
                    settings.Add(property, br.ReadBoolean());
                    break;
                case UserSettingValueType.Integer:
                    settings.Add(property, br.ReadInt32());
                    break;
                case UserSettingValueType.Float2:
                    {
                        settings.Add(property, new float2(br.ReadSingle(), br.ReadSingle()));
                        break;
                    }
                case UserSettingValueType.HelpScreenType:
                    {
                        List<HelpScreenType> hstList = new List<HelpScreenType>();
                        int hstSize = br.ReadUInt16();
                        for(int i = 0; i < hstSize; i++)
                        {
                            hstList.Add((HelpScreenType)br.ReadUInt16());
                        }
                        settings.Add(property, hstList);
                        break;
                    }
                default:
                    settings.Add(property, br.ReadByte());
                    break;
            }
        }

        public static void ReadControlKeysFromDatabase(int index, List<string> controlKeys)
        {
            Server.GetClientByIndex(index).Character.controlKeys = controlKeys;
        }

        public static void ReadSettingsFromDatabase(int index, IDictionary<string, string> settings)
        {
            IDictionary<UserSetting, object> newSettings = new Dictionary<UserSetting, object>();

            foreach (KeyValuePair<string, string> setting in settings)
            {
                UserSetting userSetting = (UserSetting)Enum.Parse(typeof(UserSetting), setting.Key);
                UserSettingValueType valueType = GetValueType(userSetting);
                switch (valueType)
                {
                    case UserSettingValueType.Byte:
                        newSettings.Add(userSetting, ParseValueType(setting.Value));
                        break;
                    case UserSettingValueType.Float:
                        newSettings.Add(userSetting, ParseValueType(setting.Value));
                        break;
                    case UserSettingValueType.Boolean:
                        newSettings.Add(userSetting, ParseValueType(setting.Value));
                        break;
                    case UserSettingValueType.Integer:
                        newSettings.Add(userSetting, ParseValueType(setting.Value));
                        break;
                    case UserSettingValueType.Float2:
                        {
                            newSettings.Add(userSetting, ParseValueType(setting.Value));
                            break;
                        }
                    case UserSettingValueType.HelpScreenType:
                        {
                            newSettings.Add(userSetting, ParseValueType(setting.Value));
                            break;
                        }
                    default:

                        break;
                }
            }

            Server.GetClientByIndex(index).Character.settings = newSettings;
        }

        private static object ParseValueType(string str)
        {
            string[] parsedStr = str.Split('|');
            switch (parsedStr[0])
            {
                case "byte":
                    return byte.Parse(parsedStr[1]);
                case "float":
                    return float.Parse(parsedStr[1]);
                case "bool":
                    return bool.Parse(parsedStr[1]);
                case "int":
                    return int.Parse(parsedStr[1]);
                case "float2":
                    return new float2(float.Parse(parsedStr[1]), float.Parse(parsedStr[2]));
                case "hstList":
                    List<HelpScreenType> list = new List<HelpScreenType>();
                    for (int i = 0; i < int.Parse(parsedStr[1]); i++)
                    {
                        int k = 2 + i;
                        list.Add((HelpScreenType)Enum.Parse(typeof(HelpScreenType), parsedStr[k]));
                    }
                    return list;
            }
            return 0;
        }

        public static UserSettingValueType GetValueType(UserSetting setting)
        {
            switch (setting)
            {
                case UserSetting.CompletedTutorials:
                    return UserSettingValueType.HelpScreenType;
                case UserSetting.LootPanelPosition:
                case UserSetting.InventoryPanelPosition:
                    return UserSettingValueType.Float2;
                case UserSetting.MusicVolume:
                case UserSetting.SoundVolume:
                case UserSetting.ViewDistance:
                case UserSetting.FlakFieldDensity:
                case UserSetting.DeadZoneMouse:
                case UserSetting.DeadZoneJoystick:
                case UserSetting.SensitivityJoystick:
                case UserSetting.CameraZoom:
                case UserSetting.HudIndicatorMinimizeDistance:
                case UserSetting.HudIndicatorTextSize:
                case UserSetting.HudIndicatorDescriptionDisplayDistance:
                    return UserSettingValueType.Float;
                case UserSetting.MouseWheelBinding:
                case UserSetting.SystemMap3DTransitionMode:
                case UserSetting.SystemMap3DCameraView:
                case UserSetting.HudIndicatorColorScheme:
                    return UserSettingValueType.Byte;
                case UserSetting.CameraMode:
                case UserSetting.GraphicsQuality:
                case UserSetting.Layout:
                case UserSetting.AntiAliasing:
                case UserSetting.FogQuality:
                    return UserSettingValueType.Integer;
                case UserSetting.CombatGui:
                case UserSetting.ShowTutorial:
                case UserSetting.InvertedVertical:
                case UserSetting.Fullscreen:
                case UserSetting.StatsIndication:
                case UserSetting.HudIndicatorShowShipNames:
                case UserSetting.AssignmentsCollapsed:
                case UserSetting.ShowStarDust:
                case UserSetting.ShowStarFog:
                case UserSetting.ShowGlowEffect:
                case UserSetting.ShowChangeFaction:
                case UserSetting.ChatShowPrefix:
                case UserSetting.ChatViewLocal:
                case UserSetting.ChatViewGlobal:
                case UserSetting.AutoLoot:
                case UserSetting.Fullframe:
                case UserSetting.ShowPopups:
                case UserSetting.ShowOutpostMessages:
                case UserSetting.ShowHeavyFightingMessages:
                case UserSetting.ShowAugmentMessages:
                case UserSetting.ShowMiningShipMessages:
                case UserSetting.ShowExperienceMessages:
                case UserSetting.HudIndicatorShowWingNames:
                case UserSetting.AdvancedFlightControls:
                case UserSetting.UseProceduralTextures:
                case UserSetting.ShowFpsAndPing:
                case UserSetting.ShowBulletImpactFx:
                case UserSetting.CombatText:
                case UserSetting.ShowEnemyIndication:
                case UserSetting.ShowFriendIndication:
                case UserSetting.HudIndicatorShowMissionArrow:
                case UserSetting.AutomaticAmmoReload:
                case UserSetting.ShowWofConfirmation:
                case UserSetting.JoystickGamepadEnabled:
                case UserSetting.ShowXbox360Buttons:
                case UserSetting.HudIndicatorShowTitles:
                case UserSetting.HighResModels:
                case UserSetting.HighResTextures:
                case UserSetting.HighQualityParticles:
                case UserSetting.AnisotropicFiltering:
                case UserSetting.ShowCutscenes:
                case UserSetting.MuteSound:
                case UserSetting.ShowDamageOverlay:
                case UserSetting.ShowShipSkins:
                case UserSetting.ShowWeaponModules:
                case UserSetting.SystemMap3DShowAsteroids:
                case UserSetting.SystemMap3DShowDynamicMissions:
                case UserSetting.SystemMap3DFormAsteroidGroups:
                case UserSetting.HudIndicatorShowTargetNames:
                case UserSetting.HudIndicatorShowShipTierIcon:
                case UserSetting.HudIndicatorBracketResizing:
                case UserSetting.HudIndicatorSelectionCrosshair:
                case UserSetting.HudIndicatorHealthBar:
                case UserSetting.ShowAssignmentMessages:
                case UserSetting.ShowXpBar:
                case UserSetting.VSync:
                case UserSetting.FramerateCapping:
                    return UserSettingValueType.Boolean;
                default:
                    Log.Add(LogSeverity.ERROR, "No value Type found for: " + setting + " (assuming bool)");
                    return UserSettingValueType.Boolean;
            }
        }
    }
}
