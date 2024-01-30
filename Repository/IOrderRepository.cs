namespace ProvaPub.Repository
{
    public interface IOrderRepository
    {
        Task<bool> ExistInThisMonth(int customerId, decimal purchaseValue);
    }
}
