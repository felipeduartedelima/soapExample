using SoapCore;
using System.Text;
using SoapCore_Demo;
using Polly;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSoapCore();
builder.Services.AddSingleton<IAuthorService, AuthorService>();
builder.Services.AddControllers();
var app = builder.Build();
app.MapGet("/", () => "Hello World!");
// Add Soap Service;
app.UseSoapEndpoint<IAuthorService>("/Service.asmx", new SoapEncoderOptions
{
    MessageVersion = System.ServiceModel.Channels.MessageVersion.Soap11WSAddressingAugust2004,
    WriteEncoding = Encoding.UTF8
});
app.UseAuthorization();
app.MapControllers();
app.Run();
