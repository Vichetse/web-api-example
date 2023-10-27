using WebApi.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Modules.Example;

public class Example : Entity
{

}

public class ExampleConfig : IEntityTypeConfiguration<Example>
{
	public void Configure(EntityTypeBuilder<Example> builder)
	{

	}
}