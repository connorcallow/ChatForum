using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Domain.DTOs;
using Domain.Models;
using HttpClients.ClientInterfaces;

namespace HttpClients.Implementations;

public class MessageHttpClient : IMessageService
{
    private readonly HttpClient client;

    public MessageHttpClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task CreateAsync(MessageCreationDto dto)
    {
        HttpResponseMessage response = await client.PostAsJsonAsync("/messages", dto);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }

    public async Task<ICollection<Message>> GetAsync(string? userName, int? userId, string? body,
        string? titleContains)
    {
        HttpResponseMessage response = await client.GetAsync("/messages");
        string content = await response.Content.ReadAsStringAsync();
        if (!response.IsSuccessStatusCode)
        {
            throw new Exception(content);
        }

        ICollection<Message> messages = JsonSerializer.Deserialize<ICollection<Message>>(content,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            })!;
        return messages;
    }

    public async Task UpdateAsync(MessageUpdateDto dto)
    {
        string dtoAsJson = JsonSerializer.Serialize(dto);
        StringContent body = new StringContent(dtoAsJson, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PatchAsync("/messages", body);
        if (!response.IsSuccessStatusCode)
        {
            string content = await response.Content.ReadAsStringAsync();
            throw new Exception(content);
        }
    }



    public async Task<MessageBasicDto> GetByIdAsync(int id)
        {
            HttpResponseMessage response = await client.GetAsync($"/messages/{id}");
            string content = await response.Content.ReadAsStringAsync();
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(content);
            }

            MessageBasicDto message = JsonSerializer.Deserialize<MessageBasicDto>(content,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                }
            )!;
            return message;
        }

    }
