using KooliProjekt.Data;
using KooliProjekt.Models;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KooliProjekt.Controllers
{
    [Route("api/Amount")]
    [ApiController]
    public class AmountApiController : ControllerBase
    {
        private readonly IAmountService _service;

        public AmountApiController(IAmountService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IEnumerable<Amount>> Get()
        {
            // Create an empty search object if you don't want any filters applied
            var search = new amountSearch();
            var result = await _service.List(1, 10000, search);  // Passing the search parameter
            return result.Results;
        }

        [HttpGet("{id}")]
        public async Task<object> Get(int id)
        {
            var list = await _service.Get(id);
            if (list == null)
            {
                return NotFound();
            }

            return list;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Amount list)
        {
            if (id != list.AmountID)  // Changing 'Id' to 'AmountID'
            {
                return BadRequest();
            }

            await _service.Save(list);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var list = await _service.Get(id);
            if (list == null)
            {
                return NotFound();
            }

            await _service.Delete(id);

            return Ok();
        }
    }
}
