namespace WebApi.Modules.Orders;

public class GetOrder

{
    public Guid Id { get; set; }
    public Guid CustomerId { get; set; }

    public string Name { get; set; } = null!;
}
 
    public class InsertOrder
{
    public string Name { get; set; } = null!;
    public Guid CustomerId{get;set;} 
}

    public class UpdateOrder
{
    public string Name { get; set; } = null!; 
}


