using Castle.Core.Resource;
using Microsoft.EntityFrameworkCore;
using Moq;
using ProvaPub.Models;

namespace ProvaPub.Tests.Mock
{
    public class TestDbSetContextMock
    {

        private static List<Customer> CreateListCustomerFake()
        {
            Customer customerInThisMonth = new Customer() { Id = 1, Name = "Teste vendas vencida" };
            Order orderInThisMonth = new Order() { CustomerId = 1, Id = 1, OrderDate = DateTime.Today, Value = 50, Customer = customerInThisMonth };
            customerInThisMonth.Orders = new List<Order>() { orderInThisMonth };

            Customer customerHaveBoughtBefore = new Customer() { Id = 2, Name = "Teste Vendas com valor maior de 100" };
            Order orderHaveBoughtBefore = new Order() { CustomerId = 2, Id = 2, OrderDate = DateTime.Today.AddMonths(-2), Value = 150, Customer = customerInThisMonth };
            customerInThisMonth.Orders = new List<Order>() { orderHaveBoughtBefore };

            Customer customer = new Customer() { Id = 3, Name = "Teste Vendas correta" };
            Order order = new Order() { CustomerId = 3, Id = 3, OrderDate = DateTime.Today.AddMonths(-2), Value = 50, Customer = customer };
            customer.Orders = new List<Order>() { order };

            List<Customer> customers = new List<Customer>();
            customers.Add(customerInThisMonth);
            customers.Add(customerHaveBoughtBefore);
            customers.Add(customer);

            return customers;
        }

        private static IQueryable<Customer> CreateQueryCustomerFake()
        {
            return CreateListCustomerFake().AsQueryable();
        }

        private static DbSet<Customer> CreateDbSetCustomerFake()
        {
            IQueryable<Customer> customerQuery = CreateQueryCustomerFake();

            Mock<DbSet<Customer>> customerDbset = new Mock<DbSet<Customer>>();
            customerDbset.As<IQueryable<Customer>>().Setup(m => m.Provider).Returns(customerQuery.Provider);
            customerDbset.As<IQueryable<Customer>>().Setup(m => m.Expression).Returns(customerQuery.Expression);
            customerDbset.As<IQueryable<Customer>>().Setup(m => m.ElementType).Returns(customerQuery.ElementType);
            customerDbset.As<IQueryable<Customer>>().Setup(m => m.GetEnumerator()).Returns(() => customerQuery.GetEnumerator());

            return customerDbset.Object;
        }

        private static List<Order> CreateListOrderFake()
        {
            Customer customerInThisMonth = new Customer() { Id = 1, Name = "Teste vendas vencida" };
            Order orderInThisMonth = new Order() { CustomerId = 1, Id = 1, OrderDate = DateTime.Today, Value = 50, Customer = customerInThisMonth };
            customerInThisMonth.Orders = new List<Order>() { orderInThisMonth };

            Customer customerHaveBoughtBefore = new Customer() { Id = 2, Name = "Teste Vendas com valor maior de 100" };
            Order orderHaveBoughtBefore = new Order() { CustomerId = 2, Id = 2, OrderDate = DateTime.Today, Value = 150, Customer = customerInThisMonth };
            customerInThisMonth.Orders = new List<Order>() { orderHaveBoughtBefore };

            Customer customer = new Customer() { Id = 3, Name = "Teste Vendas correta" };
            Order order = new Order() { CustomerId = 3, Id = 3, OrderDate = DateTime.Today.AddMonths(-2), Value = 50, Customer = customer };
            customer.Orders = new List<Order>() { order };

            List<Order> orders = new List<Order>();
            orders.Add(orderInThisMonth);
            orders.Add(orderHaveBoughtBefore);
            orders.Add(order);

            return orders;
        }

        private static IQueryable<Order> CreateQueryOrderFake()
        {
            return CreateListOrderFake().AsQueryable();
        }

        private static DbSet<Order> CreateDbSetOrderFake()
        {
            IQueryable<Order> orderQuery = CreateQueryOrderFake();

            Mock<DbSet<Order>> orderbset = new Mock<DbSet<Order>>();
            orderbset.As<IQueryable<Order>>().Setup(m => m.Provider).Returns(orderQuery.Provider);
            orderbset.As<IQueryable<Order>>().Setup(m => m.Expression).Returns(orderQuery.Expression);
            orderbset.As<IQueryable<Order>>().Setup(m => m.ElementType).Returns(orderQuery.ElementType);
            orderbset.As<IQueryable<Order>>().Setup(m => m.GetEnumerator()).Returns(() => orderQuery.GetEnumerator());

            return orderbset.Object;
        }

        public static TestDbContextMock CreateDbContext() 
        {
            var customerDbset = CreateDbSetCustomerFake();
            var orderbset = CreateDbSetOrderFake();
            var baseDate = DateTime.UtcNow.AddMonths(-1);

            var mockContext = new Mock<TestDbContextMock>();
            mockContext.Setup(c => c.Customers).Returns(customerDbset);
            mockContext.Setup(c => c.Orders).Returns(orderbset);

            mockContext.Setup(x => x.Customers.FindAsync(1).Result)
                .Returns(CreateListCustomerFake().Find(e => e.Id == 1) ?? new Customer());

            mockContext.Setup(x => x.Customers.FindAsync(2).Result)
                .Returns(CreateListCustomerFake().Find(e => e.Id == 2) ?? new Customer());

            mockContext.Setup(x => x.Customers.FindAsync(3).Result)
                .Returns(CreateListCustomerFake().Find(e => e.Id == 3) ?? new Customer());

            return mockContext.Object;

        }

    }
}
