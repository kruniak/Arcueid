namespace Arcueid.Server.Core;

public enum FailureReason
{
    None,
    ItemNotFound,
    ClientError,
    InvalidToken,
    Unauthorized,
    LockedOut,
    NotAllowed,
    GenericError
}
