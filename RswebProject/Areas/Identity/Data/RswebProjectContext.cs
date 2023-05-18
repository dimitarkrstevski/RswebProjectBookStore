using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RswebProject.Areas.Identity.Data;
using RswebProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RswebProject.Data;

public class RswebProjectContext : IdentityDbContext<RswebProjectUser>
{
    public RswebProjectContext(DbContextOptions<RswebProjectContext> options)
        : base(options)
    {
    }
    public DbSet<RswebProject.Models.Book>? Book { get; set; }

    public DbSet<RswebProject.Models.Author>? Author { get; set; }

    public DbSet<RswebProject.Models.PHouse>? PHouse { get; set; }

    public DbSet<RswebProject.Models.BookPHouse> BookPHouses { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }


}
