using CurrieTechnologies.Razor.SweetAlert2;
using FrontEndToDoListNetforemost.Data;
using FrontEndToDoListNetforemost.Services;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace FrontEndToDoListNetforemost.ViewModels
{
    public class VMLogin
    {
        private IGenericServices _services = new IGenericServices();
        public async Task<bool> ValidationsAsync(string userName, string userPassword, SweetAlertService Swal, ProtectedLocalStorage localStorage)
        {
            try
            {
                if (userName == "" && userPassword == "")
                {
                    _ = Swal.FireAsync(new SweetAlertOptions
                    {
                        Title = "Atención",
                        Text = "No ha digitado sus credenciales.",
                        Icon = SweetAlertIcon.Warning,
                        ConfirmButtonText = "Ok",
                        CancelButtonText = "Volver"
                    });

                    return false;
                }

                if (userName == "")
                {
                    _ = Swal.FireAsync(new SweetAlertOptions
                    {
                        Title = "Atención",
                        Text = "Debe agregar un nombre de usuario para acceder.",
                        Icon = SweetAlertIcon.Warning,
                        ConfirmButtonText = "Ok",
                        CancelButtonText = "Volver"
                    });

                    return false;
                }

                if (userPassword == "")
                {
                    _ = Swal.FireAsync(new SweetAlertOptions
                    {
                        Title = "Atención",
                        Text = "Debe agregar una contraseña para acceder.",
                        Icon = SweetAlertIcon.Warning,
                        ConfirmButtonText = "Ok",
                        CancelButtonText = "Volver"
                    });

                    return false;
                }

                //Acceder
                LoginRequest log = new LoginRequest()
                {
                    Email = userName,
                    Password = userPassword
                };

                var userData = await _services.SendRequestAsync<LoginRequest, RequestGeneric<UserCorrectAnswer>>(HttpMethod.Post, "Users/api/Login", "", log).ConfigureAwait(false);

                if (userData.success == true)
                {
                    var resultado = (UserCorrectAnswer)userData.result;
                    var usuario = resultado.user as TblUser;

                    localStorage.SetAsync("token", resultado.token);
                    localStorage.SetAsync("user", usuario.FirstName + " " + usuario.LastName);
                    localStorage.SetAsync("userId", usuario.IdUser);

                    return true;
                }

                _ = Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Atención",
                    Text = "Invalid username or password. Please try again.",
                    Icon = SweetAlertIcon.Warning,
                    ConfirmButtonText = "Ok",
                    CancelButtonText = "Volver"
                });

                return false;
            }
            catch (Exception e)
            {
                _ = Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Error de Servidor",
                    Text = "Ah ocurrido un error con el servidor.",
                    Icon = SweetAlertIcon.Error,
                    ConfirmButtonText = "Ok",
                    CancelButtonText = "Volver"
                });

                return false;
            }


        }
    }
}
