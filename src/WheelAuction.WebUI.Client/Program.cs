using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

WebAssemblyHostBuilder hostBuilder = WebAssemblyHostBuilder.CreateDefault(args);

WebAssemblyHost host = hostBuilder.Build();

await host.RunAsync();