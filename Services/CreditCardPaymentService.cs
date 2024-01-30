using ProvaPub.Models;
using ProvaPub.Services;

namespace ProvaPub.Services
{
    public class CreditCardPaymentService : IPaymentMethodService
    {
        public Task<Order> MakePayment(decimal paymentValue, int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
