using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class AuthRepository
{
    private readonly IMongoCollection<User> _Auth;

    public AuthRepository(IOptions<MongoDbSettings> settings, IMongoClient client)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _Auth = database.GetCollection<User>("User");
    }

    public List<User> GetAllAsync()
    {
        try{
        
        return _Auth.Find(org => true).ToList();
    
        }
        catch (Exception ex){
            Console.WriteLine("AuthRepository.GetAllAsync failed: " + ex.ToString());
            throw; // Rethrow so it still returns 500
        }
    }

    public async Task<User> GetByIdAsync(string id) =>
        await _Auth.Find(frm => frm.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(User user) =>
        await _Auth.InsertOneAsync(user);

    public async Task UpdateAsync(string id, User frm) =>
        await _Auth.ReplaceOneAsync(f => f.Id == id, frm);

    public async Task DeleteAsync(string id) =>
        await _Auth.DeleteOneAsync(o => o.Id == id);
}
