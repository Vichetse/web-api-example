using WebApi.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Modules.Example;

public class Example : Entity
{
	public string Name { get; set; } = null!;
    public string Age { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string? CountryCode { get; set; }

}

public class ExampleConfig : IEntityTypeConfiguration<Example>
{
	public void Configure(EntityTypeBuilder<Example> builder)
	{
		
	}
}