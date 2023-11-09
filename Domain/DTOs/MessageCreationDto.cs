namespace Domain.DTOs;

public class MessageCreationDto
{
    public int OwnerId { get; }
    public string Title { get; }
    public string Body { get; }

    public MessageCreationDto(int ownerId, string title, string body)
    {
        OwnerId = ownerId;
        Title = title;
        Body = body;
    }  
}