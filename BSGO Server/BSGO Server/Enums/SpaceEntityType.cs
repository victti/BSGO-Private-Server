using System;
using System.Collections.Generic;
using System.Text;

namespace BSGO_Server
{
    enum SpaceEntityType : uint
    {
        Player = 0x1000000,
        Missile = 0x2000000,
        WeaponPlatform = 50331648u,
        Cruiser = 0x4000000,
        BotFighter = 83886080u,
        AsteroidBot = 184549376u,
        Debris = 100663296u,
        Asteroid = 117440512u,
        CargoObject = 0x8000000,
        MiningShip = 150994944u,
        Outpost = 167772160u,
        Trigger = 201326592u,
        Planet = 218103808u,
        Planetoid = 234881024u,
        Mine = 251658240u,
        Volume = 0x10000000,
        JumpBeacon = 285212672u,
        SectorEvent = 301989888u,
        MineField = 318767104u,
        JumpTargetTransponder = 335544320u,
        Comet = 352321536u,
        SmartMine = 369098752u,
        CaptureTrigger = 385875968u,
        TypeMask = 520093696u,
        SpaceLocationMarker = 4026531840u,
        AsteroidGroup = 4043309056u,
        SectorMap3DFocusPoint = 4060086272u
    }
}
