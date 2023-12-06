using WebApi.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Modules.Orders;

public class Order : Entity
{

	public string TypeFood { get; set; } = null!;
	public string TypeDrink { get; set; } = null!;

}

public class ExampleConfig : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
	}
}
