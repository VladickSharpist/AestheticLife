namespace AestheticsLife.Training.Service.Models;

public class ExerciseVm
{
    public long Id { get; set; }

    public string Name { get; set; }

    public long OwnerId { get; set; }

    public long FileId { get; set; }
}