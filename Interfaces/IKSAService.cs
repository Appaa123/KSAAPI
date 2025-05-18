using KSAApi;

public interface IKSAService{
    Task AddFarmStockAsync(FarmStock newStock);

    Task DeleteFarmStockAsync(string Id);
    Task UpdateFarmStockAsync(string Id, FarmStock farmStock);
    List<FarmStock> GetFarmStockAsync();
    public CowStock getRandomCowStock();
}