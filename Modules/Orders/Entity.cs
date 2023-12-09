using WebApi.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Modules.Orders;

public class Order : Entity
{
	public Guid CusotmerID {get; set;}
	public string Name { get; set; } = null!;
	public virtual Customer.Customer ForeigKeyCustomer {get; set;} = null!;
 

}
public class ExampleConfig : IEntityTypeConfiguration<Order>
{
	public void Configure(EntityTypeBuilder<Order> builder)
	{
		builder.HasOne(o => o.ForeigKeyCustomer)
			   .WithMany(c => c.PrimaryKeyOrder)
			   .HasForeignKey(o => o.CusotmerID);
	}
}
