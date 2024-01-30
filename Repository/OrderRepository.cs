using Microsoft.EntityFrameworkCore;

namespace ProvaPub.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ITestDbContext _ctx;

        public OrderRepository(ITestDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<bool> ExistInThisMonth(int customerId, decimal purchaseValue)
        {
            var haveBoughtBefore = await _ctx.Customers.CountAsync(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return false;

            return true;
        }
    }
}
