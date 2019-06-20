using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MamaWash.Models;

namespace MamaWash.Pages.Beneficiaries
{
    public class DeleteModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;

        public DeleteModel(MamaWash.Models.MamaWashContext context)
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Beneficiaries = await _context.Beneficiaries.FindAsync(id);

            if (Beneficiaries != null)
            {
                _context.Beneficiaries.Remove(Beneficiaries);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
