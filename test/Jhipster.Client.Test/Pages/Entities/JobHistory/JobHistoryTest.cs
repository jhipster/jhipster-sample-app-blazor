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
using Jhipster.Client.Services.EntityServices.JobHistory;
using Jhipster.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace Jhipster.Client.Test.Pages.Entities.JobHistory;

public class JobHistoryTest : TestContext
{
    private readonly Mock<IJobHistoryService> _jobHistoryService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public JobHistoryTest()
    {
        _jobHistoryService = new Mock<IJobHistoryService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
        Services.AddSingleton<IJobHistoryService>(_jobHistoryService.Object);
        Services.AddSingleton<Blazored.Modal.Services.IModalService>(_modalService.Object);
        Services.AddBlazorise(options =>
            {
                options.Immediate = true;
            })
            .Replace(ServiceDescriptor.Transient<IComponentActivator, ComponentActivator>())
            .AddBootstrapProviders()
            .AddFontAwesomeIcons();
        Services.AddHttpClientInterceptor();
        //This code is needed to support recursion
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));

        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void Should_DisplayAllJobHistories_When_JobHistoriesArePresent()
    {
        //Arrange
        var jobHistories = _fixture.CreateMany<JobHistoryModel>(10);
        _jobHistoryService.Setup(service => service.GetAll()).Returns(Task.FromResult(jobHistories.ToList() as IList<JobHistoryModel>));
        var jobHistoryPage = RenderComponent<Jhipster.Client.Pages.Entities.JobHistory.JobHistory>();

        // Act
        var jobHistoriesTableBody = jobHistoryPage.Find("tbody");

        // Assert
        jobHistoriesTableBody.ChildElementCount.Should().Be(10);
    }

    [Fact]
    public void Should_DisplayNoCountry_When_JobHistoriesLengthIsZero()
    {
        //Arrange
        var jobHistories = new List<JobHistoryModel>();
        _jobHistoryService.Setup(service => service.GetAll()).Returns(Task.FromResult(jobHistories.ToList() as IList<JobHistoryModel>));
        var jobHistoryPage = RenderComponent<Jhipster.Client.Pages.Entities.JobHistory.JobHistory>();

        // Act
        var span = jobHistoryPage.Find("div>span");

        // Assert
        span.MarkupMatches("<span>No JobHistories found</span>");
    }

    [Fact]
    public void Should_DeleteJobHistory_WhenDeleteButtonClicked()
    {
        //Arrange
        var jobHistories = _fixture.CreateMany<JobHistoryModel>(10);
        _jobHistoryService.Setup(service => service.GetAll()).Returns(Task.FromResult(jobHistories.ToList() as IList<JobHistoryModel>));

        var modalRef = new Mock<IModalReference>();
        modalRef.Setup(mock => mock.Result).Returns(Task.FromResult(ModalResult.Ok(new { })));
        _modalService.Setup(service => service.Show<DeleteModal>(It.IsAny<string>())).Returns(modalRef.Object);
        var jobHistoryPage = RenderComponent<Jhipster.Client.Pages.Entities.JobHistory.JobHistory>(ComponentParameterFactory.CascadingValue(_modalService.Object));

        // Act
        var jobHistoryToDelete = jobHistories.First();

        // Assert
        jobHistoryPage.Find("td>div>button").Click();
        _jobHistoryService.Verify(service => service.Delete(jobHistoryToDelete.Id.ToString()), Times.Once);
        var jobHistoriesTableBody = jobHistoryPage.Find("tbody");
        jobHistoriesTableBody.ChildElementCount.Should().Be(9);
    }

}
