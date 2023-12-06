using AutoMapper;

namespace WebApi.Modules.Orders;

public class OrdersMapper : Profile
{
    public OrdersMapper()
    {
        CreateMap<Order, GetOrder>();
        CreateMap<InsertOrder, Order>();
        CreateMap<UpdateOrder, Order>();
    }
}