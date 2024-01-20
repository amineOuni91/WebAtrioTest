namespace WebAtrioTest.Models;

public class Job : Entity
{
    public string Organisation { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public Guid PersonId { get; set; }
    public virtual Person Person { get; set; }
}
