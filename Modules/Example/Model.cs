namespace WebApi.Modules.Example;

public class GetExampleResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public string Age { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string? Address { get; set; } 
    public string? CountryCode { get; set; }
}
 
public class InsertExampleRequest
{
    public string Name { get; set; } = null!;
    public string Age { get; set; } = null!;
    public string Gender { get; set; } = null!;
    public string? Address { get; set; } 
    public string? CountryCode { get; set; }
}


public class UpdateExampleRequest
{
    public string? Name { get; set; }
    public string? Age { get; set; } 
    public string? Gender { get; set; }
    public string? Address { get; set; } 
    public string? CountryCode { get; set; }
}


