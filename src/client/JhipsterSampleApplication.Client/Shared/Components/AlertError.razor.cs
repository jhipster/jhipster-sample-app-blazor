using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using Blazorise;
using Microsoft.AspNetCore.Components;
using JhipsterSampleApplication.Client.Shared.Constants;
using JhipsterSampleApplication.Client.Shared.Models;
using Toolbelt.Blazor;

namespace JhipsterSampleApplication.Client.Shared.Components
{
    public partial class AlertError : ComponentBase
    {
        public const string NotFoundError = "Not found";
        public const string NotReachableError = "Server not reachable";
        public const string HttpErrorHeader = "app-error";

        private List<JhiAlert> Alerts { get; } = new List<JhiAlert>();

        [Inject]
        private HttpClientInterceptor Interceptor { get; set; }

        protected override Task OnInitializedAsync()
        {
            Interceptor.AfterSend += HandleErrors;
            return base.OnInitializedAsync();
        }

        public async Task AddAlert(JhiAlert alert)
        {
            Alerts.Add(alert);
            if (alert.Timeout != 0)
            {
                RemoveAfter(alert, alert.Timeout);
            }
            await InvokeAsync(StateHasChanged);
        }

        public async Task RemoveAlert(JhiAlert alert)
        {
            Alerts.Remove(alert);
            await InvokeAsync(StateHasChanged);
        }

        public IReadOnlyCollection<JhiAlert> GetAlerts()
        {
            return Alerts.AsReadOnly();
        }

        private void HandleErrors(object s, HttpClientInterceptorEventArgs e)
        {
            if (e.Response?.IsSuccessStatusCode == true)
            {
                return;
            }
            if (e.Response == null)
            {
                AddErrorAlert(NotReachableError);
                return;
            }
            switch (e.Response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    var errorHandler = "";
                    foreach (var httpResponseHeader in e.Response.Headers)
                    {
                        if (httpResponseHeader.Key.EndsWith(HttpErrorHeader))
                        {
                            errorHandler = e.Response.Headers.GetValues(httpResponseHeader.Key).FirstOrDefault();
                        }
                    }
                    if (!string.IsNullOrEmpty(errorHandler))
                    {
                        AddErrorAlert(errorHandler);
                    }
                    break;
                case HttpStatusCode.NotFound:
                    AddErrorAlert(NotFoundError);
                    break;
                default:
                    AddErrorAlert(e.Response.Content.ToString());
                    break;
            }
        }

        private void AddErrorAlert(string errorMsg)
        {
            var alert = new JhiAlert
            {
                Type = TypeAlert.Danger,
                Msg = errorMsg,
                Timeout = 3000,
                Scoped = true,
            };
            _ = AddAlert(alert);
        }

        private void RemoveAfter(JhiAlert alert, double timeout)
        {
            var timer = new Timer { Interval = timeout };
            timer.Elapsed += (sender, args) =>
            {
                if (sender is Timer senderTimer)
                {
                    senderTimer.Stop();
                }

                _ = RemoveAlert(alert);
            };
            timer.Start();
        }

        private Color GetColorAlert(JhiAlert alert)
        {
            switch (alert.Type)
            {
                case TypeAlert.Danger:
                    return Color.Danger;
                case TypeAlert.Info:
                    return Color.Info;
                case TypeAlert.Success:
                    return Color.Success;
                case TypeAlert.Warning:
                    return Color.Warning;
                default:
                    return Color.Danger;
            }
        }
    }
}
