[Couple(Pair = "Girl", Probability = 0.7, ChildType = "SmartGirl")]
[Couple(Pair = "PrettyGirl", Probability = 1, ChildType = "PrettyGirl")]
[Couple(Pair = "SmartGirl", Probability = 0.8, ChildType = "Book")]
public sealed class Botan : Human
{
    public Botan()
    {
        Gender = Gender.Male;
    }
}

