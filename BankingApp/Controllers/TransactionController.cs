using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BankingApp.Controllers
{
    public class TransactionController : Controller
    {
        private readonly BankDbContext _bankDbContext;
        public TransactionController(BankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
        }

        public IActionResult Create()
        {
            Transaction transaction = new Transaction();
            transaction.AccountList = new SelectList(_bankDbContext.Account, "AccountId", "AccountId");
            return View(transaction);
        }
        [HttpPost]
        public IActionResult Create(IFormCollection frmCollection, Transaction transaction)
        {
            string frm = frmCollection["ddlAccount1"];
            string to= frmCollection["ddlAccount2"];
            transaction.FromAccount = frm;
            transaction.ToAccount = to;
            transaction.TransactionTime = DateTime.Now;
            var frmAccnt = _bankDbContext.Account.Where(a => a.AccountId == frm).FirstOrDefault();
            var toAccnt = _bankDbContext.Account.Where(a => a.AccountId == to).FirstOrDefault();
            if(transaction.AmountDebit>frmAccnt.Balance)
            {
                ViewBag.Message = "Insufficient balance";
                transaction.AccountList= new SelectList(_bankDbContext.Account, "AccountId", "AccountId");
                return View(transaction);
            }
            transaction.FromAccountBal = frmAccnt.Balance - transaction.AmountDebit;
            transaction.ToAccountBal = frmAccnt.Balance + transaction.AmountDebit;
            if(ModelState.IsValid)
            {
                _bankDbContext.Transaction.Add(transaction);
                _bankDbContext.SaveChanges();
                return RedirectToAction("Create");
            }
            transaction.AccountList = new SelectList(_bankDbContext.Account, "AccountId", "AccountId");
            return View(transaction);

        }

        [HttpGet]
        public IActionResult Index()
        {
            var allTransDtls = _bankDbContext.Transaction.ToList();
            return View(allTransDtls);
        }

        [HttpGet]
        public IActionResult TransDetails(int id)
        {
            var transDtl = _bankDbContext.Transaction.Find(id);
            return View(transDtl);
        }
    }
}