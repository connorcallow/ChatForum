using Domain.DTOs;
using Domain.Models;

namespace HttpClients.ClientInterfaces;

public interface IMessageService
{
    Task CreateAsync(MessageCreationDto dto); 
    
    Task<ICollection<Message>> GetAsync(
        string? userName, 
        int? userId, 
        string? body, 
        string? titleContains
    );
    Task UpdateAsync(MessageUpdateDto dto);
    Task<MessageBasicDto> GetByIdAsync(int id);
}

