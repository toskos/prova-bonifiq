using FluentAssertions;
using ProvaPub.Services;
using ProvaPub.Tests.Mock;
using Xunit;

namespace ProvaPub.Tests
{
    public class CustomerServiceTests
    {
        private readonly CustomerService _customerService;
        
        private const int customerIdInvalid = 0;
        private const decimal purchaseValueInvalid = 0;

        private const int customerId = 3;
        private const decimal purchaseValue = 3;

        private const int customerIdInvalidOperation = 21;
        private const decimal purchaseValueInvalidOperation = 1;

        private const int customerIdOrdersInThisMonth = 1;
        private const decimal purchaseValueOrdersInThisMonth = 1;

        private const int customerIdHaveBoughtBefore = 2;
        private const decimal purchaseValueHaveBoughtBefore = 200;


        public CustomerServiceTests() 
        {
            TestDbContextMock ctx = TestDbSetContextMock.CreateDbContext();
            CustomerRepositoryMock _customerRepository = new CustomerRepositoryMock(ctx);
            OrderRepositoryMock _orderRepository = new OrderRepositoryMock(ctx);

            _customerService = new CustomerService(ctx, _customerRepository, _orderRepository);
        }


        [Fact]
        public async void CanPurchase_argumentOutOfRange()
        {
            try
            {
                var test = await _customerService.CanPurchase(customerIdInvalid, purchaseValueInvalid);
                test.Should().BeFalse();
            }
            catch (ArgumentOutOfRangeException ex)
            {
                ex.Should().NotBeNull();
            }
        }
        
        [Fact]
        public async void CanPurchase_invalidOperation()
        {
            try
            {
                bool test = await _customerService.CanPurchase(customerIdInvalidOperation, purchaseValueInvalidOperation);
                test.Should().BeFalse();
            }
            catch (InvalidOperationException ex)
            {
                ex.Should().NotBeNull();
            }
        }

        [Fact]
        public async void CanPurchase_ordersInThisMonth()
        {
            bool test = await _customerService.CanPurchase(customerIdOrdersInThisMonth, purchaseValueOrdersInThisMonth);
            test.Should().BeFalse();
        }

        [Fact]
        public async void CanPurchase_haveBoughtBefore()
        {
            bool test = await _customerService.CanPurchase(customerIdHaveBoughtBefore, purchaseValueHaveBoughtBefore);
            test.Should().BeFalse();
        }

        [Fact]
        public async void CanPurchase_ok()
        {
            bool test = await _customerService.CanPurchase(customerId, purchaseValue);
            test.Should().BeTrue();
        }
    }
}
