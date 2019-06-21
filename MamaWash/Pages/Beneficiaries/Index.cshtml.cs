using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MamaWash.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

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

        public List<Beneficiary> Beneficiary { get;set; }

        public async Task OnGetAsync()
        {
            Beneficiary = new List<Beneficiary>();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer sk_test_f075718722a05eb8182c77beb0279ebe1d2249d2");

            var beneficiaryRes = await client.GetStringAsync("https://api.paystack.co/transferrecipient");
            var beneficiaryData = JsonConvert.DeserializeObject<BeneficiaryResponse>(beneficiaryRes);

            if (beneficiaryData.status)
            {
                foreach (var item in beneficiaryData.data)
                {
                    var beneficiaryBank = new BankList { Bank = item.details.bank_name, BankCode = item.details.bank_code };
                    
                    Beneficiary.Add(new Beneficiary {AccountNumber=item.details.account_number, AccountName=item.name, Bank= beneficiaryBank, RecipientCode= item.recipient_code});
                }
            }
        }
    }
}
