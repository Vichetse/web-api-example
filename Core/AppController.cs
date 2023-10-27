using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Core;

[Authorize]
[ApiController]
[Route("[controller]")]
public class MyController : ControllerBase
{
	[NonAction]
	protected Auth? GetClaim()
	{
		if (User.Identity is ClaimsIdentity claimsIdentity && claimsIdentity.Claims.Any())
			return new Auth(claimsIdentity.Claims);

		return null;
	}
}

[Authorize(Roles = "Service")]
public class MyInternalController : MyController
{
}

[Authorize(Roles = "System")]
public class MyAdminController : MyController
{
}


[AllowAnonymous]
public class MyAnonymousController : MyController
{
}