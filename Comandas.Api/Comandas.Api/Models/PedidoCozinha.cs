using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Comandas.Api.Models
{
    public class PedidoCozinha
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }//Primary Key
        public int ComandaId { get; set; }//Foreign Key
        public virtual Comanda Comanda { get; set; }//Navigation property
        public List<PedidoCozinhaItem> Itens { get; set; } = [];
    }
}
