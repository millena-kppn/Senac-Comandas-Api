using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860
namespace Comandas.Api.Controllers
{
    //CRIA A ROTA DO CONTROLADOR
    [Route("api/[controller]")]
    [ApiController]//DEFINE QUE ESSA CLASSE É UM CONTROLADOR DE API
    public class CardapioItemController : ControllerBase//HERDA DE ControllerBase para PODER RESPONDER A REQUISIÇOES HTTP
    {
        //METODO GET QUE RETORNA UMA LISTA DE CARDAPIO
        // GET: api/<ValuesController>
        [HttpGet]//ANOTAÇÃO PARA IDENTIFICAR QUE É UM GET
        public IEnumerable<CardapioItem> GetCardapios()
        {
            //CRIA UMA LISTA DE ESTATICA DE CARDAPIO E TRANSFORMA EM JSON
            return new CardapioItem[]
            {
                new CardapioItem {//CRIA O PRIMEIRO ELEMENTO DA LISTA
                    Id = 1,
                    Titulo = "X-Carne",
                    Descricao = "Bife, salada, tomate, alface, maionese",
                    Preco = 5.00m,
                    PossuiPreparo = true
                },
                new CardapioItem {//CRIA O SEGUNDO ELEMENTO DA LISTA
                    Id = 2,
                    Titulo = "X-Frango",
                    Descricao = "Frango, salada, tomate, alface, maionese",
                    Preco = 6.00m,
                    PossuiPreparo = true
                }
            };
        }
        List<CardapioItem> cardapios = new List<CardapioItem>()
        {
            new CardapioItem()
            {
                Id = 3,
                Titulo = "Coxinha de frango com catupiry",
                Preco = 6.00m,
                PossuiPreparo = true
            },
            new CardapioItem()
            {
                Id = 4,
                Titulo = "Pastel de carne",
                Preco = 5.00m,
                PossuiPreparo = true
            }
        };
        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public IResult Get(int id)// 
        {
            //BUSCAR NA LISTA O CARDAPIO PELO ID E RETORNAR O OBJETO ENCONTRADO OU NULL SE NÃO ENCONTRAR
            //JOGA O VALOR PARA VARIAVEL O PRIMEIRO ELEMENTO DE cardapios QUE TIVER O ID IGUAL AO ID PASSADO COMO PARAMETRO
            var cardapio = cardapios.FirstOrDefault(c => c.Id == id);
            if (cardapio is null)
            {
                return Results.NotFound("Cardápio não encontrado!");
            }
            //RETORNA O OBJETO ENCONTRADO OU NULL
            return Results.Ok(cardapio);
        }
        // POST api/<ValuesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
