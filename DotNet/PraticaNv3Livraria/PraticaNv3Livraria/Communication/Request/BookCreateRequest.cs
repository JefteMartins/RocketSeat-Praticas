using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace PraticaNv3Livraria.Communication.Request
{
    public class BookCreateRequest
    {

        [Required(ErrorMessage = "O título é obrigatório.")] 
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O título deve ter entre 3 e 100 caracteres.")]
        public required string Title { get; set; }

        [Required(ErrorMessage = "O autor é obrigatório.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O autor deve ter entre 3 e 100 caracteres.")]
        public required string Author { get; set; }

        [Required(ErrorMessage = "O gênero é obrigatório.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "O gênero deve ter entre 2 e 50 caracteres.")]
        public required string Genre { get; set; }

        [Required(ErrorMessage = "O preço é obrigatório.")]
        [Range(20.00, 1000.00, ErrorMessage = "O preço mínimo é R$20.00.")] 
        public decimal Price { get; set; }

        [Required(ErrorMessage = "O estoque é obrigatório.")]
        [Range(0, 10000, ErrorMessage = "O estoque não pode ser negativo.")]
        public int Stock { get; set; }
    }
}
