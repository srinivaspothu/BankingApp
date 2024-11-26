using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BankingApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankingApp.Controllers
{
    public class BanksController : Controller
    {
        private readonly BankDbContext _bankDbContext;
        public BanksController(BankDbContext bankDbContext)
        {
            _bankDbContext = bankDbContext;
        }
        public IActionResult Index()
        {
            var acctDtl = _bankDbContext.Account.ToList();
            return View(acctDtl);
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Account account)
        {
            var res = _bankDbContext.Account.Where(a => a.AccountId == account.AccountId).Count();
            if(res>0)
            {
                ModelState.AddModelError("AccountId", "Account name already exists");
                return View();
            }
            if(ModelState.IsValid)
            {
                _bankDbContext.Account.Add(account);
                _bankDbContext.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(account);
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var acct = _bankDbContext.Account.Find(id);
            if(acct==null)
            {
                return NotFound();
            }
            return View(acct);
        }

        [HttpPost]
        public IActionResult Edit(int id, Account account)
        {
            if(id!=account.Id)
            {
                return NotFound();
            }
            if(ModelState.IsValid)
            {
                try
                {
                    _bankDbContext.Account.Update(account);
                    _bankDbContext.SaveChanges();
                }
                catch(DbUpdateConcurrencyException)
                {
                    if(!AccountsExist(account.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(account);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var acct = _bankDbContext.Account.Find(id);
            if(acct==null)
            {
                return NotFound();
            }
            return View(acct);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteConfirmed(int id)
        {
            var acct = _bankDbContext.Account.Find(id);
            _bankDbContext.Account.Remove(acct);
            _bankDbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var acct = _bankDbContext.Account.Find(id);
            return View(acct);
        }

        private bool AccountsExist(int id)
        {
            return _bankDbContext.Account.Any(e => e.Id == id);
        }

    }
}