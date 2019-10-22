namespace BSGO_Server
{
    internal enum BgoAdminRoles : uint
    {
        None = 0u,
        View = 1u,
        Edit = 2u,
        Ban = 4u,
        CommunityManager = 8u,
        Developer = 0x10,
        Console = 0x20,
        GodMode = 0x400,
        Mod = 0x800
    }
}