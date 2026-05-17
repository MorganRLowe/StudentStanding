using System.Globalization;
using Microsoft.AspNetCore.Mvc;
using StudentStanding.Models;
using StudentStanding.Services;

namespace StudentStanding.Controllers;

//handles the single dashboard route. a GET to / hits Index
public class HomeController : Controller
{
    private const string CurrentStudentId = "S10-0428";

    //pool of friendly greeting templates. the placeholder is replaced with the student's first name
    //rotating these keeps the page from feeling like a robot every time maya visits
    private static readonly string[] GreetingTemplates =
    {
        "Hey {0}, here's where you stand.",
        "Welcome back, {0}.",
        "Glad you checked in, {0}.",
        "Looking sharp, {0}.",
        "Let's see how today's shaping up, {0}."
    };

    private readonly StudentDataService _data;

    public HomeController(StudentDataService data)
    {
        _data = data;
    }

    public IActionResult Index()
    {
        var student = _data.Get(CurrentStudentId);

        //pick a random greeting and drop the student's first name into it
        var template = GreetingTemplates[Random.Shared.Next(GreetingTemplates.Length)];
        var greeting = template.Replace("{0}", student.FirstName);

        //submission rate as an int percent. guards against divide-by-zero for a brand-new student
        var submissionPct = student.AssignmentsTotal > 0
            ? (int)Math.Round(student.AssignmentsSubmitted * 100.0 / student.AssignmentsTotal)
            : 100;

        //mean of the per-class grade percentages, skipping any class with no value yet
        var classGrades = new[]
        {
            student.EnglishPct, student.AlgebraPct, student.ChemistryPct,
            student.HistoryPct, student.SpanishPct, student.PePct
        }.Where(g => g.HasValue).Select(g => g!.Value).ToList();

        var averageGrade = classGrades.Count > 0
            ? (int)Math.Round(classGrades.Average())
            : 0;

        var model = new HomeViewModel
        {
            Student = student,
            Upcoming = ParseUpcoming(student.UpcomingAssignmentsRaw),
            Missing = ParseMissing(student.MissingAssignmentList),
            RecentlyGraded = ParseRecentlyGraded(student.RecentlyGradedRaw),
            Greeting = greeting,
            SubmissionPct = submissionPct,
            AverageGrade = averageGrade
        };

        return View(model);
    }

    //split the pipe-delimited string. each entry has five fields separated by double colons
    //skip malformed rows instead of throwing
    private List<RecentlyGradedItem> ParseRecentlyGraded(string raw)
    {
        var items = new List<RecentlyGradedItem>();
        if (string.IsNullOrWhiteSpace(raw)) return items;

        foreach (var entry in raw.Split('|', StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = entry.Split("::");
            if (parts.Length < 5) continue;
            if (!DateOnly.TryParseExact(parts[3], "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out var graded))
                continue;
            if (!int.TryParse(parts[4], out var score))
                continue;

            items.Add(new RecentlyGradedItem
            {
                Subject = parts[0],
                Type    = parts[1],
                Title   = parts[2],
                Graded  = graded,
                Score   = score
            });
        }
        return items;
    }

    //split the pipe-delimited string. each entry has four fields separated by double colons
    //skip malformed rows instead of throwing
    private List<UpcomingItem> ParseUpcoming(string raw)
    {
        var items = new List<UpcomingItem>();
        if (string.IsNullOrWhiteSpace(raw)) return items;

        foreach (var entry in raw.Split('|', StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = entry.Split("::");
            if (parts.Length < 4) continue;
            if (!DateOnly.TryParseExact(parts[3], "yyyy-MM-dd", CultureInfo.InvariantCulture,
                                        DateTimeStyles.None, out var due))
                continue;

            items.Add(new UpcomingItem
            {
                Subject = parts[0],
                Type    = parts[1],
                Title   = parts[2],
                Due     = due
            });
        }
        return items;
    }

    //split the pipe-delimited string. each entry has a subject prefix and a title separated by a hyphen
    //entries without a hyphen fall back to an empty subject with the whole string as the title
    private List<MissingItem> ParseMissing(string raw)
    {
        var items = new List<MissingItem>();
        if (string.IsNullOrWhiteSpace(raw)) return items;

        foreach (var entry in raw.Split('|', StringSplitOptions.RemoveEmptyEntries))
        {
            var parts = entry.Split('-', 2);
            if (parts.Length == 2)
                items.Add(new MissingItem { Subject = parts[0], Title = parts[1] });
            else
                items.Add(new MissingItem { Subject = "", Title = entry });
        }
        return items;
    }
}
