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
    public class IndexModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;

        public IndexModel(MamaWash.Models.MamaWashContext context)
        {
            _context = context;
        }

        public IList<Beneficiaries> Beneficiaries { get;set; }

        public async Task OnGetAsync()
        {
            Beneficiaries = await _context.Beneficiaries.ToListAsync();
        }
    }
}
