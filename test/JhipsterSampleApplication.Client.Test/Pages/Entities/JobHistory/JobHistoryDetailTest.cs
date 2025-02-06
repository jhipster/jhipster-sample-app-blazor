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
using JhipsterSampleApplication.Client.Pages.Entities.JobHistory;
using JhipsterSampleApplication.Client.Pages.Utils;
using JhipsterSampleApplication.Client.Services.EntityServices.JobHistory;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using Xunit;

namespace JhipsterSampleApplication.Client.Test.Pages.Entities.JobHistory;

public class JobHistoryDetailTest : TestContext
{
    private readonly Mock<IJobHistoryService> _jobhistoryService;
    private readonly Mock<INavigationService> _navigationService;
    private readonly AutoFixture.Fixture _fixture = new AutoFixture.Fixture();

    public JobHistoryDetailTest()
    {
        _jobhistoryService = new Mock<IJobHistoryService>();
        _navigationService = new Mock<INavigationService>();
        Services.AddSingleton<IJobHistoryService>(_jobhistoryService.Object);
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
    public void Should_DisplayJobHistory_When_IdIsPresent()
    {
        //Arrange
        var jobhistory = _fixture.Create<JobHistoryModel>();
        _jobhistoryService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(jobhistory));
        var jobhistoryDetail = RenderComponent<JobHistoryDetail>(ComponentParameter.CreateParameter("Id", jobhistory.Id));

        // Act
        var title = jobhistoryDetail.Find("h2");

        // Assert
        title.MarkupMatches($"<h2><span>JobHistory</span>{jobhistory.Id}</h2>");

    }

    [Fact]
    public void Should_NotDisplayJobHistory_When_IdIsNotPresent()
    {
        //Arrange
        _jobhistoryService.Setup(service => service.Get(It.IsAny<string>())).Returns(Task.FromResult(new JobHistoryModel()));
        var jobhistoryDetail = RenderComponent<JobHistoryDetail>();

        // Act
        var title = jobhistoryDetail.Find("div.col-8");

        // Assert
        title.Children.Length.Should().Be(0);

    }
}
