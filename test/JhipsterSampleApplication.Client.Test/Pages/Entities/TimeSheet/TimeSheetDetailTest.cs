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
using JhipsterSampleApplication.Client.Pages.Entities.TimeSheet;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheet;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.TimeSheet;

public class TimeSheetDetailTest : TestContext
{
    private readonly Mock<ITimeSheetService> _timesheetService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public TimeSheetDetailTest()
    {
        _timesheetService = new Mock<ITimeSheetService>();
        _navigationService = new Mock<INavigationService>();
        Services.AddSingleton<ITimeSheetService>(_timesheetService.Object);
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
    public void Should_DisplayTimeSheet_When_IdIsPresent()
    {
        //Arrange
        var timesheet = _fixture.Create<TimeSheetModel>();
        _timesheetService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(timesheet));
        var timesheetDetail = RenderComponent<TimeSheetDetail>(ComponentParameter.CreateParameter("Id", timesheet.Id));

        // Act
        var title = timesheetDetail.Find("h2");

        // Assert
        title.MarkupMatches($"<h2><span>TimeSheet</span>{timesheet.Id}</h2>");

    }

    [Fact]
    public void Should_NotDisplayTimeSheet_When_IdIsNotPresent()
    {
        //Arrange
        _timesheetService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(new TimeSheetModel()));
        var timesheetDetail = RenderComponent<TimeSheetDetail>();

        // Act
        var title = timesheetDetail.Find("div.col-8");

        // Assert
        title.Children.Length.Should().Be(0);

    }
}
