using AestheticsLife.DataAccess.Shared.Abstractions.Models;

namespace AestheticsLife.DataAccess.Training.Abstractions.Models;

public class Exercise : EntityWithFile
{
    public string Name { get; set; }

    public long OwnerId { get; set; }
}