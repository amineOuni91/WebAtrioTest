using WebAtrioTest.Models;

namespace WebAtrioTest.ViewModels;

public class PersonResultViewModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime BirthDate { get; set; }
    public List<JobResultViewModel> Jobs { get; set; } = [];

    public static explicit operator PersonResultViewModel(Person person)
    {
        return new PersonResultViewModel
        {
            Id = person.Id,
            FirstName = person.FirstName,
            LastName = person.LastName,
            BirthDate = person.BirthDate,
            Jobs = person.Jobs.Select(job => (JobResultViewModel)job).ToList()
        };
    }
}
