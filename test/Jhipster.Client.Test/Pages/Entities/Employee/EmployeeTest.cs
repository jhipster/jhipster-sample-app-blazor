using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Bunit;
using Bunit.Rendering;
using FluentAssertions;
using Jhipster.Client.Models;
using Jhipster.Client.Services.EntityServices.Employee;
using Jhipster.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.Employee;

public class EmployeeTest : TestContext
{
    private readonly Mock<IEmployeeService> _employeeService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public EmployeeTest()
    {
        _employeeService = new Mock<IEmployeeService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
        Services.AddSingleton<IEmployeeService>(_employeeService.Object);
        Services.AddSingleton<Blazored.Modal.Services.IModalService>(_modalService.Object);
        Services.AddBlazorise(options =>
            {
                options.Immediate = true;
            })
            .Replace(ServiceDescriptor.Transient<IComponentActivator, ComponentActivator>())
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();
        Services.AddHttpClientInterceptor();
        //This code is needed to support recursion
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void Should_DisplayAllEmployees_When_EmployeesArePresent()
    {
        //Arrange
        var employees = _fixture.CreateMany<EmployeeModel>(10);
        _employeeService.Setup(service => service.GetAll()).Returns(Task.FromResult(employees.ToList() as IList<EmployeeModel>));
        var employeePage = RenderComponent<Jhipster.Client.Pages.Entities.Employee.Employee>();

        // Act
        var employeesTableBody = employeePage.Find("tbody");

        // Assert
        employeesTableBody.ChildElementCount.Should().Be(10);
    }

    [Fact]
    public void Should_DisplayNoCountry_When_EmployeesLengthIsZero()
    {
        //Arrange
        var employees = new List<EmployeeModel>();
        _employeeService.Setup(service => service.GetAll()).Returns(Task.FromResult(employees.ToList() as IList<EmployeeModel>));
        var employeePage = RenderComponent<Jhipster.Client.Pages.Entities.Employee.Employee>();

        // Act
        var span = employeePage.Find("div>span");

        // Assert
        span.MarkupMatches("<span>No Employees found</span>");
    }

    [Fact]
    public void Should_DeleteEmployee_WhenDeleteButtonClicked()
    {
        //Arrange
        var employees = _fixture.CreateMany<EmployeeModel>(10);
        _employeeService.Setup(service => service.GetAll()).Returns(Task.FromResult(employees.ToList() as IList<EmployeeModel>));

        var modalRef = new Mock<IModalReference>();
        modalRef.Setup(mock => mock.Result).Returns(Task.FromResult(ModalResult.Ok(new { })));
        _modalService.Setup(service => service.Show<DeleteModal>(It.IsAny<string>())).Returns(modalRef.Object);
        var employeePage = RenderComponent<Jhipster.Client.Pages.Entities.Employee.Employee>(ComponentParameterFactory.CascadingValue(_modalService.Object));

        // Act
        var employeeToDelete = employees.First();

        // Assert
        employeePage.Find("td>div>button").Click();
        _employeeService.Verify(service => service.Delete(employeeToDelete.Id.ToString()), Times.Once);
        var employeesTableBody = employeePage.Find("tbody");
        employeesTableBody.ChildElementCount.Should().Be(9);
    }

}
