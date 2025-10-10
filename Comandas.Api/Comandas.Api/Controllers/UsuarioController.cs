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
        //Lista Usuarios 
       static List<Usuario> usuarios = new List<Usuario>()
        {
            new Usuario
            {
            Id = 1,
            Nome = "Admin",
            Email = "admin@admin.com",
            Senha = "admin123"
            },
            new Usuario
            {
            Id = 1,
            Nome = "Usuario",
            Email = "usuario@usuario.com",
            Senha = "usuario123"
            }
        };

        // GET api/<UsuarioController>
        [HttpGet]
        public IResult Get()
        {
            return Results.Ok(usuarios);
        }

        // GET: api/<UsuarioController>/5

        [HttpGet("{id}")]
        public IResult Get(int id)
        {
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
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
                Id = usuarios.Count + 1,
                Nome = usuarioCreate.Nome,
                Email = usuarioCreate.Email,
                Senha = usuarioCreate.Senha
            };
            //adiciona um usuario na lista
            usuarios.Add(usuario);
            return Results.Created($"/api/usuario/{usuario.Id}", usuario);
        }
        //ATUALIZA UM USUARIO
        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public IResult Put(int id, [FromBody] UsuarioUpdateRequest usuarioUpdate)
        {
            //BUSCA O USUARIO NA LISTA DO ID
            var usuario = usuarios.FirstOrDefault(u => u.Id == id);
            //SE NAO ENCONTRAR RETORNA NOTFOUND
            if (usuario is null)
                return Results.NotFound($"Usuário do id {id} não encontrado.");
            //SE ENCONTRAR ATUALIZA OS DADOS DO USUARIO
            usuario.Nome = usuarioUpdate.Nome;
            usuario.Email = usuarioUpdate.Email;
            usuario.Senha = usuarioUpdate.Senha;
            //RETORNA NO CONTENT
            return Results.NoContent();
        }
        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
