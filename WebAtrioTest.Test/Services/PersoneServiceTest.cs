using FluentAssertions;
using NSubstitute;
using WebAtrioTest.Models;
using WebAtrioTest.Repositories.Interfaces;
using WebAtrioTest.Services;
using WebAtrioTest.Services.Interfaces;
using WebAtrioTest.ViewModels;

namespace WebAtrioTest.Test.Services
{
    [TestFixture]
    public class PersonServiceTest
    {
        private IPersonRepository _personRepository;
        private IPersonService _personService;

        [SetUp]
        public void Setup()
        {
            _personRepository = Substitute.For<IPersonRepository>();
            _personService = new PersonService(_personRepository);
        }

        [Test]
        public async Task AddAsync_ShouldCallPersonRepositoryAddAsync()
        {
            // Arrange
            var person = new Person();

            // Act
            await _personService.AddAsync(person);

            // Assert
            await _personRepository.Received(1).AddAsync(person);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnAllPersons()
        {
            // Arrange
            var persons = new List<Person>
            {
                new Person { Id = Guid.NewGuid(), FirstName = "John" },
                new Person { Id = Guid.NewGuid(), FirstName = "Jane" }
            };
            _personRepository.GetAllAsync().Returns(persons);

            // Act
            var result = await _personService.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(persons.Select(p => (PersonResultViewModel)p));
        }

        [Test]
        public async Task GetByOrganisationAsync_ShouldReturnPersonsByOrganisation()
        {
            // Arrange
            var organisation = "A Corp";
            var persons = new List<Person>
            {
                new Person { Id = Guid.NewGuid(), FirstName = "John", Jobs = new List<Job>{ new() { Organisation = "A Corp" } } },
                new Person { Id = Guid.NewGuid(), FirstName = "Jane", Jobs = new List<Job>{ new() { Organisation = "A Corp" } } },
                new Person { Id = Guid.NewGuid(), FirstName = "Bob", Jobs = new List<Job>{ new() { Organisation = "XYZ Corp" } } }
            };
            _personRepository.GetByOrganisationAsync(organisation).Returns(persons.Where(p => p.Jobs.Any(j => j.Organisation == organisation)));

            // Act
            var result = await _personService.GetByOrganisationAsync(organisation);

            // Assert
            result.Should().BeEquivalentTo(persons.Where(p => p.Jobs.Any(j => j.Organisation == organisation)).Select(p => (PersonResultViewModel)p));
        }
    }
}
