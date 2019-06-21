﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MamaWash.Models;

namespace MamaWash.Pages.Beneficiaries
{
    public class DetailsModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;

        public DetailsModel(MamaWash.Models.MamaWashContext context)
        {
            _context = context;
        }

        public Beneficiary Beneficiary { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Beneficiary = await _context.Beneficiaries
                .Include(b => b.Bank).FirstOrDefaultAsync(m => m.ID == id);

            if (Beneficiary == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}