using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;

namespace FileData.DAOs;

public class MessageFileDao : IMessageDao
{
    private readonly FileContext context;

    public MessageFileDao(FileContext context)
    {
        this.context = context;
    }

    public Task<Message> CreateAsync(Message message)
    {
        int id = 1;
        if (context.Messages.Any())
        {
            id = context.Messages.Max(t => t.Id);
            id++;
        }

        message.Id = id;
        
        context.Messages.Add(message);
        context.SaveChanges();

        return Task.FromResult(message);
    }

    public Task<IEnumerable<Message>> GetAsync(SearchMessageParametersDto searchParams)
    {
        IEnumerable<Message> result = context.Messages.AsEnumerable();

        if (!string.IsNullOrEmpty(searchParams.Username))
        {
            // we know username is unique, so just fetch the first
            result = context.Messages.Where(message =>
                message.Owner.UserName.Equals(searchParams.Username, StringComparison.OrdinalIgnoreCase));
        }

        if (searchParams.UserId != null)
        {
            result = result.Where(m => m.Owner.Id == searchParams.UserId);
        }

      

        if (!string.IsNullOrEmpty(searchParams.TitleContains))
        {
            result = result.Where(m =>
                m.Title.Contains(searchParams.TitleContains, StringComparison.OrdinalIgnoreCase));
        }

        return Task.FromResult(result);
    }

    public Task UpdateAsync(Message toUpdate)
    {
        Message? existing = context.Messages.FirstOrDefault(message => message.Id == toUpdate.Id);
        if (existing == null)
        {
            throw new Exception($"Message with id {toUpdate.Id} does not exist!");
        }

        context.Messages.Remove(existing);
        context.Messages.Add(toUpdate);
    
        context.SaveChanges();
    
        return Task.CompletedTask;
    }

    public Task<Message?> GetByIdAsync(int id)
    {
        Message? existing = context.Messages.FirstOrDefault(m => m.Id == id);
        return Task.FromResult(existing);
    }
    
    
    
    public Task DeleteAsync(int id)
    {
        Message? existing = context.Messages.FirstOrDefault(message => message.Id == id);
        if (existing == null)
        {
            throw new Exception($"Message with id {id} does not exist!");
        }

        context.Messages.Remove(existing); 
        context.SaveChanges();
    
        return Task.CompletedTask;
    }
    
    
 
}