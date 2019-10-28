namespace BSGO_Server
{
    internal enum ManeuverType : byte
    {
        Pulse,
        Teleport,
        Rest,
        Warp,
        Directional,
        Launch,
        Rotation,
        Flip,
        Turn,
        Follow,
        DirectionalWithoutRoll,
        TurnQweasd,
        TurnToDirectionStrikes,
        TurnByPitchYawStrikes,
        TargetLaunch
    }
}
