using WebAtrioTest.Models;

namespace WebAtrioTest.Repositories.Interfaces;

public interface IPersonRepository
{
    Task<IEnumerable<Person>> GetAllAsync();
    Task<IEnumerable<Person>> GetByOrganisationAsync(string organisation);
    Task<Person> AddAsync(Person person);
}
