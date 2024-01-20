using Microsoft.EntityFrameworkCore;
using WebAtrioTest.Infrastructure;
using WebAtrioTest.Models;
using WebAtrioTest.Repositories.Interfaces;

namespace WebAtrioTest.Repositories;

public class JobRepository(PersonContext context) : IJobRepository
{
    private readonly PersonContext _context = context;
    public async Task<Job> AddAsync(Job job)
    {
        var entity = await _context.Jobs.AddAsync(job);
        await _context.SaveChangesAsync();
        return entity.Entity;
    }

    public async Task<IEnumerable<Job>> GetPersonJobsByDateAsync(Guid personId, DateTime startDate, DateTime endDate)
    {
        return await _context.Jobs.Where(j => j.PersonId == personId && j.StartDate >= startDate && (j.EndDate <= endDate || j.EndDate == null ))
            .ToListAsync();
    }
}
