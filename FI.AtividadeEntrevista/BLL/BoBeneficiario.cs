
using FI.AtividadeEntrevista.Beneficiarios;
using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace FI.AtividadeEntrevista.BLL
{
    public class BoBeneficiario
    {
        /// <summary>
        /// Inclui um novo beneficiario
        /// </summary>
        /// <param name="beneficiario">Objeto de beneficiario</param>
        public long Incluir(DML.Beneficiario beneficiario)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            return cli.Incluir(beneficiario);
        }

        /// <summary>
        /// Consulta o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public DML.Beneficiario Consultar(long id)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            return cli.Consultar(id);
        }

        /// <summary>
        /// Excluir o beneficiario pelo id
        /// </summary>
        /// <param name="id">id do beneficiario</param>
        /// <returns></returns>
        public void Excluir(long id)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            cli.Excluir(id);
        }

        public List<Beneficiario> ObterBeneficiarios(long id, long? clienteId)
        {
            DaoBeneficiario cli = new DaoBeneficiario();

            var beneficiarios = cli.ObterBeneficiarios(id);

            if (clienteId.HasValue)
            {
                beneficiarios = beneficiarios.Where(x => x.IdCliente == clienteId).ToList();
            }

            return beneficiarios;
        }

        /// <summary>
        /// Lista os beneficiarios
        /// </summary>
        public List<DML.Beneficiario> Pesquisa(long idCliente, int iniciarEm, int quantidade, string campoOrdenacao, bool crescente, out int qtd)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            return cli.Pesquisa(idCliente, iniciarEm, quantidade, campoOrdenacao, crescente, out qtd);
        }

        /// <summary>
        /// VerificaExistencia
        /// </summary>
        /// <param name="CPF"></param>
        /// <returns></returns>
        public bool VerificarExistencia(string CPF, long clienteId)
        {            
            var cpfFiltro = CPF.Replace(".", "").Replace("-","").Trim();
            DaoBeneficiario cli = new DaoBeneficiario();
            return cli.VerificarExistencia(cpfFiltro, clienteId);
        }

        public void Atualizar(Beneficiario beneficiario)
        {
            DaoBeneficiario cli = new DaoBeneficiario();
            cli.Alterar(beneficiario);
        }
    }
}
