using System.Collections;
using Microsoft.AspNetCore.Mvc;
using NuGet.Common;

namespace KSAApi.Services;

public class KSAService: IKSAService {

private FarmStockRepository _farmStockRepository;

public KSAService(FarmStockRepository farmStockRepository){

    this._farmStockRepository = farmStockRepository;

}
CowStock cowStock = new CowStock();

public CowStock getRandomCowStock() {

    cowStock.Date = new DateOnly();
    cowStock.Type = "Dry Grass";
    cowStock.Days = 30;
    cowStock.Quantity = 500;
    cowStock.Summary = "This is the cowstock for a month to all the cows in KS";

    return cowStock;
}

public List<FarmStock> GetFarmStockAsync(){
    return _farmStockRepository.GetAllAsync();
}

public async Task AddFarmStockAsync(FarmStock farmStock){
    await _farmStockRepository.CreateAsync(farmStock);
} 

public async Task UpdateFarmStockAsync(string Id, FarmStock farmStock){
    await _farmStockRepository.UpdateAsync(Id, farmStock);
} 

public async Task DeleteFarmStockAsync(string Id){
    await _farmStockRepository.DeleteAsync(Id);
} 

}