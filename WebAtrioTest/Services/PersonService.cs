using WebAtrioTest.Models;
using WebAtrioTest.Repositories.Interfaces;
using WebAtrioTest.Services.Interfaces;
using WebAtrioTest.ViewModels;

namespace WebAtrioTest.Services;

public class PersonService(IPersonRepository personRepository) : IPersonService
{
    private readonly IPersonRepository _personRepository = personRepository;
    public async Task<Person> AddAsync(Person person)
    {
        return await _personRepository.AddAsync(person);

    }

    public async Task<IEnumerable<PersonResultViewModel>> GetAllAsync()
    {
        var persons = await _personRepository.GetAllAsync();

        return persons.Select(person => (PersonResultViewModel)person);
    }

    public async Task<IEnumerable<PersonResultViewModel>> GetByOrganisationAsync(string organisation)
    {
         var persons = await _personRepository.GetByOrganisationAsync(organisation);
        return persons.Select(person => (PersonResultViewModel)person);

    }
}
