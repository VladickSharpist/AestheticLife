using AestheticsLife.DataAccess.Shared.Abstractions.Repositories;
using AestheticsLife.DataAccess.Training.Abstractions.Models;
using MassTransit;
using RabbitMq;

namespace AestheticsLife.Training.Service.Consumers;

public class ExerciseFileUploadedConsumer: IConsumer<UploadedFileReadyEvent>
{
    private IUnitOfWork _unitOfWork;
    private readonly IPublishEndpoint _publishEndpoint;
    public ExerciseFileUploadedConsumer(IUnitOfWork unitOfWork, IPublishEndpoint publishEndpoint)
    {
        _unitOfWork = unitOfWork;
        _publishEndpoint = publishEndpoint;
    }
    //remove db model using
    public async Task Consume(ConsumeContext<UploadedFileReadyEvent> context)
    {
        var message = context.Message;
        var entity = await _unitOfWork
            .GetReadonlyRepository<Exercise>()
            .SingleOrDefaultAsync(e => e.Id == context.Message.Id);
        entity.IsDataReady = true;
        entity.FilePath = message.FilePath;
        await _unitOfWork.SaveChangesAsync();
        await _publishEndpoint.Publish(new NotificationEvent
        {
            Message = $"Your data for file {message.FilePath} is ready",
            UserId = entity.OwnerId
        });
    }
}