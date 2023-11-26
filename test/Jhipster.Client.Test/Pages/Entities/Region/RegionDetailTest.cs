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
using Jhipster.Client.Pages.Entities.Region;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Region;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.Region;

public class RegionDetailTest : TestContext
{
    private readonly Mock<IRegionService> _regionService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public RegionDetailTest()
    {
        _regionService = new Mock<IRegionService>();
        _navigationService = new Mock<INavigationService>();
        Services.AddSingleton<IRegionService>(_regionService.Object);
        Services.AddSingleton<INavigationService>(_navigationService.Object);
        Services.AddBlazorise(options =>
            {
                options.Immediate = true;
            })
            .Replace(ServiceDescriptor.Transient<IComponentActivator, ComponentActivator>())
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();
        //This code is needed to support recursion
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void Should_DisplayRegion_When_IdIsPresent()
    {
        //Arrange
        var region = _fixture.Create<RegionModel>();
        _regionService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(region));
        var regionDetail = RenderComponent<RegionDetail>(ComponentParameter.CreateParameter("Id", 1L));

        // Act
        var title = regionDetail.Find("h2");

        // Assert
        title.MarkupMatches($"<h2><span>Region</span>{region.Id}</h2>");

    }

    [Fact]
    public void Should_NotDisplayRegion_When_IdIsNotPresent()
    {
        //Arrange
        _regionService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(new RegionModel()));
        var regionDetail = RenderComponent<RegionDetail>();

        // Act
        var title = regionDetail.Find("div.col-8");

        // Assert
        title.Children.Length.Should().Be(0);

    }
}
