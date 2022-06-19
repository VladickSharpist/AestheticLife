using AestheticLife.DataAccess.Domain.Abstractions.Interfaces;
using AestheticLife.DataAccess.Domain.Abstractions.Models;

namespace AestheticLife.DataAccess.Domain.Models;

public class Exercise : EntityWithFile
{
    public string Name { get; set; }

    public long OwnerId { get; set; }
}