using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EventHubPackage.Core;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Core;

internal static class AuthenticationExtension
{
	public static void AddMyAuthentication(this IServiceCollection service)
	{
		service
			.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			.AddJwtBearer(options =>
			{
				options.TokenValidationParameters =
					new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ClockSkew = TimeSpan.Zero,
						ValidateIssuerSigningKey = true,
						ValidIssuer = MyEnvironment.JwtIssuer,
						ValidAudience = MyEnvironment.JwtAudience,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(MyEnvironment.JwtKey))
					};
			});
		service.AddAuthorization();
	}

	public static string GenerateToken(Guid id, string type)
	{
		var token = new JwtSecurityTokenHandler()

			.WriteToken(
				new JwtSecurityToken(MyEnvironment.JwtIssuer,
					MyEnvironment.JwtAudience,
					new[]
					{
						new Claim("Id", id.ToString()),
						new Claim(ClaimTypes.Role, type)
					},
					expires: DateTime.Now.AddSeconds(MyEnvironment.JwtExpiredService),
					signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8
						.GetBytes(MyEnvironment.JwtKey)), SecurityAlgorithms.HmacSha256)
				)
			);
		return token;
	}
}

public class Auth
{
	public Auth(IEnumerable<Claim> claims)
	{
		var dict = claims.ToDictionary(k => k.Type, v => v.Value);
		Id = Guid.Parse(dict["Id"]);
	}

	public Guid Id { get; }
}