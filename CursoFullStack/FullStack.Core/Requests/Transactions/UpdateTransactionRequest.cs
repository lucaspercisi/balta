using FullStack.Core.Enums;
using System.ComponentModel.DataAnnotations;

namespace FullStack.Core.Requests.Transactions
{
    public class UpdateTransactionRequest : Request
    {
        [Required(ErrorMessage = "Titulo Inválido")]
        [MaxLength(80, ErrorMessage = "tamanho máximo é de 80")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Tipo inválido")]
        public ETransactionType Type { get; set; }

        [Required(ErrorMessage = "Valor inválido")]
        public decimal Amount { get; set; }

        [Required(ErrorMessage = "Categoria inválido")]
        public long CategoryId { get; set; }

        [Required(ErrorMessage = "Data inválido")]
        public DateTime? PaidOrReceuveAt { get; set; }
    }
}
