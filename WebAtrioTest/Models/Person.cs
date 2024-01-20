namespace WebAtrioTest.Models;

/// <summary>
/// Represents a person.
/// </summary>
public class Person : Entity
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public virtual ICollection<Job> Jobs { get; set; } = [];
}
