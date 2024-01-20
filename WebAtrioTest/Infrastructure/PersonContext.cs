using Microsoft.EntityFrameworkCore;
using WebAtrioTest.Models;

namespace WebAtrioTest.Infrastructure;

public class PersonContext(DbContextOptions<PersonContext> options) : DbContext(options)
{
    public DbSet<Job> Jobs { get; set; }
    public DbSet<Person> Persons { get; set; }
}
