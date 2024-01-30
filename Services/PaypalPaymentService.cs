using ProvaPub.Models;

namespace ProvaPub.Services
{
    public class PaypalPaymentService : IPaymentMethodService
    {
        public Task<Order> MakePayment(decimal paymentValue, int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
