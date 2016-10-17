using Sessao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sessao.Controllers
{
    public class CarrinhoController : Controller
    {
        //
        // GET: /Carrinho/

        public ActionResult Index()
        {
            return View(this.PegaCarrinhoDaSessao());
        }


        public ActionResult Cancelar()
        {
            Session.Abandon();
            return RedirectToAction("Index", "Produto");
        }


        public ActionResult Adicionar(int id = 0)
        {
            K19Context ctx = new K19Context();
            Produto p = ctx.Produtos.Find(id);

            if (p == null)
            {
                return HttpNotFound();
            }

            Carrinho carrinho = this.PegaCarrinhoDaSessao();
            carrinho.Produtos.Add(p);

            TempData["Mensagem"] = "Produto adicionado com sucesso!";

            return RedirectToAction("Index", "Produto");
        }


        public ActionResult Remover(int idx = 0) 
        {
            Carrinho carrinho = this.PegaCarrinhoDaSessao();
            carrinho.Produtos.RemoveAt(idx);

            return RedirectToAction("Index", "Produto");
        }


        public Carrinho PegaCarrinhoDaSessao()
        {
            if (Session["Carrinho"] == null)
            {
                Session["Carrinho"] = new Carrinho();
            }

            return Session["Carrinho"] as Carrinho;
        }



    }
}
