using Microsoft.Extensions.Options;
using MongoDB.Driver;

public class FarmStockRepository
{
    private readonly IMongoCollection<FarmStock> _farmStock;

    public FarmStockRepository(IOptions<MongoDbSettings> settings, IMongoClient client)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _farmStock = database.GetCollection<FarmStock>("FarmStock");
    }

    public List<FarmStock> GetAllAsync()
    {
        try{
        
        return _farmStock.Find(org => true).ToList();
    
        }
        catch (Exception ex){
            Console.WriteLine("FarmStockRepository.GetAllAsync failed: " + ex.ToString());
            throw; // Rethrow so it still returns 500
        }
    }

    public async Task<FarmStock> GetByIdAsync(string id) =>
        await _farmStock.Find(frm => frm.Id == id).FirstOrDefaultAsync();

    public async Task CreateAsync(FarmStock org) =>
        await _farmStock.InsertOneAsync(org);

    public async Task UpdateAsync(string id, FarmStock frm) =>
        await _farmStock.ReplaceOneAsync(f => f.Id == id, frm);

    public async Task DeleteAsync(string id) =>
        await _farmStock.DeleteOneAsync(o => o.Id == id);
}
