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
using Jhipster.Client.Pages.Entities.Department;
using Jhipster.Client.Pages.Utils;
using Jhipster.Client.Services.EntityServices.Department;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.Department
{
    public class DepartmentDetailTest : TestContext
    {
        private readonly Mock<IDepartmentService> _departmentService;
        private readonly Mock<INavigationService> _navigationService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public DepartmentDetailTest()
        {
            _departmentService = new Mock<IDepartmentService>();
            _navigationService = new Mock<INavigationService>();
            Services.AddSingleton<IDepartmentService>(_departmentService.Object);
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
        public void Should_DisplayDepartment_When_IdIsPresent()
        {
            //Arrange
            var department = _fixture.Create<DepartmentModel>();
            _departmentService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(department));
            var departmentDetail = RenderComponent<DepartmentDetail>(ComponentParameter.CreateParameter("Id", 1));

            // Act
            var title = departmentDetail.Find("h2");

            // Assert
            title.MarkupMatches($"<h2><span>Department</span>{department.Id}</h2>");

        }

        [Fact]
        public void Should_NotDisplayDepartment_When_IdIsNotPresent()
        {
            //Arrange
            _departmentService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(new DepartmentModel()));
            var departmentDetail = RenderComponent<DepartmentDetail>();

            // Act
            var title = departmentDetail.Find("div.col-8");

            // Assert
            title.Children.Length.Should().Be(0);

        }
    }
}
