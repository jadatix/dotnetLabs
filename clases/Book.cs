public class Book : IHasName
{
    private string _name;
    public string Name { get => _name; set => _name = value; }

    public override string ToString()
    {
        return $"Book: {Name}";
    }
}

