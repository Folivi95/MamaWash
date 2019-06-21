using MamaWash.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace MamaWash.Pages.Transfers
{
    public class CreateModel : PageModel
    {
        private readonly MamaWash.Models.MamaWashContext _context;
        private readonly HttpClient client = new HttpClient();

        public CreateModel(MamaWash.Models.MamaWashContext context)
        {
            _context = context;
        }

        public Beneficiary beneficiary { get; set; }
        //check status to display failed alert
        public bool FailedAlert { get; set; }
        public bool InvalidPIN { get; set; }
        //bind values from posted form
        [BindProperty]
        public Transfer Transfer { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            //set default values for failed alert and invalid pin alert
            FailedAlert = false;
            InvalidPIN = false;

            List<Beneficiary> recipient = new List<Beneficiary>();

            //get recipients
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer sk_test_f075718722a05eb8182c77beb0279ebe1d2249d2");
            string beneficiaryRes = await client.GetStringAsync("https://api.paystack.co/transferrecipient");
            BeneficiaryResponse beneficiaryData = JsonConvert.DeserializeObject<BeneficiaryResponse>(beneficiaryRes);

            if (beneficiaryData.status)
            {
                foreach (Models.Beneficiaries item in beneficiaryData.data)
                {
                    //get values to set form fields.
                    beneficiary.RecipientCode = item.recipient_code;
                    //populate select list
                    recipient.Add(new Beneficiary { RecipientCode = item.recipient_code, AccountName = item.name + " - " + item.details.account_number });
                    //populate form fields
                    ViewData["Recipients"] = new SelectList(recipient, "RecipientCode", "AccountName");
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Transfer transfer = new Transfer();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", "Bearer sk_test_f075718722a05eb8182c77beb0279ebe1d2249d2");

            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Transfer.PIN == 1234)
            {
                //make transfer request
                string content = $"{{\"source\":\"{Transfer.Source}\",\"reason\":\"{Transfer.Reason}\",\"amount\":\"{Transfer.Amount * 100}\",\"recipient\":\"{Transfer.Recipient}\"}}";
                HttpResponseMessage transferRes = await client.PostAsync("https://api.paystack.co/transferrecipient", new StringContent(content));
                //check if request is successful
                if (transferRes.IsSuccessStatusCode)
                {
                    //redirect to index page
                    return RedirectToPage("./Index");
                }
                else
                {
                    FailedAlert = true;
                    return RedirectToPage("./Create");
                }
            }
            else
            {

                return RedirectToPage("./Create");
            }

        }
    }
}