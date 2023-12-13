using Microsoft.AspNetCore.Mvc;
using WebApi.Core;

namespace WebApi.Modules.Customer;

public interface IRepositoryCustomer : IRepository<Customer>
{
	
}
public class CustomerRepository : Repository<Customer>, IRepositoryCustomer
{
	public CustomerRepository(MyDbContext context) : base(context)
	{
	
	}
}