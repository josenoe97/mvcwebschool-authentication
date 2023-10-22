using Microsoft.AspNetCore.Identity;
using MvcWebSchool_Identity.Services;
using System.Security.Claims;

namespace MvcWebIdentity.Services;

public class SeedUserClaimsInitial : ISeedUserClaimsInitial
{
    private readonly UserManager<IdentityUser> _userManager;
    public SeedUserClaimsInitial(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task SeedUserClaims()
    {
        try
        {
            IdentityUser user1 = await _userManager.FindByEmailAsync("admin@localhost");

            if(user1 is not null) // verifica se existe o usuario com email acima
            {
                var claimList = (await _userManager.GetClaimsAsync(user1)) // obtem a lista de claim do usuario
                                                   .Select(p => p.Type);

                if (!claimList.Contains("CadastradoEm"))
                {
                    var claimResult1 = await _userManager.AddClaimAsync(user1, 
                                        new Claim("CadastradoEm", "09/15/2019"));
                }
                if (!claimList.Contains("IsAdmin"))
                {
                    var claimResult2 = await _userManager.AddClaimAsync(user1,
                                        new Claim("IsAdmin", "true"));
                }
            }

            IdentityUser user2 = await _userManager.FindByEmailAsync("usuario@localhost");

            if (user2 is not null)
            {
                var claimList = (await _userManager.GetClaimsAsync(user2))
                                                   .Select(p => p.Type);

                if (!claimList.Contains("IsAdmin"))
                {
                    var claimResult = await _userManager.AddClaimAsync(user2,
                                        new Claim("Admin", "false"));
                }
                if (!claimList.Contains("Funcionario"))
                {
                    var claimResult = await _userManager.AddClaimAsync(user2,
                                        new Claim("IsFuncionario", "true"));
                }
            }
        }
        catch (Exception)
        {
            throw;
        }
    }
}
