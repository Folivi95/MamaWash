using MamaWash.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MamaWash.Pages.Beneficiaries
{
    public class IndexModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;
        private readonly HttpClient client = new HttpClient();

        public IndexModel(MamaWash.Models.MamaWashContext context)
        {
            _context = context;
        }

        public List<Beneficiary> Beneficiary { get; set; }

        public async Task OnGetAsync()
        {
            Beneficiary = new List<Beneficiary>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer sk_test_f075718722a05eb8182c77beb0279ebe1d2249d2");

            string beneficiaryRes = await client.GetStringAsync("https://api.paystack.co/transferrecipient");
            BeneficiaryResponse beneficiaryData = JsonConvert.DeserializeObject<BeneficiaryResponse>(beneficiaryRes);

            if (beneficiaryData.status)
            {
                foreach (Models.Beneficiaries item in beneficiaryData.data)
                {
                    BankList beneficiaryBank = new BankList { Bank = item.details.bank_name, BankCode = item.details.bank_code };

                    Beneficiary.Add(new Beneficiary { AccountNumber = item.details.account_number, AccountName = item.name, Bank = beneficiaryBank, RecipientCode = item.recipient_code });
                }
            }
        }
    }
}
