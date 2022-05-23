using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Bunit;
using Bunit.Rendering;
using FluentAssertions;
using Jhipster.Client.Models;
using Jhipster.Client.Pages.Entities.Location;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Location;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.Location
{
    public class LocationDetailTest : TestContext
    {
        private readonly Mock<ILocationService> _locationService;
        private readonly Mock<INavigationService> _navigationService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public LocationDetailTest()
        {
            _locationService = new Mock<ILocationService>();
            _navigationService = new Mock<INavigationService>();
            Services.AddSingleton<ILocationService>(_locationService.Object);
            Services.AddSingleton<INavigationService>(_navigationService.Object);
            Services.AddBlazorise(options =>
                {
                    options.Immediate = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();
            //This code is needed to support recursion
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        }

        [Fact]
        public void Should_DisplayLocation_When_IdIsPresent()
        {
            //Arrange
            var location = _fixture.Create<LocationModel>();
            _locationService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(location));
            var locationDetail = RenderComponent<LocationDetail>(ComponentParameter.CreateParameter("Id", 1));

            // Act
            var title = locationDetail.Find("h2");

            // Assert
            title.MarkupMatches($"<h2><span>Location</span>{location.Id}</h2>");

        }

        [Fact]
        public void Should_NotDisplayLocation_When_IdIsNotPresent()
        {
            //Arrange
            _locationService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(new LocationModel()));
            var locationDetail = RenderComponent<LocationDetail>();

            // Act
            var title = locationDetail.Find("div.col-8");

            // Assert
            title.Children.Length.Should().Be(0);

        }
    }
}
