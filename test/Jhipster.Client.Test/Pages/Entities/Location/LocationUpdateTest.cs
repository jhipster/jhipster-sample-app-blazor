using System;
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
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Location;
using Jhipster.Client.Services.EntityServices.Country;
using Jhipster.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.Location
{
    public class LocationUpdateTest : TestContext
    {
        private readonly Mock<ILocationService> _locationService;
        private readonly Mock<ICountryService> _countryService;
        private readonly Mock<IModalService> _modalService;
        private readonly Mock<INavigationService> _navigationService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public LocationUpdateTest()
        {
            _locationService = new Mock<ILocationService>();
            _modalService = new Mock<IModalService>();
            _navigationService = new Mock<INavigationService>();
            _countryService = new Mock<ICountryService>();
            Services.AddSingleton<ICountryService>(_countryService.Object);
            Services.AddSingleton<INavigationService>(_navigationService.Object);
            Services.AddSingleton<ILocationService>(_locationService.Object);
            Services.AddSingleton<IModalService>(_modalService.Object);
            Services.AddBlazorise(options =>
                {
                    options.ChangeTextOnKeyPress = true;
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
        public void Should_UpdateSelectedLocation_When_FormIsSubmitWithId()
        {
            //Arrange
            var countries = _fixture.CreateMany<CountryModel>(10);
            _countryService.Setup(service => service.GetAll()).Returns(Task.FromResult(countries.ToList() as IList<CountryModel>));

            var locationToUpdate = _fixture.Create<LocationModel>();
            locationToUpdate.Id = 1;

            _locationService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(locationToUpdate));

            var updateLocationPage = RenderComponent<Jhipster.Client.Pages.Entities.Location.LocationUpdate>(ComponentParameter.CreateParameter("Id", 1));

            // Act
            var locationForm = updateLocationPage.Find("form");
            locationForm.Submit();

            // Assert
            _locationService.Verify(service => service.Update(locationToUpdate), Times.Once);
        }

        [Fact]
        public void Should_AddLocation_When_FormIsSubmitWithoutId()
        {
            //Arrange
            var countries = _fixture.CreateMany<CountryModel>(10);
            _countryService.Setup(service => service.GetAll()).Returns(Task.FromResult(countries.ToList() as IList<CountryModel>));

            var updateLocationPage = RenderComponent<Jhipster.Client.Pages.Entities.Location.LocationUpdate>(ComponentParameter.CreateParameter("Id", 0));

            // Act
            var locationForm = updateLocationPage.Find("form");
            locationForm.Submit();

            // Assert
            _locationService.Verify(service => service.Add(It.IsAny<LocationModel>()), Times.Once);
        }

    }
}
