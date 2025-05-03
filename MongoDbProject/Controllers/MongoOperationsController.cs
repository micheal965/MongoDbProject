using Microsoft.AspNetCore.Mvc;
using MongoDbProject.Services;

namespace MongoDbProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MongoOperationsController : ControllerBase
    {
        private readonly IMongoDbServices _mongoDbServices;

        public MongoOperationsController(IMongoDbServices mongoDbServices)
        {
            _mongoDbServices = mongoDbServices;
        }
        //1
        [HttpPost("Create-Collections")]
        public async Task<IActionResult> CreateCollections()
        {

            await _mongoDbServices.CreateCollectionAsync();
            return Ok("Collections created successfully");
        }
        [HttpPost("Insert-Into-User-Collection")]
        public async Task<IActionResult> InsertDataIntoUsersCollection()
        {

            await _mongoDbServices.InsertDataIntoUsersCollectionAsync();
            return Ok("Data Inserted into UserCollection successfully");
        }
        [HttpPost("Insert-Into-Product-Collection")]
        public async Task<IActionResult> InsertDataIntoProductCollection()
        {

            await _mongoDbServices.InsertDataIntoProductCollectionAsync();
            return Ok("Data Inserted into ProdutCollection successfully");
        }

        //2
        [HttpPut("Update-Document")]
        public async Task<IActionResult> UpdateDocument()
        {

            await _mongoDbServices.UpdateDocumentAsync();
            return Ok("Data updated successfully");
        }
        //3
        [HttpDelete("Delete-Document")]
        public async Task<IActionResult> DeleteDocuments()
        {

            await _mongoDbServices.DeleteDocumentsAsync();
            return Ok("Data deleted successfully");
        }

        //4
        [HttpGet("Find-Or")]
        public async Task<IActionResult> FindOr()
        {

            var result = await _mongoDbServices.Find_Or();
            return Ok(result);
        }
        [HttpGet("Find-And")]
        public async Task<IActionResult> FindAnd()
        {

            var result = await _mongoDbServices.Find_And();
            return Ok(result);
        }
        [HttpGet("Find-In")]
        public async Task<IActionResult> FindIn()
        {

            var result = await _mongoDbServices.Find_in();
            return Ok(result);
        }
        [HttpGet("Find-All")]
        public async Task<IActionResult> FindAll()
        {

            var result = await _mongoDbServices.Find_all();
            return Ok(result);
        }
        [HttpGet("Find-Size")]
        public async Task<IActionResult> FindSize()
        {

            var result = await _mongoDbServices.Find_size();
            return Ok(result);
        }
        [HttpGet("Find-Exp")]
        public async Task<IActionResult> FindExp()
        {

            var result = await _mongoDbServices.Find_exp();
            return Ok(result);
        }

        //5
        [HttpPost("Create-Single-Index")]
        public async Task<IActionResult> CreateSingleFieldIndex()
        {

            await _mongoDbServices.CreateSingleFieldIndex();
            return Ok("Single Index created successfully");
        }
        [HttpPost("Create-Compound-Index")]
        public async Task<IActionResult> CreateCompoundIndex()
        {

            await _mongoDbServices.CreateCompoundIndex();
            return Ok("Compound Index created successfully");
        }
        [HttpPost("Create-Unique-Index")]
        public async Task<IActionResult> CreateUniqueIndex()
        {

            await _mongoDbServices.CreateUniqueIndex();
            return Ok("UniqueIndex created successfully");
        }
        //6
        [HttpPost("MultiplyAllArrayElements")]
        public async Task<IActionResult> GetMultiplyAllArrayElements()
        {
            await _mongoDbServices.MultiplyAllArrayElements();
            return Ok("Operation Done");
        }
    }
}
