using System.ComponentModel.DataAnnotations;

namespace FullStack.Core.Requests.Categories
{
    public class CreateCategoryRequest : Request
    {
        [Required(ErrorMessage = "Titulo Inválido")]
        [MaxLength(80, ErrorMessage = "tamanho máximo é de 80")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Descrição Inválida")]
        public string Description { get; set; } = string.Empty;
    }
}
