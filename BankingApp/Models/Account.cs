using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingApp.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string AccountId { get; set; }
        [Range(1, 100000)]
        public decimal Balance { get; set; }
    }
}
