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
using Jhipster.Client.Services.EntityServices.Country;
using Jhipster.Client.Shared;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.Country
{
    public class CountryTest : TestContext
    {
        private readonly Mock<ICountryService> _countryService;
        private readonly Mock<IModalService> _modalService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public CountryTest()
        {
            _countryService = new Mock<ICountryService>();
            _modalService = new Mock<IModalService>();
            Services.AddSingleton<ICountryService>(_countryService.Object);
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
        public void Should_DisplayAllCountries_When_CountriesArePresent()
        {
            //Arrange
            var countries = _fixture.CreateMany<CountryModel>(10);
            _countryService.Setup(service => service.GetAll()).Returns(Task.FromResult(countries.ToList() as IList<CountryModel>));
            var countryPage = RenderComponent<Jhipster.Client.Pages.Entities.Country.Country>();

            // Act
            var countriesTableBody = countryPage.Find("tbody");

            // Assert
            countriesTableBody.ChildElementCount.Should().Be(10);
        }

        [Fact]
        public void Should_DisplayNoCountry_When_CountriesLengthIsZero()
        {
            //Arrange
            var countries = new List<CountryModel>();
            _countryService.Setup(service => service.GetAll()).Returns(Task.FromResult(countries.ToList() as IList<CountryModel>));
            var countryPage = RenderComponent<Jhipster.Client.Pages.Entities.Country.Country>();

            // Act
            var span = countryPage.Find("div>span");

            // Assert
            span.MarkupMatches("<span>No Countries found</span>");
        }

        [Fact]
        public void Should_DeleteCountry_WhenDeleteButtonClicked()
        {
            //Arrange
            var countries = _fixture.CreateMany<CountryModel>(10);
            _countryService.Setup(service => service.GetAll()).Returns(Task.FromResult(countries.ToList() as IList<CountryModel>));

            var modalRef = new Mock<IModalReference>();
            modalRef.Setup(mock => mock.Result).Returns(Task.FromResult(ModalResult.Ok(new { })));
            _modalService.Setup(service => service.Show<DeleteModal>(It.IsAny<string>())).Returns(modalRef.Object);
            var countryPage = RenderComponent<Jhipster.Client.Pages.Entities.Country.Country>(ComponentParameterFactory.CascadingValue(_modalService.Object));

            // Act
            var countryToDelete = countries.First();

            // Assert
            countryPage.Find("td>div>button").Click();
            _countryService.Verify(service => service.Delete(countryToDelete.Id.ToString()), Times.Once);
            var countriesTableBody = countryPage.Find("tbody");
            countriesTableBody.ChildElementCount.Should().Be(9);
        }

    }
}
