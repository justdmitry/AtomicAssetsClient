# AtomicAssetsClient

Client library for interacting with AtomicHub REST API in [WAX network](https://wax.api.atomicassets.io/docs/) and [EOS network](https://eos.api.atomicassets.io/docs) (not tested yet).


[![NuGet](https://img.shields.io/nuget/v/AtomicAssetsClient.svg?maxAge=86400&style=flat)](https://www.nuget.org/packages/AtomicAssetsClient/) 


## Features

* Auto-detects request limits: requests queued (via SemaphoreSlim) and delayed if needed
* Strongly typed request parameters and returned values (instead of strings everywhere)
* Auto-request next page for list-type requests (and delays it if needed, of course)
* Target framework: `net6.0`
* Web and console ready (dependencies are only `Microsoft.Extensions.Http` and `Microsoft.Extensions.Logging.Abstractions`)
* Uses `HttpClient` (add [Polly](https://github.com/App-vNext/Polly) policies of your choice)


## Installation

```
dotnet add package AtomicAssetsClient
```

## Usage

### 1. Register in Startup.cs

```csharp
services.AddHttpClient();
services.Configure<AtomicClientOptions>(Configuration.GetSection("AtomicClientOptions"));
services.AddSingleton<IAtomicClient, AtomicClient>();
```

⚠ Important: register client as singletone for request limits to be handled correctly betweed different threads!


### 2. Use in your code

```csharp
var templates = await atomicClient.GetTemplates(collectionName: "atomic").ConfigureAwait(false);
```

## Advanced scenarios

Check [AdvancedReadme.md](https://github.com/justdmitry/AtomicAssetsClient/blob/master/AdvancedReadme.md) to know how to configure HttpClient, how to switch to EOS network, etc.


## Donate

[![Donate WAX](https://img.shields.io/badge/WAX-just.gm-orange?style=flat)](https://wax.bloks.io/account/just.gm) 
