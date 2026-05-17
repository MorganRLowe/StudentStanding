namespace StudentStanding.Models;

//one recently graded assignment. each field comes from a double-colon separated entry in the csv
public class RecentlyGradedItem
{
    public string Subject { get; set; } = "";
    public string Type    { get; set; } = "";
    public string Title   { get; set; } = "";
    public DateOnly Graded { get; set; }
    public int Score      { get; set; }
}
