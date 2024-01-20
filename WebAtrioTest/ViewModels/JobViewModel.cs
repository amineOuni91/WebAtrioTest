namespace WebAtrioTest.ViewModels;

public record JobViewModel(Guid PersonId,string Organisation, string Position, DateTime StartDate, DateTime? EndDate)
{
}
