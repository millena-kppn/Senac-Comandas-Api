using Comandas.Api.DTOs;
using Comandas.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Comandas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        //variavel estatica para simular um banco de dados
        public ComandasDbContext _context { get; set; }
        //lista estatica de usuarios
        public UsuarioController(ComandasDbContext context)
        {
            _context = context;
        }

        // GET api/<UsuarioController>
        [HttpGet]
        public IResult Get()
        {
            //conecta com o banco de dados
            //retorna todos os usuarios da lista
            var usuarios = _context.Usuarios.ToList();
            return Results.Ok(usuarios);
        }

        // GET: api/<UsuarioController>/5

        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            if (usuario is null)
            {
                return Results.NotFound("Usuário não encontrado!");
            }
            return Results.Ok(usuario);
        }
 
        // POST api/<UsuarioController>
        [HttpPost]
        public IResult Post([FromBody] UsuarioCreateRequest usuarioCreate)
        {
            if( usuarioCreate.Senha.Length < 6)
            {
                return Results.BadRequest("A senha deve ter no mínimo 6 caracteres.");
            }
            if (usuarioCreate.Nome.Length < 3)
            {
                return Results.Conflict("O nome deve ter no mínimo 3 caracteres.");
            }
            if (usuarioCreate.Email.Length < 5 || !usuarioCreate.Email.Contains("@"))
            {
                return Results.Conflict("O email deve ser válido.");
            }
            var usuario = new Usuario
            {
              
                Nome = usuarioCreate.Nome,
                Email = usuarioCreate.Email,
                Senha = usuarioCreate.Senha
            };
            //adiciona o usuario no Contexto do banco de dados
            _context.Usuarios.Add(usuario);
            //salva as alteracoes no banco de dados
            _context.SaveChanges();
            return Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }
        //ATUALIZA UM USUARIO
        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioUpdate)
        {
            //BUSCA O USUARIO NA LISTA DO ID
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Id == id);
            //SE NAO ENCONTRAR RETORNA NOTFOUND
            if (usuario is null)
                return Results.NotFound($"Usuário do id {id} não encontrado.");
            //SE ENCONTRAR ATUALIZA OS DADOS DO USUARIO
            usuario.Nome = usuarioUpdate.Nome;
            usuario.Email = usuarioUpdate.Email;
            usuario.Senha = usuarioUpdate.Senha;
            //SALVA AS ALTERACOES NO BANCO DE DADOS
            _context.SaveChanges();
            //RETORNA NO CONTENT
            return Results.NoContent();
        }
        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public IResult Delete(int id)
        {
            //busca o usuario na lista pelo id
            var usuarioLista = _context.Usuarios.FirstOrDefault(c => c.Id == id);
            ////se estiver nulo, retorna 404
            if (usuarioLista is null)
                return Results.NotFound($"Usuario {id} não encontrado!");
            //remove o usuario da lista
            _context.Usuarios.Remove(usuarioLista);
            //salva as alteracoes no banco de dados
            _context.SaveChanges();
            //retorna no content
            return Results.NoContent();
        }
    }
}
