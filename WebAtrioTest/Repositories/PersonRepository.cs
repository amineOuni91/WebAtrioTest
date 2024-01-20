using Microsoft.EntityFrameworkCore;
using WebAtrioTest.Infrastructure;
using WebAtrioTest.Models;
using WebAtrioTest.Repositories.Interfaces;

namespace WebAtrioTest.Repositories;

public class PersonRepository(PersonContext context) : IPersonRepository
{
    private readonly PersonContext _context = context;
    public async Task<Person> AddAsync(Person person)
    {
        var entity = await _context.Persons.AddAsync(person);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<IEnumerable<Person>> GetAllAsync()
    {
        return await _context.Persons.Include(p => p.Jobs)
            .OrderBy(p => p.LastName).ToListAsync();
    }

    public async Task<IEnumerable<Person>> GetByOrganisationAsync(string organisation)
    {
        return await _context.Persons.Include(p => p.Jobs)
            .Where(p => p.Jobs.Any(j => j.Organisation == organisation))
            .ToListAsync();
    }
}
