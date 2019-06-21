using MamaWash.Models;
using MamaWash.Models.ResponseVM;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MamaWash.Pages.Transfers
{
    public class IndexModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;
        private readonly HttpClient client = new HttpClient();

        public IndexModel(MamaWash.Models.MamaWashContext context)
        {
            _context = context;
        }

        public List<Transfer> Transfer { get; set; }

        public async Task OnGetAsync()
        {
            List<Transfer> transactions = new List<Transfer>();

            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer sk_test_f075718722a05eb8182c77beb0279ebe1d2249d2");

            TransactionHistoryVM transRes = JsonConvert.DeserializeObject<TransactionHistoryVM>(await client.GetStringAsync("https://api.paystack.co/transfer"));

            if (transRes.status)
            {
                foreach (Transactions item in transRes.data)
                {
                    transactions.Add(new Transfer
                    {
                        Amount = (item.amount / 100),
                        AccountNumber = item.recipient.details.account_number,
                        BankName = item.recipient.details.bank_name,
                        Name = item.recipient.name,
                        Reason = item.reason,
                        Status = item.status
                    });
                }
            }

            Transfer = transactions;
        }
    }
}
