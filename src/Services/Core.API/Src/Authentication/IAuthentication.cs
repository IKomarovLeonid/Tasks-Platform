namespace Core.API.Authentication
{
    public interface IAuthentication
    {
        bool IsAuthenticated(AuthenticationContext context);

        string GenerateToken(AuthenticationContext context);
    }
}
