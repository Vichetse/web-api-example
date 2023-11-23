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

	[HttpGet("")]
	public IActionResult Get()
	{
		var items = _repository.GetAll();
		var result = _mapper.ProjectTo<GetExampleResponse>(items);
		return Ok(result);
	}

	[HttpGet("/{id:guid}")]
	public IActionResult GetById(Guid id)
	{
		var items = _repository.GetSingle(e => e.Id == id);
		var result = _mapper.Map<GetExampleResponse>(items);
		return Ok(result);
	}

	[HttpPost]
	public IActionResult Post([FromBody] GetExampleResponse insertExampleRequest)
	{
		if (insertExampleRequest == null)
		{
			return BadRequest("Invalid data");
		}
		var items = _mapper.Map<Example>(insertExampleRequest);

		_repository.Add(items);
		_repository.Commit();
		return Ok();
	}
}