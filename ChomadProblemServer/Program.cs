using System;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

WebHost.CreateDefaultBuilder(args)
.ConfigureServices(services =>
{
    services.AddCors();
})
.Configure((webBuilderContext, app) =>
{
    if (webBuilderContext.HostingEnvironment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }
    app.UseHttpsRedirection();
    app.UseDefaultFiles();
    app.UseStaticFiles();
    app.UseRouting();
    app.UseCors();
    app.UseEndpoints(endpoints =>
    {
        endpoints.MapMethods("/answer", new[] { "POST", "OPTIONS" }, async context =>
        {
            var seedString = context.Request.Query.TryGetValue("seed", out var values) ? values.First() : "1";
            var seed = int.TryParse(seedString, out var n) ? n : 1;

            var random = new Random(seed);
            const int numOfOptions = 4;
            const int numOfQuestions = 10;
            var correct = Enumerable.Range(0, numOfQuestions).Select(_ => random.Next(0, numOfOptions) + 1).ToArray();

            var answers = await context.Request.ReadFromJsonAsync<int[]>();
            var correctCount = correct
                .Zip(answers, (c, a) => c == a)
                .Count(right => right);

            await context.Response.WriteAsJsonAsync(correctCount);
        })
        .RequireCors(option => option.AllowAnyOrigin().AllowAnyHeader().WithMethods("POST"));
    });
})
.Build()
.Run();
