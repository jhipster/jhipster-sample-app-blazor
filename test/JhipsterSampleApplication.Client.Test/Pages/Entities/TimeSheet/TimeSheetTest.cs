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
using JhipsterSampleApplication.Client.Services.EntityServices.TimeSheet;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.TimeSheet;

public class TimeSheetTest : TestContext
{
    private readonly Mock<ITimeSheetService> _timeSheetService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public TimeSheetTest()
    {
        _timeSheetService = new Mock<ITimeSheetService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
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
    public void Should_DisplayAllTimeSheets_When_TimeSheetsArePresent()
    {
        //Arrange
        var timeSheets = _fixture.CreateMany<TimeSheetModel>(10);
        _timeSheetService.Setup(service => service.GetAll()).Returns(Task.FromResult(timeSheets.ToList() as IList<TimeSheetModel>));
        var timeSheetPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.TimeSheet.TimeSheet>();

        // Act
        var timeSheetsTableBody = timeSheetPage.Find("tbody");

        // Assert
        timeSheetsTableBody.ChildElementCount.Should().Be(10);
    }

    [Fact]
    public void Should_DisplayNoCountry_When_TimeSheetsLengthIsZero()
    {
        //Arrange
        var timeSheets = new List<TimeSheetModel>();
        _timeSheetService.Setup(service => service.GetAll()).Returns(Task.FromResult(timeSheets.ToList() as IList<TimeSheetModel>));
        var timeSheetPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.TimeSheet.TimeSheet>();

        // Act
        var span = timeSheetPage.Find("div>span");

        // Assert
        span.MarkupMatches("<span>No TimeSheets found</span>");
    }

    [Fact]
    public async Task Should_DeleteTimeSheet_WhenDeleteButtonClicked()
    {
        //Arrange
        var timeSheets = _fixture.CreateMany<TimeSheetModel>(10);
        _timeSheetService.Setup(service => service.GetAll()).Returns(Task.FromResult(timeSheets.ToList() as IList<TimeSheetModel>));

        var modalRef = new Mock<IModalReference>();
        modalRef.Setup(mock => mock.Result).Returns(Task.FromResult(ModalResult.Ok(new { })));
        _modalService.Setup(service => service.Show<DeleteModal>(It.IsAny<string>())).Returns(modalRef.Object);
        var timeSheetPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.TimeSheet.TimeSheet>(ComponentParameterFactory.CascadingValue(_modalService.Object));

        // Act
        var timeSheetToDelete = timeSheets.First();

        // Assert
        await timeSheetPage.Find("td>div>button").ClickAsync(new MouseEventArgs());
        _timeSheetService.Verify(service => service.Delete(timeSheetToDelete.Id.ToString()), Times.Once);
        var timeSheetsTableBody = timeSheetPage.Find("tbody");
        timeSheetsTableBody.ChildElementCount.Should().Be(9);
    }

}
