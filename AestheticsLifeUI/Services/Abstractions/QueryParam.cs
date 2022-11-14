namespace AestheticsLifeUI.Services.Abstractions;

public record QueryParam(string ParamName, string ParamValue)
{
    public string ToUrlParam() => $"{ParamName}={ParamValue}";
}