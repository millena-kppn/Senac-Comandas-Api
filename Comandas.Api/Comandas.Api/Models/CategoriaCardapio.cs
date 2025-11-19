namespace Comandas.Api.Models;
    public class CategoriaCardapio
    {
        public int Id { get; set; }

        public string Nome { get; set; } = default!;

        public string? Descricao { get; set; }

        public ICollection<CardapioItem>? Itens { get; set; }
    }
