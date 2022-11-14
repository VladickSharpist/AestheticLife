namespace RabbitMq;

public class UploadedFileReadyEvent
{
    public long Id { get; set; }
    public string FilePath { get; set; }
}