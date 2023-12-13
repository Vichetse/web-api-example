using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core;
using WebApi.Services;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Modules.Orders;

public class OrdersController : MyController
{
	private readonly IMapper _mapper;
	private readonly IExampleRepositoryOrder _Orderrepository;

	public OrdersController(IExampleRepositoryOrder repository,IMapper mapper)
	{
		_Orderrepository = repository;
		_mapper = mapper;
	}

	[HttpGet("")]
	public IActionResult Get()
	{
		var items = _Orderrepository.GetAll();
		var result = _mapper.ProjectTo<GetOrder>(items);
		return Ok(result);
	}


	[HttpGet("GetByUserId/{customerId:guid}")]
	public IActionResult GetByUserId(Guid customerId)
	{
		var items = _Orderrepository.FindBy(e => e.CustomerId == customerId);
		var result = _mapper.ProjectTo<GetOrder>(items);
		return Ok(result);
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetById(Guid id)
	{
		var items = _Orderrepository.GetSingle(e => e.Id == id);
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

		_Orderrepository.Add(items);
		_Orderrepository.Commit();
		return Ok();
	}
	

	[HttpPut("{id:guid}")]
	public IActionResult Update(Guid id,[FromBody] UpdateOrder change)
	{
		var item = _Orderrepository.GetSingle(e => e.Id == id);
		if (item == null)
		{
			return NotFound("Item not found");
		}

		if (change.Name == null || change.Name == "")
		{
			change.Name = item.Name;	
		}
		_mapper.Map(change, item);
		_Orderrepository.Update(item);
		_Orderrepository.Commit();

		return Ok();
	}
	[HttpDelete("{id:guid}")]
	public IActionResult Delete(Guid id)
	{
		var item = _Orderrepository.GetSingle(e => e.Id == id);
		if (item == null)
		{
			return NotFound("Item not found");
		}

		_Orderrepository.Remove(item);
		_Orderrepository.Commit();

		return Ok();
	}
}