
using System.Globalization;
using System.Text.RegularExpressions;
using ExifLibrary;
using Flurl;
using Flurl.Http;
using Spectre.Console;

var username = AnsiConsole.Prompt(
    new TextPrompt<string>("Enter your Kindervibe [bold]username[/] (probably your email):")
        .PromptStyle("green")
        .ValidationErrorMessage("[red]Username is too short[/]")
        .Validate(input =>
        {
            if (input.Trim().Length > 3)
                return ValidationResult.Success();

            return ValidationResult.Error();
        }));

var password = AnsiConsole.Prompt(
    new TextPrompt<string>("Enter your Kindervibe [bold]password[/]")
        .PromptStyle("red")
        .Secret());

var token = await GetToken(username, password);

var children = await GetChildren(token);

var childSelected = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .Title("For which child do you want to create a photo backup?")
        .PageSize(10)
        .MoreChoicesText("[grey](Move up and down to reveal more children)[/]")
        .AddChoices(children.Results.Select(c => $"{c.Id} - {c.FirstName.Trim()} {c.LastName?.Trim()}")));

var childId = int.Parse(childSelected.Split(" - ")[0]);
var child = children.Results.Single(c => c.Id == childId);

AnsiConsole.Write(new Markup($"Child selected: [yellow]{childSelected}[/]"));
AnsiConsole.WriteLine();

var dateFrom = DateTime.Now.Date.AddDays(-1);
AnsiConsole.Prompt(
    new TextPrompt<string>($"[grey][[Optional]][/] Enter the starting date from which to get photos (eg: {dateFrom:yyyy-MM-dd}. Press [grey][[enter]][/] to use the date in the example):")
        .PromptStyle("green")
        .Validate(input =>
        {
            if (string.IsNullOrWhiteSpace(input))
                return ValidationResult.Success();

            if (!DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateFrom))
                return ValidationResult.Error("Enter date in yyyy-MM-dd format, e.g. 2022-12-31");

            return ValidationResult.Success();
        })
        .AllowEmpty());

var dateTo = DateTime.Now.Date.AddDays(-1);
AnsiConsole.Prompt(
    new TextPrompt<string>($"[grey][[Optional]][/] Enter the end date until which to get photos (eg: {dateTo:yyyy-MM-dd}). Press [grey][[enter]][/] to use the date in the example):")
        .PromptStyle("green")
        .Validate(input =>
        {
            if (string.IsNullOrWhiteSpace(input))
                return ValidationResult.Success();

            if (!DateTime.TryParseExact(input, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out dateTo))
                return ValidationResult.Error("Enter date in yyyy-MM-dd format, e.g. 2022-12-31");

            return ValidationResult.Success();
        })
        .AllowEmpty());

var currentDate = dateFrom;

AnsiConsole.Write(new Markup($"Getting photos from [bold]{dateFrom:yyyy-MM-dd}[/] to [bold]{dateTo:yyyy-MM-dd}[/]"));
AnsiConsole.WriteLine();
while (currentDate <= dateTo)
{
    AnsiConsole.Write(new Markup($"Getting photos for date [bold]{currentDate:yyyy-MM-dd}[/]"));
    AnsiConsole.WriteLine();

    await GetPhotos(child.Id, token, currentDate, child.FirstName.Trim());
    currentDate = currentDate.AddDays(1);
}

AnsiConsole.Write(new Markup($"Backup finished for child [bold]{child.FirstName.Trim()}[/]"));
AnsiConsole.WriteLine();
AnsiConsole.Write(new Markup("Press [bold grey][[any key]][/] to close"));
Console.ReadKey();

async Task GetPhotos(int childId, string token, DateTime currentDate, string childName)
{
    var resp = await "https://www.kindervibe.com/api/photos/child/"
        .SetQueryParam("child_pk", childId)
        .SetQueryParam("date_from", currentDate.ToString("yyyy-MM-dd"))
        .SetQueryParam("date_to", currentDate.ToString("yyyy-MM-dd"))
        .WithHeader("authorization", $"Token {token}")
        .GetJsonAsync<PhotosResultRoot>();

    resp.results.Reverse();

    var timeStamp = currentDate.Date.AddHours(14);

    if (resp.results.Count > 0)
    {
        AnsiConsole.Write(new Markup($"Found [bold]{resp.results.Count}[/] photos for date [bold]{currentDate:yyyy-MM-dd}[/]"));
        AnsiConsole.WriteLine();
    }

    var currentDir = Directory.GetCurrentDirectory();
    var dir = Path.Combine(currentDir, "Kindervibe");
    if (!Directory.Exists(dir))
        Directory.CreateDirectory(dir);

    foreach (var media in resp.results)
    {
        var bytes = await media.photo.GetBytesAsync();
        var ext = Regex.Match(media.photo, @"\.(?<ext>\w+)$").Groups["ext"].Value;
        var filename = $"{timeStamp:yyyy-MM-dd HHmm} {childName}.{ext}";

        AnsiConsole.Write(new Markup($"Downloading photo [bold]{filename}[/]"));
        AnsiConsole.WriteLine();

        var filePath = Path.Combine(dir, filename);
        File.WriteAllBytes(filePath, bytes);

        if (media.type == "image")
        {
            var file = ImageFile.FromFile(filePath);
            file.Properties.Set(ExifTag.DateTimeOriginal, timeStamp);
            file.Save(filePath);
        }

        timeStamp = timeStamp.AddMinutes(1);
    }
}

async Task<ChildrenResponse> GetChildren(string token)
{
    var resp = await "https://www.kindervibe.com/api/child/list/"
        .WithHeader("authorization", $"Token {token}")
        .GetJsonAsync<ChildrenResponse>();

    return resp;
}

async Task<string> GetToken(string username, string password)
{
    var resp = await "https://www.kindervibe.com/api/account/token/"
        .PostJsonAsync(new LoginRequest
        {
            Email = username,
            Password = password,
            AppType = 2,
            Platform = 2,
            Version = "2.0.8",
            Pntold = "c3tgz_kjXsk:APA91bHNfMQ2lFifwYnoTYgcO63lnJxAZTE2XFD8jleJGj-u76rGYQcmPu86tkypINy6HSBbeOiZup5SGHe4g4QVO_9HUx7zB7B5c-0TGcGFRxOOgDOc2chMw1nofSsBpz1fA58p2-FC",
            Pntnew = "c3tgz_kjXsk:APA91bHNfMQ2lFifwYnoTYgcO63lnJxAZTE2XFD8jleJGj-u76rGYQcmPu86tkypINy6HSBbeOiZup5SGHe4g4QVO_9HUx7zB7B5c-0TGcGFRxOOgDOc2chMw1nofSsBpz1fA58p2-FC"
        })
        .ReceiveJson<LoginResponse>();

    return resp.Token;
}
