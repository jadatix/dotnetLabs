Console.WriteLine("Cтворити 2 об'єкти класу Team, з однаковими даними та перевірити що посилання на об'єкти різні, а об'єкти рівні, вивести хеш коди");
Team team1 = new Team("organizationName", 1);
Team team2 = new Team("organizationName", 1);
Console.WriteLine(team1 == team2);
Console.WriteLine(team1.Equals(team2));
Console.WriteLine(team1.GetHashCode());
Console.WriteLine(team2.GetHashCode());
Console.WriteLine("\n\nВ блоці try/catch присвоїти властивості з номером реєстрації некоректне значення, в обробнику вивести повідомлення про помилку");
try
{
    new Team("organizationName", -1);
}
catch (ArgumentException e)
{
    Console.WriteLine(e.Message);
}
Console.WriteLine("\n\nCтворити об'єкт типу ResearchTeam, додати елементи в список публікацій та список учасників проекту та вивести дані об’єкту ResearchTeam;");
ResearchTeam researchTeam = new ResearchTeam("name", "topic", "organizationName", 1, TimeFrame.Year, new List<Paper>(), new List<Person>());
researchTeam.AddPaper(new Paper("title", new Person("differentFirstName", "lastName", new DateTime(2000, 1, 1)), new DateTime(2022, 1, 1)));
researchTeam.Members.Add(new Person("firstName", "lastName", new DateTime(2000, 1, 1)));
Console.WriteLine(researchTeam);
Console.WriteLine("\n\nВивести значення властивості Team для об’єкту ResearchTeam;");
Console.WriteLine(researchTeam.Team);
Console.WriteLine("\n\nЗа допомогою метода DeepCopy() створити повну копію об’єкта ResearchTeam. Змінити дані у вихідному об’єкті ResearchTeam та вивести копію та оригінал, копія повинна залишитися без змін.");
ResearchTeam copiedResearchTeam = (ResearchTeam)researchTeam.DeepCopy();
copiedResearchTeam.OrganizationName = "CHANGED";
Console.WriteLine(researchTeam);
Console.WriteLine(copiedResearchTeam);
Console.WriteLine("\n\nЗа допомогою оператора foreach для ітератора, визначеного в класі ResearchTeam, вивести список учасників проекту, які не мають публікацій.");
foreach (Person member in researchTeam.GetNonPublishingMembers())
    Console.WriteLine(member);
Console.WriteLine("\n\nЗа допомогою оператора foreach для ітератора з параметром, визначеного в класі ResearchTeam, вивести список всіх публікацій за останні 2 роки.");
foreach (Paper publication in researchTeam.GetRecentPublications(2))
    Console.WriteLine(publication);