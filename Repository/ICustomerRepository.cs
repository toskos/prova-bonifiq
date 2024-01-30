namespace ProvaPub.Repository
{
    public interface ICustomerRepository
    {
        Task<bool> HaveBoughtBefore(int customerId);
    }
}
