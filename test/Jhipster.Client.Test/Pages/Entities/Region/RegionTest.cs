using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Blazored.Modal;
using Blazored.Modal.Services;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Bunit;
using Bunit.Rendering;
using FluentAssertions;
using Jhipster.Client.Models;
using Jhipster.Client.Services.EntityServices.Region;
using Jhipster.Client.Shared;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.Region
{
    public class RegionTest : TestContext
    {
        private readonly Mock<IRegionService> _regionService;
        private readonly Mock<IModalService> _modalService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public RegionTest()
        {
            _regionService = new Mock<IRegionService>();
            _modalService = new Mock<IModalService>();
            Services.AddSingleton<IRegionService>(_regionService.Object);
            Services.AddSingleton<IModalService>(_modalService.Object);
            Services.AddBlazorise(options =>
                {
                    options.Immediate = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();
            Services.AddHttpClientInterceptor();
            //This code is needed to support recursion
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        }

        [Fact]
        public void Should_DisplayAllRegions_When_RegionsArePresent()
        {
            //Arrange
            var regions = _fixture.CreateMany<RegionModel>(10);
            _regionService.Setup(service => service.GetAll()).Returns(Task.FromResult(regions.ToList() as IList<RegionModel>));
            var regionPage = RenderComponent<Jhipster.Client.Pages.Entities.Region.Region>();

            // Act
            var regionsTableBody = regionPage.Find("tbody");

            // Assert
            regionsTableBody.ChildElementCount.Should().Be(10);
        }

        [Fact]
        public void Should_DisplayNoCountry_When_RegionsLengthIsZero()
        {
            //Arrange
            var regions = new List<RegionModel>();
            _regionService.Setup(service => service.GetAll()).Returns(Task.FromResult(regions.ToList() as IList<RegionModel>));
            var regionPage = RenderComponent<Jhipster.Client.Pages.Entities.Region.Region>();

            // Act
            var span = regionPage.Find("div>span");

            // Assert
            span.MarkupMatches("<span>No Regions found</span>");
        }

        [Fact]
        public void Should_DeleteRegion_WhenDeleteButtonClicked()
        {
            //Arrange
            var regions = _fixture.CreateMany<RegionModel>(10);
            _regionService.Setup(service => service.GetAll()).Returns(Task.FromResult(regions.ToList() as IList<RegionModel>));

            var modalRef = new Mock<IModalReference>();
            modalRef.Setup(mock => mock.Result).Returns(Task.FromResult(ModalResult.Ok(new { })));
            _modalService.Setup(service => service.Show<DeleteModal>(It.IsAny<string>())).Returns(modalRef.Object);
            var regionPage = RenderComponent<Jhipster.Client.Pages.Entities.Region.Region>(ComponentParameterFactory.CascadingValue(_modalService.Object));

            // Act
            var regionToDelete = regions.First();

            // Assert
            regionPage.Find("td>div>button").Click();
            _regionService.Verify(service => service.Delete(regionToDelete.Id.ToString()), Times.Once);
            var regionsTableBody = regionPage.Find("tbody");
            regionsTableBody.ChildElementCount.Should().Be(9);
        }

    }
}
