using KSAApi;

public interface IKSAService{
    Task AddFarmStockAsync(FarmStock newStock);
    Task<List<FarmStock>> GetFarmStockAsync();
    public Task<CowStock> getRandomCowStock();
}