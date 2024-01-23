using AutoMapper;

namespace WebApi.Modules.Customer;

public class CustomerMapper : Profile
{
    public CustomerMapper()
    {
        CreateMap<Customer, GetCustomer>();
        CreateMap<InsertCustomer, Customer>();
        CreateMap<UpdateCustomer, Customer>();
          CreateMap<DeleteCustomer, Customer>();
    }
}