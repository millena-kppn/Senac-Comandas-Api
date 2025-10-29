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
        public ComandasDbContext _context { get; set; }
        public ComandaController(ComandasDbContext context)
        {
            _context = context;
        }
        // GET: api/<ComandaController>
        [HttpGet]
        public IResult Get()
        {
            var comandas = _context.Comandas.ToList();
            return Results.Ok(comandas);
        }
        // GET api/<ComandaController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var comanda = _context.Comandas.FirstOrDefault(c => c.Id == id);
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
                NomeCliente = comandaCreate.NomeCliente,
                NuneroMesa = comandaCreate.NumeroMesa,
            };
            var comandaItens = new List<ComandaItem>();
            foreach (var cardapioItemId in comandaCreate.CardapioItens)
            {
                var comandaItem = new ComandaItem
                {
                    CardapioItemId = cardapioItemId,
                    Comanda = Novacomanda,
                };
                comandaItens.Add(comandaItem);

                //Criar o pedidio de cozinha de acordo com o cadastro do cardapio possui preparo
                var cardapioItem = _context.CardapioItems.FirstOrDefault(c => c.Id == cardapioItemId);
            }
            Novacomanda.Itens = comandaItens;
            _context.Comandas.Add(Novacomanda);
            _context.SaveChanges();
            return Results.Created($"/api/comanda/{Novacomanda.Id}", Novacomanda);


            //CONTINUAR A EDITAR AQUI


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
