using RabbitMq;

namespace Logic.Shared.Abstractions.Models;

public class UploadToken
{
    public UploadedFileReadyEvent Message { get; set; }
    public string QueueName { get; set; }
}