using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Comandas.Api.Models
{
    public class Usuario
    {
        [Key]//Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]//Primary Key
        public int Id { get; set; }
        public string Nome { get; set; } = default!;
        public string Email { get; set; } = default!;
        public string Senha { get; set; } = default!;
    }
}
