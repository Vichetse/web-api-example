using Microsoft.AspNetCore.Mvc;
using WebApi.Core;

namespace WebApi.Modules.Orders;

public interface IRepositoryOrder : IRepository<Order>
{

}
public class OrderRepository : Repository<Order>, IRepositoryOrder
{
	public OrderRepository(MyDbContext context) : base(context)
	{
		
	}
}