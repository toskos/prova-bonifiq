using Microsoft.EntityFrameworkCore;

namespace ProvaPub.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ITestDbContext _ctx;

        public CustomerRepository(ITestDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> HaveBoughtBefore(int customerId)
        {
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = await _ctx.Orders.CountAsync(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
                return false;

            return true;
        }
    }
}
