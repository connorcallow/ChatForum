namespace Domain.Models;

public class Message
{
    
    public int Id { get; set; }
    public User Owner { get; }
    public string Title { get; }
    public string Body { get; set; }


    public Message(User owner, string title, string body)
    {
        Owner = owner;
        Title = title;
        Body = body;
    }
    
}