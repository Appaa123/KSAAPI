using KSAApi;

public interface IAuthService{
    Task AddUserAsync(User newStock);

    Task DeleteUserAsync(string Id);
    Task UpdateUserAsync(string Id, User User);
    List<User> GetUserAsync();
}