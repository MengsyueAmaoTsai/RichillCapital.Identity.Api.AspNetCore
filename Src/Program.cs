using RichillCapital.Identity.Api;

var app = await WebApplication
    .CreateBuilder(args)
    .ConfigureServices()
    .Build()
    .ConfigurePipeline();

await app.RunAsync();

public partial class Program
{
}