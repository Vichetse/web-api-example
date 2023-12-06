using Microsoft.AspNetCore.Mvc;
using WebApi.Core;

namespace WebApi.Modules.ReturnData;

public interface IExampleRepositoryMarge : IRepository<Marge>
{

}
public class MargeRepository : Repository<Marge>, IExampleRepositoryMarge
{
	public MargeRepository(MyDbContext context) : base(context)
	{
		
	}
}
