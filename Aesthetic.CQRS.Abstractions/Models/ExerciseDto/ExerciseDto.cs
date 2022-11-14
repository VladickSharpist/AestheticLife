namespace Aesthetic.CQRS.Abstractions.Models.ExerciseDto;

public class ExerciseDto: IFileWithData
{
    public long Id { get; set; }

    public string Name { get; set; }

    public long OwnerId { get; set; }

    public string? FileName { get; set; }

    public string? FilePath { get; set; }

    public bool IsDataReady { get; set; }
}