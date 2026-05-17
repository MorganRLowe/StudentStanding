using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using StudentStanding.Models;

namespace StudentStanding.Services;

//registered as a singleton in Program.cs
//the csv loads once at app start and every request shares the same in-memory dictionary
//for read-only data this is cheaper than re-parsing on every page load
public class StudentDataService
{
    private const string CsvPath = "Data/students.csv";
    private readonly Dictionary<string, Student> _byId;

    public StudentDataService()
    {
        //the csv has about ninety columns and we only map about thirty on the Student class
        //these two settings tell csvhelper to ignore columns we did not map instead of throwing
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            MissingFieldFound = null,
            HeaderValidated   = null
        };

        using var reader = new StreamReader(CsvPath);
        using var csv    = new CsvReader(reader, config);

        _byId = new Dictionary<string, Student>();
        foreach (var s in csv.GetRecords<Student>())
        {
            _byId[s.StudentId] = s;
        }
    }

    //single-student demo. the controller always passes maya's hardcoded id
    //a multi-student build would return Student? and use TryGetValue to handle a bad id
    public Student Get(string id) => _byId[id];
}
