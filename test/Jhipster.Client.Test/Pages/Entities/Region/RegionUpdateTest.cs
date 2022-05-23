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
using Jhipster.Client.Services.EntityServices.Region;
using Jhipster.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.Region
{
    public class RegionUpdateTest : TestContext
    {
        private readonly Mock<IRegionService> _regionService;
        private readonly Mock<IModalService> _modalService;
        private readonly Mock<INavigationService> _navigationService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public RegionUpdateTest()
        {
            _regionService = new Mock<IRegionService>();
            _modalService = new Mock<IModalService>();
            _navigationService = new Mock<INavigationService>();
            Services.AddSingleton<INavigationService>(_navigationService.Object);
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

            string BlazoriseVersion = "1.0.4.0";
            var moduleInterop = JSInterop.SetupModule("./_content/Blazorise/select.js?v=" + BlazoriseVersion);
            moduleInterop.Mode = JSRuntimeMode.Loose;
        }

        [Fact]
        public void Should_UpdateSelectedRegion_When_FormIsSubmitWithId()
        {
            //Arrange

            var regionToUpdate = _fixture.Create<RegionModel>();
            regionToUpdate.Id = 1;

            _regionService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(regionToUpdate));

            var updateRegionPage = RenderComponent<Jhipster.Client.Pages.Entities.Region.RegionUpdate>(ComponentParameter.CreateParameter("Id", 1));

            // Act
            var regionForm = updateRegionPage.Find("form");
            regionForm.Submit();

            // Assert
            _regionService.Verify(service => service.Update(regionToUpdate), Times.Once);
        }

        [Fact]
        public void Should_AddRegion_When_FormIsSubmitWithoutId()
        {
            //Arrange

            var updateRegionPage = RenderComponent<Jhipster.Client.Pages.Entities.Region.RegionUpdate>(ComponentParameter.CreateParameter("Id", 0));

            // Act
            var regionForm = updateRegionPage.Find("form");
            regionForm.Submit();

            // Assert
            _regionService.Verify(service => service.Add(It.IsAny<RegionModel>()), Times.Once);
        }

    }
}
