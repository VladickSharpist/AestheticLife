namespace AestheticLife.Web.Models.Response;

public class CurrentUserResponseVm
{
    public string Id { get; set; }

    public string UserName { get; set; }

    public string Name { get; set; }

    public string Surname { get; set; }

    public string MiddleName { get; set; }

    public string Email { get; set; }

    public IEnumerable<string> Roles { get; set; }
}