using WebApi.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Modules.Customer;

public class Customer : Entity
{

	public string Name { get; set; }= null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;

	public virtual ICollection<Orders.Order> PrimaryKeyOrder{get; set;} = new List<Orders.Order>();

}

public class ExampleConfig : IEntityTypeConfiguration<Customer>
{
	public void Configure(EntityTypeBuilder<Customer> builder)
	{
		builder.HasMany(c => c.PrimaryKeyOrder)
				.WithOne(o => o.ForeigKeyCustomer)
				.HasForeignKey(o => o.CusotmerID);
	}
}