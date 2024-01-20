using WebAtrioTest.Models;

namespace WebAtrioTest.ViewModels;

public class JobResultViewModel
{
    public string Organisation { get; set; }
    public string Position { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }

    public static explicit operator JobResultViewModel(Job job)
    {
        return new JobResultViewModel
        {
            Organisation = job.Organisation,
            Position = job.Position,
            StartDate = job.StartDate,
            EndDate = job.EndDate ?? null
        };
    }
}
