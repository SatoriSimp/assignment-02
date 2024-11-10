using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Repositories;
using System.Data;
using WebApplication1.Request;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/SilverJewelry")]
    public class SilverJewelry : ODataController
    {
        private readonly ISilverJewelryRepository _repository;

        public SilverJewelry(ISilverJewelryRepository repository)
        {
            _repository = repository;
        }

        //[Authorize(Roles = "1, 2")]
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> All()
        {
            return Ok(await _repository.GetAll());
        }

        [HttpPost]
        [Authorize(Roles = "1")]
        public async Task<IActionResult> Add([FromBody] JewelryRequest jewelry)
        {
            if (!ModelState.IsValid) return BadRequest();

            BusinessObjects.SilverJewelry sj = new BusinessObjects.SilverJewelry()
            {
                SilverJewelryId = jewelry.Id,
                SilverJewelryName = jewelry.Name,
                ProductionYear = jewelry.ProductionYear,
                MetalWeight = jewelry.MetalWeight,
                CreatedDate = jewelry.CreatedDate,
                CategoryId = jewelry.CategoryId,
                Price = jewelry.Price,
            };

            await _repository.Add(sj);
            return CreatedAtAction(nameof(Add), new { id = sj.SilverJewelryId }, jewelry);
        }

        [Authorize(Roles = "1")]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] JewelryRequest jewelry, [FromODataUri] string id)
        {
            BusinessObjects.SilverJewelry sj = new BusinessObjects.SilverJewelry()
            {
                SilverJewelryId = jewelry.Id,
                SilverJewelryName = jewelry.Name,
                ProductionYear = jewelry.ProductionYear,
                MetalWeight = jewelry.MetalWeight,
                CreatedDate = jewelry.CreatedDate,
                CategoryId = jewelry.CategoryId
            };

            await _repository.Update(sj, id);
            return Updated(jewelry);
        }

        [Authorize(Roles = "1")]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromODataUri] string id)
        {
            await _repository.Delete(id);
            return NoContent();
        }
    }
}
