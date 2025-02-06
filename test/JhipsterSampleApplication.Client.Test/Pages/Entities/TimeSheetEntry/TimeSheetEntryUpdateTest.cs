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
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheetEntry;
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheet;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.TimeSheetEntry;

public class TimeSheetEntryUpdateTest : TestContext
{
    private readonly Mock<ITimeSheetEntryService> _timeSheetEntryService;
    private readonly Mock<ITimeSheetService> _timesheetService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public TimeSheetEntryUpdateTest()
    {
        _timeSheetEntryService = new Mock<ITimeSheetEntryService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
        _navigationService = new Mock<INavigationService>();
        _timesheetService = new Mock<ITimeSheetService>();
        Services.AddSingleton<ITimeSheetService>(_timesheetService.Object);
        Services.AddSingleton<INavigationService>(_navigationService.Object);
        Services.AddSingleton<ITimeSheetEntryService>(_timeSheetEntryService.Object);
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
    public async Task Should_UpdateSelectedTimeSheetEntry_When_FormIsSubmitWithId()
    {
        //Arrange
        var timesheets = _fixture.CreateMany<TimeSheetModel>(10);
        _timesheetService.Setup(service => service.GetAll()).Returns(Task.FromResult(timesheets.ToList() as IList<TimeSheetModel>));

        var timeSheetEntryToUpdate = _fixture.Create<TimeSheetEntryModel>();
        timeSheetEntryToUpdate.Id = 1L;

        _timeSheetEntryService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(timeSheetEntryToUpdate));

        var updateTimeSheetEntryPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.TimeSheetEntry.TimeSheetEntryUpdate>(ComponentParameter.CreateParameter("Id", 1L));

        // Act
        var timeSheetEntryForm = updateTimeSheetEntryPage.Find("form");
        await timeSheetEntryForm.SubmitAsync();

        // Assert
        _timeSheetEntryService.Verify(service => service.Update(timeSheetEntryToUpdate), Times.Once);
    }

    [Fact]
    public async Task Should_AddTimeSheetEntry_When_FormIsSubmitWithoutId()
    {
        //Arrange
        var timesheets = _fixture.CreateMany<TimeSheetModel>(10);
        _timesheetService.Setup(service => service.GetAll()).Returns(Task.FromResult(timesheets.ToList() as IList<TimeSheetModel>));

        var updateTimeSheetEntryPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.TimeSheetEntry.TimeSheetEntryUpdate>();

        // Act
        var timeSheetEntryForm = updateTimeSheetEntryPage.Find("form");
        await timeSheetEntryForm.SubmitAsync();

        // Assert
        _timeSheetEntryService.Verify(service => service.Add(It.IsAny<TimeSheetEntryModel>()), Times.Once);
    }

}
