using ApiRestNetforemost.IServices;
using ApiRestNetforemost.ModelBDNetforemost;

namespace ApiRestNetforemost.Services
{
    public class PriorityServices : ITaskPriority
    {
        public List<TblPriority> GetTblPriorities()
        {
            try
            {
                using(NetforemostBDToDoListContext db = new NetforemostBDToDoListContext())
                {
                    var listTblPriorities = db.TblPriorities.ToList();
                    return listTblPriorities;
                }
            }catch (Exception ex)
            {
                throw;
            }
        }
    }
}
