public abstract class Human : IHasName
{
    private string _name;
    public string Name { get => _name; set => _name = value; }
    public string Surname { get; init; }
    public string PatronymicName { get; init; }
    public Gender Gender { get; protected set; }

    public override string ToString()
    {
        return $"Human: {Name} | {Surname} | {PatronymicName}";
    }
}

