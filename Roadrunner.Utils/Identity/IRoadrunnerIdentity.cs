namespace Roadrunner.Utils.Identity
{
    public interface IRoadrunnerIdentity
    {
        bool IsAuthenticated();
        string GetUserId();
        void ThrowUnautorizedIfNotAuthenticated();
    }
}
