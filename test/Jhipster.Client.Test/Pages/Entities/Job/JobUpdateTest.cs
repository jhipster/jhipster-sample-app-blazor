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
using Jhipster.Client.Services.EntityServices.Job;
using Jhipster.Client.Services.EntityServices.PieceOfWork;
using Jhipster.Client.Services.EntityServices.Employee;
using Jhipster.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.Job
{
    public class JobUpdateTest : TestContext
    {
        private readonly Mock<IJobService> _jobService;
        private readonly Mock<IPieceOfWorkService> _pieceofworkService;
        private readonly Mock<IEmployeeService> _employeeService;
        private readonly Mock<IModalService> _modalService;
        private readonly Mock<INavigationService> _navigationService;
        private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

        public JobUpdateTest()
        {
            _jobService = new Mock<IJobService>();
            _modalService = new Mock<IModalService>();
            _navigationService = new Mock<INavigationService>();
            _pieceofworkService = new Mock<IPieceOfWorkService>();
            Services.AddSingleton<IPieceOfWorkService>(_pieceofworkService.Object);
            _employeeService = new Mock<IEmployeeService>();
            Services.AddSingleton<IEmployeeService>(_employeeService.Object);
            Services.AddSingleton<INavigationService>(_navigationService.Object);
            Services.AddSingleton<IJobService>(_jobService.Object);
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
        public void Should_UpdateSelectedJob_When_FormIsSubmitWithId()
        {
            //Arrange
            var pieceofworks = _fixture.CreateMany<PieceOfWorkModel>(10);
            _pieceofworkService.Setup(service => service.GetAll()).Returns(Task.FromResult(pieceofworks.ToList() as IList<PieceOfWorkModel>));
            var employees = _fixture.CreateMany<EmployeeModel>(10);
            _employeeService.Setup(service => service.GetAll()).Returns(Task.FromResult(employees.ToList() as IList<EmployeeModel>));

            var jobToUpdate = _fixture.Create<JobModel>();
            jobToUpdate.Id = 1;

            _jobService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(jobToUpdate));

            var updateJobPage = RenderComponent<Jhipster.Client.Pages.Entities.Job.JobUpdate>(ComponentParameter.CreateParameter("Id", 1));

            // Act
            var jobForm = updateJobPage.Find("form");
            jobForm.Submit();

            // Assert
            _jobService.Verify(service => service.Update(jobToUpdate), Times.Once);
        }

        [Fact]
        public void Should_AddJob_When_FormIsSubmitWithoutId()
        {
            //Arrange
            var pieceofworks = _fixture.CreateMany<PieceOfWorkModel>(10);
            _pieceofworkService.Setup(service => service.GetAll()).Returns(Task.FromResult(pieceofworks.ToList() as IList<PieceOfWorkModel>));
            var employees = _fixture.CreateMany<EmployeeModel>(10);
            _employeeService.Setup(service => service.GetAll()).Returns(Task.FromResult(employees.ToList() as IList<EmployeeModel>));

            var updateJobPage = RenderComponent<Jhipster.Client.Pages.Entities.Job.JobUpdate>(ComponentParameter.CreateParameter("Id", 0));

            // Act
            var jobForm = updateJobPage.Find("form");
            jobForm.Submit();

            // Assert
            _jobService.Verify(service => service.Add(It.IsAny<JobModel>()), Times.Once);
        }

    }
}
