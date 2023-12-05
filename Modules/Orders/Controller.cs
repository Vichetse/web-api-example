using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core;
using WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Modules.Orders;

public class OrdersController : MyController
{
	private readonly IMapper _mapper;
	private readonly IExampleRepository _repository;

	public OrdersController(
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
		var result = _mapper.ProjectTo<GetOrder>(items);
		return Ok(result);
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetById(Guid id)
	{
		var items = _repository.GetSingle(e => e.Id == id);
		var result = _mapper.Map<GetOrder>(items);
		return Ok(result);
	}

	[HttpPost]
	public IActionResult Post([FromBody] InsertOrder insertOrder)
	{
		if (insertOrder == null)
		{
			return BadRequest("Invalid data");
		}
		var items = _mapper.Map<Order>(insertOrder);

		_repository.Add(items);
		_repository.Commit();
		return Ok();
	}

	[HttpPut("{id:guid}")]
	public IActionResult Update(Guid id,[FromBody] UpdateOrder change)
	{
		var item = _repository.GetSingle(e => e.Id == id);
		if (item == null)
		{
			return NotFound("Item not found");
		}

		if (change.TypeFood == null || change.TypeFood == "")
		{
			change.TypeFood = item.TypeFood;	
		}
		if (change.TypeDrink == null || change.TypeDrink == "")
		{
			change.TypeDrink = item.TypeDrink;
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