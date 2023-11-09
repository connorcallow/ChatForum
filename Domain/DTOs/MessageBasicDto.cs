namespace Domain.DTOs;

public class MessageBasicDto
{
    public int Id { get; }
    public string OwnerName { get; }
    public string Title { get; }
    public string  Body { get;  }

    public MessageBasicDto(int id, string ownerName, string title, string body)
    {
        Id = id;
        OwnerName = ownerName;
        Title = title;
        Body = body;
    }
}