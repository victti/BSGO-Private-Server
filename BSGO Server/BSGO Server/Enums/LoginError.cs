namespace BSGO_Server
{
    internal enum LoginError
    {
        Unknown,
        AlreadyConnected,
        WrongProtocol,
        WrongSession,
        WrongUserId,
        WrongPlayerId,
        WrongPlayerName
    }
}