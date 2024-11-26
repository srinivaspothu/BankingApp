using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public DateTime TransactionTime { get; set; }
        [Range(1,10000)]
        [Required(ErrorMessage="Amount to Transfer must be between $1 to $10000")]
        public decimal AmountDebit { get; set; }
        public decimal FromAccountBal { get; set; }
        public decimal ToAccountBal { get; set; }

        [NotMapped]
        public SelectList AccountList { get; set; }
    }
}
