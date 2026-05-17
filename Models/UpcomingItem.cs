namespace StudentStanding.Models;

//one upcoming assignment. each field comes from a double-colon separated entry in the csv
public class UpcomingItem
{
    public string Subject { get; set; } = "";
    public string Type    { get; set; } = "";
    public string Title   { get; set; } = "";
    public DateOnly Due   { get; set; }
}
