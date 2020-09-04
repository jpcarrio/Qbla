using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Qbla.Controllers;
using Qbla.Data;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Qbla.Util
{
    public class UtilsController : Controller
    {
        private readonly ApplicationDbContext _context;        

        public UtilsController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
            
        }

        public string GetCaseId()
        {
            var PayId = "";

            if (_context.Customers.Count() > 0)
            {
                PayId = _context.Customers.OrderByDescending(x => x.Id).First().CaseId;
                var SubCaseId = PayId.Split("-");
                var sequence = int.Parse(SubCaseId[1]) + 1;
                PayId = sequence.ToString();
                while (PayId.Length < 4)
                {
                    PayId = "0" + PayId;
                }

                PayId = DateTime.Today.Year.ToString().Substring(2) + "-" + PayId;

                
            }
            else
            {
                PayId = DateTime.Today.Year.ToString().Substring(2) + "-0001";
            }

            return PayId;
        }
    }
}
