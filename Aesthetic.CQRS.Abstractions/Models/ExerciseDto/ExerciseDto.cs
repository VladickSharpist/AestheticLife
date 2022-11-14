namespace Aesthetic.CQRS.Abstractions.Models.ExerciseDto;

public class ExerciseDto
{
    public long Id { get; set; }

    public string Name { get; set; }

    public long OwnerId { get; set; }

    public long FileId { get; set; }
}