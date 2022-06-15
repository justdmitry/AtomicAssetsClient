# AtomicAssetsClient: Advanced scenarios

## Configure HttpClient

AtomicClient requests named instance from HttpClientFactory, 
so configure that named instance:

```csharp
services.AddHttpClient(new AtomicClientOptions().HttpClientName).ConfigureHttpClient(...)
```

## Use with EOS network

WAX network is used by default. To switch to EOS network change `AtomicClientOptions.Endpoint`:

```csharp
services.Configure<AtomicClientOptions>(o => o.Endpoint = new Uri("https://eos.api.atomicassets.io"));
```

## Use WAX and EOS simultaneously in same app

You need two different instances of `AtomicClient`, initialized with different `Endpoint` values in `AtomicOptions`.

If you use advanced DI libraries which supports multiple registrations - use it, I guess you don't need help.

If you use default .NET DI, you may need additional classes:

```csharp
public interface IWaxAtomicClient : IAtomicClient { /* Nothing inside */ }

public interface IEosAtomicClient : IAtomicClient { /* Nothing inside */ }
    
public class WaxAtomicClient : AtomicClient, IWaxAtomicClient
{
    public WaxAtomicClient(ILogger<AtomicClient> logger, IOptions<AtomicClientOptions> options, HttpClientFactory httpClientFactory)
        : base(logger, options, httpClientFactory)
    {
        // Nothing inside
    }
}

public class EosAtomicClient : AtomicClient, IEosAtomicClient
{
    public EosAtomicClient(ILogger<AtomicClient> logger, IOptions<AtomicClientOptions> options, HttpClientFactory httpClientFactory) 
        : base(logger, options, httpClientFactory)
    {
        options.Value.Endpoint = new Uri("https://eos.api.atomicassets.io");
    }
}
```

Then register them both:

```csharp
services.AddHttpClient();
services.Configure<AtomicClientOptions>(Configuration.GetSection("AtomicClientOptions"));
services.AddSingleton<IWaxAtomicClient, WaxAtomicClient>();
services.AddSingleton<IEosAtomicClient, EosAtomicClient>();
```

And consume `IWaxAtomicClient` or `IEosAtomicClient` as needed.