using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core;
using WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Modules.Example;

public class ExampleController : MyController
{
	// private readonly ICloudStorageSingletonService _service;
	private readonly IMapper _mapper;
	private readonly IExampleRepository _repository;

	public ExampleController(
		IExampleRepository repository,
		ICloudStorageSingletonService service, 
        IMapper mapper
        )
	{
		_repository = repository;
		// _service = service;
		_mapper = mapper;
	}

    [HttpGet]
    public IActionResult Gets()
    {
        return Ok("Hello World");
    }
}