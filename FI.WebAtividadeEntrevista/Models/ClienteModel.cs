using FI.AtividadeEntrevista.Validations;
using FI.WebAtividadeEntrevista.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebAtividadeEntrevista.Models
{
    /// <summary>
    /// Classe de Modelo de Cliente
    /// </summary>
    public class ClienteModel
    {
        public long Id { get; set; }
        
        /// <summary>
        /// CEP
        /// </summary>
        [Required]
        [RegularExpression("^\\d{5}-?\\d{3}$", ErrorMessage = "CEP com formato incorreto")]
        public string CEP { get; set; }

        /// <summary>
        /// Cidade
        /// </summary>
        [Required]
        public string Cidade { get; set; }

        /// <summary>
        /// E-mail
        /// </summary>
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "Digite um e-mail válido")]
        public string Email { get; set; }

        /// <summary>
        /// Estado
        /// </summary>
        [Required]
        [MaxLength(2)]
        public string Estado { get; set; }

        /// <summary>
        /// Logradouro
        /// </summary>
        [Required]
        public string Logradouro { get; set; }

        /// <summary>
        /// Nacionalidade
        /// </summary>
        [Required]
        public string Nacionalidade { get; set; }

        /// <summary>
        /// Nome
        /// </summary>
        [Required]
        public string Nome { get; set; }

        /// <summary>
        /// Sobrenome
        /// </summary>
        [Required]
        public string Sobrenome { get; set; }

        /// <summary>
        /// Telefone
        /// </summary>
        [RegularExpression(@"^\(?\d{2}\)?[\s-]?9?\d{4}[-]?\d{4}$", ErrorMessage = "Telefone com formato incorreto.")]
        public string Telefone { get; set; }

        /// <summary>
        /// CPF
        /// </summary>
        [Required]
        [MaxLength(14)]
        [RegularExpression(@"^([\d]{3}).([\d]{3}).([\d]{3})-([\d]{2})$", ErrorMessage = "Digite um CPF válido")]
        [ValidaCPF]
        public string CPF { get; set; }

        /// <summary>
        /// Beneficiarios
        /// </summary>
        public List<BeneficiarioModel> Beneficiarios { get; set; } = new List<BeneficiarioModel>();
    }    
}