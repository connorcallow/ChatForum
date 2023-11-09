using Domain.DTOs;
using Domain.Models;

namespace Application.LogicInterfaces;

public interface IMessageLogic
{
    Task<Message> CreateAsync(MessageCreationDto dto);
    Task<IEnumerable<Message>> GetAsync(SearchMessageParametersDto searchParameters);
    
    Task UpdateAsync(MessageUpdateDto message);
    
    Task DeleteAsync(int id);
    
    Task<MessageBasicDto> GetByIdAsync(int id);
}