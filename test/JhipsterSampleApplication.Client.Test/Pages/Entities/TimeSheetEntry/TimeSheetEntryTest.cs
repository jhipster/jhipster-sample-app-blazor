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
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheetEntry;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.TimeSheetEntry;

public class TimeSheetEntryTest : TestContext
{
    private readonly Mock<ITimeSheetEntryService> _timeSheetEntryService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public TimeSheetEntryTest()
    {
        _timeSheetEntryService = new Mock<ITimeSheetEntryService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
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
    public void Should_DisplayAllTimeSheetEntries_When_TimeSheetEntriesArePresent()
    {
        //Arrange
        var timeSheetEntries = _fixture.CreateMany<TimeSheetEntryModel>(10);
        _timeSheetEntryService.Setup(service => service.GetAll()).Returns(Task.FromResult(timeSheetEntries.ToList() as IList<TimeSheetEntryModel>));
        var timeSheetEntryPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.TimeSheetEntry.TimeSheetEntry>();

        // Act
        var timeSheetEntriesTableBody = timeSheetEntryPage.Find("tbody");

        // Assert
        timeSheetEntriesTableBody.ChildElementCount.Should().Be(10);
    }

    [Fact]
    public void Should_DisplayNoCountry_When_TimeSheetEntriesLengthIsZero()
    {
        //Arrange
        var timeSheetEntries = new List<TimeSheetEntryModel>();
        _timeSheetEntryService.Setup(service => service.GetAll()).Returns(Task.FromResult(timeSheetEntries.ToList() as IList<TimeSheetEntryModel>));
        var timeSheetEntryPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.TimeSheetEntry.TimeSheetEntry>();

        // Act
        var span = timeSheetEntryPage.Find("div>span");

        // Assert
        span.MarkupMatches("<span>No TimeSheetEntries found</span>");
    }

    [Fact]
    public async Task Should_DeleteTimeSheetEntry_WhenDeleteButtonClicked()
    {
        //Arrange
        var timeSheetEntries = _fixture.CreateMany<TimeSheetEntryModel>(10);
        _timeSheetEntryService.Setup(service => service.GetAll()).Returns(Task.FromResult(timeSheetEntries.ToList() as IList<TimeSheetEntryModel>));

        var modalRef = new Mock<IModalReference>();
        modalRef.Setup(mock => mock.Result).Returns(Task.FromResult(ModalResult.Ok(new { })));
        _modalService.Setup(service => service.Show<DeleteModal>(It.IsAny<string>())).Returns(modalRef.Object);
        var timeSheetEntryPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.TimeSheetEntry.TimeSheetEntry>(ComponentParameterFactory.CascadingValue(_modalService.Object));

        // Act
        var timeSheetEntryToDelete = timeSheetEntries.First();

        // Assert
        await timeSheetEntryPage.Find("td>div>button").ClickAsync(new MouseEventArgs());
        _timeSheetEntryService.Verify(service => service.Delete(timeSheetEntryToDelete.Id.ToString()), Times.Once);
        var timeSheetEntriesTableBody = timeSheetEntryPage.Find("tbody");
        timeSheetEntriesTableBody.ChildElementCount.Should().Be(9);
    }

}
