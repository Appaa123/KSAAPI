using KSAApi;

public interface IUserService{
    Task AddUserAsync(User newStock);

    Task DeleteUserAsync(string Id);
    Task UpdateUserAsync(string Id, User User);
    List<User> GetUserAsync();
}