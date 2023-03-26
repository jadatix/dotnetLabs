public class Person : INameAndCopy
{
    private string _name = default!;
    private string _lastName = default!;
    private System.DateTime _birthDate;

    public string Name { get => _name; set => _name = value; }
    public string LastName { get => _lastName; set => _lastName = value; }
    public System.DateTime BirthDate { get => _birthDate; set => _birthDate = value; }

    public int Year
    {
        get => _birthDate.Year;
        set => _birthDate = new DateTime(Year, _birthDate.Month, _birthDate.Day);
    }
    public Person() : this("", "", new DateTime()) { }
    public Person(string name, string lastName, System.DateTime birthDate)
    {
        Name = name;
        LastName = lastName;
        BirthDate = birthDate;
    }
    public static bool operator ==(Person person1, Person person2) => person1.Equals(person2);
    public static bool operator !=(Person person1, Person person2) => !person1.Equals(person2);

    public sealed override int GetHashCode() => Name.GetHashCode() ^ LastName.GetHashCode() ^ BirthDate.GetHashCode();
    public sealed override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        Person person = (Person)obj;
        return Name.Equals(person.Name) && LastName.Equals(person.LastName) && BirthDate.Equals(person.BirthDate);
    }

    public object DeepCopy()
    {
        return new Person(Name, LastName, BirthDate);
    }

    public sealed override string ToString() => "\nname: " + Name + " last name: " + LastName + " birth date: " + BirthDate;
    public virtual string ToShortString() => "\nname: " + Name + " last name: " + LastName;
}
