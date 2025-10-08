using Comandas.Api.Models;

namespace Comandas.Api.DTOs
{
    public class ComandaCreateRequest
    {
        public int NuneroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public int[] cardapioItens { get; set; } = default!;
    }
}
