using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace MvcWebSchool_Identity.Policies
{
    public class TempoCadastroHandler : AuthorizationHandler<TempoCadastroRequirement>
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, TempoCadastroRequirement requirement)
        {
            if (context.User.HasClaim(c => c.Type == "CadastradoEm"))
        {
            var data = context.User.FindFirst(c => c.Type == "CadastradoEm").Value;

            var dataCadastro = DateTime.Parse(data);

            double tempoCadastro = await Task.Run(() =>
                            (DateTime.Now.Date - dataCadastro.Date).TotalDays);

            var tempoEmAnos = tempoCadastro / 360;

            if (tempoEmAnos >= requirement.TempoCadastroMinimo)
            {
                context.Succeed(requirement);
            }
            return;
        }
        }
    }
}
