﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;

namespace PaymentAPI.Controllers
{
    public class PaymentDetailsController : Controller
    {
        private readonly PaymentDetailContext _context;

        public PaymentDetailsController(PaymentDetailContext context)
        {
            _context = context;
        }

        // GET: PaymentDetails
        public async Task<IActionResult> Index()
        {
              return View(await _context.PaymentDetails.ToListAsync());
        }

        // GET: PaymentDetails/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.PaymentDetails == null)
            {
                return NotFound();
            }

            var paymentDetail = await _context.PaymentDetails
                .FirstOrDefaultAsync(m => m.PaymentDetailId == id);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            return View(paymentDetail);
        }

        // GET: PaymentDetails/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PaymentDetails/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PaymentDetailId,CardOwnerName,CardNumber,ExpirationDate,SecurityCode")] PaymentDetail paymentDetail)
        {
            if (ModelState.IsValid)
            {
                _context.Add(paymentDetail);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(paymentDetail);
        }

        // GET: PaymentDetails/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.PaymentDetails == null)
            {
                return NotFound();
            }

            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail == null)
            {
                return NotFound();
            }
            return View(paymentDetail);
        }

        // POST: PaymentDetails/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PaymentDetailId,CardOwnerName,CardNumber,ExpirationDate,SecurityCode")] PaymentDetail paymentDetail)
        {
            if (id != paymentDetail.PaymentDetailId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(paymentDetail);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PaymentDetailExists(paymentDetail.PaymentDetailId))
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
            return View(paymentDetail);
        }

        // GET: PaymentDetails/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.PaymentDetails == null)
            {
                return NotFound();
            }

            var paymentDetail = await _context.PaymentDetails
                .FirstOrDefaultAsync(m => m.PaymentDetailId == id);
            if (paymentDetail == null)
            {
                return NotFound();
            }

            return View(paymentDetail);
        }

        // POST: PaymentDetails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.PaymentDetails == null)
            {
                return Problem("Entity set 'PaymentDetailContext.PaymentDetails'  is null.");
            }
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            if (paymentDetail != null)
            {
                _context.PaymentDetails.Remove(paymentDetail);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PaymentDetailExists(int id)
        {
          return _context.PaymentDetails.Any(e => e.PaymentDetailId == id);
        }
    }
}