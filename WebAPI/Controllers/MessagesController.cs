using Application.LogicInterfaces;
using Domain.DTOs;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]

[Route("[controller]")]

public class MessagesController : ControllerBase
{
    private readonly IMessageLogic messageLogic;

    public MessagesController(IMessageLogic messageLogic)
    {
        this.messageLogic = messageLogic;
    }
    
    [HttpPost]
    public async Task<ActionResult<Message>> CreateAsync([FromBody]MessageCreationDto dto)
    {
        try
        {
            Message created = await messageLogic.CreateAsync(dto);
            return Created($"/messages/{created.Id}", created);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Message>>> GetAsync([FromQuery] string? userName, [FromQuery] int? userId, [FromQuery] string? titleContains,[FromQuery] string? body)
    {
        try
        {
           
            SearchMessageParametersDto parameters = new(userName, userId,body, titleContains);
            var messages = await messageLogic.GetAsync(parameters);
            return Ok(messages);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpPatch]
    public async Task<ActionResult> UpdateAsync([FromBody] MessageUpdateDto dto)
    {
        try
        {
            await messageLogic.UpdateAsync(dto);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult> DeleteAsync([FromRoute] int id)
    {
        try
        {
            await messageLogic.DeleteAsync(id);
            return Ok();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    [HttpGet("{id:int}")]
    public async Task<ActionResult<MessageBasicDto>> GetById([FromRoute] int id)
    {
        try
        {
            MessageBasicDto result = await messageLogic.GetByIdAsync(id);
            return Ok(result);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return StatusCode(500, e.Message);
        }
    }
    
}