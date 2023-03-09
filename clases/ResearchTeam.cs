class ResearchTeam
{
    private string _topic = default!;
    private string _organizationName = default!;
    private int _registrationNumber;
    private TimeFrame _researchDuration;
    private Paper[] _publications = default!;

    public string Topic { get => _topic; init => _topic = value; }
    public string OrganizationName { get => _organizationName; init => _organizationName = value; }
    public int RegistrationNumber { get => _registrationNumber; init => _registrationNumber = value; }
    public TimeFrame ResearchDuration { get => _researchDuration; init => _researchDuration = value; }
    public Paper[] Publications { get => _publications; set => _publications = value; }
    public Paper? LastPublication
    {
        get
        {
            if (Publications == null || Publications.Length == 0)
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
    public bool this[TimeFrame timeFrame]
    {
        get => ResearchDuration == timeFrame;
    }
    public void AddPaper(params Paper[] papers)
    {
        if (papers == null)
            return;
        if (Publications == null)
            Publications = papers;
        else
        {
            Paper[] newPublications = new Paper[Publications.Length + papers.Length];
            Publications.CopyTo(newPublications, 0);
            papers.CopyTo(newPublications, Publications.Length);
            Publications = newPublications;
        }
    }
    public sealed override string ToString()
    {
        string result = "\ntopic: " + Topic + " organization name: " + OrganizationName + " registration number: " + RegistrationNumber + " research duretion: " + ResearchDuration;
        if (Publications != null)
        {
            result += "\npublications:";
            foreach (Paper publication in Publications)
                result += publication;
        }
        return result;
    }
    public ResearchTeam(string topic, string organizationName, int registrationNumber, TimeFrame researchDuretion, Paper[] pubications)
    {
        Topic = topic;
        OrganizationName = organizationName;
        RegistrationNumber = registrationNumber;
        ResearchDuration = researchDuretion;
        Publications = pubications;
    }

    public ResearchTeam() : this("", "", 0, TimeFrame.Year, new Paper[0]) { }

}