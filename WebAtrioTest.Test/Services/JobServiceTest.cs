using FluentAssertions;
using NSubstitute;
using WebAtrioTest.Models;
using WebAtrioTest.Repositories.Interfaces;
using WebAtrioTest.Services;
using WebAtrioTest.Services.Interfaces;

namespace WebAtrioTest.Test.Services
{
    [TestFixture]
    public class JobServiceTest
    {
        private IJobRepository _jobRepository;
        private IJobService _jobService;

        [SetUp]
        public void Setup()
        {
            _jobRepository = Substitute.For<IJobRepository>();
            _jobService = new JobService(_jobRepository);
        }

        [Test]
        public async Task AddAsync_ShouldCallJobRepositoryAddAsync()
        {
            // Arrange
            var job = new Job();

            // Act
            await _jobService.AddAsync(job);

            // Assert
            await _jobRepository.Received(1).AddAsync(job);
        }

        [Test]
        public async Task GetPersonJobsByDateAsync_ShouldCallJobRepositoryGetPersonJobsByDateAsync()
        {
            // Arrange
            var personId = Guid.NewGuid();
            var startDate = DateTime.Now;
            var endDate = DateTime.Now.AddDays(7);
            var expectedJobs = new List<Job>();

            _jobRepository.GetPersonJobsByDateAsync(personId, startDate, endDate)
                .Returns(expectedJobs);

            // Act
            var result = await _jobService.GetPersonJobsByDateAsync(personId, startDate, endDate);

            // Assert
            await _jobRepository.Received(1).GetPersonJobsByDateAsync(personId, startDate, endDate);
            result.Should().BeEquivalentTo(expectedJobs);
        }
    }
}
