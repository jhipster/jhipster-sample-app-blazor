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
using JhipsterSampleApplication.Client.Services.EntityServices.Job;
using JhipsterSampleApplication.Client.Shared;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.Job;

public class JobTest : TestContext
{
    private readonly Mock<IJobService> _jobService;
    private readonly Mock<Blazored.Modal.Services.IModalService> _modalService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public JobTest()
    {
        _jobService = new Mock<IJobService>();
        _modalService = new Mock<Blazored.Modal.Services.IModalService>();
        Services.AddSingleton<IJobService>(_jobService.Object);
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
    public void Should_DisplayAllJobs_When_JobsArePresent()
    {
        //Arrange
        var jobs = _fixture.CreateMany<JobModel>(10);
        _jobService.Setup(service => service.GetAll()).Returns(Task.FromResult(jobs.ToList() as IList<JobModel>));
        var jobPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.Job.Job>();

        // Act
        var jobsTableBody = jobPage.Find("tbody");

        // Assert
        jobsTableBody.ChildElementCount.Should().Be(10);
    }

    [Fact]
    public void Should_DisplayNoCountry_When_JobsLengthIsZero()
    {
        //Arrange
        var jobs = new List<JobModel>();
        _jobService.Setup(service => service.GetAll()).Returns(Task.FromResult(jobs.ToList() as IList<JobModel>));
        var jobPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.Job.Job>();

        // Act
        var span = jobPage.Find("div>span");

        // Assert
        span.MarkupMatches("<span>No Jobs found</span>");
    }

    [Fact]
    public async Task Should_DeleteJob_WhenDeleteButtonClicked()
    {
        //Arrange
        var jobs = _fixture.CreateMany<JobModel>(10);
        _jobService.Setup(service => service.GetAll()).Returns(Task.FromResult(jobs.ToList() as IList<JobModel>));

        var modalRef = new Mock<IModalReference>();
        modalRef.Setup(mock => mock.Result).Returns(Task.FromResult(ModalResult.Ok(new { })));
        _modalService.Setup(service => service.Show<DeleteModal>(It.IsAny<string>())).Returns(modalRef.Object);
        var jobPage = RenderComponent<JhipsterSampleApplication.Client.Pages.Entities.Job.Job>(ComponentParameterFactory.CascadingValue(_modalService.Object));

        // Act
        var jobToDelete = jobs.First();

        // Assert
        await jobPage.Find("td>div>button").ClickAsync(new MouseEventArgs());
        _jobService.Verify(service => service.Delete(jobToDelete.Id.ToString()), Times.Once);
        var jobsTableBody = jobPage.Find("tbody");
        jobsTableBody.ChildElementCount.Should().Be(9);
    }

}
