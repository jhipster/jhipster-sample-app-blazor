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
using Jhipster.Client.Pages.Entities.PieceOfWork;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.PieceOfWork;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.PieceOfWork
{
    public class PieceOfWorkDetailTest : TestContext
    {
        private readonly Mock<IPieceOfWorkService> _pieceofworkService;
        private readonly Mock<INavigationService> _navigationService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public PieceOfWorkDetailTest()
        {
            _pieceofworkService = new Mock<IPieceOfWorkService>();
            _navigationService = new Mock<INavigationService>();
            Services.AddSingleton<IPieceOfWorkService>(_pieceofworkService.Object);
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
        public void Should_DisplayPieceOfWork_When_IdIsPresent()
        {
            //Arrange
            var pieceofwork = _fixture.Create<PieceOfWorkModel>();
            _pieceofworkService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(pieceofwork));
            var pieceofworkDetail = RenderComponent<PieceOfWorkDetail>(ComponentParameter.CreateParameter("Id", 1));

            // Act
            var title = pieceofworkDetail.Find("h2");

            // Assert
            title.MarkupMatches($"<h2><span>PieceOfWork</span>{pieceofwork.Id}</h2>");

        }

        [Fact]
        public void Should_NotDisplayPieceOfWork_When_IdIsNotPresent()
        {
            //Arrange
            _pieceofworkService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(new PieceOfWorkModel()));
            var pieceofworkDetail = RenderComponent<PieceOfWorkDetail>();

            // Act
            var title = pieceofworkDetail.Find("div.col-8");

            // Assert
            title.Children.Length.Should().Be(0);

        }
    }
}
