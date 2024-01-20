using WebAtrioTest.Models;

namespace WebAtrioTest.Services.Interfaces;

public interface IJobService
{
    Task<Job> AddAsync(Job job);
    Task<IEnumerable<Job>> GetPersonJobsByDateAsync(Guid personId, DateTime startDate, DateTime endDate);
}
