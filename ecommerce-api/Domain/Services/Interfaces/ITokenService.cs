using ecommerce_api.Domain.Models.Identity;

namespace ecommerce_api.Domain.Services.Interfaces;

public interface ITokenService
{
    string CreateToken(AppUser user);
}