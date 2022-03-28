namespace Core.API.Authentication
{
    public interface IAuthentication
    {
        bool IsAuthenticated(AuthenticationContext context);
    }
}
