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

	[HttpGet("{id:guid}")]
	public IActionResult GetById(Guid id)
	{
		var items = _repository.GetSingle(e => e.Id == id);
		var result = _mapper.Map<GetExampleResponse>(items);
		return Ok(result);
	}

	[HttpPost]
	public IActionResult Post([FromBody] InsertExampleRequest insertExampleRequest)
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

	[HttpPut("{id:guid}")]
	public IActionResult Update(Guid id,[FromBody] UpdateExampleRequest change)
	{
		var item = _repository.GetSingle(e => e.Id == id);
		if (item == null)
		{
			return NotFound("Item not found");
		}

		if (change.Name == null || change.Name == "")
		{
			change.Name = item.Name;	
		}
		if (change.Age == null || change.Age == "")
		{
			change.Age = item.Age;
		}
		if (change.Gender == null || change.Gender == "")
		{
			change.Gender = item.Gender;
		}
		if (change.Address == null || change.Address == "")
		{
			change.Address = item.Address;
		}
		if (change.CountryCode == null || change.CountryCode == "")
		{
			change.CountryCode = item.CountryCode;
		}
		_mapper.Map(change, item);
		_repository.Update(item);
		_repository.Commit();

		return Ok();
	}
	[HttpDelete("{id:guid}")]
	public IActionResult Delete(Guid id)
	{
		var item = _repository.GetSingle(e => e.Id == id);
		if (item == null)
		{
			return NotFound("Item not found");
		}

		_repository.Remove(item);
		_repository.Commit();

		return Ok();
	}
}