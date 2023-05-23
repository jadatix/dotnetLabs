public class Game
{
    private Human[] _males;
    private Human[] _females;

    public Game()
    {
        _males = new Human[]
        {
                new Student(),
                new Botan(),
        };
        _females = new Human[]
        {
                new Girl(),
                new PrettyGirl(),
                new SmartGirl(),
        };
    }

    public void Run()
    {
        PrintInstructions();

        var gen = new Random();
        while (true)
        {
            Console.Clear();
            if (DateTime.Now.DayOfWeek == DayOfWeek.Sunday)
            {
                Console.WriteLine("App doen't work on Sundays.");
                break;
            }

            Console.WriteLine("Enter boy's name: ");
            var maleName = Console.ReadLine();

            Console.WriteLine("Enter girl's name: ");
            var femaleName = Console.ReadLine();

            var male = _males[gen.Next(_males.Length)];
            male.Name = maleName;

            var female = _females[gen.Next(_females.Length)];
            female.Name = femaleName;

            try
            {
                IHasName result = Couple(male, female);
                ConsoleColor initialColor = Console.ForegroundColor;
                if (result == null)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine($"Unfotunately {male.Name} and {female.Name} don't like each other");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("We've got a match!");
                    Console.WriteLine($"{male.Name} and {female.Name} liked each other!");
                    Console.WriteLine($"In a relashionship they've got {GetIHasNameType(result)}");
                    Console.WriteLine($"They named him/her {result.Name}");
                }
                Console.ForegroundColor = initialColor;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }



            Console.WriteLine("Press Q or F10 to exit.");
            ConsoleKeyInfo consoleKeyInfo = Console.ReadKey();
            if (consoleKeyInfo.Key == ConsoleKey.Q || consoleKeyInfo.Key == ConsoleKey.F10) break;
        }
    }

    private IHasName Couple(Human first, Human second)
    {
        if (first.Gender == second.Gender)
        {
            throw new SameGenderException();
        }

        double firstSympathy = 0;
        string firstChildType = string.Empty;
        Type firstType = first.GetType();
        var firstAttributes = firstType.GetCustomAttributes(false);
        foreach (CoupleAttribute couple in firstAttributes)
        {
            if (couple.Pair == second.GetType().Name)
            {
                firstSympathy = couple.Probability;
                firstChildType = couple.ChildType;
            }
        }
        Console.WriteLine($"{first.Name} likes {second.Name} for {firstSympathy * 100}%");

        double secondSympathy = 0;
        Type secondType = second.GetType();
        var secondAttributes = secondType.GetCustomAttributes(false);
        foreach (CoupleAttribute couple in secondAttributes)
        {
            if (couple.Pair == first.GetType().Name)
            {
                secondSympathy = couple.Probability;
            }
        }
        Console.WriteLine($"{second.Name} likes {first.Name} for {secondSympathy * 100}%");

        if (!DoesOccur(firstSympathy, secondSympathy))
        {
            return null;
        }

        string name = string.Empty;
        foreach (var method in secondType.GetMethods())
        {
            if (method.ReturnType == typeof(string))
            {
                try
                {
                    name = method.Invoke(second, null) as string;
                }
                catch
                {
                    name = "Undefined";
                }
                break;
            }
        }

        Type childType = Type.GetType(firstChildType);
        var child = Activator.CreateInstance(childType) as IHasName;
        child.Name = name;

        var patronymicName = childType.GetProperty(nameof(Human.PatronymicName));
        var fatherName = first.Gender == Gender.Male ? first.Name : second.Name;
        patronymicName?.SetValue(child, fatherName);

        return child;
    }

    private void PrintInstructions()
    {
        Console.WriteLine("Welcome!");
        Console.WriteLine("This console app determines if there will be a pairing between a boy and a girl.");
        Console.WriteLine("As a result, you'll receive information about their relationship and potential child.");
        Console.WriteLine("You will be prompted to enter the names of the boy and girl.");
        Console.WriteLine("Press any key to continue...");
        Console.ReadLine();

    }

    private bool DoesOccur(double firstProbability, double secondProbability)
    {
        var gen = new Random();
        return gen.NextDouble() < firstProbability * secondProbability;
    }
    private string GetObjectType(object obj)
    {
        return obj.GetType().Name;
    }
    private string GetIHasNameType(IHasName obj)
    {
        return GetObjectType(obj);
    }

}

