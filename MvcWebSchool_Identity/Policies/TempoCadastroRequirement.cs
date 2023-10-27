using Microsoft.AspNetCore.Authorization;

namespace MvcWebSchool_Identity.Policies
{
    public class TempoCadastroRequirement : IAuthorizationRequirement
    {
        public int TempoCadastroMinimo { get; set; }

        public TempoCadastroRequirement(int tempoCadastroMinimo)
        {
            TempoCadastroMinimo = tempoCadastroMinimo;
        }
    }

}
