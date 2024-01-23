using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core;
using WebApi.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using WebApi.Modules.Customer;

namespace WebApi.Modules.Orders;

public class OrdersController : MyController
{
	private readonly IMapper _mapper;
	private readonly IRepositoryOrder _Orderrepository;
	private readonly IRepositoryCustomer _Customerrepository;

	public OrdersController(IRepositoryOrder repositoryorders, IMapper mapper, IRepositoryCustomer repositoryCustomer)
	{
		_Orderrepository = repositoryorders;
		_mapper = mapper;
		_Customerrepository = repositoryCustomer;
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
	public IActionResult Insert([FromBody] InsertOrder insertOrder)
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
	public IActionResult Update(Guid id, [FromBody] UpdateOrder change)
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


	[HttpDelete("DeleteByCustomerId/{id:guid}")]
	public IActionResult DeleteByCustomerId(Guid id)
	{
		var customer = _Customerrepository.GetSingle(e => e.Id == id);
		if (customer == null)
		{
			return NotFound("Customer not found");
		}
	
		_Customerrepository.Remove(customer);
		_Customerrepository.Commit();

		return Ok();
	}

	private static void LogOrderDetails(Order order)
	{
		Console.WriteLine($"Order {order.Id} details logged before deletion.");
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