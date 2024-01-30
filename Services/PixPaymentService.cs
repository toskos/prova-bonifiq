using ProvaPub.Models;

namespace ProvaPub.Services
{
    public class PixPaymentService : IPaymentMethodService
    {
        public Task<Order> MakePayment(decimal paymentValue, int customerId)
        {
            throw new NotImplementedException();
        }
    }
}
