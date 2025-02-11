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
