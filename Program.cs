var researchTeam = new ResearchTeam("topic", "organizationName", 1, TimeFrame.Year, new Paper[] { new Paper("title", new Person("firstName", "lastName", new DateTime(2000, 1, 1)), new DateTime(2000, 1, 1)) });
Console.WriteLine(researchTeam);
Console.WriteLine($"2yrs {researchTeam[TimeFrame.TwoYears]}, year {researchTeam[TimeFrame.Year]}, long {researchTeam[TimeFrame.Long]}");

researchTeam = new ResearchTeam { Topic = "New topic", OrganizationName = "New organizationName", RegistrationNumber = 1, ResearchDuretion = TimeFrame.Year, Publications = new Paper[] { new Paper { PublicationName = "title", Author = new Person { Name = "firstName", LastName = "lastName", BirthDate = new DateTime(2000, 1, 1) }, PublicationDate = new DateTime(2000, 1, 1) } } };
Console.WriteLine(researchTeam);
researchTeam.AddPaper(new Paper { PublicationName = "title", Author = new Person { Name = "firstName", LastName = "lastName", BirthDate = new DateTime(2000, 1, 1) }, PublicationDate = new DateTime(2000, 1, 1) });
Console.WriteLine(researchTeam);
Console.WriteLine(researchTeam.LastPublication);

char[] delimiterChars = { ' ', ',', '\t' };
Console.WriteLine("Enter nRows and nColumns (separated by space or comma): ");
string[] input = Console.ReadLine()?.Split(delimiterChars, System.StringSplitOptions.RemoveEmptyEntries) ?? new string[2] { "3", "2" };
int nRows = int.Parse(input[0]);
int nColumns = int.Parse(input[1]);
//create Person one demential array nRows x nColumns length and fill it with random data 
Person[] people = new Person[nRows * nColumns];
for (int i = 0; i < people.Length; i++)
    people[i] = new Person { Name = "name" + i, LastName = "lastName" + i, BirthDate = new DateTime(2000 + i, 1, 1) };

//create Person two demential array nRows x nColumns length and fill it with random data
Person[,] people2D = new Person[nRows, nColumns];
for (int i = 0; i < nRows; i++)
    for (int j = 0; j < nColumns; j++)
        people2D[i, j] = new Person { Name = "name" + i + j, LastName = "lastName" + i + j, BirthDate = new DateTime(2000 + i + j, 1, 1) };
//create Person jagged array nRows x nColumns length where first row has 1 element, second - 2 and so on, but last row should have nAll - nActual(change), and fill it with random data
int n = (int) Math.Round(Math.Sqrt(2*nRows*nColumns + 1 / 4) - 1 / 2);
Person[][] peopleJagged = new Person[n][];
for (int i = 0; i < n-1; i++)
{
    peopleJagged[i] = new Person[i + 1];
    for (int j = 0; j < i + 1; j++)
        peopleJagged[i][j] = new Person { Name = "name" + i + j, LastName = "lastName" + i + j, BirthDate = new DateTime(2000 + i + j, 1, 1) };
}
int delta = nRows * nColumns - n * (n - 1) / 2;
peopleJagged[n-1] = new Person[delta];
for (int i = 0; i < delta; i++)
    peopleJagged[n-1][i] = new Person { Name = "name" + i + delta, LastName = "lastName" + i + delta, BirthDate = new DateTime(2000 + i + delta, 1, 1) };

// output jagged array
for (int i = 0; i < peopleJagged.Length; i++)
{
    Console.WriteLine($"{i+1}: {string.Join(" ", peopleJagged[i].Cast<object>())}");
}

var getTime = (Action action) =>
{
    var watch = System.Diagnostics.Stopwatch.StartNew();
    action();
    watch.Stop();
    return watch.Elapsed;
};
var time = getTime(() =>
{
    for (int i = 0; i < people.Length; i++)
        people[i].Name = "Andrii";
});
var time2D = getTime(() =>
{
    for (int i = 0; i < nRows; i++)
        for (int j = 0; j < nColumns; j++)
            people2D[i, j].Name = "Andrii";
});
var timeJagged = getTime(() =>
{
    for (int i = 0; i < peopleJagged.Length; i++)
        for (int j = 0; j < peopleJagged[i].Length; j++)
            peopleJagged[i][j].Name = "Andrii";
});

Console.WriteLine($"1D array: {time}ms, 2D array: {time2D}ms, jagged array: {timeJagged}ms");