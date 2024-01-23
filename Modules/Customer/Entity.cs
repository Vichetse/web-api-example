using WebApi.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace WebApi.Modules.Customer;
public class Customer : AuditableEntity
{
	public string Name { get; set; }= null!;
    public string Phone { get; set; } = null!;
    public string Address { get; set; } = null!;
	public ICollection<Orders.Order>  Orders{ get; set; } = null!;
}

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
	public void Configure(EntityTypeBuilder<Customer> builder)
	{
		builder.HasMany(c => c.Orders)
			   .WithOne(o => o.Customer)
			   .HasForeignKey(o => o.CustomerId);
		
	}
}