using FI.AtividadeEntrevista.Validations;
using System.ComponentModel.DataAnnotations;

namespace FI.WebAtividadeEntrevista.Models
{
    public class BeneficiarioModel
    {
        public long Id { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        [MaxLength(14)]
        [RegularExpression(@"^([\d]{3}).([\d]{3}).([\d]{3})-([\d]{2})$", ErrorMessage = "Digite um CPF válido")]
        [ValidaCPF]
        public string CPF { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// IdCliente
        /// </summary>
        [Required]
        public long IdCliente { get; set; }
    }
}
