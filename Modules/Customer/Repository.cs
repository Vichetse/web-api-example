using Microsoft.AspNetCore.Mvc;
using WebApi.Core;

namespace WebApi.Modules.Customer;

public interface IExampleRepositoryCustomer : IRepository<Customer>
{
	
}
public class CustomerRepository : Repository<Customer>, IExampleRepositoryCustomer
{
	public CustomerRepository(MyDbContext context) : base(context)
	{
	
	}
}