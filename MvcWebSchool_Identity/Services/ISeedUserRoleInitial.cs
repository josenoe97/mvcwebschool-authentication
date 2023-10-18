namespace MvcWebSchool_Identity.Services
{
    public interface ISeedUserRoleInitial
    {
        Task SeedRolesAsync();

        Task SeedUsersAsync();
    }
}
