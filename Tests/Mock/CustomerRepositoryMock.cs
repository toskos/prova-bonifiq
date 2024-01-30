using Microsoft.EntityFrameworkCore;
using ProvaPub.Repository;

namespace ProvaPub.Tests.Mock
{
    public class CustomerRepositoryMock : ICustomerRepository
    {
        private readonly ITestDbContext _ctx;

        public CustomerRepositoryMock(ITestDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<bool> HaveBoughtBefore(int customerId)
        {
            var baseDate = DateTime.UtcNow.AddMonths(-1);
            var ordersInThisMonth = _ctx.Orders.Count(s => s.CustomerId == customerId && s.OrderDate >= baseDate);
            if (ordersInThisMonth > 0)
                return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}
