using CsvHelper.Configuration.Attributes;

namespace StudentStanding.Models;

//one student row from the csv
//the Name attribute on each property tells csvhelper which csv column feeds it
//we only map the columns the dashboard uses. csvhelper ignores the rest
public class Student
{
    [Name("student_id")]      public string StudentId  { get; set; } = "";
    [Name("FirstName")]       public string FirstName  { get; set; } = "";
    [Name("grade_level")]     public int    GradeLevel { get; set; }

    //per-class running grade percent. nullable because the csv allows missing values
    [Name("english_10_grade_pct")]    public int? EnglishPct   { get; set; }
    [Name("algebra_2_grade_pct")]     public int? AlgebraPct   { get; set; }
    [Name("chemistry_grade_pct")]     public int? ChemistryPct { get; set; }
    [Name("world_history_grade_pct")] public int? HistoryPct   { get; set; }
    [Name("spanish_2_grade_pct")]     public int? SpanishPct   { get; set; }
    [Name("pe_health_grade_pct")]     public int? PePct        { get; set; }

    //per-class last test score and the class average on that test
    [Name("english_10_last_test_score")]        public int? EnglishLastTest      { get; set; }
    [Name("english_10_last_test_class_avg")]    public int? EnglishLastTestAvg   { get; set; }
    [Name("algebra_2_last_test_score")]         public int? AlgebraLastTest      { get; set; }
    [Name("algebra_2_last_test_class_avg")]     public int? AlgebraLastTestAvg   { get; set; }
    [Name("chemistry_last_test_score")]         public int? ChemistryLastTest    { get; set; }
    [Name("chemistry_last_test_class_avg")]     public int? ChemistryLastTestAvg { get; set; }
    [Name("world_history_last_test_score")]     public int? HistoryLastTest      { get; set; }
    [Name("world_history_last_test_class_avg")] public int? HistoryLastTestAvg   { get; set; }
    [Name("spanish_2_last_test_score")]         public int? SpanishLastTest      { get; set; }
    [Name("spanish_2_last_test_class_avg")]     public int? SpanishLastTestAvg   { get; set; }
    [Name("pe_health_last_test_score")]         public int? PeLastTest           { get; set; }
    [Name("pe_health_last_test_class_avg")]     public int? PeLastTestAvg        { get; set; }

    //assignment counts. the controller divides submitted by total to get the percent shown in the snapshot
    [Name("assignments_total")]     public int AssignmentsTotal     { get; set; }
    [Name("assignments_submitted")] public int AssignmentsSubmitted { get; set; }

    //school attendance percentage. shown in the snapshot tile row
    [Name("attendance_pct")] public decimal AttendancePct { get; set; }

    //three pipe-delimited string columns. the controller splits each into a typed list
    [Name("upcoming_assignments")]         public string UpcomingAssignmentsRaw   { get; set; } = "";
    [Name("missing_assignment_list")]      public string MissingAssignmentList    { get; set; } = "";
    [Name("recently_graded_assignments")]  public string RecentlyGradedRaw        { get; set; } = "";

    [Name("lunch_balance_cents")] public int LunchBalanceCents { get; set; }

    //the student's self-reported goal. shown at the top of the page to ground it
    [Name("self_reported_goal")] public string SelfReportedGoal { get; set; } = "";
}
