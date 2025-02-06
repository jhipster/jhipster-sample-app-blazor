using System;
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
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.Region;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.Region;

public class RegionUpdateTest : TestContext
{
    private readonly Mock<IRegionService> _regionService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public RegionUpdateTest()
    {
        _regionService = new Mock<IRegionService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
        _navigationService = new Mock<INavigationService>();
        Services.AddSingleton<INavigationService>(_navigationService.Object);
        Services.AddSingleton<IRegionService>(_regionService.Object);
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
    public async Task Should_UpdateSelectedRegion_When_FormIsSubmitWithId()
    {
        //Arrange

        var regionToUpdate = _fixture.Create<RegionModel>();
        regionToUpdate.Id = 1L;

        _regionService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(regionToUpdate));

        var updateRegionPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.Region.RegionUpdate>(ComponentParameter.CreateParameter("Id", 1L));

        // Act
        var regionForm = updateRegionPage.Find("form");
        await regionForm.SubmitAsync();

        // Assert
        _regionService.Verify(service => service.Update(regionToUpdate), Times.Once);
    }

    [Fact]
    public async Task Should_AddRegion_When_FormIsSubmitWithoutId()
    {
        //Arrange

        var updateRegionPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.Region.RegionUpdate>();

        // Act
        var regionForm = updateRegionPage.Find("form");
        await regionForm.SubmitAsync();

        // Assert
        _regionService.Verify(service => service.Add(It.IsAny<RegionModel>()), Times.Once);
    }

}
