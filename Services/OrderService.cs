using ProvaPub.Models;

namespace ProvaPub.Services
{
    public class OrderService
	{

        private readonly PaypalPaymentService _paypalPaymentService;
        private readonly PixPaymentService _pixPaymentService;
        private readonly CreditCardPaymentService _creditCardPaymentService;

        public OrderService(PaypalPaymentService paypalPaymentService, PixPaymentService pixPaymentService, CreditCardPaymentService creditCardPaymentService)
        {
            _paypalPaymentService = paypalPaymentService;
            _pixPaymentService = pixPaymentService;
            _creditCardPaymentService = creditCardPaymentService;
        }

        private async Task<Order> PayOrder(decimal paymentValue)
		{
			return await Task.FromResult( new Order()
			{
				Value = paymentValue
			});
		}

        public async Task<Order> PayOrderPaypal(decimal paymentValue, int customerId)
        {
            await _paypalPaymentService.MakePayment(paymentValue, customerId);

            return await PayOrder(paymentValue);
        }

        public async Task<Order> PayOrderPix(decimal paymentValue, int customerId)
        {
            await _pixPaymentService.MakePayment(paymentValue, customerId);

            return await PayOrder(paymentValue);
        }

        public async Task<Order> PayOrderCreditCard(decimal paymentValue, int customerId)
        {
            await _creditCardPaymentService.MakePayment(paymentValue, customerId);

            return await PayOrder(paymentValue);
        }

    }
}
