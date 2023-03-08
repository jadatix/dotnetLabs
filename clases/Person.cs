public class Person
{
    private string _name;
    private string _lastName;
    private System.DateTime _birthDate;

    public string Name { get => _name; set => _name = value; }
    public string LastName { get => _lastName; init => _lastName = value; }
    public System.DateTime BirthDate { get => _birthDate; init => _birthDate = value; }

    public int year
    {
        get => _birthDate.Year;
        set => _birthDate = new DateTime(year, _birthDate.Month, _birthDate.Day);
    }
    public Person() : this("", "", new DateTime()) { }
    public Person(string name, string lastName, System.DateTime birthDate)
    {
        _name = name;
        _lastName = lastName;
        _birthDate = birthDate;
    }

    public sealed override string ToString() => "\nname: " + _name + " last_name: " + _lastName + " birth date: " + _birthDate;
    public virtual string ToShortString() => "\nname: " + _name + " last_name: " + _lastName;
}
