using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ComandaController : ControllerBase
    {
       static List<Comanda> comandas = new List<Comanda>()
        {
            new Comanda
            {
               Id = 1,
               NomeCliente = "João Queiroz",
               NuneroMesa = 1,
               Itens = new List<ComandaItem>()
               {
                  new ComandaItem
                  {
                   Id = 1,
                   CardapioItemId = 1,
                   ComandaId = 1,
                  }
               } 
            },
            new Comanda
            {
               Id = 2,
               NomeCliente = "Maria Silva",
               NuneroMesa = 2,
               Itens = new List<ComandaItem>()
               {
                  new ComandaItem
                  {
                   Id = 2,
                   CardapioItemId = 2,
                   ComandaId = 2,
                  }
               }
            }
        };
        // GET: api/<ComandaController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(comandas);
        }

        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null)
            {
                return Results.NotFound("Comanda não encontrada!");
            }
            return Results.Ok(comanda);
        }

        // POST api/<ComandaController>
        [HttpPost]
        public IResult Post([FromBody] ComandaCreateRequest comandaCreate)
        {
            if (comandaCreate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            if (comandaCreate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter no mínimo 3 caracteres.");
            var Novacomanda = new Comanda
            {
                Id = comandas.Count + 1,
                NomeCliente = comandaCreate.NomeCliente,
                NuneroMesa = comandaCreate.NumeroMesa,
            };
            var comandaItens = new List<ComandaItem>();
            foreach (var cardapioItemId in comandaCreate.CardapioItens)
            {
                var comandaItem = new ComandaItem
                {
                    Id = comandaItens.Count + 1,
                    CardapioItemId = cardapioItemId,
                    ComandaId = Novacomanda.Id,
                };
                comandaItens.Add(comandaItem);
            }
            Novacomanda.Itens = comandaItens;
            comandas.Add(Novacomanda);
            return Results.Created($"/api/comanda/{Novacomanda.Id}", Novacomanda);

        }

        // PUT api/<ComandaController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] ComandaUpdateRequest comandaUpdate)
        {
            var comanda = comandas.FirstOrDefault(c => c.Id == id);
            if (comanda is null)
                return Results.NotFound("Comanda não encontrada!");
            if (comandaUpdate.NomeCliente.Length < 3)
                return Results.BadRequest("O nome do cliente deve ter no mínimo 3 caracteres.");
            if (comandaUpdate.NumeroMesa <= 0)
                return Results.BadRequest("O número da mesa deve ser maior que zero.");
            //atualiza as informações da comanda
            comanda.NomeCliente = comandaUpdate.NomeCliente;
            comanda.NuneroMesa = comandaUpdate.NumeroMesa;
            //retorna 204 Sem conteudo
            return Results.NoContent();
        }

        // DELETE api/<ComandaController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            //pesquisa uma comanda na Lista de comandas pelo ID de comanda
            //que veio no paremetro da REQUSET
            var comanda = comandas.FirstOrDefault(c => c.Id == id);
            //se não encontrou a comanda pesquisa
            if (comanda is null)
            //retorna um codigo 404 não encontrado
                return Results.NotFound("Comandas não encontrada");
            //remove a comanda da lista de comandas 
            var removidosComSucesso = comandas.Remove(comanda);
            if (removidosComSucesso)
            //retorna 204 sem conteudo
                return Results.NoContent();
            return Results.StatusCode(500);
        }
    }
}
