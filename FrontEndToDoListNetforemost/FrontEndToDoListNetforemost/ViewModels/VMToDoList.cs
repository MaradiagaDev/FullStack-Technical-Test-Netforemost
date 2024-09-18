using CurrieTechnologies.Razor.SweetAlert2;
using FrontEndToDoListNetforemost.Data;
using FrontEndToDoListNetforemost.Services;
using System.Collections.Generic;
using System.Threading;

namespace FrontEndToDoListNetforemost.ViewModels
{
    public class VMToDoList
    {
        private IGenericServices _services = new IGenericServices();
        public async Task<List<TblTasks>> GetTblTasks(string token,string userId, int pageNumber, int pageSize, SweetAlertService Swal)
        {
            try
            {
                GetTaskDto dto = new GetTaskDto() { userID = userId,pageNumber = pageNumber, pageSize = pageSize};

                var dataFacturas = await _services.SendRequestAsync<GetTaskDto, RequestGeneric<GetTaskResultDto>>(HttpMethod.Post, $"Tasks/api/GetAllTask", token,dto).ConfigureAwait(false);

                return dataFacturas.result.Data;
            }
            catch (Exception ex)
            {
                   
                _ = Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Error de Servidor",
                    Text = "Ah ocurrido un error con el servidor.",
                    Icon = SweetAlertIcon.Error,
                    ConfirmButtonText = "Ok",
                    CancelButtonText = "Volver"
                });

                return new List<TblTasks>();
            }
        }

        public async Task<List<TblPriority>> GetTblPriorities(string token, SweetAlertService Swal)
        {
            try
            {
                var dataPriorities = await _services.SendRequestAsync<PrioritiesDTO, PrioritiesDTO>(HttpMethod.Get, $"Priority/api/GetAllPriorities", token).ConfigureAwait(false);

                return dataPriorities.result;
            }
            catch (Exception ex)
            {

                _ = Swal.FireAsync(new SweetAlertOptions
                {
                    Title = "Error de Servidor",
                    Text = "Ah ocurrido un error con el servidor.",
                    Icon = SweetAlertIcon.Error,
                    ConfirmButtonText = "Ok",
                    CancelButtonText = "Volver"
                });

                return new List<TblPriority>();
            }
        }

        public async Task<Boolean> AddNewTask(string tag,string title,string description, DateTime duedate,int idPriority,string token,string userId, SweetAlertService Swal)
        {
            try
            {
                TaskDTO dto = new TaskDTO() 
                {
                    IdTask = string.Empty,
                    IdUser = userId,
                    Title = title,
                    DescriptionTask = description,
                    ExpirationDate = duedate,
                    Finished = false,
                    Deleted = false,
                    Tags = tag,
                    IdPriority = idPriority
                };

                var dataPriorities = await _services.SendRequestAsync<TaskDTO, RequestGeneric<TblTasks>>(HttpMethod.Post, $"Tasks/api/CreateTask", token,dto).ConfigureAwait(false);

                if(dataPriorities.success == true)
                {
                    _ = Swal.FireAsync(new SweetAlertOptions
                    {
                        Title = "Correcto",
                        Text = "Tarea agregada correctamente.",
                        Icon = SweetAlertIcon.Success,
                        ConfirmButtonText = "Ok",
                        CancelButtonText = "Volver"
                    });
                }
                else
                {
                    _ = Swal.FireAsync(new SweetAlertOptions
                    {
                        Title = "Atención",
                        Text = dataPriorities.messages,
                        Icon = SweetAlertIcon.Warning,
                        ConfirmButtonText = "Ok",
                        CancelButtonText = "Volver"
                    });
                }

                return dataPriorities.result == null ? false:true;
            }
            catch (Exception ex)
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
