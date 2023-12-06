using Microsoft.AspNetCore.Mvc;
using WebApi.Modules.Orders;
using WebApi.Modules.Customer;
using WebApi.Core;
using AutoMapper;

namespace WebApi.Modules.ReturnData;

public class MargeController : MyController
{
	private readonly IMapper _mapper;
	private readonly IExampleRepositoryCustomer _customerRepository;
	private readonly IExampleRepositoryOrder _OrderRepository;

	public MargeController(IExampleRepositoryCustomer customerRepository, IMapper mapper, IExampleRepositoryOrder OrderRepository)
	{
		_mapper = mapper;
		_customerRepository = customerRepository;
		_OrderRepository = OrderRepository;
	}

	[HttpGet("{id:guid}")]
	public IActionResult GetOrdersByCustomerId(Guid customerId)
	{
		var customer = _customerRepository.GetSingle(e => e.Id == customerId);
		var resultcustomer = _mapper.Map<Get>(customer);
		if (customer == null)
		{
			return NotFound();
		}

		var order = _OrderRepository.GetSingle(e => e.Id == customerId);
		var resultorders = _mapper.Map<Get>(order);
		if (customer == null)
		{
			return NotFound();
		}

		_mapper.Map(resultcustomer, resultorders);
		_customerRepository.Commit();
		return Ok();
	}
}