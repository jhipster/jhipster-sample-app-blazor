using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using JhipsterSampleApplication.Client.Models;
using JhipsterSampleApplication.Client.Services.AccountServices;
using JhipsterSampleApplication.Client.Shared.Constants;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace JhipsterSampleApplication.Client.Test.Helpers;

public class MockRegisterService : IRegisterService
{

    public const string SuccessUsername = "testSuccess";

    public const string EmailUsername = "testEmail";

    public const string LoginUsername = "testLogin";

    public virtual Task<HttpResponseMessage> Save(UserSaveModel registerModel)
    {
        var resultMsg = new RegisterResultRequest();
        resultMsg.Detail = "";
        resultMsg.Params = "";
        resultMsg.Status = 400;
        resultMsg.Params = "";
        resultMsg.TraceId = "";
        resultMsg.Type = "";
        if (registerModel.Login == SuccessUsername)
        {
            return Task.Run(() => new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent("")
            });
        }

        if (registerModel.Login == EmailUsername)
        {
            resultMsg.Type = ErrorConst.EmailAlreadyUsedType;
        }

        if (registerModel.Login == LoginUsername)
        {
            resultMsg.Type = ErrorConst.LoginAlreadyUsedType;
        }

        return Task.Run(() => new HttpResponseMessage()
        {
            StatusCode = HttpStatusCode.BadRequest,
            Content = new StringContent(JsonSerializer.Serialize(resultMsg), Encoding.UTF8, "application/json")
        });

    }
}
