using WebApi.Core;

namespace WebApi.Modules.Example;

public interface IExampleRepository : IRepository<Example>
{

}

public class ExampleRepository : Repository<Example>, IExampleRepository
{
	public ExampleRepository(MyDbContext context) : base(context)
	{
	}

}