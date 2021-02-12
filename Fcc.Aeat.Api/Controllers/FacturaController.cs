using Fcc.Aeat.Api.Models;
using Fcc.Aeat.Factura.Queries.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Fcc.Aeat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FacturaController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IFacturaQueries _facturaQueries;

        public FacturaController(IFacturaQueries facturaQueries, IMediator mediator)
        {
            _facturaQueries = facturaQueries;
            _mediator = mediator;
        }

        // GET: api/<FacturaController>
        [HttpGet]
        public async Task<IActionResult> Get([FromBody] FacturaRequestDto facturaRequestDto)
        {
            try
            {

                var facturaRequest = FacturaRequestDto.MapToFacturaRequest(facturaRequestDto);
                var facturas = await _facturaQueries.GetAll(facturaRequest);
                return Ok(facturas);
            }
            catch (System.Exception ex)
            {
                return BadRequest(new ProblemDetails
                {
                    Status = 500,
                    Detail = ex.Message
                });
            }
        }

        // GET api/<FacturaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<FacturaController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FacturaRequestDto facturaRequestDto)
        {
            var facturaAddCommand = FacturaRequestDto.MapToFacturaAddCommand(facturaRequestDto);
            await _mediator.Send(facturaAddCommand);
            return Ok();
        }

        // PUT api/<FacturaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<FacturaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
