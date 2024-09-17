using System;
using System.Collections.Generic;

namespace ApiRestNetforemost.ModelBDNetforemost
{
    public partial class TblUser
    {
        public TblUser()
        {
            TblTasks = new HashSet<TblTask>();
        }

        public string IdUser { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Telephone { get; set; } = null!;
        public string UserPassword { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public virtual ICollection<TblTask> TblTasks { get; set; }
    }
}
