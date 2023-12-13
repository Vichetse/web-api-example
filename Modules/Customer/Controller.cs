using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebApi.Core;
using WebApi.Modules.Orders;

namespace WebApi.Modules.Customer;

public class CustomerController : MyController
{
	private readonly IMapper _mapper;
	private readonly IExampleRepositoryCustomer _Customerepository;
	private readonly IExampleRepositoryOrder _Orderrepository;

	public CustomerController(
		IExampleRepositoryCustomer repository,
        IMapper mapper,
		IExampleRepositoryOrder order
        )
	{
		_Customerepository = repository;
		_mapper = mapper;
		_Orderrepository = order;
	}

	[HttpGet("")]
	public IActionResult Get()
	{
		var items = _Customerepository.GetAll();
		// var orderedItems = items.OrderBy(e => e.Id);
		var result = _mapper.ProjectTo<GetCustomer>(items);
		return Ok(result);
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetById(Guid id)
	{
		var items = _Customerepository.GetSingle(e => e.Id == id);
		var customers = _mapper.Map<GetCustomer>(items);
		

		var orders = _Orderrepository.FindBy(e => e.Id == id);
		var itemorder = _mapper.ProjectTo<GetOrder>(orders);



		return Ok();
	}


	[HttpPost]
	public IActionResult Post([FromBody] InsertCustomer insertCustomer)
	{
		if (insertCustomer == null)
		{
			return BadRequest("Invalid data");
		}
		var items = _mapper.Map<Customer>(insertCustomer);

		_Customerepository.Add(items);
		_Customerepository.Commit();
		return Ok();
	}
	[HttpPut("{id:guid}")]
	public IActionResult Update(Guid id,[FromBody] UpdateCustomer change)
	{
		var item = _Customerepository.GetSingle(e => e.Id == id);
		if (item == null)
		{
			return NotFound("Item not found");
		}
		// old 1 new 5

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
		_Customerepository.Update(item);
		
		_Customerepository.Commit();

		return Ok();
	}
	[HttpDelete("{id:guid}")]
	public IActionResult Delete(Guid id)
	{
		var item = _Customerepository.GetSingle(e => e.Id == id);
		if (item == null)
		{
			return NotFound("Item not found");
		}

		_Customerepository.Remove(item);
		_Customerepository.Commit();

		return Ok();
	}
}