// See https://aka.ms/new-console-template for more information

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using System.Text;

Console.WriteLine("Let's try some PlantUml!");

CreateHostBuilder(args).Build().Run();
await Task.Delay(TimeSpan.FromMinutes(5));

static IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.ConfigureKestrel(options =>
            {
                options.Limits.MaxRequestBufferSize = 302768;
                options.Limits.MaxRequestLineSize = 302768;
            });
            webBuilder.UseUrls("http://localhost:5001");
            webBuilder.Configure(app => app.Run(async ctx =>
            {
                if (ctx.Request.Path == "/plantuml-core.jar")
                {
                    ctx.Response.StatusCode = 200;
                    ctx.Response.Headers.ContentType = "application/octet-stream";
                    await ctx.Response.Body.WriteAsync(File.ReadAllBytes("Server/plantuml-core.jar"));
                }

                if (ctx.Request.Path == "/plantuml-decoder.js")
                {
                    ctx.Response.StatusCode = 200;
                    ctx.Response.Headers.ContentType = "text/javascript";
                    await ctx.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(File.ReadAllText("Server/plantuml-decoder.js")));
                }

                if (ctx.Request.Path == "/plantuml-core.jar.js")
                {
                    ctx.Response.StatusCode = 200;
                    ctx.Response.Headers.ContentType = "text/javascript";
                    await ctx.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(File.ReadAllText("Server/plantuml-core.jar.js")));
                }

                if (ctx.Request.Path == "/plantumlRenderer.html")
                {
                    ctx.Response.StatusCode = 200;
                    ctx.Response.Headers.ContentType = "text/html";
                    await ctx.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(File.ReadAllText("Server/plantumlRenderer.html")));
                }

                if (ctx.Request.Path.Value.Contains("/render"))
                {
                    ctx.Response.StatusCode = 200;
                    ctx.Response.Headers.ContentType = "text/html";
                    await ctx.Response.Body.WriteAsync(Encoding.UTF8.GetBytes(File.ReadAllText("Server/plantumlRender.html")));
                }
            }));
        });