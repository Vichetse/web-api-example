namespace WebApi.Modules.Orders;

public class GetOrder
{
    public Guid Id { get; set; }
    public string TypeFood { get; set; } = null!;
    public string TypeDrink { get; set; } = null!;
}
 
 
public class InsertOrder
{
    public string TypeFood { get; set; } = null!;
    public string TypeDrink { get; set; } = null!;
}


    public class UpdateOrder
{
    public string TypeFood { get; set; } = null!;
    public string TypeDrink { get; set; } = null!;
}


