using System;

public class Paper
{
    public string PublicationName { get; set; }
    public Person Author { get; set; }
    public System.DateTime PublicationDate { get; set; }

    public Paper() : this("", new Person(), new DateTime()) { }

    public Paper(string publicationName, Person author, DateTime publishDate)
    {
        PublicationName = publicationName;
        Author = author;
        PublicationDate = publishDate;
    }

    public sealed override string ToString() => "\npublication name: " + PublicationName + " author: " + Author + " publish date: " + PublicationDate;
}