using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Qbla.Data;
using Qbla.Models;
using Qbla.Util;

namespace Qbla.Controllers
{
    public class CustomersController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UtilsController _utils;

        public CustomersController(ApplicationDbContext context, UtilsController utilsController)
        {
            _context = context;
            _utils = utilsController;
        }
                
        [Route("[controller]")]
        // GET: Customers
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.Customers.ToListAsync());
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> DetailsAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customers customers = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            ViewBag.CaseId = _utils.GetCaseId();

            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([Bind("Id,CaseId,Firstname,Middlename,Lastname,Address,City,Zipcode,Phone,Email,BirthCountry,MigrationStatus")] Customers customers)
        {
            if (ModelState.IsValid)
            {
                _context.Add(customers);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customers);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> EditAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customers customers = await _context.Customers.FindAsync(id);
            if (customers == null)
            {
                return NotFound();
            }
            return View(customers);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, [Bind("Id,Guid,Firstname,Middlename,Lastname,Address,City,Zipcode,Phone,Email,BirthCountry,MigrationStatus")] Customers customers)
        {
            if (id != customers.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customers);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomersExists(customers.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(customers);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Customers customers = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customers == null)
            {
                return NotFound();
            }

            return View(customers);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(int id)
        {
            Customers customers = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customers);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CustomersExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }

        [HttpGet]
        // GET: Customers/Create
        public IActionResult AddCustomer()
        {
            ViewBag.CaseId = _utils.GetCaseId();

            var services = new List<Services>();
            services = _context.Services.ToList();
            
            ViewBag.Services = services;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddCustomerAsync(CustServices payment)
        {
            if (ModelState.IsValid)
            {
                var CaseId = "";

                var record = _context.PaymentView.Where(x => x.Firstname == payment.Firstname &&
                                                        x.Lastname == payment.Lastname &&
                                                        x.Address == payment.Address ||
                                                        x.Email == payment.Email && !String.IsNullOrEmpty(x.Email)).ToList();

                if (record.Count > 0)
                {
                    CaseId = record[0].CustomerId.ToString();
                }
                else
                {
                    CaseId = payment.CaseId;
                    var cust = new Customers
                    {
                        CaseId = payment.CaseId,
                        Firstname = payment.Firstname,
                        Middlename = payment.Middlename,
                        Lastname = payment.Lastname,
                        Address = payment.Address,
                        City = payment.City,
                        Zipcode = payment.Zipcode,
                        Phone = payment.Phone,
                        Email = payment.Email,
                        BirthCountry = payment.BirthCountry,
                        MigrationStatus = payment.MigrationStatus
                    };

                    _context.Add(cust);
                }

                foreach (Payments paymentItem in payment.Payments)
                {
                    var pay = new Payments
                    {
                        CustomerId = CaseId,
                        ServiceId = paymentItem.ServiceId,
                        Amount = paymentItem.Amount,
                        Beneficiary = paymentItem.Beneficiary,
                        PayDate = paymentItem.PayDate,
                        Description = paymentItem.Description
                    };

                    _context.Add(pay);
                }

                await _context.SaveChangesAsync();

                return RedirectToAction("AddCustomer");
            }
            else
            {
                ViewBag.Error = true;
            }
            return View();
        }

        [HttpGet]       
        public IActionResult ServiceItem(int id)
        {
            var services = new List<Services>();
            services = _context.Services.ToList();

            ViewBag.Services = services;
            ViewBag.Id = id;

            var pv = new PaymentView();

            return View(pv);
        }
    }
}
