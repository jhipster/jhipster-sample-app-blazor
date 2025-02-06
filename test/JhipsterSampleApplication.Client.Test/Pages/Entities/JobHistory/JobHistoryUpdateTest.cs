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
using JhipsterSampleApplication.Client.Services.EntityServices.JobHistory;
using JhipsterSampleApplication.Client.Services.EntityServices.Job;
using JhipsterSampleApplication.Client.Services.EntityServices.Department;
using JhipsterSampleApplication.Client.Services.EntityServices.Employee;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.JobHistory;

public class JobHistoryUpdateTest : TestContext
{
    private readonly Mock<IJobHistoryService> _jobHistoryService;
    private readonly Mock<IJobService> _jobService;
    private readonly Mock<IDepartmentService> _departmentService;
    private readonly Mock<IEmployeeService> _employeeService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public JobHistoryUpdateTest()
    {
        _jobHistoryService = new Mock<IJobHistoryService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
        _navigationService = new Mock<INavigationService>();
        _jobService = new Mock<IJobService>();
        Services.AddSingleton<IJobService>(_jobService.Object);
        _departmentService = new Mock<IDepartmentService>();
        Services.AddSingleton<IDepartmentService>(_departmentService.Object);
        _employeeService = new Mock<IEmployeeService>();
        Services.AddSingleton<IEmployeeService>(_employeeService.Object);
        Services.AddSingleton<INavigationService>(_navigationService.Object);
        Services.AddSingleton<IJobHistoryService>(_jobHistoryService.Object);
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
    public async Task Should_UpdateSelectedJobHistory_When_FormIsSubmitWithId()
    {
        //Arrange
        var jobs = _fixture.CreateMany<JobModel>(10);
        _jobService.Setup(service => service.GetAll()).Returns(Task.FromResult(jobs.ToList() as IList<JobModel>));
        var departments = _fixture.CreateMany<DepartmentModel>(10);
        _departmentService.Setup(service => service.GetAll()).Returns(Task.FromResult(departments.ToList() as IList<DepartmentModel>));
        var employees = _fixture.CreateMany<EmployeeModel>(10);
        _employeeService.Setup(service => service.GetAll()).Returns(Task.FromResult(employees.ToList() as IList<EmployeeModel>));

        var jobHistoryToUpdate = _fixture.Create<JobHistoryModel>();
        jobHistoryToUpdate.Id = 1L;

        _jobHistoryService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(jobHistoryToUpdate));

        var updateJobHistoryPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.JobHistory.JobHistoryUpdate>(ComponentParameter.CreateParameter("Id", 1L));

        // Act
        var jobHistoryForm = updateJobHistoryPage.Find("form");
        await jobHistoryForm.SubmitAsync();

        // Assert
        _jobHistoryService.Verify(service => service.Update(jobHistoryToUpdate), Times.Once);
    }

    [Fact]
    public async Task Should_AddJobHistory_When_FormIsSubmitWithoutId()
    {
        //Arrange
        var jobs = _fixture.CreateMany<JobModel>(10);
        _jobService.Setup(service => service.GetAll()).Returns(Task.FromResult(jobs.ToList() as IList<JobModel>));
        var departments = _fixture.CreateMany<DepartmentModel>(10);
        _departmentService.Setup(service => service.GetAll()).Returns(Task.FromResult(departments.ToList() as IList<DepartmentModel>));
        var employees = _fixture.CreateMany<EmployeeModel>(10);
        _employeeService.Setup(service => service.GetAll()).Returns(Task.FromResult(employees.ToList() as IList<EmployeeModel>));

        var updateJobHistoryPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.JobHistory.JobHistoryUpdate>();

        // Act
        var jobHistoryForm = updateJobHistoryPage.Find("form");
        await jobHistoryForm.SubmitAsync();

        // Assert
        _jobHistoryService.Verify(service => service.Add(It.IsAny<JobHistoryModel>()), Times.Once);
    }

}
