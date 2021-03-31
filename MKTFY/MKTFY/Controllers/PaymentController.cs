using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MKTFY.App.Exceptions;
using MKTFY.Models.Entities;
using MKTFY.Models.ViewModels;
using Stripe;
using Stripe.Checkout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MKTFY.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly StripeSettings _stripeSettings;

        public PaymentController(IOptions<StripeSettings> stripeSettings)
        {
            _stripeSettings = stripeSettings.Value;
        }

        [HttpPost("createpayment")] // will use async but may add await later
        public async Task<IActionResult> CreatePayment([FromBody] CheckOutRequest req)
        {
            var options = new SessionCreateOptions
            {
                SuccessUrl = req.SuccessUrl,
                CancelUrl = req.FailureUrl,
                PaymentMethodTypes = new List<string>
                {
                    "card",
                },
                Mode = "Listing",
                LineItems = new List<SessionLineItemOptions>
                {
                    new SessionLineItemOptions
                    {
                        Price = req.PriceId,
                        Quantity = 1,
                    },
                },
            }; //subscription // from mode

            var service = new SessionService();
            service.Create(options);
            try
            {
                var session = await service.CreateAsync(options);
                return Ok(new CheckOutResponse
                {
                    SessionId = session.Id,
                    PublicKey = _stripeSettings.PublicKey
                });
            }
            catch (StripeException e) //StripeException e //new NotFoundException
            {
                string message = e.StripeError.Message;
                //Console.WriteLine(e.StripeError.Message);
                return BadRequest(message);
            }
        }
        /*
        public async Task<PaymentSourceResult> GetPaymentSourceFromProviderAsync(IBillable billable, string externalPaymentSourceId)
        {
            global::Stripe.StripeConfiguration.ApiKey = _configuration.Value.SecretKey;
            var cardService = new CardService();
            var foundPaymentSource = cardService.Get(billable.StripeCustomerId, externalPaymentSourceId);

            return new PaymentSourceResult
            {
                CardBrand = foundPaymentSource.Brand,
                CardLast4 = foundPaymentSource.Last4,
                ExpMonth = foundPaymentSource.ExpMonth,
                ExpYear = foundPaymentSource.ExpYear
            };
        }
        
        public async Task<SingleChargeResult> EXPRESSAccountSingleChargeAsync(IStripeConnectBillable stripeConnectBillable, IStripeConnectAble stripeConnectAble, IPaymentSource paymentSource,
           decimal amount, decimal applicationFee, Currency currency, string description = "")
        {
            // Payment source was a raw Stripe Payment (eg raw token) so we will single charge the token directly
            if (paymentSource is StripePaymentSource src)
            {
                global::Stripe.StripeConfiguration.ApiKey = _stripeConfiguration.Value.SecretKey;
                var currencyString = CurrencyDefiner(currency);
                var chargeService = new ChargeService();
                var res = await chargeService.CreateAsync(
                    new ChargeCreateOptions
                    {
                        Amount = (long?)(amount * 100),
                        Currency = currencyString,
                        Source = src.StripeToken,
                        Description = description,
                        ApplicationFeeAmount = (long?)(applicationFee * 100),
                        TransferData = new ChargeTransferDataOptions
                        {
                            Destination = stripeConnectAble.StripeConnectOnboard.StripeConnectSystemAccountId,
                        }
                    });
                return SuccesResultCreator(res);
            }
            // Payment source was a saved payment source (eg user added it to their profile)
            if (paymentSource is BillablePaymentSource src2)
            {
                global::Stripe.StripeConfiguration.ApiKey = _stripeConfiguration.Value.SecretKey;
                // Stripe will not let us bill a card ID directly. We need to specify the stripe customer and card ID
                if (string.IsNullOrEmpty(stripeConnectBillable.StripeCustomerId))
                    throw new InvalidOperationException("No Customer is set up for this IBillable object");
                var currencyString = CurrencyDefiner(currency);
                var chargeService = new ChargeService();
                var res = await chargeService.CreateAsync(
                    new ChargeCreateOptions
                    {
                        Amount = (long?)(amount * 100),
                        Currency = currencyString,
                        Customer = stripeConnectBillable.StripeCustomerId,
                        Source = src2.ExternalId,
                        Description = description,
                        ApplicationFeeAmount = (long?)(applicationFee * 100),
                        TransferData = new ChargeTransferDataOptions
                        {
                            Destination = stripeConnectAble.StripeConnectOnboard.StripeConnectSystemAccountId,
                        }
                    });
                return SuccesResultCreator(res);
            }
            throw new InvalidPaymentSourceException("Invalid payment source");
        }*/

    }
}
