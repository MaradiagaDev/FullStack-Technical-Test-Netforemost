using System;
using System.Collections.Generic;

namespace ApiRestNetforemost.ModelBDNetforemost
{
    public partial class TblPriority
    {
        public TblPriority()
        {
            TblTasks = new HashSet<TblTask>();
        }

        public int IdPriority { get; set; }
        public string? NamePriority { get; set; }

        public virtual ICollection<TblTask> TblTasks { get; set; }
    }
}
