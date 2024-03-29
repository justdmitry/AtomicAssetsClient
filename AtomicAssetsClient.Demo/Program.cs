﻿using AtomicAssetsClient;
using AtomicAssetsClient.Demo;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureLogging(l => l.AddSystemdConsole());
builder.ConfigureServices(services =>
{
    services.AddHttpClient();
    services.AddOptions<AtomicClientOptions>();
    services.AddSingleton<IAtomicClient, AtomicClient>();
    services.AddHostedService<DemoService>();
});

var app = builder.Build();

await app.RunAsync();