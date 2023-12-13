using WebApi.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Modules.Orders;

public class Order : Entity
{
	public Guid CustomerId { get; set; }
	public string Name { get; set; } = null!;
	public Customer.Customer Customer { get; set; } = null!;

}
public class ExampleConfig : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.HasOne(c => c.Customer)
			   .WithMany(o => o.Orders)
			   .HasForeignKey(o => o.CustomerId);
	}
}
