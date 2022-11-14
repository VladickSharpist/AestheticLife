using AestheticsLife.DataAccess.Shared.Abstractions.Models;

namespace AestheticLife.DataAccess.Domain.Models;

public class File : BaseEntity
{
    public string RelativePath { get; set; }

    public string Name { get; set; }

    public string Extension { get; set; }

    public double? Duration { get; set; }

    public string Hash { get; set; }
}