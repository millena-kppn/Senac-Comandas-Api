namespace Comandas.Api.DTOs
{
    public class ComandaUpdateRequest
    {
        //adicionar duas propiedades:
        //NomeCliente e NumeroMesa
        public string NomeCliente { get; set; } = default!;
        public int NumeroMesa { get; set; }
    }
}
