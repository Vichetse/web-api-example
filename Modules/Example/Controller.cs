using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core;
using WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Modules.Example;

public class ExampleController : MyController
{
	private readonly IMapper _mapper;
	private readonly IExampleRepository _repository;

	public ExampleController(
		IExampleRepository repository,
        IMapper mapper
        )
	{
		_repository = repository;
		_mapper = mapper;
	}

    [HttpGet]
    public IActionResult Gets()
    {
      var items = _repository.GetAll();
	  var result = _mapper.ProjectTo<GetExampleResponse>(items); 
	  return Ok(result);
    }
}