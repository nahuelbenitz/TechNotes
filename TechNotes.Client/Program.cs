using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TechNotes.Application.Notes;
using TechNotes.Client.Features.Notes;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddScoped<INotesOverviewService, NotesOverviewServiceClient>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
