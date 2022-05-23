using System;
using System.Net.Http;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;
using AutoMapper;
using Blazored.Modal;
using Blazored.SessionStorage;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services;
using Jhipster.Client.Services.AccountServices;
using Jhipster.Client.Services.EntityServices;
using Jhipster.Client.Services.EntityServices.Region;
using Jhipster.Client.Services.EntityServices.Country;
using Jhipster.Client.Services.EntityServices.Location;
using Jhipster.Client.Services.EntityServices.Department;
using Jhipster.Client.Services.EntityServices.PieceOfWork;
using Jhipster.Client.Services.EntityServices.Employee;
using Jhipster.Client.Services.EntityServices.Job;
using Jhipster.Client.Services.EntityServices.JobHistory;
// jhipster-needle-add-using-for-services - JHipster will add using services
using Jhipster.Client.Services.EntityServices.User;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using System.Text.Json.Serialization;

namespace Jhipster.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.Services
                .AddBlazorise(options =>
                {
                    options.Immediate = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();


            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddBlazoredSessionStorage(config =>
            {
                config.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                config.JsonSerializerOptions.IgnoreReadOnlyProperties = true;
                config.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                config.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                config.JsonSerializerOptions.ReadCommentHandling = JsonCommentHandling.Skip;
                config.JsonSerializerOptions.WriteIndented = false;
            });

            builder.Services.AddBlazoredModal();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddScoped<AuthenticationStateProvider, AuthenticationService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IRegisterService, RegisterService>();

            builder.Services.AddScoped<IRegionService, RegionService>();
            builder.Services.AddScoped<ICountryService, CountryService>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IPieceOfWorkService, PieceOfWorkService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();
            builder.Services.AddScoped<IJobService, JobService>();
            builder.Services.AddScoped<IJobHistoryService, JobHistoryService>();
            // jhipster-needle-add-services-in-di - JHipster will add services in DI

            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddTransient(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            }.EnableIntercept(sp));

            builder.Services.AddAuthorizationCore();

            var host = builder.Build();

            await host.RunAsync();
        }
    }
}
