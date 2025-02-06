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
using JhipsterSampleApplication.Client.Services.EntityServices.Location;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.Location;

public class LocationTest : TestContext
{
    private readonly Mock<ILocationService> _locationService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public LocationTest()
    {
        _locationService = new Mock<ILocationService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
        Services.AddSingleton<ILocationService>(_locationService.Object);
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
    public void Should_DisplayAllLocations_When_LocationsArePresent()
    {
        //Arrange
        var locations = _fixture.CreateMany<LocationModel>(10);
        _locationService.Setup(service => service.GetAll()).Returns(Task.FromResult(locations.ToList() as IList<LocationModel>));
        var locationPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.Location.Location>();

        // Act
        var locationsTableBody = locationPage.Find("tbody");

        // Assert
        locationsTableBody.ChildElementCount.Should().Be(10);
    }

    [Fact]
    public void Should_DisplayNoCountry_When_LocationsLengthIsZero()
    {
        //Arrange
        var locations = new List<LocationModel>();
        _locationService.Setup(service => service.GetAll()).Returns(Task.FromResult(locations.ToList() as IList<LocationModel>));
        var locationPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.Location.Location>();

        // Act
        var span = locationPage.Find("div>span");

        // Assert
        span.MarkupMatches("<span>No Locations found</span>");
    }

    [Fact]
    public async Task Should_DeleteLocation_WhenDeleteButtonClicked()
    {
        //Arrange
        var locations = _fixture.CreateMany<LocationModel>(10);
        _locationService.Setup(service => service.GetAll()).Returns(Task.FromResult(locations.ToList() as IList<LocationModel>));

        var modalRef = new Mock<IModalReference>();
        modalRef.Setup(mock => mock.Result).Returns(Task.FromResult(ModalResult.Ok(new { })));
        _modalService.Setup(service => service.Show<DeleteModal>(It.IsAny<string>())).Returns(modalRef.Object);
        var locationPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.Location.Location>(ComponentParameterFactory.CascadingValue(_modalService.Object));

        // Act
        var locationToDelete = locations.First();

        // Assert
        await locationPage.Find("td>div>button").ClickAsync(new MouseEventArgs());
        _locationService.Verify(service => service.Delete(locationToDelete.Id.ToString()), Times.Once);
        var locationsTableBody = locationPage.Find("tbody");
        locationsTableBody.ChildElementCount.Should().Be(9);
    }

}
