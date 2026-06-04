using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace Assignment.Core.Entities.Identity
{
    public class AppUser : IdentityUser
    {
        public DateTime? CreatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public List<TaskModel> TasksModel { get; set; }
    }
}
