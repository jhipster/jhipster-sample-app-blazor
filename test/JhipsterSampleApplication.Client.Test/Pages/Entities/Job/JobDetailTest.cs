using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;
using Bunit;
using Bunit.Rendering;
using FluentAssertions;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Pages.Entities.Job;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.Job;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.Job;

public class JobDetailTest : TestContext
{
    private readonly Mock<IJobService> _jobService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public JobDetailTest()
    {
        _jobService = new Mock<IJobService>();
        _navigationService = new Mock<INavigationService>();
        Services.AddSingleton<IJobService>(_jobService.Object);
        Services.AddSingleton<INavigationService>(_navigationService.Object);
        Services.AddBlazorise(options =>
            {
                options.Immediate = true;
            })
            .Replace(ServiceDescriptor.Transient<IComponentActivator, ComponentActivator>())
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
        //This code is needed to support recursion
        _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            .ForEach(b => _fixture.Behaviors.Remove(b));
        _fixture.Behaviors.Add(new OmitOnRecursionBehavior(1));
        JSInterop.Mode = JSRuntimeMode.Loose;
    }

    [Fact]
    public void Should_DisplayJob_When_IdIsPresent()
    {
        //Arrange
        var job = _fixture.Create<JobModel>();
        _jobService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(job));
        var jobDetail = RenderComponent<JobDetail>(ComponentParameter.CreateParameter("Id", job.Id));

        // Act
        var title = jobDetail.Find("h2");

        // Assert
        title.MarkupMatches($"<h2><span>Job</span>{job.Id}</h2>");

    }

    [Fact]
    public void Should_NotDisplayJob_When_IdIsNotPresent()
    {
        //Arrange
        _jobService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(new JobModel()));
        var jobDetail = RenderComponent<JobDetail>();

        // Act
        var title = jobDetail.Find("div.col-8");

        // Assert
        title.Children.Length.Should().Be(0);

    }
}
