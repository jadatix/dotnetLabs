class ResearchTeam
{
    private string _topic;
    private string _organizationName;
    private int _registrationNumber;
    private TimeFrame _researchDuretion;
    private Paper[] _publications;

    public string Topic { get => _topic; init => _topic = value; }
    public string OrganizationName { get => _organizationName; init => _organizationName = value; }
    public int RegistrationNumber { get => _registrationNumber; init => _registrationNumber = value; }
    public TimeFrame ResearchDuretion { get => _researchDuretion; init => _researchDuretion = value; }
    public Paper[] Publications { get => _publications; init => _publications = value; }
    public Paper? LastPublication
    {
        get
        {
            if (_publications == null || _publications.Length == 0)
                return null;
            Paper lastPublication = _publications[0];
            foreach (Paper publication in _publications)
            {
                if (publication.PublicationDate > lastPublication.PublicationDate)
                    lastPublication = publication;
            }
            return lastPublication;
        }
    }
    public bool this[TimeFrame timeFrame]
    {
        get => _researchDuretion == timeFrame;
    }
    public void AddPaper(params Paper[] papers)
    {
        if (papers == null)
            return;
        if (_publications == null)
            _publications = papers;
        else
        {
            Paper[] newPublications = new Paper[_publications.Length + papers.Length];
            _publications.CopyTo(newPublications, 0);
            papers.CopyTo(newPublications, _publications.Length);
            _publications = newPublications;
        }
    }
    public sealed override string ToString()
    {
        string result = "\ntopic: " + _topic + " organization name: " + _organizationName + " registration number: " + _registrationNumber + " research duretion: " + _researchDuretion;
        if (_publications != null)
        {
            result += "\npublications:";
            foreach (Paper publication in _publications)
                result += publication;
        }
        return result;
    }
    public ResearchTeam(string _topic, string _organizationName, int _registrationNumber, TimeFrame _researchDuretion, Paper[] _publications)
    {
        this._topic = _topic;
        this._organizationName = _organizationName;
        this._registrationNumber = _registrationNumber;
        this._researchDuretion = _researchDuretion;
        this._publications = _publications;
    }

    public ResearchTeam() : this("", "", 0, TimeFrame.Year, new Paper[0]) { }

}