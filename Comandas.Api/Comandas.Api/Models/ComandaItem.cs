using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Comandas.Api.Models
{
    public class ComandaItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int ComandaId { get; set; }
        public virtual Comanda Comanda { get; set; } = default!;
        public int CardapioItemId { get; set; }
        public CardapioItem CardapioItem { get; set; } 
    }
}
