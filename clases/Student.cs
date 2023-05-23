[Couple(Pair = "Girl", Probability = 0.7, ChildType = "Girl")]
[Couple(Pair = "PrettyGirl", Probability = 1, ChildType = "PrettyGirl")]
[Couple(Pair = "SmartGirl", Probability = 0.5, ChildType = "Girl")]
public sealed class Student : Human
{
    public Student()
    {
        Gender = Gender.Male;
    }
}
