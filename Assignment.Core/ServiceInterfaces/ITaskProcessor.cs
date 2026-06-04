using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment.Core.ServiceInterfaces
{
    public interface ITaskProcessor
    {
        Task ProcessTaskAsync(int taskId);
    }
}
