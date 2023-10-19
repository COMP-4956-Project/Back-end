using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MVC_Backend_Frontend.Models;

namespace MVC_Backend_Frontend.Data;

public class ApplicationDbContext : IdentityDbContext<CustomUser, CustomRole, string> {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }
}
