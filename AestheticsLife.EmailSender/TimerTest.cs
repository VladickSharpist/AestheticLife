using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AestheticsLife.EmailSender;

public static class TimerTest
{
    [FunctionName("TimerTest")]
    public static async Task RunAsync([TimerTrigger("0 * * * * *")] TimerInfo myTimer, ILogger log)
    {
        Console.WriteLine($"C# Timer trigger function executed at: {DateTime.UtcNow}");
    }
}