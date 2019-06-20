using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MamaWash.Models;

namespace MamaWash.Pages.Beneficiaries
{
    public class CreateModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;

        public CreateModel(MamaWash.Models.MamaWashContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Beneficiaries Beneficiaries { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Beneficiaries.Add(Beneficiaries);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}