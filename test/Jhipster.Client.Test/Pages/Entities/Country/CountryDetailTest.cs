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
using Jhipster.Client.Pages.Entities.Country;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Country;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.Country
{
    public class CountryDetailTest : TestContext
    {
        private readonly Mock<ICountryService> _countryService;
        private readonly Mock<INavigationService> _navigationService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public CountryDetailTest()
        {
            _countryService = new Mock<ICountryService>();
            _navigationService = new Mock<INavigationService>();
            Services.AddSingleton<ICountryService>(_countryService.Object);
            Services.AddSingleton<INavigationService>(_navigationService.Object);
            Services.AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();
            //This code is needed to support recursion
            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
                .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        }

        [Fact]
        public void Should_DisplayCountry_When_IdIsPresent()
        {
            //Arrange
            var country = _fixture.Create<CountryModel>();
            _countryService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(country));
            var countryDetail = RenderComponent<CountryDetail>(ComponentParameter.CreateParameter("Id", 1));

            // Act
            var title = countryDetail.Find("h2");

            // Assert
            title.MarkupMatches($"<h2><span>Country</span>{country.Id}</h2>");

        }

        [Fact]
        public void Should_NotDisplayCountry_When_IdIsNotPresent()
        {
            //Arrange
            _countryService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(new CountryModel()));
            var countryDetail = RenderComponent<CountryDetail>();

            // Act
            var title = countryDetail.Find("div.col-8");

            // Assert
            title.Children.Length.Should().Be(0);

        }
    }
}
