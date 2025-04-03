using KSAApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KSAApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class FarmStockController : ControllerBase
    {

        public readonly IKSAService _ksaService;
        private static List<FarmStock> farmStockList = new List<FarmStock>();

        public FarmStockController(IKSAService kSAService){
            _ksaService = kSAService;
        }

        [HttpGet]

        public async Task<List<FarmStock>> GetCowStock(){
            return await _ksaService.GetFarmStockAsync();
        }

        [HttpPut]

        public IActionResult UpdateFarmStock(string Id, [FromBody] FarmStock farmStock){
            if (farmStock == null)
            {
                  return BadRequest("Invalid data.");
            }
            _ksaService.UpdateFarmStockAsync(Id, farmStock);
            farmStockList.Add(farmStock);
            return CreatedAtAction(nameof(GetStockById), new { id = farmStock.Id }, farmStock);
        }

        [HttpDelete]

         public IActionResult DeleteFarmStock(string Id){
            var stock = farmStockList.FirstOrDefault(s => s.Id == Id);
            // if (stock == null)
            // {
            //     return NotFound();
            // }            
            try{
            _ksaService.DeleteFarmStockAsync(Id);
            }
            catch (Exception ex) {
             Console.WriteLine(ex);
            }

            Console.WriteLine(stock + "Deleted successfully");
            return Ok(stock);
        }


        [HttpPost]

        public IActionResult AddFarmStock([FromBody] FarmStock newStock){
            if (newStock == null)
            {
                  return BadRequest("Invalid data.");
            }

            _ksaService.AddFarmStockAsync(newStock);
            farmStockList.Add(newStock);
            return CreatedAtAction(nameof(GetStockById), new { id = newStock.Id }, newStock);
        }

          [HttpGet("{id}")]
         public IActionResult GetStockById(string id)
        {
            var stock = farmStockList.FirstOrDefault(s => s.Id == id);
            if (stock == null)
            {
                return NotFound();
            }
            return Ok(stock);
        }
    }
} 
