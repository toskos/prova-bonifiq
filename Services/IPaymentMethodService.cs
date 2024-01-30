using ProvaPub.Models;

namespace ProvaPub.Services
{
    public interface IPaymentMethodService
    {
        public Task<Order> MakePayment(decimal paymentValue, int customerId);
    }
}
