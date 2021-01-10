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
using Jhipster.Client.Services.EntityServices.PieceOfWork;
using Jhipster.Client.Shared;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.PieceOfWork
{
    public class PieceOfWorkTest : TestContext
    {
        private readonly Mock<IPieceOfWorkService> _pieceOfWorkService;
        private readonly Mock<IModalService> _modalService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public PieceOfWorkTest()
        {
            _pieceOfWorkService = new Mock<IPieceOfWorkService>();
            _modalService = new Mock<IModalService>();
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
        public void Should_DisplayAllPieceOfWorks_When_PieceOfWorksArePresent()
        {
            //Arrange
            var pieceOfWorks = _fixture.CreateMany<PieceOfWorkModel>(10);
            _pieceOfWorkService.Setup(service => service.GetAll()).Returns(Task.FromResult(pieceOfWorks.ToList() as IList<PieceOfWorkModel>));
            var pieceOfWorkPage = RenderComponent<Jhipster.Client.Pages.Entities.PieceOfWork.PieceOfWork>();

            // Act
            var pieceOfWorksTableBody = pieceOfWorkPage.Find("tbody");

            // Assert
            pieceOfWorksTableBody.ChildElementCount.Should().Be(10);
        }

        [Fact]
        public void Should_DisplayNoCountry_When_PieceOfWorksLengthIsZero()
        {
            //Arrange
            var pieceOfWorks = new List<PieceOfWorkModel>();
            _pieceOfWorkService.Setup(service => service.GetAll()).Returns(Task.FromResult(pieceOfWorks.ToList() as IList<PieceOfWorkModel>));
            var pieceOfWorkPage = RenderComponent<Jhipster.Client.Pages.Entities.PieceOfWork.PieceOfWork>();

            // Act
            var span = pieceOfWorkPage.Find("div>span");

            // Assert
            span.MarkupMatches("<span>No PieceOfWorks found</span>");
        }

        [Fact]
        public void Should_DeletePieceOfWork_WhenDeleteButtonClicked()
        {
            //Arrange
            var pieceOfWorks = _fixture.CreateMany<PieceOfWorkModel>(10);
            _pieceOfWorkService.Setup(service => service.GetAll()).Returns(Task.FromResult(pieceOfWorks.ToList() as IList<PieceOfWorkModel>));

            var modalRef = new Mock<IModalReference>();
            modalRef.Setup(mock => mock.Result).Returns(Task.FromResult(ModalResult.Ok(new { })));
            _modalService.Setup(service => service.Show<DeleteModal>(It.IsAny<string>())).Returns(modalRef.Object);
            var pieceOfWorkPage = RenderComponent<Jhipster.Client.Pages.Entities.PieceOfWork.PieceOfWork>(ComponentParameterFactory.CascadingValue(_modalService.Object));

            // Act
            var pieceOfWorkToDelete = pieceOfWorks.First();

            // Assert
            pieceOfWorkPage.Find("td>div>button").Click();
            _pieceOfWorkService.Verify(service => service.Delete(pieceOfWorkToDelete.Id.ToString()), Times.Once);
            var pieceOfWorksTableBody = pieceOfWorkPage.Find("tbody");
            pieceOfWorksTableBody.ChildElementCount.Should().Be(9);
        }

    }
}
