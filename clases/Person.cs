public class Person
{
    private string _name = default!;
    private string _lastName = default!;
    private System.DateTime _birthDate;

    public string Name { get => _name; set => _name = value; }
    public string LastName { get => _lastName; init => _lastName = value; }
    public System.DateTime BirthDate { get => _birthDate; init => _birthDate = value; }

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

    public sealed override string ToString() => "\nname: " + Name + " last name: " + LastName + " birth date: " + BirthDate;
    public virtual string ToShortString() => "\nname: " + Name + " last name: " + LastName;
}
