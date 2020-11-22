using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Blazored.Modal.Services;
using Jhipster.Client.Models;
using Jhipster.Client.Services.AccountServices;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Json;
using Jhipster.Client.Shared.Constants;

namespace Jhipster.Client.Pages.Account
{
    public partial class Register : ComponentBase
    {
        [Inject]
        private IRegisterService RegisterService{ get; set; }

        [CascadingParameter]
        private IModalService ModalService { get; set; }

        public RegisterModel RegisterModel = new RegisterModel();

        private EditContext EditContext { get; set; }

        private EditForm editForm;

        public bool Success { get; private set; }
        public bool Error { get; private set; }
        public bool ErrorEmailExists { get; private set; }
        public bool ErrorUserExists { get; private set; }

        private bool IsInvalid { get; set; }
        

        protected override async Task OnInitializedAsync()
        {
            IsInvalid = true;
            EditContext = new EditContext(RegisterModel);
            EditContext.OnFieldChanged += IsInvalidForm;
            await base.OnInitializedAsync();
        }

        private void IsInvalidForm(object s, FieldChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(RegisterModel.Username) &&
                !string.IsNullOrEmpty(RegisterModel.ConfirmPassword) &&
                !string.IsNullOrEmpty(RegisterModel.Password) &&
                !string.IsNullOrEmpty(RegisterModel.Email))
            {
                if (editForm?.EditContext.Validate() == true)
                {
                    IsInvalid = false;
                    return;
                }
            }

            IsInvalid = true;
        }

        private async Task HandleSubmit()
        {
            SetAllErrorFalse();
            var result = await RegisterService.Save(new UserSaveModel{
                Email = RegisterModel.Email,
                Login = RegisterModel.Username,
                Password = RegisterModel.Password,
                LangKey = "en"
            });
            if (result.IsSuccessStatusCode)
            {
                Success = true;
            }
            else
            {
                await ProcessError(result);
            }
        }

        private void SetAllErrorFalse()
        {
            Success = false;
            Error = false;
            ErrorEmailExists = false;
            ErrorUserExists = false;
        }

        private async Task ProcessError(HttpResponseMessage result)
        {
            if (result.StatusCode != HttpStatusCode.BadRequest) // other status code
            {
                Error = true;
                return;
            }

            try
            {
                var res = await result.Content.ReadFromJsonAsync<RegisterResultRequest>();
                ErrorEmailExists = res.Type == ErrorConst.EmailAlreadyUsedType;
                ErrorUserExists = res.Type == ErrorConst.LoginAlreadyUsedType;
                if (!ErrorEmailExists && !ErrorUserExists) // if unknown error
                {
                    Error = true;
                }
            }
            catch (Exception)
            {
                Error = true; // json pars error
            }
            
        }

        private async Task SignIn()
        {
            ModalService.Show<Login>("Sign In");
        }
    }
}
