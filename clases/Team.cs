public class Team : INameAndCopy
{

    private string _organizationName = default!;
    private int _registrationNumber;
    public string Name { get; set; }
    public string OrganizationName { get => _organizationName; set => _organizationName = value; }
    public int RegistrationNumber
    {
        get => _registrationNumber; init
        {
            if (value < 0)
                throw new ArgumentException("Registration number must be positive");
            _registrationNumber = value;
        }
    }
    public Team() : this("", 0) { }
    public Team(string name, string organizationName, int registrationNumber)
    {
        Name = name;
        OrganizationName = organizationName;
        RegistrationNumber = registrationNumber;
    }
    public Team(string organizationName, int registrationNumber) : this("", organizationName, registrationNumber) { }
    object INameAndCopy.DeepCopy()
    {
        Team newTeam = new Team(OrganizationName, RegistrationNumber);
        newTeam.Name = Name;
        return newTeam;
    }
    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType())
            return false;
        Team team = (Team)obj;
        return Name.Equals(team.Name) && OrganizationName.Equals(team.OrganizationName) && RegistrationNumber.Equals(team.RegistrationNumber);
    }
    public override int GetHashCode() => Name.GetHashCode() ^ OrganizationName.GetHashCode() ^ RegistrationNumber.GetHashCode();
    public override string ToString() => "\norganization name: " + OrganizationName + " registration number: " + RegistrationNumber;
}