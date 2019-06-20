using MamaWash.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            var client = new HttpClient();
            //get list of banks
            if (await _context.BankList.CountAsync() < 1)
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer sk_test_f075718722a05eb8182c77beb0279ebe1d2249d2");
                var responseData = await client.GetStringAsync("https://api.paystack.co/bank");
                BankGetRequest res = JsonConvert.DeserializeObject<BankGetRequest>(responseData);

                foreach (BankItem item in res.data)
                {
                    NewBank = new BankList { BankCode = item.code, Bank = item.name };
                    _context.BankList.Add(NewBank);
                }
                await _context.SaveChangesAsync();
            }

            BankList = await _context.BankList.ToListAsync();

            //get account balance and display it
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer sk_test_f075718722a05eb8182c77beb0279ebe1d2249d2");
            var response = await client.GetStringAsync("https://api.paystack.co/balance");
            var row = JsonConvert.DeserializeObject<BalanceData>(response);

            //check if account balance returned a value
            if (row.status)
            {
                foreach (var item in row.data)
                {
                    AccountBalance = string.Format("{0:n}", Convert.ToDecimal(item.balance) / 100);
                }
            }
            else
            {
                AccountBalance = "Not Available";
            }

            Banks = new SelectList(BankList);

            client.Dispose();
        }
    }
}
