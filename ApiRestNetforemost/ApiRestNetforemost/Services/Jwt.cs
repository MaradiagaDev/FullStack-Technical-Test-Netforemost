using ApiRestNetforemost.ModelBDNetforemost;
using System.Security.Claims;

namespace ApiRestNetforemost.Services
{
    public class Jwt
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Subject { get; set; }

        public static bool ValidateToken(ClaimsIdentity identity)
        {
            try
            {
                if (identity.Claims.Count() == 0)
                {
                    return false;
                }

                var id = identity.Claims.FirstOrDefault(x => x.Type == "Id").Value;

                using (NetforemostBDToDoListContext db = new NetforemostBDToDoListContext())
                {
                    return db.TblUsers.Where( user => user.IdUser == id).Any();
                }
            }
            catch (Exception ex)
            {
                throw;
            } 
        }
    }
}
