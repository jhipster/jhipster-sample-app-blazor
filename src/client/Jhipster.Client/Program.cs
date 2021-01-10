using System;
using System.Net.Http;
using System.Reflection;
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
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Toolbelt.Blazor.Extensions.DependencyInjection;

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
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();


            builder.RootComponents.Add<App>("app");

            builder.Services.AddSingleton<ISessionStorageService, SessionStorageService>().AddSingleton<ISyncSessionStorageService, SessionStorageService>();
            builder.Services.AddBlazoredModal();

            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());

            builder.Services.AddSingleton<AuthenticationStateProvider, AuthenticationService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();

            builder.Services.AddSingleton<IUserService, UserService>();
            builder.Services.AddSingleton<IRegisterService, RegisterService>();

            builder.Services.AddSingleton<IRegionService, RegionService>();
            builder.Services.AddSingleton<ICountryService, CountryService>();
            builder.Services.AddSingleton<ILocationService, LocationService>();
            builder.Services.AddSingleton<IDepartmentService, DepartmentService>();
            builder.Services.AddSingleton<IPieceOfWorkService, PieceOfWorkService>();
            builder.Services.AddSingleton<IEmployeeService, EmployeeService>();
            builder.Services.AddSingleton<IJobService, JobService>();
            builder.Services.AddSingleton<IJobHistoryService, JobHistoryService>();
            // jhipster-needle-add-services-in-di - JHipster will add services in DI

            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddTransient(sp => new HttpClient
            {
                BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
            }.EnableIntercept(sp));

            builder.Services.AddAuthorizationCore();

            var host = builder.Build();

            host.Services
                .UseBootstrapProviders()
                .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
