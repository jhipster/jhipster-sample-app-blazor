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
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheet;
using JhipsterSampleApplication.Client.Services.EntityServices.Employee;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.TimeSheet;

public class TimeSheetUpdateTest : TestContext
{
    private readonly Mock<ITimeSheetService> _timeSheetService;
    private readonly Mock<IEmployeeService> _employeeService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public TimeSheetUpdateTest()
    {
        _timeSheetService = new Mock<ITimeSheetService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
        _navigationService = new Mock<INavigationService>();
        _employeeService = new Mock<IEmployeeService>();
        Services.AddSingleton<IEmployeeService>(_employeeService.Object);
        Services.AddSingleton<INavigationService>(_navigationService.Object);
        Services.AddSingleton<ITimeSheetService>(_timeSheetService.Object);
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
    public async Task Should_UpdateSelectedTimeSheet_When_FormIsSubmitWithId()
    {
        //Arrange
        var employees = _fixture.CreateMany<EmployeeModel>(10);
        _employeeService.Setup(service => service.GetAll()).Returns(Task.FromResult(employees.ToList() as IList<EmployeeModel>));

        var timeSheetToUpdate = _fixture.Create<TimeSheetModel>();
        timeSheetToUpdate.Id = Guid.NewGuid();

        _timeSheetService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(timeSheetToUpdate));

        var updateTimeSheetPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.TimeSheet.TimeSheetUpdate>(ComponentParameter.CreateParameter("Id", Guid.NewGuid()));

        // Act
        var timeSheetForm = updateTimeSheetPage.Find("form");
        await timeSheetForm.SubmitAsync();

        // Assert
        _timeSheetService.Verify(service => service.Update(timeSheetToUpdate), Times.Once);
    }

    [Fact]
    public async Task Should_AddTimeSheet_When_FormIsSubmitWithoutId()
    {
        //Arrange
        var employees = _fixture.CreateMany<EmployeeModel>(10);
        _employeeService.Setup(service => service.GetAll()).Returns(Task.FromResult(employees.ToList() as IList<EmployeeModel>));

        var updateTimeSheetPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.TimeSheet.TimeSheetUpdate>();

        // Act
        var timeSheetForm = updateTimeSheetPage.Find("form");
        await timeSheetForm.SubmitAsync();

        // Assert
        _timeSheetService.Verify(service => service.Add(It.IsAny<TimeSheetModel>()), Times.Once);
    }

}
