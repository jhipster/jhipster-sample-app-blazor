using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Bunit;
using Bunit.Rendering;
using FluentAssertions;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Entities.Employee;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.Employee;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.Employee;

public class EmployeeDetailTest : TestContext
{
    private readonly Mock<IEmployeeService> _employeeService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public EmployeeDetailTest()
    {
        _employeeService = new Mock<IEmployeeService>();
        _navigationService = new Mock<INavigationService>();
        Services.AddSingleton<IEmployeeService>(_employeeService.Object);
        Services.AddSingleton<INavigationService>(_navigationService.Object);
        Services.AddBlazorise(options =>
            {
                options.Immediate = true;
            })
            .Replace(ServiceDescriptor.Transient<IComponentActivator, ComponentActivator>())
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
        //This code is needed to support recursion
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void Should_DisplayEmployee_When_IdIsPresent()
    {
        //Arrange
        var employee = _fixture.Create<EmployeeModel>();
        _employeeService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(employee));
        var employeeDetail = RenderComponent<EmployeeDetail>(ComponentParameter.CreateParameter("Id", employee.Id));

        // Act
        var title = employeeDetail.Find("h2");

        // Assert
        title.MarkupMatches($"<h2><span>Employee</span>{employee.Id}</h2>");

    }

    [Fact]
    public void Should_NotDisplayEmployee_When_IdIsNotPresent()
    {
        //Arrange
        _employeeService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(new EmployeeModel()));
        var employeeDetail = RenderComponent<EmployeeDetail>();

        // Act
        var title = employeeDetail.Find("div.col-8");

        // Assert
        title.Children.Length.Should().Be(0);

    }
}
