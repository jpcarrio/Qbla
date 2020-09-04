using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Qbla.Data;
using Qbla.Models;
using Qbla.Util;

namespace Qbla.Controllers
{
    public class PaymentsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UtilsController _utils;

        public PaymentsController(ApplicationDbContext context, UtilsController utilsController)
        {
            _context = context;
            _utils = utilsController;
        }

        [Route("[controller]")]
        // GET: Payments
        public async Task<IActionResult> IndexAsync()
        {
            return View(await _context.PaymentView.ToListAsync());
        }

        public async Task<IActionResult> CustPaymentsAsync()
        {
            return View(await _context.PaymentView.ToListAsync());
        }

        public async Task<IActionResult> CustDateAsync()
        {
            return View(await _context.PaymentView.ToListAsync());
        }

        // GET: Payments/Details/5
        public async Task<IActionResult> DetailsAsync(int? id)
        {
            var customers = new List<Customers>();
            customers = _context.Customers.ToList();

            var services = new List<Services>();
            services = _context.Services.ToList();

            ViewBag.Customers = customers;
            ViewBag.Services = services;

            if (id == null)
            {
                return NotFound();
            }

            PaymentView payments = await _context.PaymentView
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payments == null)
            {
                return NotFound();
            }

            return View(payments);
        }

        // GET: Payments/Create
        public IActionResult Create()
        {
            var customers = new List<Customers>();
            customers = _context.Customers.ToList();

            var services = new List<Services>();
            services = _context.Services.ToList();

            ViewBag.Customers = customers;
            ViewBag.Services = services;            

            return View();
        }

        // POST: Payments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAsync([Bind("Id,CustomerId,ServiceId,Beneficiary,Amount,PayDate,Description")] Payments payments)
        {
            if (ModelState.IsValid)
            {               
                _context.Add(payments);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(payments);
        }

        // GET: Payments/Edit/5
        public async Task<IActionResult> EditAsync(int? id)
        {
            var customers = new List<Customers>();
            customers = _context.Customers.ToList();

            var services = new List<Services>();
            services = _context.Services.ToList();

            ViewBag.Customers = customers;
            ViewBag.Services = services;

            if (id == null)
            {
                return NotFound();
            }

            PaymentView payments = await _context.PaymentView.FindAsync(id);
            if (payments == null)
            {
                return NotFound();
            }
            return View(payments);
        }

        // POST: Payments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> EditAsync(int id, [Bind("Id,CustomerId,ServiceId,Beneficiary,Amount,PayDate,Description")] Payments payments)
        {
            if (id != payments.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(payments);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentsExists(payments.Id))
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
            return View(payments);
        }

        // GET: Payments/Delete/5
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Payments payments = await _context.Payments
                .FirstOrDefaultAsync(m => m.Id == id);
            if (payments == null)
            {
                return NotFound();
            }

            return View(payments);
        }

        // POST: Payments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmedAsync(int id)
        {
            Payments payments = await _context.Payments.FindAsync(id);
            _context.Payments.Remove(payments);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentsExists(int id)
        {
            return _context.Payments.Any(e => e.Id == id);
        }
    }
}
