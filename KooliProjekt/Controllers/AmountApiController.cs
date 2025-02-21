using System.Numerics;
using KooliProjekt.Data;
using KooliProjekt.Services;
using Microsoft.AspNetCore.Mvc;


namespace KooliProjekt.Controllers
{
    [Route("api/Amounts")]
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
            var result = await _service.List(1, 10000);
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

        [HttpPost]
        public async Task<object> Post([FromBody] Amount list)
        {
            await _service.Save(list);

            return Ok(list);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Amount list)
        {
            if (id != list.AmountID)
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