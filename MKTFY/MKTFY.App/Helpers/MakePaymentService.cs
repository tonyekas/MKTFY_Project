using Stripe;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKTFY.App.Helpers
{
    public class MakePaymentService
    {
        public static async Task<dynamic> PayAsync(string Cardnumber, int month, int year, string cvc, int value) //string Cardnumber, int month, int year, string cvc, int value
        {
            StripeConfiguration.ApiKey = "sk_test_51IWcRSIQhq5DrjgohRGasBiXovWGY8TkYMaOyivi0DGviyivZfpwkWGMel27dVmf3X3Eh2gB9iXEVgd5khZrspzN00Efx4QCyo";
            var optionsToken = new TokenCreateOptions
            {
                Card = new TokenCardOptions
                {
                    Number = Cardnumber,
                    ExpMonth = month,
                    ExpYear = year,
                    Cvc = cvc,
                }
            };
            var servicetoken = new TokenService();
            Token stripetoken = await servicetoken.CreateAsync(optionsToken);
            var opt = new ChargeCreateOptions
            {
                Amount = value,
                Currency = "cad",
                Description = "MKTFY",
                Source = stripetoken.Id
            };
            var service = new ChargeService();
            Charge charge = await service.CreateAsync(opt);
            if (charge.Paid)
            {
                return "Success";
            }
            else
            {
                return "failed";
            }
            //try
            //{         
            //}
            //catch (Exception ex)
            //{return ex.Message;
            //}
        }
        public static async Task<dynamic> CreateAccountLink(string Account, string RefUrl, string RetUrl, string Type)
        {
            StripeConfiguration.ApiKey = "sk_test_51IWcRSIQhq5DrjgohRGasBiXovWGY8TkYMaOyivi0DGviyivZfpwkWGMel27dVmf3X3Eh2gB9iXEVgd5khZrspzN00Efx4QCyo";

            var options = new AccountLinkCreateOptions
            {
                Account = Account, //"acct_1032D82eZvKYlo2C",
                RefreshUrl = RefUrl, //"https://example.com/reauth",
                ReturnUrl = RefUrl, //"https://example.com/return",
                Type = Type, //"account_onboarding",
            };
            var service = new AccountLinkService();
            var accountLink = service.Create(options);

            return accountLink;
        }
    }
}
