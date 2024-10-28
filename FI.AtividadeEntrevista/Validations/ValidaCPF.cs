using FI.AtividadeEntrevista.BLL;
using System.ComponentModel.DataAnnotations;

namespace FI.AtividadeEntrevista.Validations
{
    public class ValidaCPF : ValidationAttribute
    {
        int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        string tempCpf;
        string digito;
        int soma;
        int resto;

        BoCliente BoCliente = new BoCliente();
        BoBeneficiario BoBeneficiario = new BoBeneficiario();  

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return ValidarCPF((string)value, validationContext);
        }

        private ValidationResult ValidarCPF(string cpf, ValidationContext validationContext)
        {
            cpf = cpf.Replace(".", "").Replace("-", "").Trim();
            string context = string.Empty;
            var obj = (dynamic)validationContext.ObjectInstance;
            var nome = (string)obj.Nome;

            switch (validationContext.ObjectType.Name)
            {
                case "ClienteModel":
                    context = "Cliente";
                    break;
                case "BeneficiarioModel":
                    context = "Beneficiario";
                    break;
            }

            if (ValidaTamanhoCPF(cpf) == false)
                return new ValidationResult($"O CPF do {context} {nome} deve conter 11 digitos.");

            if (ValidaStringCPF(cpf) == false)
                return new ValidationResult($"O CPF do {context} {nome} deve conter apenas números.");

            if (ValidaDigitoCPF(cpf) == false)
                return new ValidationResult($"CPF do {context} {nome} inválido, por favor verifique.");

            if (ValidaExistenciaCPF(cpf, validationContext) == true)
                return new ValidationResult($"CPF do {context} {nome} já cadastrado no sistema.");

            return ValidationResult.Success;
        }

        private bool ValidaTamanhoCPF(string cpf)
        {
            if (cpf.Length != 11)
                return false;

            return true;
        }

        private bool ValidaStringCPF(string cpf)
        {            
            foreach (char c in cpf)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        private bool ValidaDigitoCPF(string cpf)
        {
            tempCpf = cpf.Substring(0, 9);
            soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = resto.ToString();
            tempCpf = tempCpf + digito;
            soma = 0;

            for (int i = 0; i < 10; i++)
                soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

            resto = soma % 11;

            if (resto < 2)
                resto = 0;
            else
                resto = 11 - resto;

            digito = digito + resto.ToString();

            if (!cpf.EndsWith(digito))
                return false;

            return true;
        }

        private bool ValidaExistenciaCPF(string cpf, ValidationContext validationContext)
        {            
            var context = (dynamic)validationContext.ObjectInstance;

            switch (validationContext.ObjectType.Name)
            {
                case "ClienteModel":
                    var clienteExistente = BoCliente.VerificarExistencia(cpf);

                    if (clienteExistente && context.Id == 0)
                        return true;
                    break;
                case "BeneficiarioModel":
                    if (context.IdCliente != 0 && context.Id == 0)
                    {
                        var benefExistente = BoBeneficiario.VerificarExistencia(cpf, context.IdCliente);
                        
                        if (benefExistente)
                            return true;
                    }

                    break;
            }

            return false;
        }
    }
}
