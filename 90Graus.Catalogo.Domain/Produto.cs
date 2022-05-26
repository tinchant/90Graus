using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _90Graus.Catalogo.Domain
{
    public class Produto
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required] 
        public string Nome { get; set; } = String.Empty;
        [Required] 
        public string Imagem { get; set; } = string.Empty;
        [Required] 
        public string Descricao { get; set; } = string.Empty;
        public int CatalogoDeProdutoId { get; set; }
    }


}
