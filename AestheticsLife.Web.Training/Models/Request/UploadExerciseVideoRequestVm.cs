namespace AestheticsLife.Web.Training.Models.Request;

public class UploadExerciseVideoRequestVm
{
    public long EntityId { get; set; }
    
    public string RelativePath { get; set; }

    public string Name { get; set; }

    public string Extension { get; set; }
}