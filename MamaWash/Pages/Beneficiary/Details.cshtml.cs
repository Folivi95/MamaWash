using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MamaWash.Models;

namespace MamaWash.Pages.Beneficiary
{
    public class DetailsModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;

        public DetailsModel(MamaWash.Models.MamaWashContext context)
        {
            _context = context;
        }

        public Beneficiaries Beneficiaries { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Beneficiaries = await _context.Beneficiaries.FirstOrDefaultAsync(m => m.ID == id);

            if (Beneficiaries == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
