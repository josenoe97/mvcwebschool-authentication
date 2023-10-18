using Microsoft.AspNetCore.Identity;

namespace MvcWebSchool_Identity.Services
{
    public class SeedUserRoleInitial : ISeedUserRoleInitial
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public SeedUserRoleInitial(UserManager<IdentityUser> userManager,
                                   RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        
        public async Task SeedRolesAsync()
        {
            //Teste para verificar se aquele "User" ja não exite 
            //Caso não exista uma igual é criado a role User
            if (! await _roleManager.RoleExistsAsync("User"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "User";
                role.NormalizedName = "USER";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }

            //Teste para verificar se aquele "Admin" ja não exite 
            if (! await _roleManager.RoleExistsAsync("Admin"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Admin";
                role.NormalizedName = "ADMIN";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }

            //Teste para verificar se aquele "Gerente" ja não exite 
            if (!await _roleManager.RoleExistsAsync("Gerente"))
            {
                IdentityRole role = new IdentityRole();
                role.Name = "Gerente";
                role.NormalizedName = "GERENTE";
                role.ConcurrencyStamp = Guid.NewGuid().ToString();

                IdentityResult roleResult = await _roleManager.CreateAsync(role);
            }
        }

        public async Task SeedUsersAsync()
        {
            //Pesquisa se o email descrito é null, caso seja é por que não existe um email igual
            if(await _userManager.FindByEmailAsync("usuario@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "usuario@localhost";
                user.Email = "usuario@localhost";
                user.NormalizedUserName = "USUARIO@LOCALHOST";
                user.NormalizedEmail = "USUARIO@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString(); // Identificador unico de 128bits unico em todo mundo

                //Criação                                                          //Password
                IdentityResult userResult = await _userManager.CreateAsync(user, "Numsey#2023");

                //Verificar se o usuario foi criado com sucesso 
                //Atribui o usuario criado a role User
                if (userResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "User");
                }
            }

            if (await _userManager.FindByEmailAsync("admin@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@localhost";
                user.Email = "admin@localhost";
                user.NormalizedUserName = "ADMIN@LOCALHOST";
                user.NormalizedEmail = "ADMIN@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString(); // Identificador unico de 128bits unico em todo mundo

                //Criação                                                          //Password
                IdentityResult userResult = await _userManager.CreateAsync(user, "Numsey#2023");

                //Verificar se o usuario foi criado com sucesso 
                //Atribui o usuario criado a role User
                if (userResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }

            }

            if (await _userManager.FindByEmailAsync("gerente@localhost") == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "gerente@localhost";
                user.Email = "gerente@localhost";
                user.NormalizedUserName = "GERENTE@LOCALHOST";
                user.NormalizedEmail = "GERENTE@LOCALHOST";
                user.EmailConfirmed = true;
                user.LockoutEnabled = false;
                user.SecurityStamp = Guid.NewGuid().ToString(); // Identificador unico de 128bits unico em todo mundo

                //Criação                                                          //Password
                IdentityResult userResult = await _userManager.CreateAsync(user, "Numsey#2023");

                //Verificar se o usuario foi criado com sucesso 
                //Atribui o usuario criado a role User
                if (userResult.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Gerente");
                }

            }
        }
    }
}
