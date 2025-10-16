using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController] //define que essa classe é um controller de API
    public class CardapioItemController : ControllerBase // herda de ControllerBase para poder responder requisições HTTP
    {
        static public List<CardapioItem> cardapios = new List<CardapioItem>() {
            new CardapioItem
            {
                Id = 1,
                Descricao = "Coxinha",
                Preco = 5.50m,
                PossuiPreparo = true
            },
            new CardapioItem
            {
                Id = 2,
                Descricao = "X-Salada",
                Preco = 25.50m,
                PossuiPreparo = true
            }
        };
        // metodo get que retorna a lista de cardapio
        // GET: api/<CardapioItemController>
        [HttpGet] // Anotação que indica que esse método responde a requisições GET
        public IResult GetCardapios()
        {
            //cria uma lista estatica de cardápio e transforma em json
            return Results.Ok(cardapios);
        }
        // GET api/<CardapioItemController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            //BUSCA NA LISTA de cardapios de acordo com ID do parametro
            //Joga o valor para a variavel o primeiro elemento de acordo com o id
            var cardapio = cardapios.FirstOrDefault(c => c.Id == id);
            if (cardapio == null)
            {
                //se nao encontrar o cardapio com o id, retorna 404
                return Results.NotFound("Cardápio não encontrado!");
            }
            //retorna o valor para o endpoint da api
            return Results.Ok(cardapio);

        }

        // POST api/<CardapioItemController>
        [HttpPost]
        public IResult Post([FromBody] CardapioItemCreateRequest cardapio)
        {
            if (cardapio.Titulo.Length < 3)
              return Results.BadRequest("O título deve ter no mínimo 3 caracteres.");
            if (cardapio.Descricao.Length < 3)
                return Results.BadRequest("A descrição deve ter no mínimo 3 caracteres.");
            if (cardapio.Preco <= 0)
                return Results.BadRequest("O preço deve ser maior que zero.");
            var cardapioItem = new CardapioItem
            {
                Id = cardapios.Count + 1,
                Titulo = cardapio.Titulo,
                Descricao = cardapio.Descricao,
                Preco = cardapio.Preco,
                PossuiPreparo = cardapio.PossuiPreparo
            };
            // adiciona o cardapio na lista
            cardapios.Add(cardapioItem);
            return Results.Created($"/api/cardapio/{cardapioItem.Id}", cardapioItem);

        }

        // PUT api/<CardapioItemController>/5
        [HttpPut("{id}")]
        static IResult Put(int id, [FromBody] CardapioItemUpdateRequest cardapio)
        {
            var cardapioItem = cardapios.FirstOrDefault(c => c.Id == id);
            if (cardapioItem is null)
                return Results.NotFound($"Cardápio {id} não encontrado!");
                cardapioItem.Titulo = cardapio.Titulo;
                cardapioItem.Descricao = cardapio.Descricao;
                cardapioItem.Preco = cardapio.Preco;
                cardapioItem.PossuiPreparo = cardapio.PossuiPreparo;
                return Results.NoContent();
        }

        // DELETE api/<CardapioItemController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            //busca o cardapio na lista pelo id
            var cardapioItem = cardapios
                .FirstOrDefault(c => c.Id == id);
            //se estiver nulo, retorna 404
            if (cardapioItem is null)
                return Results.NotFound($"Cardápio {id} não encontrado!");
            //remove o cardapio da lista
            var removidoComSucesso = cardapios.Remove(cardapioItem);
            //se removido com sucesso, retorna 204
            if (removidoComSucesso)
                return Results.NoContent();
            //se nao, retorna 500
                return Results.StatusCode(500);
        }
    }
}
