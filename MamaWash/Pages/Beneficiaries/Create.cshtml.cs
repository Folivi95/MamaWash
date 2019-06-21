using MamaWash.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MamaWash.Pages.Beneficiaries
{
    public class CreateModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;
        private readonly HttpClient client = new HttpClient();

        public CreateModel(MamaWash.Models.MamaWashContext context)
        {
            _context = context;
        }

        public string AccountName { get; set; }
        public string RecipientCode { get; set; }
        [BindProperty]
        public Beneficiary Beneficiary { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            List<BankList> banks = new List<BankList>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer sk_test_f075718722a05eb8182c77beb0279ebe1d2249d2");
            //get list of banks
            string responseData = await client.GetStringAsync("https://api.paystack.co/bank");
            BankGetRequest res = JsonConvert.DeserializeObject<BankGetRequest>(responseData);

            foreach (BankItem item in res.data)
            {
                banks.Add(new BankList { BankCode = item.code, Bank = item.name });
            }
            ViewData["Banks"] = new SelectList(banks, "BankCode", "Bank");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return RedirectToPage("./Create");
            }

            //validate account details
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization",
                "Bearer sk_test_f075718722a05eb8182c77beb0279ebe1d2249d2");

            string request = string.Format($"https://api.paystack.co/bank/resolve?account_number={Beneficiary.AccountNumber}&bank_code={Beneficiary.Bank.BankCode}");

            //get response
            HttpResponseMessage resDataStream = await client.GetAsync(request);
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
            HttpResponseMessage recipientRes = await client.PostAsync("https://api.paystack.co/transferrecipient", new StringContent(content));

            //check if request is successful
            if (recipientRes.IsSuccessStatusCode)
            {
                return RedirectToPage("./Index");
            }
            else
            {
                return RedirectToPage("./Create");
            }
        }
    }
}