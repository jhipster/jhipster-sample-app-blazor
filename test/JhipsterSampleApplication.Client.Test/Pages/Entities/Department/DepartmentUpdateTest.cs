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
using JhipsterSampleApplication.Client.Services.EntityServices.Department;
using JhipsterSampleApplication.Client.Services.EntityServices.Location;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.Department;

public class DepartmentUpdateTest : TestContext
{
    private readonly Mock<IDepartmentService> _departmentService;
    private readonly Mock<ILocationService> _locationService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public DepartmentUpdateTest()
    {
        _departmentService = new Mock<IDepartmentService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
        _navigationService = new Mock<INavigationService>();
        _locationService = new Mock<ILocationService>();
        Services.AddSingleton<ILocationService>(_locationService.Object);
        Services.AddSingleton<INavigationService>(_navigationService.Object);
        Services.AddSingleton<IDepartmentService>(_departmentService.Object);
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
    public async Task Should_UpdateSelectedDepartment_When_FormIsSubmitWithId()
    {
        //Arrange
        var locations = _fixture.CreateMany<LocationModel>(10);
        _locationService.Setup(service => service.GetAll()).Returns(Task.FromResult(locations.ToList() as IList<LocationModel>));

        var departmentToUpdate = _fixture.Create<DepartmentModel>();
        departmentToUpdate.Id = 1L;

        _departmentService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(departmentToUpdate));

        var updateDepartmentPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.Department.DepartmentUpdate>(ComponentParameter.CreateParameter("Id", 1L));

        // Act
        var departmentForm = updateDepartmentPage.Find("form");
        await departmentForm.SubmitAsync();

        // Assert
        _departmentService.Verify(service => service.Update(departmentToUpdate), Times.Once);
    }

    [Fact]
    public async Task Should_AddDepartment_When_FormIsSubmitWithoutId()
    {
        //Arrange
        var locations = _fixture.CreateMany<LocationModel>(10);
        _locationService.Setup(service => service.GetAll()).Returns(Task.FromResult(locations.ToList() as IList<LocationModel>));

        var updateDepartmentPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.Department.DepartmentUpdate>();

        // Act
        var departmentForm = updateDepartmentPage.Find("form");
        await departmentForm.SubmitAsync();

        // Assert
        _departmentService.Verify(service => service.Add(It.IsAny<DepartmentModel>()), Times.Once);
    }

}
