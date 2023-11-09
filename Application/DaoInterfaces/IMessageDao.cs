using Domain.DTOs;
using Domain.Models;

namespace Application.DaoInterfaces;

public interface IMessageDao
{
    Task<Message> CreateAsync(Message message);  
    Task<IEnumerable<Message>> GetAsync(SearchMessageParametersDto searchParameters);
    
    Task UpdateAsync(Message message);
    
    Task<Message?> GetByIdAsync(int id);
    
    Task DeleteAsync(int id);
}