using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MesaController : ControllerBase
    {

        List<Mesa> mesas = new List<Mesa>()
        {
            new Mesa
            {
                Id = 1,
                NumeroMesa = 1,
                SituacaoMesa = 0 //0 - Disponível, 1 - Ocupada, 2 - Reservada
            },
            new Mesa
            {
                Id = 2,
                NumeroMesa = 2,
                SituacaoMesa = 1 //0 - Disponível, 1 - Ocupada, 2 - Reservada
            }
        };
        // GET: api/<MesaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<MesaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MesaController>
        [HttpPost]
        public void Post([FromBody] MesaCreateRequest mesaCreate)
        {
        }

        // PUT api/<MesaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] MesaUpdateRequest mesaUpadate)
        {
            if (mesaUpadate.NumeroMesa <= 0)
              return Results.BadRequest("O número da mesa deve ser maior que zero.");
            if (mesaUpadate.SituacaoMesa < 0 || mesaUpadate.SituacaoMesa > 2)
              return Results.BadRequest("A situação da mesa deve ser 0 (Disponível), 1 (Ocupada) ou 2 (Reservada).");
            var mesa =  mesas.FirstOrDefault(m => m.Id == id);
            if (mesa is null)
              return Results.NotFound("Mesa não encontrada!");
            mesa.NumeroMesa = mesaUpadate.NumeroMesa;
            mesa.SituacaoMesa = mesaUpadate.SituacaoMesa;
              return Results.NoContent();
        }

        // DELETE api/<MesaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
