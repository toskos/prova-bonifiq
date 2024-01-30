using ProvaPub.Repository;

namespace ProvaPub.Services
{
    public class CustomerService
    {
        private readonly ITestDbContext _ctx;

        private readonly ICustomerRepository _customerRepository;

        private readonly IOrderRepository _orderRepository;
        
        public CustomerService(ITestDbContext ctx, ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            _ctx = ctx;
            _customerRepository = customerRepository;
            _orderRepository = orderRepository;
        }

        public async Task<bool> CanPurchase(int customerId, decimal purchaseValue)
        {
            if (customerId <= 0) throw new ArgumentOutOfRangeException(nameof(customerId));

            if (purchaseValue <= 0) throw new ArgumentOutOfRangeException(nameof(purchaseValue));

            //Business Rule: Non registered Customers cannot purchase
            var customer = await _ctx.Customers.FindAsync(customerId);//_ctx.Customers.Where(x => x.Id.Equals(customerId)).FirstOrDefaultAsync();//
            if (customer == null) throw new InvalidOperationException($"Customer Id {customerId} does not exists");

            //Business Rule: A customer can purchase only a single time per month
            if(!await _customerRepository.HaveBoughtBefore(customerId))
                return false;

            //Business Rule: A customer that never bought before can make a first purchase of maximum 100,00
            if (!await _orderRepository.ExistInThisMonth(customerId, purchaseValue))
                return false;

            return true;
        }
    }
}
