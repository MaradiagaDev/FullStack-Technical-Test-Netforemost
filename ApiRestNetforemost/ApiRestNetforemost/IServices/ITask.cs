using ApiRestNetforemost.DTO;
using ApiRestNetforemost.ModelBDNetforemost;

namespace ApiRestNetforemost.Services
{
    public interface ITask
    {
        public dynamic GetAllTask(string userId, int pageNumber = 1, int pageSize = 10);
        public TblTask CreateTask(TaskDTO taskDTO);
        public TblTask DeleteTask(string IdTask);
        public TblTask UpdateTask(TaskDTO taskDTO);
    }
}
