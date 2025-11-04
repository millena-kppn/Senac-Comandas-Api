using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Comandas.Api.Models;

public class PedidoCozinhaItem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    public int PedidoCozinhaId { get; set; }//Foreign Key
    public virtual PedidoCozinha PedidoCozinha { get; set; }//Navigation property
    public int ComandaItemId { get; set; }//Foreign Key
    public virtual ComandaItem ComandaItem { get; set; }//Navigation property
}
