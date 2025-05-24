using KSAApi;

public interface IAuthService
{
    public Task<bool> validateUser(User user);

}