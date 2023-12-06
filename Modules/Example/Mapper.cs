using AutoMapper;

namespace WebApi.Modules.Example;

public class ExampleMapper : Profile
{
    public ExampleMapper()
    {
        CreateMap<Example, GetExampleResponse>();
        CreateMap<InsertExampleRequest, Example>();
        CreateMap<UpdateExampleRequest, Example>();
        
    }
}