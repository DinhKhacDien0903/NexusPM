namespace NexusPM.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
}

public class ApplicationRole : IdentityRole<Guid>
{
}
