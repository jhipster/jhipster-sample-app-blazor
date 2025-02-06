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
using JhipsterSampleApplication.Client.Services.EntityServices.PieceOfWork;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.PieceOfWork;

public class PieceOfWorkUpdateTest : TestContext
{
    private readonly Mock<IPieceOfWorkService> _pieceOfWorkService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public PieceOfWorkUpdateTest()
    {
        _pieceOfWorkService = new Mock<IPieceOfWorkService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
        _navigationService = new Mock<INavigationService>();
        Services.AddSingleton<INavigationService>(_navigationService.Object);
        Services.AddSingleton<IPieceOfWorkService>(_pieceOfWorkService.Object);
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
    public async Task Should_UpdateSelectedPieceOfWork_When_FormIsSubmitWithId()
    {
        //Arrange

        var pieceOfWorkToUpdate = _fixture.Create<PieceOfWorkModel>();
        pieceOfWorkToUpdate.Id = 1L;

        _pieceOfWorkService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(pieceOfWorkToUpdate));

        var updatePieceOfWorkPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.PieceOfWork.PieceOfWorkUpdate>(ComponentParameter.CreateParameter("Id", 1L));

        // Act
        var pieceOfWorkForm = updatePieceOfWorkPage.Find("form");
        await pieceOfWorkForm.SubmitAsync();

        // Assert
        _pieceOfWorkService.Verify(service => service.Update(pieceOfWorkToUpdate), Times.Once);
    }

    [Fact]
    public async Task Should_AddPieceOfWork_When_FormIsSubmitWithoutId()
    {
        //Arrange

        var updatePieceOfWorkPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.PieceOfWork.PieceOfWorkUpdate>();

        // Act
        var pieceOfWorkForm = updatePieceOfWorkPage.Find("form");
        await pieceOfWorkForm.SubmitAsync();

        // Assert
        _pieceOfWorkService.Verify(service => service.Add(It.IsAny<PieceOfWorkModel>()), Times.Once);
    }

}
