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
using Jhipster.Client.Services.EntityServices.PieceOfWork;
using Jhipster.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.PieceOfWork
{
    public class PieceOfWorkUpdateTest : TestContext
    {
        private readonly Mock<IPieceOfWorkService> _pieceOfWorkService;
        private readonly Mock<IModalService> _modalService;
        private readonly Mock<INavigationService> _navigationService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public PieceOfWorkUpdateTest()
        {
            _pieceOfWorkService = new Mock<IPieceOfWorkService>();
            _modalService = new Mock<IModalService>();
            _navigationService = new Mock<INavigationService>();
            Services.AddSingleton<INavigationService>(_navigationService.Object);
            Services.AddSingleton<IPieceOfWorkService>(_pieceOfWorkService.Object);
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
        public void Should_UpdateSelectedPieceOfWork_When_FormIsSubmitWithId()
        {
            //Arrange

            var pieceOfWorkToUpdate = _fixture.Create<PieceOfWorkModel>();
            pieceOfWorkToUpdate.Id = 1;

            _pieceOfWorkService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(pieceOfWorkToUpdate));

            var updatePieceOfWorkPage = RenderComponent<Jhipster.Client.Pages.Entities.PieceOfWork.PieceOfWorkUpdate>(ComponentParameter.CreateParameter("Id", 1));

            // Act
            var pieceOfWorkForm = updatePieceOfWorkPage.Find("form");
            pieceOfWorkForm.Submit();

            // Assert
            _pieceOfWorkService.Verify(service => service.Update(pieceOfWorkToUpdate), Times.Once);
        }

        [Fact]
        public void Should_AddPieceOfWork_When_FormIsSubmitWithoutId()
        {
            //Arrange

            var updatePieceOfWorkPage = RenderComponent<Jhipster.Client.Pages.Entities.PieceOfWork.PieceOfWorkUpdate>(ComponentParameter.CreateParameter("Id", 0));

            // Act
            var pieceOfWorkForm = updatePieceOfWorkPage.Find("form");
            pieceOfWorkForm.Submit();

            // Assert
            _pieceOfWorkService.Verify(service => service.Add(It.IsAny<PieceOfWorkModel>()), Times.Once);
        }

    }
}
