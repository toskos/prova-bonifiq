using ProvaPub.Repository;

namespace ProvaPub.Tests.Mock
{
    public class OrderRepositoryMock : IOrderRepository
    {
        private readonly ITestDbContext _ctx;

        public OrderRepositoryMock(ITestDbContext ctx)
        {
            _ctx = ctx;
        }

        public Task<bool> ExistInThisMonth(int customerId, decimal purchaseValue)
        {
            var haveBoughtBefore = _ctx.Customers.Count(s => s.Id == customerId && s.Orders.Any());
            if (haveBoughtBefore == 0 && purchaseValue > 100)
                return Task.FromResult(false);

            return Task.FromResult(true);
        }
    }
}
