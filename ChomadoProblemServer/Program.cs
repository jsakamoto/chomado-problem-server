using System.Runtime.InteropServices;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();

await using var app = builder.Build();
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseStaticFiles();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().WithMethods("POST"));

app.MapOpenApi();
app.UseHealthChecks("/health");

// This provides "http://host:port/swagger/"
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/openapi/v1.json", "v1");
});

// This provides "http://host:port/scalar/v1"
app.MapScalarApiReference();

app.MapPost("/answer", ([FromBody] int[] answers, int? seed) =>
{
    var random = new Random(seed ?? 1);
    const int numOfOptions = 4;
    const int numOfQuestions = 10;
    var correct = Enumerable.Range(0, numOfQuestions).Select(_ => random.Next(0, numOfOptions) + 1).ToArray();

    var correctCount = correct
        .Zip(answers, (c, a) => c == a)
        .Count(right => right);

    return correctCount;
})
.WithName("PostAnser");

app.MapGet("/runtime-information", () => new
{
    RuntimeInformation.FrameworkDescription,
    RuntimeInformation.OSDescription,
    ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString()
})
.WithName("GetRuntimeInformation");

await app.RunAsync();
