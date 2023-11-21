using Application.DaoInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EfcDataAccess.DAOs;

public class MessageEfcDao :IMessageDao
{
    private readonly MessageContext context;

    public MessageEfcDao(MessageContext context)
    {
        this.context = context;
    }
    
    public async Task<Message> CreateAsync(Message message)
    {
        EntityEntry<Message> added = await context.Messages.AddAsync(message);
        await context.SaveChangesAsync();
        return added.Entity;
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<Message>> GetAsync(SearchMessageParametersDto searchParameters)
    {
        IQueryable<Message> query = context.Messages.Include(message => message.Owner).AsQueryable();
    
        if (!string.IsNullOrEmpty(searchParameters.Username))
        {
            // we know username is unique, so just fetch the first
            query = query.Where(message =>
                message.Owner.UserName.ToLower().Equals(searchParameters.Username.ToLower()));
        }
    
        if (searchParameters.UserId != null)
        {
            query = query.Where(m => m.Owner.Id == searchParameters.UserId);
        }
        
    
        if (!string.IsNullOrEmpty(searchParameters.TitleContains))
        {
            query = query.Where(m =>
                m.Title.ToLower().Contains(searchParameters.TitleContains.ToLower()));
        }

        List<Message> result = await query.ToListAsync();
        return result;
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(Message message)
    {
        context.Messages.Update(message);
        await context.SaveChangesAsync();
        throw new NotImplementedException();
    }

    public async Task<Message?> GetByIdAsync(int messageId)
    {
        Message? found = await context.Messages
            .Include(message => message.Owner)
            .SingleOrDefaultAsync(message => message.Id == messageId);
        return found;
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }
}
    
