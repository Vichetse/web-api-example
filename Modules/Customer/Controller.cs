using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core;
using WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Modules.Customer;

public class CustomerController : MyController
{
	private readonly IMapper _mapper;
	private readonly IExampleRepository _repository;

	public CustomerController(
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
		var result = _mapper.ProjectTo<GetCustomer>(items);
		return Ok(result);
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetById(Guid id)
	{
		var items = _repository.GetSingle(e => e.Id == id);
		var result = _mapper.Map<GetCustomer>(items);
		return Ok(result);
	}

	[HttpPost]
	public IActionResult Post([FromBody] InsertCustomer insertCustomer)
	{
		if (insertCustomer == null)
		{
			return BadRequest("Invalid data");
		}
		var items = _mapper.Map<Customer>(insertCustomer);

		_repository.Add(items);
		_repository.Commit();
		return Ok();
	}

	[HttpPut("{id:guid}")]
	public IActionResult Update(Guid id,[FromBody] UpdateCustomer change)
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
		if (change.Phone == null || change.Phone == "")
		{
			change.Phone = item.Phone;
		}
		if (change.Address == null || change.Address == "")
		{
			change.Address = item.Address;
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