using System;
using System.Collections.Generic;

namespace ProjectTreinritten.Domain.Entities
{
    public partial class Departments
    {
        public Departments()
        {
            Adults = new HashSet<Adults>();
        }

        public int Id { get; set; }
        public string Department { get; set; }

        public ICollection<Adults> Adults { get; set; }
    }
}
