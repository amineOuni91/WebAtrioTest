using WebAtrioTest.Models;

namespace WebAtrioTest.Repositories.Interfaces;

public interface IJobRepository
{
    Task<Job> AddAsync(Job job);
    Task<IEnumerable<Job>> GetPersonJobsByDateAsync(Guid personId, DateTime startDate, DateTime endDate);

}
