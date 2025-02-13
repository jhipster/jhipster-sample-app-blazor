using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Bunit;
using Bunit.Rendering;
using FluentAssertions;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.Employee;
using JhipsterSampleApplication.Client.Services.EntityServices.Department;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.Employee;

public class EmployeeUpdateTest : TestContext
{
    private readonly Mock<IEmployeeService> _employeeService;
    private readonly Mock<IDepartmentService> _departmentService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public EmployeeUpdateTest()
    {
        _employeeService = new Mock<IEmployeeService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
        _navigationService = new Mock<INavigationService>();
        _departmentService = new Mock<IDepartmentService>();
        Services.AddSingleton<IDepartmentService>(_departmentService.Object);
        Services.AddSingleton<INavigationService>(_navigationService.Object);
        Services.AddSingleton<IEmployeeService>(_employeeService.Object);
        Services.AddSingleton<Blazored.Modal.Services.IModalService>(_modalService.Object);
        Services.AddBlazorise(options =>
            {
                options.Immediate = true;
            })
            .Replace(ServiceDescriptor.Transient<IComponentActivator, ComponentActivator>())
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
        Services.AddHttpClientInterceptor();
        //This code is needed to support recursion
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public async Task Should_UpdateSelectedEmployee_When_FormIsSubmitWithId()
    {
        //Arrange
        var employees = _fixture.CreateMany<EmployeeModel>(10);
        _employeeService.Setup(service => service.GetAll()).Returns(Task.FromResult(employees.ToList() as IList<EmployeeModel>));
        var departments = _fixture.CreateMany<DepartmentModel>(10);
        _departmentService.Setup(service => service.GetAll()).Returns(Task.FromResult(departments.ToList() as IList<DepartmentModel>));

        var employeeToUpdate = _fixture.Create<EmployeeModel>();
        employeeToUpdate.Id = 1L;

        _employeeService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(employeeToUpdate));

        var updateEmployeePage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.Employee.EmployeeUpdate>(ComponentParameter.CreateParameter("Id", 1L));

        // Act
        var employeeForm = updateEmployeePage.Find("form");
        await employeeForm.SubmitAsync();

        // Assert
        _employeeService.Verify(service => service.Update(employeeToUpdate), Times.Once);
    }

    [Fact]
    public async Task Should_AddEmployee_When_FormIsSubmitWithoutId()
    {
        //Arrange
        var employees = _fixture.CreateMany<EmployeeModel>(10);
        _employeeService.Setup(service => service.GetAll()).Returns(Task.FromResult(employees.ToList() as IList<EmployeeModel>));
        var departments = _fixture.CreateMany<DepartmentModel>(10);
        _departmentService.Setup(service => service.GetAll()).Returns(Task.FromResult(departments.ToList() as IList<DepartmentModel>));

        var updateEmployeePage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.Employee.EmployeeUpdate>();

        // Act
        var employeeForm = updateEmployeePage.Find("form");
        await employeeForm.SubmitAsync();

        // Assert
        _employeeService.Verify(service => service.Add(It.IsAny<EmployeeModel>()), Times.Once);
    }

}
