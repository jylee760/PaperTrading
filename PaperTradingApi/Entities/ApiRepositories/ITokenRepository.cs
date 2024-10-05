using Microsoft.AspNetCore.Identity;

namespace PaperTrading.Entities.ApiRepositories
{
    public interface ITokenRepository
    {
        string CreateJwtTokeN(IdentityUser user, List<string> roles);
    }
}
