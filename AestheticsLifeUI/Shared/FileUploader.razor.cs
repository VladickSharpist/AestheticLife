using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using AestheticsLifeUI.DataAccess.Abstractions;
using AestheticsLifeUI.Services.Abstractions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;

namespace AestheticsLifeUI.Shared;

public partial class FileUploader
{
    [Inject] public IHttpService HttpService { get; set; }
    [Inject] public IJSRuntime Js { get; set; }
    [Parameter] public string FileName { get; set; }
    [Parameter, EditorRequired] public string PrepareUploadRequestUrl { get; set; }
    public IBrowserFile FileData { get; set; }
    
    
    string Message = "No file selected";

    private void OnFileInputChange(InputFileChangeEventArgs e)
    {
        FileData = e.File;
        Message = $"{FileData.Name}.{FileData.ContentType} file is selected";
        StateHasChanged();
    }
    
    public async void PrepareAndUpload()
    {
        if (FileData.Size == 0L)
        {
            await Js.InvokeAsync<object>("alert","File is required");
            return;
        }
        var name = !string.IsNullOrEmpty(FileName) ? FileName : FileData.Name;
        var response = await HttpService.HttpClient.PostAsJsonAsync(PrepareUploadRequestUrl, new FileUploadPrepareVm { FileName = name });
        if (response.IsSuccessStatusCode)
        {
            var content = new MultipartFormDataContent();
            content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
            var fileStream = FileData.OpenReadStream(51200000L);
            content.Add(new StreamContent(fileStream, Convert.ToInt32(fileStream.Length)), "file", FileData.Name);
            var url = HttpService.BuildUrl("fileUploader", "upload", "", null,
                new QueryParam("uploadToken", await response.Content.ReadAsStringAsync()));
            await HttpService.HttpClient.PostAsync(url, content);
            StateHasChanged();
        }
    }
    
    public class FileUploadPrepareVm
    {
        public string FileName { get; set; }
    }
}