using WebAtrioTest.Models;
using WebAtrioTest.Repositories.Interfaces;
using WebAtrioTest.Services.Interfaces;

namespace WebAtrioTest.Services;

public class JobService(IJobRepository jobRepository) : IJobService
{
    private readonly IJobRepository _jobRepository = jobRepository;
    public async Task<Job> AddAsync(Job job)
    {
        return await _jobRepository.AddAsync(job);
    }

    public async Task<IEnumerable<Job>> GetPersonJobsByDateAsync(Guid personId, DateTime startDate, DateTime endDate)
    {
        return await _jobRepository.GetPersonJobsByDateAsync(personId, startDate, endDate);
    }
}
