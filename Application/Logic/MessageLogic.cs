using Application.DaoInterfaces;
using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace Application.Logic;

public class MessageLogic : IMessageLogic
{
    private readonly IMessageDao messageDao;
    private readonly IUserDao userDao;

    public MessageLogic(IMessageDao messageDao, IUserDao userDao)
    {
        this.messageDao = messageDao;
        this.userDao = userDao;
    }

    public async Task<Message> CreateAsync(MessageCreationDto dto)
    {
        User? user = await userDao.GetByIdAsync(dto.OwnerId); 
        if (user == null)
        {
            throw new Exception($"User with id {dto.OwnerId} was not found.");
        }
        Message message = new Message(user.Id, dto.Title, dto.Body);
        ValidateMessage(message);
        
        Message created = await messageDao.CreateAsync(message);
        return created;
    }
    
    public Task<IEnumerable<Message>> GetAsync(SearchMessageParametersDto searchParameters)
    {
        return messageDao.GetAsync(searchParameters);
    }
    

    public async Task UpdateAsync(MessageUpdateDto dto)
    {
        Message? existing = await messageDao.GetByIdAsync(dto.Id);

        if (existing == null)
        {
            throw new Exception($"Message with ID {dto.Id} not found!");
        }

        User? user = null;
        if (dto.OwnerId != null)
        {
            user = await userDao.GetByIdAsync((int)dto.OwnerId);
            if (user == null)
            {
                throw new Exception($"User with id {dto.OwnerId} was not found.");
            }
        }

        User userToUse = user ?? existing.Owner;
        string titleToUse = dto.Title ?? existing.Title;
        string bodyToUse = dto.Body ?? existing.Body;
    
        Message updated = new (userToUse.Id, titleToUse, bodyToUse)
        {
            Body = bodyToUse,
            Id = existing.Id,
        };

        ValidateMessage(updated);

        await messageDao.UpdateAsync(updated);
    }
    
    public async Task DeleteAsync(int id)
    {
        Message? message = await messageDao.GetByIdAsync(id);
        if (message == null)
        {
            throw new Exception($"Message with ID {id} was not found!");
        }
        await messageDao.DeleteAsync(id);
    }
    
    public async Task<MessageBasicDto> GetByIdAsync(int id)
    {
         Message? message = await messageDao.GetByIdAsync(id);
        if (message == null)
        {
            throw new Exception($"Message with id {id} not found");
        }

        return new MessageBasicDto(message.Id, message.Owner.UserName, message.Title, message.Body);
    }
    
    private void ValidateMessage(Message dto)
    {
        if (string.IsNullOrEmpty(dto.Title)) throw new Exception("Title cannot be empty.");
        // other validation stuff
    }
}