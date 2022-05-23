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
using Jhipster.Client.Services.EntityServices.Department;
using Jhipster.Client.Shared;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.Department
{
    public class DepartmentTest : TestContext
    {
        private readonly Mock<IDepartmentService> _departmentService;
        private readonly Mock<IModalService> _modalService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public DepartmentTest()
        {
            _departmentService = new Mock<IDepartmentService>();
            _modalService = new Mock<IModalService>();
            Services.AddSingleton<IDepartmentService>(_departmentService.Object);
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
        public void Should_DisplayAllDepartments_When_DepartmentsArePresent()
        {
            //Arrange
            var departments = _fixture.CreateMany<DepartmentModel>(10);
            _departmentService.Setup(service => service.GetAll()).Returns(Task.FromResult(departments.ToList() as IList<DepartmentModel>));
            var departmentPage = RenderComponent<Jhipster.Client.Pages.Entities.Department.Department>();

            // Act
            var departmentsTableBody = departmentPage.Find("tbody");

            // Assert
            departmentsTableBody.ChildElementCount.Should().Be(10);
        }

        [Fact]
        public void Should_DisplayNoCountry_When_DepartmentsLengthIsZero()
        {
            //Arrange
            var departments = new List<DepartmentModel>();
            _departmentService.Setup(service => service.GetAll()).Returns(Task.FromResult(departments.ToList() as IList<DepartmentModel>));
            var departmentPage = RenderComponent<Jhipster.Client.Pages.Entities.Department.Department>();

            // Act
            var span = departmentPage.Find("div>span");

            // Assert
            span.MarkupMatches("<span>No Departments found</span>");
        }

        [Fact]
        public void Should_DeleteDepartment_WhenDeleteButtonClicked()
        {
            //Arrange
            var departments = _fixture.CreateMany<DepartmentModel>(10);
            _departmentService.Setup(service => service.GetAll()).Returns(Task.FromResult(departments.ToList() as IList<DepartmentModel>));

            var modalRef = new Mock<IModalReference>();
            modalRef.Setup(mock => mock.Result).Returns(Task.FromResult(ModalResult.Ok(new { })));
            _modalService.Setup(service => service.Show<DeleteModal>(It.IsAny<string>())).Returns(modalRef.Object);
            var departmentPage = RenderComponent<Jhipster.Client.Pages.Entities.Department.Department>(ComponentParameterFactory.CascadingValue(_modalService.Object));

            // Act
            var departmentToDelete = departments.First();

            // Assert
            departmentPage.Find("td>div>button").Click();
            _departmentService.Verify(service => service.Delete(departmentToDelete.Id.ToString()), Times.Once);
            var departmentsTableBody = departmentPage.Find("tbody");
            departmentsTableBody.ChildElementCount.Should().Be(9);
        }

    }
}
