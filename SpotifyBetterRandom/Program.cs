// See https://aka.ms/new-console-template for more information

using System.Security.Principal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SpotifyBetterRandom;
using SpotifyBetterRandom.Services.Implementations;

HostApplicationBuilder builder = Host.CreateApplicationBuilder();


builder.Services.AddInfrastructureServices();
builder.Services.AddDebugServices();


using IHost host = builder.Build();

var test = host.Services.GetService<TestService>();

await host.RunAsync();



