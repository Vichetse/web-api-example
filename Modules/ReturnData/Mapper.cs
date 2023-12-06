using AutoMapper;

namespace WebApi.Modules.ReturnData;
public class OrdersMapper : Profile
{
    public OrdersMapper()
    {
        CreateMap<Marge, Get>();
    }
}