public class Person
{
    private string name { get; init; }
    private string lastName { get; init; }
    private System.DateTime birthDate { get; init; }
    public int year
    {
        get { return year; }
        init
        {
            birthDate = new DateTime(year, birthDate.Month, birthDate.Day);
        }
    }
    public Person() { name = ""; lastName = ""; }
    public Person(string name, string lastName, System.DateTime birthDate)
    {
        this.name = name;
        this.lastName = lastName;
        this.birthDate = birthDate;
    }
    
    public override string ToString()
    {
        return "\nname: " +  name + " last_name: " + lastName + " birth date: " + birthDate;
    }
    public virtual string ToShortString(){
        return "\nname: " + name + " last_name: " + lastName;
    }
}
