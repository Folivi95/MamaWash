using MamaWash.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

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
            ViewData["Banks"] = new SelectList(_context.BankList, "BankCode", "Bank");
            return Page();
        }

        public string AccountName { get; set; }
        public string RecipientCode { get; set; }
        [BindProperty]
        public Beneficiary Beneficiary { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Create");
            }

            //validate account details
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                "Bearer sk_test_f075718722a05eb8182c77beb0279ebe1d2249d2");

            var request = string.Format($"https://api.paystack.co/bank/resolve?account_number={Beneficiary.AccountNumber}&bank_code={Beneficiary.Bank.BankCode}");

            //get response
            var resDataStream = await client.GetAsync(request);
            //check if account details are valid
            if (resDataStream.IsSuccessStatusCode)
            {
                string resData = await resDataStream.Content.ReadAsStringAsync();
                AccountValidation res = JsonConvert.DeserializeObject<AccountValidation>(resData);
                AccountName = res.data.account_name;
            }
            else
            {
                return RedirectToPage("./Create");
            }


            //create recipient after validating account details
            string content = $"{{\"type\":\"nuban\",\"name\":\"{AccountName}\",\"description\":\"{Beneficiary.AccountName}\",\"account_number\":\"{Beneficiary.AccountNumber}\",\"bank_code\":\"{Beneficiary.Bank.BankCode}\",\"currency\":\"NGN\"}}";
            var recipientRes = await client.PostAsync("https://api.paystack.co/transferrecipient", new StringContent(content));

            //check if request is successful
            if (recipientRes.IsSuccessStatusCode)
            {
                var recipientData = await recipientRes.Content.ReadAsStringAsync();
                var recipient = JsonConvert.DeserializeObject<CreateRecipient>(recipientData);
                RecipientCode = recipient.data.recipient_code;
            }
            else
            {
                return RedirectToPage("./Create");
            }

            _context.Beneficiaries.Add(Beneficiary);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Create");
        }
    }
}