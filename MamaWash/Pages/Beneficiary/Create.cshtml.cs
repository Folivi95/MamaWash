using MamaWash.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MamaWash.Pages.Beneficiary
{
    public class CreateModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;

        public CreateModel(MamaWash.Models.MamaWashContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> OnGet()
        {
            //get list of banks
            IQueryable<string> bankQuery = from b in _context.BankList
                                           orderby b.Bank
                                           select b.Bank;

            Banks = new SelectList(await bankQuery.Distinct().ToListAsync());
            return Page();
        }

        public SelectList Banks { get; set; }

        [BindProperty]
        public Beneficiaries Beneficiaries { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            

            //validate account number
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer sk_test_f075718722a05eb8182c77beb0279ebe1d2249d2");
            
            var res = await client.GetStringAsync($"https://api.paystack.co/bank/resolve?account_number={Beneficiaries.AccountNumber}&bank_code={Beneficiaries.BankCode}")

            //create beneficiary object to store in database


            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (ModelState.IsValid)
            {
                
                


            }

            if (await TryUpdateModelAsync<Beneficiaries>(
                beneficiary,
                "beneficiaries",
                s => s.AccountNumber, s => s.BankName))
            {
                
            }
            _context.Beneficiaries.Add(Beneficiaries);
            await _context.SaveChangesAsync();

            //dispose httpclient
            client.Dispose();
            return RedirectToPage("./Index");
        }
    }
}