using MamaWash.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MamaWash.Pages.Banks
{
    public class IndexModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;

        public IndexModel(MamaWash.Models.MamaWashContext context)
        {
            _context = context;
        }

        public BankList NewBank { get; set; }
        public IList<BankList> BankList { get; set; }
        public SelectList Banks { get; set; }
        public string AccountBalance { get; set; }

        public async Task OnGetAsync()
        {
            HttpClient client = new HttpClient();

            //get account balance and display it
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer sk_test_f075718722a05eb8182c77beb0279ebe1d2249d2");
            string response = await client.GetStringAsync("https://api.paystack.co/balance");
            BalanceData row = JsonConvert.DeserializeObject<BalanceData>(response);

            //check if account balance returned a value
            if (row.status)
            {
                foreach (Models.Balance item in row.data)
                {
                    AccountBalance = string.Format("{0:n}", Convert.ToDecimal(item.balance) / 100);
                }
            }
            else
            {
                AccountBalance = "Not Available";
            }

            client.Dispose();
        }
    }
}
