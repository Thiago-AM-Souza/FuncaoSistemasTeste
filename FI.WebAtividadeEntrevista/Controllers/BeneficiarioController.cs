using FI.AtividadeEntrevista.BLL;
using FI.AtividadeEntrevista.DML;
using FI.WebAtividadeEntrevista.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace FI.WebAtividadeEntrevista.Controllers
{
    public class BeneficiarioController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Incluir()
        //{
        //    return View();
        //}

        [HttpPost]
        public JsonResult ObterBeneficiarios(long? idCliente, long id = 0)
        {
            try
            {
                BoBeneficiario boBeneficiario = new BoBeneficiario();
                List<Beneficiario> beneficiarios = Enumerable.Empty<Beneficiario>().ToList();


                beneficiarios = boBeneficiario.ObterBeneficiarios(id, idCliente);

                //Return result to jTable
                return Json(beneficiarios);
            }
            catch (Exception ex)
            {
                return Json(new { Result = "ERROR", Message = ex.Message });
            }
        }

        [HttpPost]
        public JsonResult Incluir(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {
                if (model.IdCliente != 0
                    && bo.VerificarExistencia(model.CPF, model.IdCliente))
                {
                    Response.StatusCode = 400;
                    return Json("Já existe um beneficiário cadastrado com esse CPF para este cliente.");
                }

                model.Id = bo.Incluir(new Beneficiario()
                {
                    Nome = model.Nome,
                    CPF = model.CPF,
                    IdCliente = model.IdCliente,
                });


                return Json("Cadastro efetuado com sucesso");
            }
        }

        [HttpPost]
        public JsonResult Atualizar(BeneficiarioModel model)
        {
            BoBeneficiario bo = new BoBeneficiario();

            if (!this.ModelState.IsValid)
            {
                List<string> erros = (from item in ModelState.Values
                                      from error in item.Errors
                                      select error.ErrorMessage).ToList();

                Response.StatusCode = 400;
                return Json(string.Join(Environment.NewLine, erros));
            }
            else
            {                
                var beneficiario = new Beneficiario()
                {
                    Id = model.Id,
                    CPF = model.CPF,
                    Nome = model.Nome
                };

                bo.Atualizar(beneficiario);

                return Json("Beneficiário atualizado com sucesso.");
            }
        }

        [HttpPost]
        public JsonResult Delete(long id)
        {
            BoBeneficiario bo = new BoBeneficiario();
            string msg;

            try
            {
                bo.Excluir(id);
                msg = "Beneficiário excluido com sucesso.";
            }
            catch
            {
                msg = "Erro ao excluir beneficiário.";
            }

            return Json(msg);
        }
    }
}
