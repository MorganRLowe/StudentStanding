namespace StudentStanding.Models;

//bundle the controller hands to the Home Index view
//keeps the view's @model declaration to a single type
public class HomeViewModel
{
    public Student Student { get; set; } = new Student();
    public List<UpcomingItem> Upcoming { get; set; } = new List<UpcomingItem>();
    public List<MissingItem> Missing { get; set; } = new List<MissingItem>();
    public List<RecentlyGradedItem> RecentlyGraded { get; set; } = new List<RecentlyGradedItem>();

    //rotating greeting set by the controller
    public string Greeting { get; set; } = "";

    //percent of assignments turned in. shown in the snapshot tile row
    public int SubmissionPct { get; set; }

    //mean of the per-class grade percentages. shown in the snapshot tile row
    public int AverageGrade { get; set; }
}
