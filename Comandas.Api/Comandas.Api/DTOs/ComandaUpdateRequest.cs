namespace Comandas.Api.DTOs
{
    public class ComandaUpdateRequest
    {
        public int NuneroMesa { get; set; }
        public string NomeCliente { get; set; } = default!;
        public int[] cardapioItens { get; set; } = default!;
    }
}
