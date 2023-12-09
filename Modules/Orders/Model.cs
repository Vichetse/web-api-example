namespace WebApi.Modules.Orders;

public class GetOrder

{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
}
 
    public class InsertOrder
{
    public string Name { get; set; } = null!;
}

    public class UpdateOrder
{
    public string Name { get; set; } = null!; 
}


