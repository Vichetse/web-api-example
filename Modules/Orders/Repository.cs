using Microsoft.AspNetCore.Mvc;
using WebApi.Core;

namespace WebApi.Modules.Orders;

public interface IExampleRepositoryOrder : IRepository<Order>
{

}
public class OrderRepository : Repository<Order>, IExampleRepositoryOrder
{
	public OrderRepository(MyDbContext context) : base(context)
	{
	}
}