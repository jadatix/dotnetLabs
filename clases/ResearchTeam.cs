using System.Collections.Generic;

class ResearchTeam : Team
{
    private string _topic = default!;
    private TimeFrame _researchDuration;
    private List<Paper> _publications = new List<Paper>();
    private List<Person> _members = new List<Person>();

    public string Topic { get => _topic; set => _topic = value; }
    public TimeFrame ResearchDuration { get => _researchDuration; set => _researchDuration = value; }
    public List<Paper> Publications { get => _publications; set => _publications = value; }
    public Team Team { get => new Team(OrganizationName, RegistrationNumber); init { OrganizationName = value.OrganizationName; RegistrationNumber = value.RegistrationNumber; } }
    public List<Person> Members { get => _members; set => _members = value; }
    public Paper? LastPublication
    {
        get
        {
            if (Publications == null || Publications.Count == 0)
                return null;
            Paper lastPublication = Publications[0];
            foreach (Paper publication in Publications)
            {
                if (publication.PublicationDate > lastPublication.PublicationDate)
                    lastPublication = publication;
            }
            return lastPublication;
        }
    }
    public ResearchTeam(string name, string topic, string organizationName, int registrationNumber, TimeFrame researchDuration, List<Paper> publications, List<Person> members) : base(organizationName, registrationNumber)
    {
        Name = name;
        Topic = topic;
        ResearchDuration = researchDuration;
        Publications = publications;
        Members = members;
    }
    public ResearchTeam() : this("", "", "", 0, TimeFrame.Year, new List<Paper>(), new List<Person>()) { }
    public bool this[TimeFrame timeFrame]
    {
        get => ResearchDuration == timeFrame;
    }
    public void AddPaper(params Paper[] papers)
    {
        if (papers == null)
            return;
        Publications.AddRange(papers);
    }
    public sealed override string ToString()
    {
        string result = "\ntopic: " + Topic + " organization name: " + OrganizationName + " registration number: " + RegistrationNumber + " research duration: " + ResearchDuration;
        if (Publications != null)
        {
            result += "\npublications:";
            foreach (Paper publication in Publications)
                result += publication;
        }
        return result;
    }

    public object DeepCopy()
    {
        List<Paper> copiedPublications = new List<Paper>();
        foreach (Paper publication in Publications)
        //no DeepCopy() for Paper, so we use the constructor
            copiedPublications.Add(new Paper(publication.PublicationName, (Person) publication.Author.DeepCopy(), publication.PublicationDate));

        List<Person> copiedMembers = new List<Person>();
        //we have DeepCopy() for Person, so we use it
        foreach (Person member in Members)
            copiedMembers.Add((Person)member.DeepCopy());

        return new ResearchTeam(Name, Topic, OrganizationName, RegistrationNumber, ResearchDuration, copiedPublications, copiedMembers);
    }
    public IEnumerable<Person> GetNonPublishingMembers()
    {
        foreach (Person member in Members)
        {
            bool hasPublication = false;
            foreach (Paper publication in Publications)
            {
                if (publication.Author.Equals(member))
                {
                    hasPublication = true;
                    break;
                }
            }
            if (!hasPublication)
            {
                yield return member;
            }
        }
    }

    public IEnumerable<Paper> GetRecentPublications(int n)
    {
        DateTime cutoffDate = DateTime.Now.AddYears(-n);
        foreach (Paper publication in Publications)
        {
            if (publication.PublicationDate > cutoffDate)
            {
                yield return publication;
            }
        }
    }

}
