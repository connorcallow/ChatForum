using System.ComponentModel.DataAnnotations;

namespace Domain.Models;

public class Message
{
    
    
    public int Id { get; set; }
    public User Owner { get;  set; }
    public int OwnerId { get;  set; }
    public string Title { get;  set;}
    public string Body { get;  set; }


    public Message(int ownerId, string title, string body)
    {
        OwnerId = ownerId;
        Title = title;
        Body = body;
    }
    private Message(){}
}