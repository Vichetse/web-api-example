using WebApi.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Modules.ReturnData;

public class Marge : Entity
{
	public string Name { get; set; }= null!;
}

public class ExampleConfig : IEntityTypeConfiguration<Marge>
{
	public void Configure(EntityTypeBuilder<Marge> builder)
	{
		
	}
}