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
using JhipsterSampleApplication.Client.Pages.Entities.TimeSheetEntry;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheetEntry;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.TimeSheetEntry;

public class TimeSheetEntryDetailTest : TestContext
{
    private readonly Mock<ITimeSheetEntryService> _timesheetentryService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public TimeSheetEntryDetailTest()
    {
        _timesheetentryService = new Mock<ITimeSheetEntryService>();
        _navigationService = new Mock<INavigationService>();
        Services.AddSingleton<ITimeSheetEntryService>(_timesheetentryService.Object);
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
    public void Should_DisplayTimeSheetEntry_When_IdIsPresent()
    {
        //Arrange
        var timesheetentry = _fixture.Create<TimeSheetEntryModel>();
        _timesheetentryService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(timesheetentry));
        var timesheetentryDetail = RenderComponent<TimeSheetEntryDetail>(ComponentParameter.CreateParameter("Id", timesheetentry.Id));

        // Act
        var title = timesheetentryDetail.Find("h2");

        // Assert
        title.MarkupMatches($"<h2><span>TimeSheetEntry</span>{timesheetentry.Id}</h2>");

    }

    [Fact]
    public void Should_NotDisplayTimeSheetEntry_When_IdIsNotPresent()
    {
        //Arrange
        _timesheetentryService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(new TimeSheetEntryModel()));
        var timesheetentryDetail = RenderComponent<TimeSheetEntryDetail>();

        // Act
        var title = timesheetentryDetail.Find("div.col-8");

        // Assert
        title.Children.Length.Should().Be(0);

    }
}
