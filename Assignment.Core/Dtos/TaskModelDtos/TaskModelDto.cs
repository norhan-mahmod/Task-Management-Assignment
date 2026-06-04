using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment.Core.Entities.Identity;
using Assignment.Core.Enums;

namespace Assignment.Core.Dtos.TaskModelDtos
{
    public class TaskModelDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority Priority { get; set; }
    }

    public class TaskModelUpdateDto : TaskModelDto
    {
        public int Id { get; set; }
    }
    public class TaskModelReturnDto : TaskModelUpdateDto
    {
        public Status Status { get; set; }
        public string StatusLabel { get; set; }
        public string PriorityLabel { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
