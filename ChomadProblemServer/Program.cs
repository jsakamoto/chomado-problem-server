using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args).UseWasiConnectionListener();
builder.Services.AddCors();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

await using var app = builder.Build();
if (app.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();
app.UseHttpsRedirection();
app.UseDefaultFiles();
app.UseBundledStaticFiles();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyHeader().WithMethods("POST"));
app.UseSwagger();
app.UseSwaggerUI();

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
});

app.MapGet("/runtime-information", () => new
{
    RuntimeInformation.FrameworkDescription,
    RuntimeInformation.OSDescription,
    ProcessArchitecture = RuntimeInformation.ProcessArchitecture.ToString()
});

await app.RunAsync();
