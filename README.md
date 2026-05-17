# Student Standing View

Academic check-in dashboard for one student (Maya Patel, S10-0428).
Built as a take-home assessment.

## Stack

- ASP.NET Core MVC on .NET 10
- Bootstrap 5 (CSS only, no JS bundle)
- Bootstrap Icons (via CDN)
- CsvHelper for the data load
- No database. All data lives in `Data/students.csv` and loads into memory at startup.

## How to run

Requires the .NET 10 SDK.

    dotnet run

The terminal prints a URL (usually `https://localhost:5001`).
Open it in a browser. The root path renders the dashboard.

## Project layout

    Controllers/   HomeController.Index builds the view model
    Models/        Student (CSV map), HomeViewModel, three small POCOs
    Services/      StudentDataService, singleton that loads the CSV once
    Views/Home/    Index.cshtml, the dashboard
    Views/Shared/  _Layout.cshtml, navbar shell
    wwwroot/       Bootstrap, custom app.css, the school logo
    Data/          students.csv

## What's intentionally NOT here

- No student switcher (single-user page per the spec)
- No auth (out of scope)
- No tests, CI, or deployment configuration (out of scope)
- No mobile-first work (desktop is the target)
