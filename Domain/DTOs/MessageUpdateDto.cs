namespace Domain.DTOs;

public class MessageUpdateDto
{
    public int Id { get; }
    public int? OwnerId { get; set; }
    public string? Title { get; set; }
    public string? Body { get; set; }

    public MessageUpdateDto(int id)
    {
        Id = id;
    }
}