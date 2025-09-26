namespace Comandas.Api.Models
{
    public class Comanda
    {
        public int Id { get; set; }
        public int NuneroMesa { get; set; } 
        public int NomeCliente { get; set; }
        public List<ComandaItem> Itens { get; set; } = new List<ComandaItem>();
    }
}
