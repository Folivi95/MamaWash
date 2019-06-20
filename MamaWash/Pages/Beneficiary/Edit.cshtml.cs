using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MamaWash.Models;

namespace MamaWash.Pages.Beneficiary
{
    public class EditModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;

        public EditModel(MamaWash.Models.MamaWashContext context)
        {
            _context = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Beneficiaries).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeneficiariesExists(Beneficiaries.ID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool BeneficiariesExists(int id)
        {
            return _context.Beneficiaries.Any(e => e.ID == id);
        }
    }
}
