using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Bunit;
using FluentAssertions;
using JhipsterSampleApplication.Client.Services;
using JhipsterSampleApplication.Client.Shared.Components;
using JhipsterSampleApplication.Client.Test.Pages.TestPages;
using Moq;
using Moq.Protected;
using JhipsterSampleApplication.Client.Shared.Constants;
using JhipsterSampleApplication.Client.Shared.Models;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Xunit;
using Blazorise;
using Blazorise.Bootstrap5;
using Blazorise.Icons.FontAwesome;

namespace JhipsterSampleApplication.Client.Test;

public class AlertErrorTest : TestContext
{
    public AlertErrorTest()
    {
        Services.AddBlazorise(options =>
        {
            options.Immediate = true;
        })
            .Replace(ServiceDescriptor.Transient<IComponentActivator, ComponentActivator>())
            .AddBootstrap5Providers()
            .AddFontAwesomeIcons();
        JSInterop.Mode = JSRuntimeMode.Loose;
    }
    [Fact]
    public async Task Should_Display404_When_404()
    {
        HttpResponseMessage response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.NotFound,
            Content = new StringContent(@""),
        };

        var alertComponent = await InitAlertErrorTest(response);

        alertComponent.WaitForAssertion(() =>
        {
            var alerts = alertComponent.Instance.AlertError.GetAlerts();
            (alerts.Count > 0).Should().BeTrue();
            alerts.First(a => a.Msg == AlertError.NotFoundError).Should().NotBeNull();
        });
    }

    [Fact]
    public async Task Should_DisplayNotReach_When_NotReach()
    {
        var alertComponent = await InitAlertErrorTest(null);

        alertComponent.WaitForAssertion(() =>
        {
            var alerts = alertComponent.Instance.AlertError.GetAlerts();
            (alerts.Count > 0).Should().BeTrue();
            alerts.First(a => a.Msg == AlertError.NotReachableError).Should().NotBeNull();
        });
    }

    [Fact]
    public async Task Should_DisplayErrorHeader_When_Error()
    {
        HttpResponseMessage response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest,
            Content = new StringContent(@"Error Test"),
        };
        response.Headers.Add($"test-{AlertError.HttpErrorHeader}", "test");

        var alertComponent = await InitAlertErrorTest(response);

        alertComponent.WaitForAssertion(() =>
        {
            var alerts = alertComponent.Instance.AlertError.GetAlerts();
            (alerts.Count > 0).Should().BeTrue();
            alerts.First(a => a.Msg == $"test").Should().NotBeNull();
        });
    }

    [Fact]
    public async Task Should_NotAddError_When_BadRequest_Without_ErrorHeader()
    {
        HttpResponseMessage response = new HttpResponseMessage
        {
            StatusCode = HttpStatusCode.BadRequest,
            Content = new StringContent(@"Error Test"),
        };

        var alertComponent = await InitAlertErrorTest(response);

        alertComponent.WaitForAssertion(() =>
        {
            var alerts = alertComponent.Instance.AlertError.GetAlerts();
            (alerts.Count == 0).Should().BeTrue();
        });
    }

    [Fact]
    public void Should_Add_And_Remove_When_AlertTimout()
    {
        Services.AddHttpClientInterceptor();
        var alertComponent = RenderComponent<TestAlertError>();

        var alert = new JhiAlert
        {
            Msg = "test",
            Type = TypeAlert.Danger,
            Timeout = 100
        };

        alertComponent.Instance.AlertError.AddAlert(alert);

        alertComponent.WaitForAssertion(() =>
        {
            var alerts = alertComponent.Instance.AlertError.GetAlerts();
            (alerts.Count > 0).Should().BeTrue();
            alerts.First(a => a.Msg == $"test").Should().NotBeNull();
        });
        alertComponent.WaitForAssertion(() =>
        {
            var alerts = alertComponent.Instance.AlertError.GetAlerts();
            (alerts.Count == 0).Should().BeTrue();
        });
    }

    private async Task<IRenderedComponent<TestAlertError>> InitAlertErrorTest(HttpResponseMessage response)
    {
        Services.AddHttpClientInterceptor();
        var alertComponent = RenderComponent<TestAlertError>();
        var handlerMock = new Mock<HttpMessageHandler>();

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(response);
        var httpClient = new HttpClient(handlerMock.Object);
        httpClient.EnableIntercept(Services);
        httpClient.Timeout = TimeSpan.FromMilliseconds(60);
        try
        {
            await httpClient.GetAsync("https://localhost:5000");
        }
        catch (Exception)
        {
            // ignored
        }

        return alertComponent;
    }
}
