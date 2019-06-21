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
    public class IndexModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;

        public IndexModel(MamaWash.Models.MamaWashContext context)
        {
            _context = context;
        }

        public IList<Beneficiary> Beneficiary { get;set; }

        public async Task OnGetAsync()
        {
            Beneficiary = await _context.Beneficiaries
                .Include(b => b.Bank).ToListAsync();
        }
    }
}