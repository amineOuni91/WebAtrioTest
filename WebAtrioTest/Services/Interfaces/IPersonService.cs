using WebAtrioTest.Models;
using WebAtrioTest.ViewModels;

namespace WebAtrioTest.Services.Interfaces;

public interface IPersonService
{
    Task<Person> AddAsync(Person person);
    Task<IEnumerable<PersonResultViewModel>> GetAllAsync();
    Task<IEnumerable<PersonResultViewModel>> GetByOrganisationAsync(string organisation);
}
