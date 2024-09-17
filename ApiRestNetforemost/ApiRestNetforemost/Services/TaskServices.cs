using ApiRestNetforemost.DTO;
using ApiRestNetforemost.ModelBDNetforemost;
using Microsoft.AspNetCore.Mvc;

namespace ApiRestNetforemost.Services
{
    public class TaskServices : ITask
    {
        public dynamic GetAllTask(string userId, int pageNumber = 1, int pageSize = 10)
        {
            try
            {
                using(NetforemostBDToDoListContext db = new NetforemostBDToDoListContext())
                {
                    var tasks = db.TblTasks
                        .Where(task => task.IdUser == userId && !task.Deleted)
                        .Skip((pageNumber - 1) * pageSize)
                        .Take(pageSize)
                        .ToList();

                    int totalItems = tasks.Count();

                    var response = new
                    {
                        TotalItems = totalItems,
                        PageNumber = pageNumber,
                        PageSize = pageSize,
                        TotalPages = (int)System.Math.Ceiling(totalItems / (double)pageSize),
                        Data = tasks
                    };

                    return response;
                }
            }catch (Exception ex)
            {
                throw;
            }
        }

        public TblTask CreateTask(TaskDTO taskDTO)
        {
            try
            {
                using (NetforemostBDToDoListContext db = new NetforemostBDToDoListContext())
                {
                    TblTask task = new TblTask()
                    {
                        IdTask = Guid.NewGuid().ToString(),
                        IdUser = taskDTO.IdUser,
                        Title = taskDTO.Title,
                        DescriptionTask = taskDTO.DescriptionTask,
                        ExpirationDate = taskDTO.ExpirationDate,
                        Finished = taskDTO.Finished,
                        Deleted = taskDTO.Deleted,
                        Tags = taskDTO.Tags,
                        IdPriority = taskDTO.IdPriority,
                        CreatedAt = DateTime.Now
                    };

                    db.Add(task);
                    db.SaveChanges();

                    return task;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public TblTask DeleteTask(string IdTask)
        {
            try
            {
                using(NetforemostBDToDoListContext db = new NetforemostBDToDoListContext())
                {
                    var tblTask = db.TblTasks.Where(task => task.IdTask == IdTask).FirstOrDefault();

                    if (tblTask != null)
                    {
                       tblTask.Deleted = true;
                       tblTask.UpdatedAt = DateTime.Now;
                       db.Update(tblTask);
                       db.SaveChanges();

                       return tblTask;
                    }

                    return null;
                }
            }catch (Exception ex)
            {
                throw;
            }
        }

        public TblTask UpdateTask(TaskDTO taskDTO)
        {
            try
            {
                using(NetforemostBDToDoListContext db = new NetforemostBDToDoListContext())
                {
                    var tblTask = db.TblTasks.Where(task => task.IdTask == taskDTO.IdTask).FirstOrDefault();

                    if (!string.IsNullOrEmpty(taskDTO.Title))
                    {
                        tblTask.Title = taskDTO.Title;
                    }

                    if (!string.IsNullOrEmpty(taskDTO.DescriptionTask))
                    {
                        tblTask.DescriptionTask = taskDTO.DescriptionTask;
                    }

                    if (taskDTO.ExpirationDate != null)
                    {
                        tblTask.ExpirationDate = taskDTO.ExpirationDate;
                    }

                    if(taskDTO.Finished != null)
                    {
                        tblTask.Finished = taskDTO.Finished;
                    }

                    if (taskDTO.IdPriority != null)
                    {
                        tblTask.IdPriority = taskDTO.IdPriority;
                    }

                    if (taskDTO.Tags != null)
                    {
                        tblTask.Tags = taskDTO.Tags;
                    }

                    db.Update(tblTask);
                    db.SaveChanges();

                    return tblTask;
                }
            }catch (Exception ex)
            {
                throw;
            }
        }
        
    }
}
