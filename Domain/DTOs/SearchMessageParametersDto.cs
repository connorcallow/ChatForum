namespace Domain.DTOs;

public class SearchMessageParametersDto
{
    public string? Username { get;}
    public int? UserId { get;}
    public string? Body { get;}
    public string? TitleContains { get;}

    public SearchMessageParametersDto(string? username, int? userId, string? body, string? titleContains)
    {
        Username = username;
        UserId = userId;
       Body = body;
        TitleContains = titleContains;
    }
    
}